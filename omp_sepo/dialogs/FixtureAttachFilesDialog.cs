using imp_exp;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class FixtureAttachFilesDialog : Form
    {
        public FixtureAttachFilesDialog()
        {
            InitializeComponent();

            okButton.Enabled = IsCorrectData();

            directoryBox.TextValueChanged += DirectoryBox_TextValueChanged;
            detailsBox.SelectedValueChanged += FileBox_SelectedValueChanged;
            fixtureBox.SelectedValueChanged += FileBox_SelectedValueChanged;
            fixtureNodesBox.SelectedValueChanged += FileBox_SelectedValueChanged;
            okButton.Click += OkButton_Click;

            try
            {
                FillFileBoxes();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(
                    exc.Message + " " + exc.StackTrace,
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private bool IsCorrectData()
        {
            return (
                directoryBox.TextValue != "" &&
                detailsBox.SelectedValue != null &&
                fixtureBox.SelectedValue != null &&
                fixtureNodesBox.SelectedValue != null
                );
        }

        private void DirectoryBox_TextValueChanged(object s, ui_lib.TextChangedEventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void FileBox_SelectedValueChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private Dictionary<decimal, string> GetFileGroups(decimal botype)
        {
            Dictionary<decimal, string> groups = new Dictionary<decimal, string>();

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = "select code, name from attachments_groups where botype = :botype";
            command.Parameters.Add("botype", botype);

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                groups.Add(reader.GetDecimal(0), reader.GetString(1));
            }

            return groups;
        }

        private void SetFileGroupsFilter()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"insert into sepo_attachment_groups_filter(botype, grcode)
                                    values(:botype, :grcode)";

            OracleParameter p_botype = new OracleParameter("botype", OracleDbType.Decimal);
            OracleParameter p_grcode = new OracleParameter("grcode", OracleDbType.Decimal);

            command.Parameters.AddRange(new OracleParameter[] { p_botype, p_grcode });

            // детали
            p_botype.Value = 2;
            p_grcode.Value = detailsBox.SelectedValue;
            command.ExecuteNonQuery();

            // оснастка
            p_botype.Value = 32;
            p_grcode.Value = fixtureBox.SelectedValue;
            command.ExecuteNonQuery();

            // узлы оснастки
            p_botype.Value = 31;
            p_grcode.Value = fixtureNodesBox.SelectedValue;
            command.ExecuteNonQuery();
        }

        private void FillFileBoxes()
        {
            detailsBox.ValueMember = "Key";
            detailsBox.DisplayMember = "Value";
            detailsBox.DataSource = new BindingSource(GetFileGroups(2), null);

            fixtureBox.ValueMember = "Key";
            fixtureBox.DisplayMember = "Value";
            fixtureBox.DataSource = new BindingSource(GetFileGroups(32), null);

            fixtureNodesBox.ValueMember = "Key";
            fixtureNodesBox.DisplayMember = "Value";
            fixtureNodesBox.DataSource = new BindingSource(GetFileGroups(31), null);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                FixtureAttachFiles mgr = new FixtureAttachFiles();

                try
                {
                    mgr.CreateDocuments(directoryBox.TextValue);

                    SetFileGroupsFilter();
                    mgr.AttachDocuments();

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}