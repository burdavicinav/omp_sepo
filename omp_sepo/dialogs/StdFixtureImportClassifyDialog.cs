using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdFixtureImportClassifyDialog : Form
    {
        public StdFixtureImportClassifyDialog()
        {
            InitializeComponent();

            SetTypesData();
            SetOwnersData();
            SetCatalogsData();

            nameBox.TextChanged += OnNameChanged;

            okButton.Enabled = IsCorrectData();
            okButton.Click += OnAcceptClicked;
        }

        private bool IsCorrectData()
        {
            return ownerBox.SelectedValue != null
                && typeBox.SelectedValue != null
                && nameBox.Text != String.Empty
                && catalogBox.SelectedValue != null;
        }

        private void SetTypesData()
        {
            var data = new[]{
                new { id = 1, name = "Стандартная оснастка" },
                new { id = 2, name = "Оснастка"}
                };

            typeBox.ValueMember = "id";
            typeBox.DisplayMember = "name";
            typeBox.DataSource = data;
        }

        private void SetCatalogsData()
        {
            Dictionary<decimal, string> catalogs = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = "select f_level, f_name from sepo_std_folders where f_owner = :f_owner";

            OracleParameter p_catalog = command.Parameters.Add("f_owner", null);
            p_catalog.Value = 0;

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                catalogs.Add(reader.GetDecimal(0), reader.GetDecimal(0) + " - " + reader.GetString(1));
            }

            catalogBox.ValueMember = "Key";
            catalogBox.DisplayMember = "Value";
            catalogBox.DataSource = new BindingSource(catalogs, null);
        }

        private void SetOwnersData()
        {
            Dictionary<decimal, string> owners = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
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

        private void OnNameChanged(object sender, EventArgs args)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void OnAcceptClicked(object sender, EventArgs args)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    int objtype = (int)typeBox.SelectedValue;

                    OracleCommand command = new OracleCommand();
                    command.Connection = obj_lib.Module.Connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_name", nameBox.Text);
                    command.Parameters.Add("p_owner", ownerBox.SelectedValue);
                    command.Parameters.Add("p_level", catalogBox.SelectedValue);

                    if (objtype == 1)
                    {
                        command.CommandText = "pkg_sepo_import_global.createstdclassify";
                    }
                    else
                    {
                        command.CommandText = "pkg_sepo_import_global.createoldfixtureclassify";
                    }

                    command.ExecuteNonQuery();

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