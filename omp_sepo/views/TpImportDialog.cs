using imp_exp;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TpImportDialog : Form
    {
        public TpImportDialog()
        {
            InitializeComponent();

            SetGroupData();
            SetLetterData();
            SetOwnerData();
            SetStateData();

            okButton.Enabled = IsAcceptButton();
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            OnAccept();
        }

        private bool IsAcceptButton()
        {
            return (
                prodBox.SelectedValue != null &&
                literaBox.SelectedValue != null &&
                stateBox.SelectedValue != null &&
                ownerBox.SelectedValue != null
                );
        }

        private void SetGroupData()
        {
            Dictionary<decimal, string> groups = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, name from distr_group_ext order by name";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                groups.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            prodBox.ValueMember = "Key";
            prodBox.DisplayMember = "Value";
            prodBox.DataSource = new BindingSource(groups, null);
        }

        private void SetLetterData()
        {
            Dictionary<decimal, string> letters = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, name from letters order by name";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                letters.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            literaBox.ValueMember = "Key";
            literaBox.DisplayMember = "Value";
            literaBox.DataSource = new BindingSource(letters, null);
        }

        private void SetOwnerData()
        {
            Dictionary<decimal, string> owners = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select owner, name from owner_name order by name";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                owners.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            ownerBox.ValueMember = "Key";
            ownerBox.DisplayMember = "Value";
            ownerBox.DataSource = new BindingSource(owners, null);
        }

        private void SetStateData()
        {
            Dictionary<decimal, string> states = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, name from businessobj_states
                                    where botype = :id_type order by name";

            command.Parameters.Add("id_type", 60);

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                states.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            stateBox.ValueMember = "Key";
            stateBox.DisplayMember = "Value";
            stateBox.DataSource = new BindingSource(states, null);
        }

        private void OnAccept()
        {
            try
            {
                imp_exp.Module.Connection = Module.Connection;
                TPManager manager = new TPManager();
                manager.Load(
                    (decimal)prodBox.SelectedValue,
                    (decimal)literaBox.SelectedValue,
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
    }
}