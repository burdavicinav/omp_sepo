using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdFixtureOmpImportDialog : Form
    {
        public StdFixtureOmpImportDialog()
        {
            InitializeComponent();

            SetOwnersData();
            SetStateData();
            SetUnitsData();
            SetDefaultTypeData();

            okButton.Enabled = IsCorrectData();
            okButton.Click += OnAcceptClicked;
        }

        private void DisableTriggers()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select trigger_name from sepo_import_triggers_disable";

            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                OracleCommand exec_command = new OracleCommand();
                exec_command.Connection = Module.Connection;
                exec_command.CommandText =
                    @"alter trigger " + reader.GetString(0) + " disable";

                exec_command.ExecuteNonQuery();
            }
        }

        private void EnableTriggers()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select trigger_name from sepo_import_triggers_disable";

            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                OracleCommand exec_command = new OracleCommand();
                exec_command.Connection = Module.Connection;
                exec_command.CommandText =
                    @"alter trigger " + reader.GetString(0) + " enable";

                exec_command.ExecuteNonQuery();
            }
        }

        private bool IsCorrectData()
        {
            return gostOwnerBox.SelectedValue != null
                && gostStateBox.SelectedValue != null
                && stdTypeBox.SelectedValue != null
                && stdOwnerBox.SelectedValue != null
                && stdMeasBox.SelectedValue != null
                && stdStateBox.SelectedValue != null
                && fixTypeBox.SelectedValue != null
                && fixOwnerBox.SelectedValue != null
                && fixMeasBox.SelectedValue != null
                && fixStateBox.SelectedValue != null;
        }

        private void SetOwnersData()
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

            gostOwnerBox.ValueMember = "Key";
            gostOwnerBox.DisplayMember = "Value";
            gostOwnerBox.DataSource = new BindingSource(owners, null);

            stdOwnerBox.ValueMember = "Key";
            stdOwnerBox.DisplayMember = "Value";
            stdOwnerBox.DataSource = new BindingSource(owners, null);

            fixOwnerBox.ValueMember = "Key";
            fixOwnerBox.DisplayMember = "Value";
            fixOwnerBox.DataSource = new BindingSource(owners, null);
        }

        private BindingSource GetStateData(decimal id_type)
        {
            Dictionary<decimal, string> states = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, name from businessobj_states
                                    where botype = :id_type order by name";

            command.Parameters.Add("id_type", id_type);

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                states.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            return new BindingSource(states, null);
        }

        private void SetStateData()
        {
            gostStateBox.ValueMember = "Key";
            gostStateBox.DisplayMember = "Value";
            gostStateBox.DataSource = GetStateData(256);

            stdStateBox.ValueMember = "Key";
            stdStateBox.DisplayMember = "Value";
            stdStateBox.DataSource = GetStateData(33);

            fixStateBox.ValueMember = "Key";
            fixStateBox.DisplayMember = "Value";
            fixStateBox.DataSource = GetStateData(32);
        }

        private void SetUnitsData()
        {
            Dictionary<decimal, string> units = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, shortname from measures order by shortname";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                units.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            stdMeasBox.ValueMember = "Key";
            stdMeasBox.DisplayMember = "Value";
            stdMeasBox.DataSource = new BindingSource(units, null);

            fixMeasBox.ValueMember = "Key";
            fixMeasBox.DisplayMember = "Value";
            fixMeasBox.DataSource = new BindingSource(units, null);
        }

        private void SetDefaultTypeData()
        {
            Dictionary<decimal, string> types = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select code, name from fixture_types order by name";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                types.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            stdTypeBox.ValueMember = "Key";
            stdTypeBox.DisplayMember = "Value";
            stdTypeBox.DataSource = new BindingSource(types, null);

            fixTypeBox.ValueMember = "Key";
            fixTypeBox.DisplayMember = "Value";
            fixTypeBox.DataSource = new BindingSource(types, null);
        }

        private void OnAcceptClicked(object sender, EventArgs args)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    DisableTriggers();

                    OracleCommand command;

                    command = new OracleCommand();
                    command.Connection = Module.Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.importstdgosts";

                    command.Parameters.Add("p_owner", gostOwnerBox.SelectedValue);
                    command.Parameters.Add("p_state", gostStateBox.SelectedValue);
                    command.ExecuteNonQuery();

                    command = new OracleCommand();
                    command.Connection = Module.Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.loadstdfixture";

                    command.Parameters.Add("p_type_default", stdTypeBox.SelectedValue);
                    command.Parameters.Add("p_owner", stdOwnerBox.SelectedValue);
                    command.Parameters.Add("p_meascode", stdMeasBox.SelectedValue);
                    command.Parameters.Add("p_state", stdStateBox.SelectedValue);
                    command.ExecuteNonQuery();

                    command = new OracleCommand();
                    command.Connection = Module.Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.loadoldfixture";

                    command.Parameters.Add("p_type_default", fixTypeBox.SelectedValue);
                    command.Parameters.Add("p_owner", fixOwnerBox.SelectedValue);
                    command.Parameters.Add("p_meascode", fixMeasBox.SelectedValue);
                    command.Parameters.Add("p_state", fixStateBox.SelectedValue);
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
                finally
                {
                    EnableTriggers();
                }
            }
        }
    }
}