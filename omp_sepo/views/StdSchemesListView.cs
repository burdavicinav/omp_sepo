using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo.views
{
    public class StdSchemesListView : ListView
    {
        private ContextMenuStrip menu;

        private ToolStripItem folders;

        private IViewRepository<V_SEPO_STD_SCHEMES> schemesRepo;

        public Control Form { get; set; }

        public StdSchemesListView(IViewRepository<V_SEPO_STD_SCHEMES> repo)
        {
            schemesRepo = repo;
        }

        public StdSchemesListView() : this(new ViewRepository<V_SEPO_STD_SCHEMES>())
        {
            //ViewSettings();
            //MenuSettings();
            //UpdateScene();
        }

        public StdSchemesListView(decimal lvl, bool is_edit_items) : this()
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
            StdFoldersView folders_view = new StdFoldersView((int)item.Tag);

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
                    (int)item.Tag,
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
            vitem.Tag = item.ID_RECORD;
            vitem.Checked = (item.ISEDIT == 1) ? true : false;

            vitem.Text = item.F_TABLE;
            vitem.SubItems.Add(item.H_LEVEL.ToString());
            vitem.SubItems.Add(item.F_NAME);
            vitem.SubItems.Add(item.TBL_DESCR);
            vitem.SubItems.Add(item.SCHEME_NAME);
            vitem.SubItems.Add(item.OMP_NAME);

            this.Items.Add(vitem);
        }

        public void UpdateScene(decimal lvl = -1, bool is_edit_items = false)
        {
            var schemes = schemesRepo.GetQuery();

            if (lvl > -1)
            {
                schemes = schemes.Where(x => x.H_LEVEL == lvl);
            }

            if (is_edit_items)
            {
                schemes = schemes.Where(x => x.ISEDIT == ((is_edit_items) ? 1 : 0));
            }

            schemes.OrderBy(x => x.F_TABLE);

            foreach (var scheme in schemes)
            {
                AddItem(scheme);
            }
        }
    }
}