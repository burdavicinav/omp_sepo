using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class EquipmentModelsImport : Form
    {
        private void SetClassifyData()
        {
            List<NameObject> objects = new List<NameObject>();

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"select code, clcode || ': ' || clname as name
                                    from classify where cltype = :cltype order by clname";

            command.Parameters.Add("cltype", 15);

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                NameObject obj = new NameObject();
                obj.Code = reader.GetDecimal(0);
                obj.Name = reader.GetString(1);

                objects.Add(obj);
            }

            classifyBox.ValueMember = "code";
            classifyBox.DisplayMember = "name";
            classifyBox.DataSource = objects;
        }

        private void SetOwnerData()
        {
            List<NameObject> objects = new List<NameObject>();

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"select owner, name from owner_name order by name";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                NameObject obj = new NameObject();
                obj.Code = reader.GetDecimal(0);
                obj.Name = reader.GetString(1);

                objects.Add(obj);
            }

            ownerBox.ValueMember = "code";
            ownerBox.DisplayMember = "name";
            ownerBox.DataSource = objects;
        }

        private bool IsCorrectData()
        {
            return (
                classifyBox.SelectedValue != null &&
                ownerBox.SelectedValue != null &&
                fileBox.TextValue != String.Empty
                );
        }

        private void ChangeAcceptEnabled(object sender, EventArgs args)
        {
            okButton.Enabled = IsCorrectData();
        }

        public EquipmentModelsImport()
        {
            InitializeComponent();

            fileBox.Filter = "(*.xml)|*.xml";
            fileBox.Mode = ui_lib.UiFileMode.Open;

            classifyBox.SelectedValueChanged += ChangeAcceptEnabled;
            ownerBox.SelectedValueChanged += ChangeAcceptEnabled;
            fileBox.TextValueChanged += ChangeAcceptEnabled;

            okButton.Enabled = false;
            SetClassifyData();
            SetOwnerData();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                decimal classify = Convert.ToDecimal(classifyBox.SelectedValue);
                decimal owner = Convert.ToDecimal(ownerBox.SelectedValue);

                imp_exp.ModelsEquipmentManager mngr = new imp_exp.ModelsEquipmentManager();

                mngr.LoadFromXml(fileBox.TextValue);
                mngr.Load(classify, owner);

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