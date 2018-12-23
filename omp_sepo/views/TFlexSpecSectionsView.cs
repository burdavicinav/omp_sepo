using obj_lib;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexSpecSectionsView : UserControl
    {
        private ContextMenuStrip menu;

        private void RefrechData()
        {
            foreach (var item in Module.Context.SEPO_TFLEX_SPEC_SECTIONS)
            {
                AddRow(item);
            }
        }

        private DataGridViewRow AddRow(SEPO_TFLEX_SPEC_SECTIONS item)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(scene, item.SECTION_);
            row.Tag = item.ID;

            scene.Rows.Add(row);

            return row;
        }

        private void UpdateRow(DataGridViewRow row, SEPO_TFLEX_SPEC_SECTIONS item)
        {
            row.Cells[0].Value = item.SECTION_;
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

        public TFlexSpecSectionsView()
        {
            InitializeComponent();

            InizializeMenu();

            RefrechData();
        }

        protected void OnAddItemClick(object sender, EventArgs e)
        {
            TFlexSpecSectionDialog dialog = new TFlexSpecSectionDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SEPO_TFLEX_SPEC_SECTIONS section =
                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Where(x => x.ID == dialog.Id).FirstOrDefault();

                AddRow(section);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            decimal id = (decimal)row.Tag;

            TFlexSpecSectionDialog dialog = new TFlexSpecSectionDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SEPO_TFLEX_SPEC_SECTIONS section =
                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Where(x => x.ID == dialog.Id).FirstOrDefault();

                UpdateRow(row, section);
            }
        }

        protected void OnDeleteItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];

            decimal id = (decimal)row.Tag;

            SEPO_TFLEX_SPEC_SECTIONS section =
                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Where(x => x.ID == id).FirstOrDefault();

            Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Remove(section);
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