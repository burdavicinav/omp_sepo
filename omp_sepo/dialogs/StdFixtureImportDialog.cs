using imp_exp;
using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdFixtureImportDialog : Form
    {
        public StdFixtureImportDialog()
        {
            InitializeComponent();

            okButton.Enabled = IsAcceptButton();

            stdFixturePath.TextValueChanged += OnPathChanged;
            enumPath.TextValueChanged += OnPathChanged;
            tpFilePath.TextValueChanged += OnPathChanged;
            entitiesFilePath.TextValueChanged += OnPathChanged;
            tpDirectoryPath.TextValueChanged += OnPathChanged;
            okButton.Click += OnAccept;
        }

        private bool IsAcceptButton()
        {
            return (
                !stdFixturePath.IsEmpty &&
                !enumPath.IsEmpty &&
                !tpDirectoryPath.IsEmpty &&
                !tpFilePath.IsEmpty &&
                !entitiesFilePath.IsEmpty
                );
        }

        private void OnPathChanged(object s, ui_lib.TextChangedEventArgs e)
        {
            okButton.Enabled = IsAcceptButton();
        }

        private void LoadData()
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                imp_exp.Module.Connection = Module.Connection;

                try
                {
                    StandardFixtureManager mng = new StandardFixtureManager();
                    mng.LoadFromXml(stdFixturePath.TextValue, enumPath.TextValue);

                    TPManager tp_mng = new TPManager();
                    //tp_mng.StartImporting += OnImport;
                    //tp_mng.ImportingTP += OnTpImport;

                    tp_mng.LoadFromXml(
                        tpFilePath.TextValue,
                        TPImportGroup.TP,
                        entitiesFilePath.TextValue,
                        tpDirectoryPath.TextValue,
                        tpFilePrefix.Text
                        );

                    transaction.Commit();

                    MessageBox.Show(
                        "Операция успешно завершена!",
                        "Информация",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();

                    MessageBox.Show(
                        exc.Message + " " + exc.StackTrace,
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        private void OnAccept(object s, EventArgs e)
        {
            LoadData();
        }

        //private void OnImport(object sender, EventArgs e)
        //{
        //    this.Invoke(new ThreadStart(delegate
        //    {
        //        logLabel.Text = "Удаление загруженных данных";
        //    }));
        //}

        //private void OnTpImport(object sender, EventArgs e)
        //{
        //    this.Invoke(new ThreadStart(delegate
        //    {
        //        logLabel.Text = "Загрузка ТП";
        //    }));
        //}
    }
}