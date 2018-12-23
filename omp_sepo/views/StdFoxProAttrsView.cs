using obj_lib;
using omp_sepo.dialogs;
using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdFoxProAttrsView : ListView
    {
        private ContextMenuStrip menu;
        private ToolStripItem addMenuItem, editMenuItem, deleteMenuItem;

        public StdFoxProAttrsView()
        {
            this.View = View.Details;
            this.MultiSelect = false;
            this.FullRowSelect = true;
            this.GridLines = true;

            this.Columns.Add("Краткое наименование", -2);
            this.Columns.Add("Наименование", -2);
            this.Columns.Add("Тип", -2);

            this.MouseDown += OnSceneClick;

            InizializeMenu();
            UpdateScene();
        }

        private void OnSceneClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menu.Show(this, e.X, e.Y);

                if (this.SelectedItems.Count == 0)
                {
                    editMenuItem.Enabled = false;
                    deleteMenuItem.Enabled = false;
                }
                else
                {
                    editMenuItem.Enabled = true;
                    deleteMenuItem.Enabled = true;
                }
            }
        }

        private void InizializeMenu()
        {
            menu = new ContextMenuStrip();

            addMenuItem = menu.Items.Add("Добавить");
            addMenuItem.Click += OnAddItemClick;

            editMenuItem = menu.Items.Add("Изменить");
            editMenuItem.Click += OnEditItemClick;

            deleteMenuItem = menu.Items.Add("Удалить");
            deleteMenuItem.Click += OnDeleteItemClick;
        }

        private void OnAddItemClick(object sender, EventArgs args)
        {
            StdFoxProAttrsDialog dlg = new StdFoxProAttrsDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                V_SEPO_STD_FOXPRO_ATTRS attr = new V_SEPO_STD_FOXPRO_ATTRS();
                attr.Id = dlg.Id;
                attr.Name = dlg.Name_;
                attr.Shortname = dlg.Shortname;
                attr.Type_ = dlg.Type_;

                AddItem(attr);
            }
        }

        private void OnEditItemClick(object sender, EventArgs args)
        {
            if (this.SelectedItems.Count > 0)
            {
                ListViewItem item = this.SelectedItems[0];

                StdFoxProAttrsDialog dlg = new StdFoxProAttrsDialog(
                    (decimal)item.Tag,
                    item.SubItems[0].Text,
                    item.SubItems[1].Text,
                    item.SubItems[2].Text
                    );

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    V_SEPO_STD_FOXPRO_ATTRS attr = new V_SEPO_STD_FOXPRO_ATTRS();
                    attr.Id = dlg.Id;
                    attr.Name = dlg.Name_;
                    attr.Shortname = dlg.Shortname;
                    attr.Type_ = dlg.Type_;

                    UpdateItem(item, attr);
                }
            }
        }

        private void OnDeleteItemClick(object sender, EventArgs args)
        {
            if (this.SelectedItems.Count > 0)
            {
                ListViewItem item = this.SelectedItems[0];
                decimal id = (decimal)item.Tag;

                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = Module.Connection;
                    cmd.CommandText = "delete from sepo_std_foxpro_attrs where id = :id";

                    cmd.Parameters.Add("id", id);
                    cmd.ExecuteNonQuery();

                    this.Items.Remove(item);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void AddItem(V_SEPO_STD_FOXPRO_ATTRS attr)
        {
            ListViewItem item = new ListViewItem(attr.Shortname);
            item.SubItems.Add(attr.Name);
            item.SubItems.Add(attr.TypeName);
            item.Tag = attr.Id;

            this.Items.Add(item);
        }

        private void UpdateItem(ListViewItem item, V_SEPO_STD_FOXPRO_ATTRS attr)
        {
            item.SubItems[0].Text = attr.Shortname;
            item.SubItems[1].Text = attr.Name;
            item.SubItems[2].Text = attr.TypeName;
        }

        private void UpdateScene()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Module.Connection;
            cmd.CommandText = "select id, shortname, name, type_ from sepo_std_foxpro_attrs order by id";

            using (OracleDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    V_SEPO_STD_FOXPRO_ATTRS attr = new V_SEPO_STD_FOXPRO_ATTRS();
                    attr.Id = rd.GetDecimal(0);
                    attr.Shortname = rd.GetString(1);
                    attr.Name = rd.GetString(2);
                    attr.Type_ = rd.GetInt16(3);

                    AddItem(attr);
                }
            }
        }
    }
}