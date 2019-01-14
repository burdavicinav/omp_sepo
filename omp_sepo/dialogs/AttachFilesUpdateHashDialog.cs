using ICSharpCode.SharpZipLib.BZip2;
using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class AttachFilesUpdateHashDialog : Form
    {
        private IRepository<DOCUMENTS_PARAMS> _repoDocParam;
        private IViewRepository<DOCUMENTS_PARTS> _repoDoc;

        public AttachFilesUpdateHashDialog(
            IRepository<DOCUMENTS_PARAMS> repoDocParam,
            IViewRepository<DOCUMENTS_PARTS> repoDoc)
        {
            _repoDocParam = repoDocParam;
            _repoDoc = repoDoc;

            InitializeComponent();

            backWorker.WorkerReportsProgress = true;
            backWorker.WorkerSupportsCancellation = true;
            backWorker.RunWorkerCompleted += OnWorkerCompleted;

            backWorker.DoWork += OnRun;
            backWorker.ProgressChanged += OnProgressChanged;

            okButton.Click += OnAcceptButton;
            cancelButton.Click += OnCancelButton;

            progress.Step = 1;
            progress.Value = 0;
        }

        public AttachFilesUpdateHashDialog() : this(
            new Repository<DOCUMENTS_PARAMS>(),
            new ViewRepository<DOCUMENTS_PARTS>())
        { }

        private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Операция отменена!");
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message + e.Error.StackTrace, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Операция выполнена!");
            }

            progressLabel.Text = "Обработано файлов";
            progress.Value = 0;
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressLabel.Text = "Обработано файлов: " + e.ProgressPercentage + " из " + progress.Maximum;
            progress.PerformStep();
        }

        private void OnRun(object sender, DoWorkEventArgs e)
        {
            int counter = 0;

            using (var transaction = new UnitOfWork())
            {
                try
                {
                    var docs = _repoDocParam
                                    .GetQuery()
                                    .Where(x => x.FILENAME.ToLower().EndsWith(".grb"));

                    foreach (var docParam in docs)
                    {
                        if (backWorker.CancellationPending)
                        {
                            transaction.Rollback();

                            e.Cancel = true;
                            return;
                        }

                        DOCUMENTS_PARTS docPart = null;

                        try
                        {
                            docPart = _repoDoc.GetById(docParam.CODE);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        byte[] docContent = { };

                        if (docPart.COMPRESSED == 1)
                        {
                            Stream inStream = new MemoryStream(docPart.DATA);
                            inStream.Seek(8, SeekOrigin.Begin);

                            Stream outStream = new MemoryStream();

                            BZip2.Decompress(inStream, outStream, false);
                            outStream.Seek(0, SeekOrigin.Begin);

                            docContent = new byte[outStream.Length];
                            outStream.Read(docContent, 0, (int)outStream.Length);
                        }
                        else
                        {
                            docContent = docPart.DATA;
                        }

                        if (docContent == null) continue;

                        SHA1 s = new SHA1CryptoServiceProvider();
                        byte[] hash = s.ComputeHash(docContent);

                        string hash1 = (System.BitConverter.ToString(hash)).Replace("-", "").ToLower();

                        docParam.HASH_ALG = 1;
                        docParam.HASH = hash1;

                        _repoDocParam.Update(docParam);
                        backWorker.ReportProgress(++counter);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void OnAcceptButton(object sender, EventArgs e)
        {
            if (!backWorker.IsBusy)
            {
                progress.Maximum = _repoDocParam.GetQuery().Count();

                backWorker.RunWorkerAsync();
                okButton.Enabled = false;
            }
        }

        private void OnCancelButton(object sender, EventArgs e)
        {
            if (backWorker.IsBusy && backWorker.WorkerSupportsCancellation)
            {
                backWorker.CancelAsync();
                okButton.Enabled = true;
            }
            else
            {
                this.Close();
            }
        }

        private void AttachFilesUpdateHashDialog_Load(object sender, EventArgs e)
        {
        }
    }
}