using obj_lib;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexSignDocsView : UserControl
    {
        private ContextMenuStrip menu;

        private void RefrechData()
        {
            foreach (var item in Module.Context.SEPO_TFLEX_SIGN_DOCS)
            {
                AddRow(item);
            }
        }

        private DataGridViewRow AddRow(SEPO_TFLEX_SIGN_DOCS item)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(scene, item.SIGN);
            row.Tag = item.ID;

            scene.Rows.Add(row);

            return row;
        }

        private void UpdateRow(DataGridViewRow row, SEPO_TFLEX_SIGN_DOCS item)
        {
            row.Cells[0].Value = item.SIGN;
        }

        private void InizializeMenu()
        {
            menu = new ContextMenuStrip();

            ToolStripItem addMenuItem = menu.Items.Add("Добавить");
            addMenuItem.Name = "Add";
            addMenuItem.Click += OnAddItemClick;

            ToolStripItem editMenuItem = menu.Items.Add("Изменить");
            editMenuItem.Name = "Edit";
            editMenuItem.Click += OnEditItemClick;

            ToolStripItem deleteMenuItem = menu.Items.Add("Удалить");
            deleteMenuItem.Name = "Delete";
            deleteMenuItem.Click += OnDeleteItemClick;

            menu.VisibleChanged += OnMenuVisible;

            this.ContextMenuStrip = menu;
        }

        public TFlexSignDocsView()
        {
            InitializeComponent();

            InizializeMenu();

            RefrechData();
        }

        protected void OnAddItemClick(object sender, EventArgs e)
        {
            TFlexDocsSignDialog dialog = new TFlexDocsSignDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SEPO_TFLEX_SIGN_DOCS sign =
                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Where(x => x.ID == dialog.Id).FirstOrDefault();

                AddRow(sign);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            decimal id = (decimal)row.Tag;

            TFlexDocsSignDialog dialog = new TFlexDocsSignDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SEPO_TFLEX_SIGN_DOCS sign =
                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Where(x => x.ID == dialog.Id).FirstOrDefault();

                UpdateRow(row, sign);
            }
        }

        protected void OnDeleteItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];

            decimal id = (decimal)row.Tag;

            SEPO_TFLEX_SIGN_DOCS sign =
                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Where(x => x.ID == id).FirstOrDefault();

            Module.Context.SEPO_TFLEX_SIGN_DOCS.Remove(sign);
            Module.Context.SaveChanges();

            scene.Rows.Remove(row);
        }

        protected void OnMenuVisible(object sender, EventArgs e)
        {
            if (menu.Visible)
            {
                menu.Items["Edit"].Enabled = scene.SelectedRows.Count > 0;
                menu.Items["Delete"].Enabled = scene.SelectedRows.Count > 0;
            }
        }
    }
}