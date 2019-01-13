using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdFoxProAttrsView : ListView
    {
        private IRepository<SEPO_STD_FOXPRO_ATTRS> attrsRepo;

        private ContextMenuStrip menu;
        private ToolStripItem addMenuItem, editMenuItem, deleteMenuItem;

        public StdFoxProAttrsView(IRepository<SEPO_STD_FOXPRO_ATTRS> repo)
        {
            attrsRepo = repo;

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

        public StdFoxProAttrsView() : this(new Repository<SEPO_STD_FOXPRO_ATTRS>())
        {
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
            StdFoxProAttrsDialog dlg = new StdFoxProAttrsDialog(attrsRepo);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AddItem(dlg.Attr);
            }
        }

        private void OnEditItemClick(object sender, EventArgs args)
        {
            if (this.SelectedItems.Count > 0)
            {
                ListViewItem item = this.SelectedItems[0];

                int id = (int)item.Tag;
                SEPO_STD_FOXPRO_ATTRS attr = attrsRepo.GetById(id);

                StdFoxProAttrsDialog dlg = new StdFoxProAttrsDialog(attr, attrsRepo);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    UpdateItem(item, dlg.Attr);
                }
            }
        }

        private void OnDeleteItemClick(object sender, EventArgs args)
        {
            if (this.SelectedItems.Count > 0)
            {
                ListViewItem item = this.SelectedItems[0];
                decimal id = (int)item.Tag;

                try
                {
                    SEPO_STD_FOXPRO_ATTRS attr = attrsRepo.GetById(id);
                    attrsRepo.Delete(attr);

                    this.Items.Remove(item);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void AddItem(SEPO_STD_FOXPRO_ATTRS attr)
        {
            ListViewItem item = new ListViewItem(attr.SHORTNAME);
            item.SubItems.Add(attr.NAME);
            item.SubItems.Add(attr.TYPENAME);
            item.Tag = attr.ID;

            this.Items.Add(item);
        }

        private void UpdateItem(ListViewItem item, SEPO_STD_FOXPRO_ATTRS attr)
        {
            item.SubItems[0].Text = attr.SHORTNAME;
            item.SubItems[1].Text = attr.NAME;
            item.SubItems[2].Text = attr.TYPENAME;
        }

        private void UpdateScene()
        {
            Items.Clear();

            foreach (var attr in attrsRepo.GetQuery().OrderBy(x => x.ID))
            {
                AddItem(attr);
            }
        }
    }
}