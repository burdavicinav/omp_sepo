using csv_lib;
using obj_lib;
using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TpImportLinkOperationsDialog : Form
    {
        public TpImportLinkOperationsDialog()
        {
            InitializeComponent();

            fileBox.Filter = "(*.csv)|*.csv";
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (fileBox.TextValue != string.Empty)
            {
                using (var transaction = Module.Connection.BeginTransaction())
                {
                    try
                    {
                        OracleCommand clear = new OracleCommand();
                        clear.Connection = transaction.Connection;
                        clear.CommandText = "delete from omp_adm.sepo_tech_oper_links";

                        clear.ExecuteNonQuery();

                        CsvColumn[] columns = {
                            new CsvColumn("reckey"),
                            new CsvColumn("tpopername"),
                            new CsvColumn("tpopercode"),
                            new CsvColumn("opercode"),
                            new CsvColumn("variantcode"),
                            new CsvColumn("name")
                        };

                        CsvFile file = new CsvFile(fileBox.TextValue, '&', columns);
                        file.Load("omp_adm.sepo_tech_oper_links", transaction.Connection);

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
        }
    }
}