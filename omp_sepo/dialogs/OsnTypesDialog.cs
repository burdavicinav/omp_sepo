using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class OsnTypesDialog : Form
    {
        public decimal Id { get; set; }

        public string OmpName { get; private set; }

        private bool IsCorrect()
        {
            return (searchBox.SelectedItem != null
                && omegaBox.SelectedValue != null);
        }

        public OsnTypesDialog()
        {
            InitializeComponent();
        }

        public OsnTypesDialog(decimal id) : this()
        {
            Id = id;

            OracleCommand command = new OracleCommand(
                "select * from v_sepo_osn_types where id = :id",
                obj_lib.Module.Connection
                );

            command.Parameters.Add("id", id);
            OracleDataReader reader = command.ExecuteReader();
            reader.Read();

            string search_type = reader.GetString(1);
            searchBox.Items.Add(search_type);
            searchBox.SelectedIndex = 0;

            Dictionary<decimal, string> omp_types = new Dictionary<decimal, string>();
            omp_types.Add(0, null);

            OracleCommand command_tp = new OracleCommand(
                "select code, name from fixture_types order by name",
                obj_lib.Module.Connection
                );

            reader = command_tp.ExecuteReader();
            while (reader.Read())
            {
                omp_types.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            omegaBox.ValueMember = "Key";
            omegaBox.DisplayMember = "Value";
            omegaBox.DataSource = new BindingSource(omp_types, null);

            okButton.Enabled = IsCorrect();
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand command = new OracleCommand(
                    "update sepo_osn_types set id_type = :id_type where id = :id",
                    obj_lib.Module.Connection
                    );

                object id_type = null;
                if ((decimal)omegaBox.SelectedValue > 0) id_type = omegaBox.SelectedValue;

                command.Parameters.Add("id_type", id_type);
                command.Parameters.Add("id", Id);

                command.ExecuteNonQuery();
                OmpName = omegaBox.Text;

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