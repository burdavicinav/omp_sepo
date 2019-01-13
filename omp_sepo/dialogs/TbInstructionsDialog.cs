using imp_exp;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TbInstructionsDialog : Form
    {
        private void SetStateFilter()
        {
            OracleCommand state_command = new OracleCommand();
            state_command.Connection = obj_lib.Module.Connection;
            state_command.CommandText =
                "select code, name from businessobj_states where botype = :botype order by name";

            OracleParameter p_type = new OracleParameter("botype", 267);
            state_command.Parameters.Add(p_type);

            Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
            OracleDataReader reader = state_command.ExecuteReader();
            while (reader.Read())
            {
                dict.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            stateBox.ValueMember = "Key";
            stateBox.DisplayMember = "Value";
            stateBox.DataSource = new BindingSource(dict, null);
        }

        private void SetOwnerFilter()
        {
            OracleCommand owner_command = new OracleCommand();
            owner_command.Connection = obj_lib.Module.Connection;
            owner_command.CommandText =
                "select owner, name from owner_name order by name";

            OracleParameter p_type = new OracleParameter("botype", 267);
            owner_command.Parameters.Add(p_type);

            Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
            OracleDataReader reader = owner_command.ExecuteReader();
            while (reader.Read())
            {
                dict.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            ownerBox.ValueMember = "Key";
            ownerBox.DisplayMember = "Value";
            ownerBox.DataSource = new BindingSource(dict, null);
        }

        private bool IsCorrectData()
        {
            return (filePathBox.TextValue != String.Empty
                    && stateBox.SelectedValue != null
                    && ownerBox.SelectedValue != null);
        }

        public TbInstructionsDialog()
        {
            InitializeComponent();

            SetStateFilter();
            SetOwnerFilter();

            okButton.Enabled = IsCorrectData();
            filePathBox.TextValueChanged += FilePathBox_TextValueChanged;
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                TbInstructionsManager mgr = new TbInstructionsManager();
                mgr.LoadFromXml(filePathBox.TextValue);
                mgr.Load(
                    (decimal)stateBox.SelectedValue,
                    (decimal)ownerBox.SelectedValue
                    );

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
                MessageBox.Show(
                    exc.Message + " " + exc.StackTrace,
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void FilePathBox_TextValueChanged(object s, ui_lib.TextChangedEventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }
    }
}