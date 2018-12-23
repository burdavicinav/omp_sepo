using obj_lib;
using omp_sepo.dialogs;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class OsnTypesListView : ListView
    {
        private void AddItem(V_SEPO_OSN_TYPES tps)
        {
            ListViewItem item = new ListViewItem(tps.ShortName);
            item.SubItems.Add(tps.OmpName);
            item.Tag = tps.Id;

            Items.Add(item);
        }

        private void SelectObjects()
        {
            OracleCommand command = new OracleCommand(
                "select * from v_sepo_osn_types order by shortname",
                Module.Connection
                );

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                V_SEPO_OSN_TYPES tps = new V_SEPO_OSN_TYPES();
                tps.Id = reader.GetDecimal(0);
                if (!reader.IsDBNull(1)) tps.ShortName = reader.GetString(1);
                if (!reader.IsDBNull(2)) tps.OmpCode = reader.GetDecimal(2);
                if (!reader.IsDBNull(3)) tps.OmpName = reader.GetString(3);

                AddItem(tps);
            }
        }

        public OsnTypesListView()
        {
            this.View = View.Details;
            this.GridLines = true;
            this.MultiSelect = false;
            this.FullRowSelect = true;

            this.Columns.Add("Тип Search", -2);
            this.Columns.Add("Тип КИС Омега", -2);

            SelectObjects();

            this.MouseDoubleClick += OsnTypesListView_MouseDoubleClick;
        }

        private void OsnTypesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem row = this.SelectedItems[0];
            decimal id = (decimal)row.Tag;

            OsnTypesDialog dialog = new OsnTypesDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                row.SubItems[1].Text = dialog.OmpName;
            }
        }
    }
}