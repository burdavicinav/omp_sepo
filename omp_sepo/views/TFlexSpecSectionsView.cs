using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexSpecSectionsView : UserControl
    {
        private IRepository<SEPO_TFLEX_SPEC_SECTIONS> sectionsRepo;
        private IRepository<SEPO_TFLEX_SECTION_TYPES> sectionTypesRepo;

        private void RefrechData()
        {
            foreach (var item in sectionsRepo.GetQuery())
            {
                //obj_lib.Module.OpenSession().Evict(item);
                AddRow(item);
            }
        }

        private void RefreshTypeData(int id_section)
        {
            typesScene.Rows.Clear();

            foreach (var item in sectionTypesRepo.GetQuery()
                .Where(x => x.ID_SECTION.ID == id_section))
            {
                AddTypeRow(item);
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

        private DataGridViewRow AddTypeRow(SEPO_TFLEX_SECTION_TYPES item)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(typesScene, item.SIGN, item.NAME);
            row.Tag = item.ID;

            typesScene.Rows.Add(row);

            return row;
        }

        private void UpdateRow(DataGridViewRow row, SEPO_TFLEX_SPEC_SECTIONS item)
        {
            row.Cells[0].Value = item.SECTION_;
        }

        private void UpdateTypeRow(DataGridViewRow row, SEPO_TFLEX_SECTION_TYPES item)
        {
            row.Cells[0].Value = item.SIGN;
            row.Cells[1].Value = item.NAME;
        }

        private void InizializeMenu()
        {
            scene.ContextMenuStrip = new ContextMenuStrip();
            typesScene.ContextMenuStrip = new ContextMenuStrip();

            scene.ContextMenuStrip.VisibleChanged += OnSceneContextMenuStripVisibleChanged;
            typesScene.ContextMenuStrip.VisibleChanged += OnTypesSceneContextMenuStripVisibleChanged;

            ContextMenuBuilder menuBuilder = new ContextMenuBuilder(scene.ContextMenuStrip);
            menuBuilder.BlockEditStripItem(OnAddItemClick, OnEditItemClick, OnDeleteItemClick);

            ContextMenuBuilder typesMenuBuilder = new ContextMenuBuilder(typesScene.ContextMenuStrip);
            typesMenuBuilder.BlockEditStripItem(OnAddSectionTypeClick, OnEditSectionTypeClick, OnDeleteSectionTypeClick);
        }

        private void OnTypesSceneContextMenuStripVisibleChanged(object sender, EventArgs e)
        {
            if (typesScene.ContextMenuStrip.Visible)
            {
                scene.ContextMenuStrip.Items["Add"].Enabled = scene.SelectedRows.Count > 0;
                typesScene.ContextMenuStrip.Items["Edit"].Enabled = typesScene.SelectedRows.Count > 0;
                typesScene.ContextMenuStrip.Items["Delete"].Enabled = typesScene.SelectedRows.Count > 0;
            }
        }

        private void OnSceneContextMenuStripVisibleChanged(object sender, EventArgs e)
        {
            if (scene.ContextMenuStrip.Visible)
            {
                scene.ContextMenuStrip.Items["Edit"].Enabled = scene.SelectedRows.Count > 0;
                scene.ContextMenuStrip.Items["Delete"].Enabled = scene.SelectedRows.Count > 0;
            }
        }

        public TFlexSpecSectionsView(IRepository<SEPO_TFLEX_SPEC_SECTIONS> repo, IRepository<SEPO_TFLEX_SECTION_TYPES> typesRepo)
        {
            sectionsRepo = repo;
            sectionTypesRepo = typesRepo;
        }

        public TFlexSpecSectionsView() : this(
            new Repository<SEPO_TFLEX_SPEC_SECTIONS>(),
            new Repository<SEPO_TFLEX_SECTION_TYPES>())
        {
            InitializeComponent();

            scene.RowStateChanged += Scene_RowStateChanged;

            InizializeMenu();

            RefrechData();
        }

        private void Scene_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                int id_section = (int)e.Row.Tag;

                RefreshTypeData(id_section);
            }
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

        protected void OnAddSectionTypeClick(object sender, EventArgs e)
        {
            int id_section = (int)scene.SelectedRows[0].Tag;

            SEPO_TFLEX_SPEC_SECTIONS section = sectionsRepo.GetQuery()
                .Where(x => x.ID == id_section)
                .FirstOrDefault();

            TFlexSpecSectionTypeDialog dialog = new TFlexSpecSectionTypeDialog(section);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddTypeRow(dialog.TFlexSectionType);
            }
        }

        protected void OnEditSectionTypeClick(object sender, EventArgs e)
        {
            DataGridViewRow row = typesScene.SelectedRows[0];
            int id = (int)row.Tag;

            SEPO_TFLEX_SECTION_TYPES type = sectionTypesRepo.GetById(id);

            TFlexSpecSectionTypeDialog dialog = new TFlexSpecSectionTypeDialog(type);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                UpdateTypeRow(row, type);
            }
        }

        protected void OnDeleteSectionTypeClick(object sender, EventArgs e)
        {
            DataGridViewRow row = typesScene.SelectedRows[0];

            int id = (int)row.Tag;

            using (UnitOfWork transaction = new UnitOfWork())
            {
                SEPO_TFLEX_SECTION_TYPES type = sectionTypesRepo.GetById(id);

                try
                {
                    sectionTypesRepo.Delete(type);
                    transaction.Commit();

                    typesScene.Rows.Remove(row);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}