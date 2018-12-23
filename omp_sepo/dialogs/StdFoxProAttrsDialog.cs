using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdFoxProAttrsDialog : Form
    {
        public DialogType DialogType { get; private set; }

        public decimal Id { get; set; }

        public string Shortname
        {
            get
            {
                return shortnameBox.Text;
            }
        }

        public string Name_
        {
            get
            {
                return nameBox.Text;
            }
        }

        public short Type_
        {
            get
            {
                return (short)typeBox.SelectedValue;
            }
        }

        public StdFoxProAttrsDialog()
        {
            InitializeComponent();

            DialogType = DialogType.Add;
            this.Text = "Создание атрибута";
            SetData();
            okButton.Enabled = IsCorrectData();
            okButton.Click += OnOkClicked;

            shortnameBox.TextChanged += OnDataChanged;
            nameBox.TextChanged += OnDataChanged;
            typeBox.SelectedValueChanged += OnDataChanged;
        }

        public StdFoxProAttrsDialog(decimal id, string shortname, string name, string type)
        {
            InitializeComponent();

            DialogType = DialogType.Edit;
            this.Text = "Редактирование атрибута";
            SetData();
            okButton.Enabled = IsCorrectData();
            okButton.Click += OnOkClicked;

            shortnameBox.TextChanged += OnDataChanged;
            nameBox.TextChanged += OnDataChanged;
            typeBox.SelectedValueChanged += OnDataChanged;

            Id = id;
            shortnameBox.Text = shortname;
            nameBox.Text = name;
            typeBox.Text = type;
        }

        private void OnDataChanged(object sender, EventArgs args)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void SetData()
        {
            var type = new[] {
                new { id = (short)1, name = "Строка" },
                new { id = (short)2, name = "Число" },
                new { id = (short)3, name = "Целое число" },
                new { id = (short)4, name = "Дата" }
                };

            typeBox.ValueMember = "id";
            typeBox.DisplayMember = "name";
            typeBox.DataSource = type;
        }

        private bool IsCorrectData()
        {
            return shortnameBox.Text != String.Empty
                && nameBox.Text != String.Empty
                && typeBox.SelectedValue != null;
        }

        private void OnOkClicked(object sender, EventArgs args)
        {
            if (DialogType == DialogType.Add)
            {
                try
                {
                    OracleCommand sq_cmd = new OracleCommand();
                    sq_cmd.Connection = Module.Connection;
                    sq_cmd.CommandText = "select sq_sepo_std_foxpro_attrs.nextval from dual";

                    decimal id = (decimal)sq_cmd.ExecuteScalar();

                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = Module.Connection;
                    cmd.CommandText = @"insert into sepo_std_foxpro_attrs (id, shortname, name, type_)
                                    values (:id, :shortname, :name, :type_)";

                    OracleParameter p_id = new OracleParameter("id", id);
                    OracleParameter p_shortname = new OracleParameter("shortname", shortnameBox.Text);
                    OracleParameter p_name = new OracleParameter("name", nameBox.Text);
                    OracleParameter p_type = new OracleParameter("type_", typeBox.SelectedValue);

                    cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_shortname, p_name, p_type });
                    cmd.ExecuteNonQuery();

                    Id = id;
                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = Module.Connection;
                    cmd.CommandText = @"update sepo_std_foxpro_attrs set shortname = :shortname,
                                        name = :name, type_ = :type_ where id = :id";

                    OracleParameter p_id = new OracleParameter("id", Id);
                    OracleParameter p_shortname = new OracleParameter("shortname", shortnameBox.Text);
                    OracleParameter p_name = new OracleParameter("name", nameBox.Text);
                    OracleParameter p_type = new OracleParameter("type_", typeBox.SelectedValue);

                    cmd.Parameters.AddRange(new OracleParameter[] { p_shortname, p_name, p_type, p_id });
                    cmd.ExecuteNonQuery();

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}