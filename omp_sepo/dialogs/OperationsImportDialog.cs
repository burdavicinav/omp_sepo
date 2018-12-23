using imp_exp;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo.dialogs
{
    public partial class OperationsImportDialog : Form
    {
        private void LoadOperTypes()
        {
            Dictionary<decimal, string> types = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = "select code, name from techoperation_types order by name";

            types.Add(-1, String.Empty);

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    types.Add(reader.GetDecimal(0), reader.GetString(1));
                }
            }

            operTypeBox.ValueMember = "Key";
            operTypeBox.DisplayMember = "Value";
            operTypeBox.DataSource = new BindingSource(types, null);
        }

        public OperationsImportDialog()
        {
            InitializeComponent();
            LoadOperTypes();

            fileBox.TextValueChanged += ChangeAcceptEnable;
            fileDopBox.TextValueChanged += ChangeAcceptEnable;
            operTypeBox.SelectedValueChanged += ChangeOperTypeValue;

            okButton.Enabled = false;
            TaskAccept = false;
        }

        private void ChangeOperTypeValue(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void ChangeAcceptEnable(object sender, TextChangedEventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private bool TaskAccept { get; set; }

        private bool IsCorrectData()
        {
            return (fileBox.TextValue != String.Empty &&
                fileDopBox.TextValue != String.Empty &&
                operTypeBox.SelectedValue != null
                );
        }

        private void Task()
        {
            try
            {
                decimal? opertype = ((decimal)operTypeBox.SelectedValue == -1) ? null : (decimal?)operTypeBox.SelectedValue;

                imp_exp.Module.Connection = omp_sepo.Module.Connection;

                TechnologyOperationsManager import = new TechnologyOperationsManager();
                import.LoadFromXml(fileBox.TextValue, fileDopBox.TextValue);
                import.Load(opertype);

                TaskAccept = true;

                MessageBox.Show(
                    "Операция успешно завершена!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            catch (Exception exc)
            {
                MessageBox.Show(
                    exc.Message + " " + exc.StackTrace,
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            finally
            {
                okButton.Enabled = true;
                cancelButton.Enabled = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (TaskAccept)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                okButton.Enabled = false;
                cancelButton.Enabled = false;

                Thread thread = new Thread(Task);
                thread.Start();
            }
        }
    }
}