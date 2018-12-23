using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class FixtureImportObjectsDialog : Form
    {
        public FixtureImportObjectsDialog()
        {
            InitializeComponent();

            fixtureNodesBox.CheckedChanged += OnFixtureNodesBox;
            fixtureBox.CheckedChanged += OnFixtureBox;
            detailsBox.CheckedChanged += OnDetailsBox;
            specificationsBox.CheckedChanged += OnSpecificationsBox;
            okButton.Click += OnAcceptClicked;

            SetStateData();
            SetDefaultTypeData();
            SetUnitsData();
            SetOwnersData();

            okButton.Enabled = IsCorrectData();
        }

        private void OnFixtureNodesBox(object sender, EventArgs args)
        {
            fixtureNodesGroup.Enabled = fixtureNodesBox.Checked;
            okButton.Enabled = IsCorrectData();
        }

        private void OnFixtureBox(object sender, EventArgs args)
        {
            fixtureGroup.Enabled = fixtureBox.Checked;
            okButton.Enabled = IsCorrectData();
        }

        private void OnDetailsBox(object sender, EventArgs args)
        {
            detailsGroup.Enabled = detailsBox.Checked;
            okButton.Enabled = IsCorrectData();
        }

        private void OnSpecificationsBox(object sender, EventArgs args)
        {
            okButton.Enabled = IsCorrectData();
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

            unitsBox.ValueMember = "Key";
            unitsBox.DisplayMember = "Value";
            unitsBox.DataSource = new BindingSource(units, null);
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

            ownersBox.ValueMember = "Key";
            ownersBox.DisplayMember = "Value";
            ownersBox.DataSource = new BindingSource(owners, null);
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
            nodesStateBox.ValueMember = "Key";
            nodesStateBox.DisplayMember = "Value";
            nodesStateBox.DataSource = GetStateData(31);

            fixtureStateBox.ValueMember = "Key";
            fixtureStateBox.DisplayMember = "Value";
            fixtureStateBox.DataSource = GetStateData(32);

            detailsStateBox.ValueMember = "Key";
            detailsStateBox.DisplayMember = "Value";
            detailsStateBox.DataSource = GetStateData(2);
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

            nodesDefaultTypeBox.ValueMember = "Key";
            nodesDefaultTypeBox.DisplayMember = "Value";
            nodesDefaultTypeBox.DataSource = new BindingSource(types, null);

            fixtureDefaultTypeBox.ValueMember = "Key";
            fixtureDefaultTypeBox.DisplayMember = "Value";
            fixtureDefaultTypeBox.DataSource = new BindingSource(types, null);
        }

        private bool IsCorrectData()
        {
            return unitsBox.SelectedValue != null && ownersBox.SelectedValue != null &&
                (fixtureNodesBox.Checked && nodesStateBox.SelectedValue != null &&
                nodesDefaultTypeBox.SelectedValue != null
                ||
                fixtureBox.Checked && fixtureStateBox.SelectedValue != null &&
                fixtureDefaultTypeBox.SelectedValue != null
                ||
                detailsBox.Checked && detailsStateBox.SelectedValue != null
                ||
                specificationsBox.Checked
                );
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

        private void OnAcceptClicked(object sender, EventArgs args)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    DisableTriggers();

                    OracleCommand command;

                    if (fixtureNodesBox.Checked)
                    {
                        command = new OracleCommand();
                        command.Connection = Module.Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pkg_sepo_import_global.loadfixturenodes";

                        command.Parameters.Add("p_owner", ownersBox.SelectedValue);
                        command.Parameters.Add("p_meascode", unitsBox.SelectedValue);
                        command.Parameters.Add("p_state", nodesStateBox.SelectedValue);
                        command.Parameters.Add("p_type_default", nodesDefaultTypeBox.SelectedValue);

                        command.ExecuteNonQuery();
                    }

                    if (fixtureBox.Checked)
                    {
                        command = new OracleCommand();
                        command.Connection = Module.Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pkg_sepo_import_global.loadfixture";

                        command.Parameters.Add("p_owner", ownersBox.SelectedValue);
                        command.Parameters.Add("p_meascode", unitsBox.SelectedValue);
                        command.Parameters.Add("p_state", fixtureStateBox.SelectedValue);
                        command.Parameters.Add("p_type_default", fixtureDefaultTypeBox.SelectedValue);

                        command.ExecuteNonQuery();
                    }

                    if (detailsBox.Checked)
                    {
                        command = new OracleCommand();
                        command.Connection = Module.Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pkg_sepo_import_global.loadfixturedetails";

                        command.Parameters.Add("p_owner", ownersBox.SelectedValue);
                        command.Parameters.Add("p_meascode", unitsBox.SelectedValue);
                        command.Parameters.Add("p_state", detailsStateBox.SelectedValue);

                        command.ExecuteNonQuery();
                    }

                    if (specificationsBox.Checked)
                    {
                        command = new OracleCommand();
                        command.Connection = Module.Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pkg_sepo_import_global.loadfixturespecifications";

                        command.Parameters.Add("p_meascode", unitsBox.SelectedValue);
                        command.ExecuteNonQuery();
                    }

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