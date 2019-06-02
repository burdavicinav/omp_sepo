using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexSignDocsView : UserControl
    {
        private IRepository<SEPO_TFLEX_SIGN_DOCS> signDocsRepo;

        private void RefrechData()
        {
            foreach (var item in signDocsRepo.GetQuery())
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
            this.ContextMenuStrip = new ContextMenuStrip();

            ContextMenuBuilder menuBuilder = new ContextMenuBuilder(this.ContextMenuStrip);
            menuBuilder.BlockEditStripItem(OnAddItemClick, OnEditItemClick, OnDeleteItemClick);

            this.ContextMenuStrip.VisibleChanged += OnMenuVisible;
        }

        public TFlexSignDocsView(IRepository<SEPO_TFLEX_SIGN_DOCS> repo)
        {
            signDocsRepo = repo;

            InitializeComponent();

            InizializeMenu();

            RefrechData();
        }

        public TFlexSignDocsView() : this(new Repository<SEPO_TFLEX_SIGN_DOCS>())
        {
        }

        protected void OnAddItemClick(object sender, EventArgs e)
        {
            TFlexDocsSignDialog dialog = new TFlexDocsSignDialog(signDocsRepo);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddRow(dialog.DocSign);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            int id = (int)row.Tag;

            SEPO_TFLEX_SIGN_DOCS docSign = signDocsRepo.GetById(id);

            TFlexDocsSignDialog dialog = new TFlexDocsSignDialog(docSign, signDocsRepo);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                UpdateRow(row, dialog.DocSign);
            }
        }

        protected void OnDeleteItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];

            int id = (int)row.Tag;

            using (var transaction = new UnitOfWork())
            {
                try
                {
                    SEPO_TFLEX_SIGN_DOCS signDoc = signDocsRepo.GetById(id);
                    signDocsRepo.Delete(signDoc);

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
            if (this.ContextMenuStrip.Visible)
            {
                this.ContextMenuStrip.Items["Edit"].Enabled = scene.SelectedRows.Count > 0;
                this.ContextMenuStrip.Items["Delete"].Enabled = scene.SelectedRows.Count > 0;
            }
        }
    }
}