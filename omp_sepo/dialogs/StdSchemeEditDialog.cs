using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdSchemeEditDialog : Form
    {
        public string OmpScheme
        {
            get
            {
                return ompSchemeBox.Text;
            }
        }

        private int IdRecord { get; set; }

        public StdSchemeEditDialog()
        {
            InitializeComponent();

            ompSchemeBox.TextChanged += OmpSchemeBox_TextChanged;
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = obj_lib.Module.Connection;
                command.CommandText = @"update sepo_std_schemes
                                        set omp_name = :omp_name, isedit = :isedit where id_record = :id_record";

                command.Parameters.Add("omp_name", ompSchemeBox.Text);
                command.Parameters.Add("isedit", 1);
                command.Parameters.Add("id_record", IdRecord);

                command.ExecuteNonQuery();

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

        public StdSchemeEditDialog(
            int id_record,
            string lvl,
            string tname,
            string catalog,
            string t_descr,
            string scheme,
            string omp_scheme
            ) : this()
        {
            IdRecord = id_record;

            lvlBox.Text = lvl;
            tableBox.Text = tname;
            catalogBox.Text = catalog;
            tableDescrBox.Text = t_descr;
            schemeBox.Text = scheme;
            ompSchemeBox.Text = omp_scheme;
        }

        private void OmpSchemeBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = ompSchemeBox.Text != String.Empty;
        }
    }
}