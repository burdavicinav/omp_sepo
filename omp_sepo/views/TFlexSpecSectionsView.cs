using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexSpecSectionsView : UserControl
    {
        private IRepository<SEPO_TFLEX_SPEC_SECTIONS> sectionsRepo;

        private ContextMenuStrip menu;

        private void RefrechData()
        {
            foreach (var item in sectionsRepo.GetQuery())
            {
                //obj_lib.Module.OpenSession().Evict(item);
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

        public TFlexSpecSectionsView(IRepository<SEPO_TFLEX_SPEC_SECTIONS> repo)
        {
            sectionsRepo = repo;
        }

        public TFlexSpecSectionsView() : this(new Repository<SEPO_TFLEX_SPEC_SECTIONS>())
        {
            InitializeComponent();

            InizializeMenu();

            RefrechData();
        }

        protected void OnAddItemClick(object sender, EventArgs e)
        {
            TFlexSpecSectionDialog dialog = new TFlexSpecSectionDialog(sectionsRepo);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddRow(dialog.TFlexSpecSection);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            int id = (int)row.Tag;

            SEPO_TFLEX_SPEC_SECTIONS section = sectionsRepo.GetById(id);

            TFlexSpecSectionDialog dialog = new TFlexSpecSectionDialog(section);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                UpdateRow(row, section);
            }
        }

        protected void OnDeleteItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];

            int id = (int)row.Tag;

            using (UnitOfWork transaction = new UnitOfWork())
            {
                SEPO_TFLEX_SPEC_SECTIONS section = sectionsRepo.GetById(id);

                try
                {
                    sectionsRepo.Delete(section);
                    transaction.Commit();

                    scene.Rows.Remove(row);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
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