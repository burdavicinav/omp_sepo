using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdSchemesDialog : Form
    {
        public decimal Lvl
        {
            get
            {
                return (decimal)lvlBox.SelectedValue;
            }
        }

        public bool IsEditItems
        {
            get
            {
                return editBox.Checked;
            }
        }

        public StdSchemesDialog()
        {
            InitializeComponent();

            FillData();
        }

        private void FillData()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = "select f_level, f_name from sepo_std_folders where f_owner = :f_owner";

            OracleParameter p_owner = command.Parameters.Add("f_owner", null);
            p_owner.Value = 0;

            Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
            dict.Add(-1, null);

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dict.Add(reader.GetDecimal(0), reader.GetDecimal(0) + " - " + reader.GetString(1));
                }
            }

            lvlBox.ValueMember = "Key";
            lvlBox.DisplayMember = "Value";
            lvlBox.DataSource = new BindingSource(dict, null);
        }
    }
}