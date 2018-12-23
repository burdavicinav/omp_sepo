using obj_lib;
using omp_sepo.dialogs;
using Oracle.DataAccess.Client;
using System;
using System.Text;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo.views
{
    public class StdSchemesListView : ListView
    {
        private ContextMenuStrip menu;

        private ToolStripItem folders;

        public Control Form { get; set; }

        public StdSchemesListView()
        {
            //ViewSettings();
            //MenuSettings();
            //UpdateScene();
        }

        public StdSchemesListView(decimal lvl, bool is_edit_items)
        {
            //ViewSettings();
            //MenuSettings();
            //UpdateScene(lvl, is_edit_items);
        }

        public void ViewSettings()
        {
            //this.Enabled = false;
            this.View = View.Details;
            this.MultiSelect = false;
            this.FullRowSelect = true;
            this.CheckBoxes = true;
            this.GridLines = true;

            this.Columns.Add("Таблица", -2);
            this.Columns.Add("Верхний уровень", -2);
            this.Columns.Add("Каталог", -2);
            this.Columns.Add("Описание таблицы", -2);
            this.Columns.Add("Наименование схемы", -2);
            this.Columns.Add("Наименование схемы в КИС \"Омега\"", -2);

            //this.ItemCheck += StdSchemesListView_ItemCheck;
            this.MouseDoubleClick += StdSchemesListView_MouseDoubleClick;
            this.MouseClick += StdSchemesListView_MouseClick;
        }

        public void MenuSettings()
        {
            menu = new ContextMenuStrip();
            folders = menu.Items.Add("Каталоги");
            folders.Click += Folders_Click;
        }

        private void Folders_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.SelectedItems[0];
            StdFoldersView folders_view = new StdFoldersView((decimal)item.Tag);

            StringBuilder sb = new StringBuilder("Каталоги: ");
            sb.Append(item.Text);

            Control mdi_client = (Form.Parent);
            ((IMdiForm)(mdi_client.Parent)).AddChild(sb.ToString(), folders_view);
        }

        private void StdSchemesListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menu.Show(this, e.X, e.Y);
            }
        }

        private void StdSchemesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ListViewItem item = this.GetItemAt(e.X, e.Y);
                item.Checked = !item.Checked;

                StdSchemeEditDialog dialog = new StdSchemeEditDialog(
                    (decimal)item.Tag,
                    item.SubItems[1].Text,
                    item.SubItems[0].Text,
                    item.SubItems[2].Text,
                    item.SubItems[3].Text,
                    item.SubItems[4].Text,
                    item.SubItems[5].Text
                    );

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[5].Text = dialog.OmpScheme;
                    item.Checked = true;
                }
            }
        }

        private void StdSchemesListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            e.NewValue = e.CurrentValue;
        }

        private void AddItem(V_SEPO_STD_SCHEMES item)
        {
            ListViewItem vitem = new ListViewItem();
            vitem.Tag = item.IdRecord;
            vitem.Checked = (item.IsEdit == 1) ? true : false;

            vitem.Text = item.FTable;
            vitem.SubItems.Add(item.HLevel.ToString());
            vitem.SubItems.Add(item.FName);
            vitem.SubItems.Add(item.TblDescr);
            vitem.SubItems.Add(item.SchemeName);
            vitem.SubItems.Add(item.OmpName);

            this.Items.Add(vitem);
        }

        public void UpdateScene(decimal lvl = -1, bool is_edit_items = false)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;

            StringBuilder comstr = new StringBuilder();
            comstr.Append(@"select id_record, f_table, h_level, f_name, tbl_descr,
                                    scheme_name, omp_name, istable, isedit
                                    from v_sepo_std_schemes where 1=1");

            if (lvl > -1)
            {
                comstr.Append(" and h_level = :lvl");
                command.Parameters.Add("lvl", lvl);
            }

            if (is_edit_items)
            {
                comstr.Append(" and isedit = :isedit");
                command.Parameters.Add("isedit", 1);
            }

            comstr.Append(" order by f_table");

            command.CommandText = comstr.ToString();

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    V_SEPO_STD_SCHEMES item = new V_SEPO_STD_SCHEMES();
                    item.IdRecord = reader.GetDecimal(0);
                    item.FTable = reader.GetString(1);
                    item.HLevel = reader.GetDecimal(2);
                    item.FName = reader.GetString(3);
                    item.TblDescr = reader.GetString(4);
                    item.SchemeName = reader.GetString(5);
                    item.OmpName = reader.GetString(6);
                    item.IsTable = reader.GetInt16(7);
                    item.IsEdit = reader.GetInt16(8);

                    AddItem(item);
                }
            }
        }
    }
}