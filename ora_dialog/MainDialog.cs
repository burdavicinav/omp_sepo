using general;
using Oracle.DataAccess.Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ora_dialog
{
    public partial class MainDialog : Form
    {
        private Version AssemblyVersion { get; set; }

        public OracleConnection Connection { get; set; }

        public string ConnectionString { get; set; }

        public ConnectionObject[] Connections { get; set; }

        public MainDialog(Version asmVersion)
        {
            AssemblyVersion = asmVersion;

            InitializeComponent();

            okButton.Enabled = false;
            this.Text = "v." + AssemblyVersion.ToString();
        }

        public MainDialog(Version version, ConnectionObject[] connections) : this(version)
        {
            Connections = connections;

            try
            {
                serverBox.DisplayMember = "Name";
                serverBox.ValueMember = "Tns";
                serverBox.DataSource = Connections;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SetConnection();
        }

        private bool IsCorrectData()
        {
            return
                serverBox.SelectedValue != null &&
                loginBox.Text != String.Empty &&
                passBox.Text != String.Empty;
        }

        private void serverBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
            serverBox.BackColor = (serverBox.SelectedValue != null) ?
                SystemColors.Window : SystemColors.Info;
        }

        private void loginBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
            loginBox.BackColor = (loginBox.Text != String.Empty) ?
                SystemColors.Window : SystemColors.Info;
        }

        private void passBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
            passBox.BackColor = (passBox.Text != String.Empty) ?
                SystemColors.Window : SystemColors.Info;
        }

        private void VerifyVersion()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Connection;
            command.CommandText = "select property_value from omp_sepo_properties where id = 1";

            string prop_value = command.ExecuteScalar().ToString();

            string current_version = AssemblyVersion.ToString();
            if (prop_value != current_version) throw new VersionException();
        }

        private void SetConnection()
        {
            OracleConnectionStringBuilder connectionString = new OracleConnectionStringBuilder();
            connectionString.DataSource = serverBox.SelectedValue.ToString();
            connectionString.UserID = loginBox.Text;
            connectionString.Password = passBox.Text;

            ConnectionString = connectionString.ToString();

            OracleConnection connection = new OracleConnection(ConnectionString);

            try
            {
                connection.Open();
                Connection = connection;

                VerifyVersion();
                DialogResult = DialogResult.OK;
            }
            catch (OracleException exc)
            {
                MessageBox.Show(
                    exc.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (VersionException exc)
            {
                MessageBox.Show(
                    exc.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}