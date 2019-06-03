using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace omp_sepo.dialogs
{
    public partial class FixtureAttachFilesDialog : Form
    {
        // private FixtureAttachFilesById attachManager;

        private readonly BackgroundWorker backWorker;

        private ContextMenuStrip logMenu;

        private int currentColumnIndex = -1;

        private int currentSortOrder = 0;

        public FixtureAttachFilesDialog()
        {
            InitializeComponent();

            okButton.Enabled = IsCorrectData();

            directoryBox.TextValueChanged += DirectoryBox_TextValueChanged;

            backWorker = new BackgroundWorker();

            backWorker.WorkerReportsProgress = true;
            backWorker.WorkerSupportsCancellation = true;
            backWorker.DoWork += OnRun;
            backWorker.RunWorkerCompleted += OnWorkerCompleted;
            backWorker.ProgressChanged += OnProgressChanged;

            okButton.Click += OnAcceptButton;
            cancelButton.Click += OnCancelButton;

            logMenu = new ContextMenuStrip();
            logMenu.Items.Add("Выгрузка в *.csv", null, OnLoad);
            logView.ContextMenuStrip = logMenu;

#if DEBUG
            //directoryBox.TextValue = @"C:\Users\Alexander\Downloads\Telegram Desktop\Files1";
            //okButton.Enabled = IsCorrectData();
#endif
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != currentColumnIndex)
            {
                currentColumnIndex = e.Column;
                currentSortOrder = 1;
            }
            else
            {
                currentSortOrder *= (-1);
            }

            logView.ListViewItemSorter = new FixtureAttachFileLogCompare(currentColumnIndex, currentSortOrder);
        }

        private void ResetLog()
        {
            endFilesCount.Text = "0";
            addFilesCount.Text = "0";

            progress.Step = 1;
            progress.Value = 0;

            logView.Items.Clear();
        }

        private bool IsCorrectData()
        {
            return directoryBox.TextValue != String.Empty;
        }

        private void DirectoryBox_TextValueChanged(object s, ui_lib.TextChangedEventArgs e)
        {
            //attachManager = new FixtureAttachFilesById(directoryBox.TextValue);

            //attachManager.BeginRun += OnBeginRun;
            //attachManager.EndRun += OnEndRun;
            //attachManager.BeginAttachFile += OnAttachFile;
            //attachManager.EndAttachFile += OnEndAttachFile;

            okButton.Enabled = IsCorrectData();

            ResetLog();

            //int countFiles = attachManager.CountFiles();

            DirectoryInfo dirInfo = new DirectoryInfo(directoryBox.TextValue);

            int countFiles = dirInfo.EnumerateFiles().Count();

            countFilesLabel.Text = countFiles.ToString();
            progress.Maximum = countFiles;
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FixtureAttachFileProgress prog = e.UserState as FixtureAttachFileProgress;

            if (prog.Type == FixtureAttachFileProgressType.BeginAttachFile)
            {
                currentFileLabel.Text = prog.FileName;
            }
            else
            {
                progress.PerformStep();

                int.TryParse(endFilesCount.Text, out int count);
                count++;

                endFilesCount.Text = count.ToString();

                bool isSuccess = false;

                foreach (var obj in prog.Objects)
                {
                    if (obj.State == FixtureAttachFileState.Success)
                    {
                        isSuccess = true;
                    }

                    ListViewItem item = new ListViewItem(obj.IdDoc.ToString());
                    item.SubItems.Add(prog.FileName);
                    item.SubItems.Add(obj.Sign);
                    item.SubItems.Add(obj.ObjectType);
                    item.SubItems.Add(obj.ObjectRevision.ToString());
                    item.SubItems.Add(obj.State.ToString());

                    logView.Items.Add(item);
                }

                if (prog.Objects.Count == 0)
                {
                    ListViewItem item = new ListViewItem(String.Empty);
                    item.SubItems.Add(prog.FileName);
                    item.SubItems.Add(String.Empty);
                    item.SubItems.Add(String.Empty);
                    item.SubItems.Add(String.Empty);
                    item.SubItems.Add("Error file");

                    logView.Items.Add(item);
                }

                if (isSuccess)
                {
                    int.TryParse(addFilesCount.Text, out int addCount);
                    addCount++;

                    addFilesCount.Text = addCount.ToString();
                }
            }
        }

        private void OnRun(object sender, DoWorkEventArgs e)
        {
            //attachManager.Run();

            int counter = 0;

            DirectoryInfo directory = new DirectoryInfo(directoryBox.TextValue);

            foreach (var file in directory.GetFiles())
            {
                if (backWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                counter++;

                List<FixtureAttachFileObject> objects = new List<FixtureAttachFileObject>();

                FixtureAttachFileProgress beginProgress = new FixtureAttachFileProgress();
                beginProgress.Type = FixtureAttachFileProgressType.BeginAttachFile;
                beginProgress.FileName = file.Name;

                backWorker.ReportProgress(counter, beginProgress);

                if (Regex.IsMatch(file.Name, @"^\d+"))
                {
                    int idDoc = Convert.ToInt32(Regex.Match(file.Name, @"^\d+").Value);

                    // файл
                    FileStream stream = File.OpenRead(file.FullName);

                    byte[] bytes = new byte[file.Length];
                    stream.Read(bytes, 0, (int)file.Length);

                    stream.Close();

                    // контрольная сумма
                    SHA1 s = new SHA1CryptoServiceProvider();
                    byte[] hash = s.ComputeHash(bytes);

                    string hashStr = (System.BitConverter.ToString(hash)).Replace("-", "").ToLower();

                    // транзакция
                    OracleConnection connection = obj_lib.Module.OpenSession().Connection as OracleConnection;

                    OracleCommand command = new OracleCommand("pkg_sepo_import_global.attach_fixture_file");
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter p_result = new OracleParameter(
                        "p_result", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

                    OracleParameter p_iddoc = new OracleParameter("p_iddoc", idDoc);
                    OracleParameter p_docname = new OracleParameter("p_docname", file.Name);
                    OracleParameter p_hash = new OracleParameter("p_hash", hashStr);
                    OracleParameter p_data = new OracleParameter("p_data", OracleDbType.Blob);

                    command.Parameters.AddRange(new OracleParameter[] { p_result, p_iddoc, p_docname, p_hash, p_data });

                    p_data.Value = bytes;
                    command.ExecuteNonQuery();

                    OracleRefCursor result = (OracleRefCursor)p_result.Value;

                    using (OracleDataReader rd = result.GetDataReader())
                    {
                        while (rd.Read())
                        {
                            FixtureAttachFileObject obj = new FixtureAttachFileObject();
                            obj.IdDoc = rd.GetInt32(1);
                            obj.Sign = rd.GetString(3);
                            obj.ObjectType = rd.GetString(4);
                            obj.ObjectRevision = rd.GetInt32(5);
                            obj.State = (rd.GetInt16(6) == 1) ? FixtureAttachFileState.Success : FixtureAttachFileState.None;

                            objects.Add(obj);
                        }
                    }
                }

                FixtureAttachFileProgress endProgress = new FixtureAttachFileProgress();

                endProgress.Type = FixtureAttachFileProgressType.EndAttachFile;
                endProgress.FileName = file.Name;
                endProgress.Objects = objects;

                backWorker.ReportProgress(counter, endProgress);
            }
        }

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

            okButton.Enabled = true;
            //logView.ColumnClick += OnColumnClick;
        }

        private void OnAcceptButton(object sender, EventArgs e)
        {
            if (!backWorker.IsBusy)
            {
                ResetLog();
                //logView.ColumnClick -= OnColumnClick;

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
                DialogResult = DialogResult.Cancel;
            }
        }

        private void OnBeginRun(object sender, EventArgs e)
        {
            //MessageBox.Show("Begin!");
        }

        private void OnEndRun(object sender, EventArgs e)
        {
            //MessageBox.Show("End!");
        }

        private void OnAttachFile(object sender, FixtureAttachFileStartArgs e)
        {
            currentFileLabel.Text = e.FileName;
        }

        private void OnEndAttachFile(object sender, FixtureAttachFileEndArgs e)
        {
            progress.PerformStep();

            int.TryParse(endFilesCount.Text, out int count);
            count++;

            endFilesCount.Text = count.ToString();

            bool isSuccess = false;

            foreach (var obj in e.Objects)
            {
                if (obj.State == FixtureAttachFileState.Success)
                {
                    isSuccess = true;
                }

                ListViewItem item = new ListViewItem(e.FileName);
                item.SubItems.Add(obj.Sign);
                item.SubItems.Add(obj.State.ToString());

                logView.Items.Add(item);
            }

            if (e.Objects.Count == 0)
            {
                ListViewItem item = new ListViewItem(e.FileName);
                item.SubItems.Add("");
                item.SubItems.Add("Error file");

                logView.Items.Add(item);
            }

            if (isSuccess)
            {
                int.TryParse(addFilesCount.Text, out int addCount);
                addCount++;

                addFilesCount.Text = addCount.ToString();
            }

            //System.Threading.Thread.Sleep(1000);
            //MessageBox.Show("EndFile! " + e.FileName);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "(*.csv)|*.csv";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = dialog.OpenFile();

                StringBuilder sb = new StringBuilder();

                foreach (ListViewItem item in logView.Items)
                {
                    sb.Append(item.Text);
                    sb.Append("&");
                    sb.Append(item.SubItems[1].Text);
                    sb.Append("&");
                    sb.Append(item.SubItems[2].Text);
                    sb.Append("&");
                    sb.Append(item.SubItems[3].Text);
                    sb.Append("&");
                    sb.Append(item.SubItems[4].Text);
                    sb.Append("&");
                    sb.Append(item.SubItems[5].Text);
                    sb.Append("\r\n");
                }

                string data = sb.ToString();
                byte[] bytes = Encoding.Default.GetBytes(data);

                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }
    }
}