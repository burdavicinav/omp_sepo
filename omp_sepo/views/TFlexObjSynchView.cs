using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class TFlexObjSynchView : UserControl
    {
        private IViewRepository<V_SEPO_TFLEX_OBJ_SYNCH> _repoObjSynchView;

        private IRepository<SEPO_TFLEX_OBJ_SYNCH> _repoObjSynch;

        public TFlexObjSynchView(
            IViewRepository<V_SEPO_TFLEX_OBJ_SYNCH> repoView,
            IRepository<SEPO_TFLEX_OBJ_SYNCH> repo)
        {
            _repoObjSynchView = repoView;
            _repoObjSynch = repo;

            InitializeComponent();

            InizializeMenu();

            RefrechData();
        }

        public TFlexObjSynchView() : this(
            new ViewRepository<V_SEPO_TFLEX_OBJ_SYNCH>(),
            new Repository<SEPO_TFLEX_OBJ_SYNCH>())
        {
        }

        private void RefrechData()
        {
            foreach (var item in _repoObjSynchView.GetQuery())
            {
                AddRow(item);
            }
        }

        private DataGridViewRow AddRow(V_SEPO_TFLEX_OBJ_SYNCH item)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(
                scene,
                item.TFLEX_SECTION,
                item.TFLEX_DOCSIGN,
                item.SECTYPE_NAME,
                item.BOTYPESHORTNAME,
                item.BOSTATESHORTNAME,
                item.FILEGROUPSHORTNAME,
                item.OMPSECTIONNAME,
                item.PARAM_DEPENDENCE,
                item.PARAM,
                item.PARAM_EXPRESSION
                );

            row.Tag = item.ID;

            scene.Rows.Add(row);

            return row;
        }

        private void UpdateRow(DataGridViewRow row, V_SEPO_TFLEX_OBJ_SYNCH item)
        {
            row.Cells[0].Value = item.TFLEX_SECTION;
            row.Cells[1].Value = item.TFLEX_DOCSIGN;
            row.Cells[2].Value = item.SECTYPE_NAME;
            row.Cells[3].Value = item.BOTYPESHORTNAME;
            row.Cells[4].Value = item.BOSTATESHORTNAME;
            row.Cells[5].Value = item.FILEGROUPSHORTNAME;
            row.Cells[6].Value = item.OMPSECTIONNAME;
            row.Cells[7].Value = item.PARAM_DEPENDENCE;
            row.Cells[8].Value = item.PARAM;
            row.Cells[9].Value = item.PARAM_EXPRESSION;
        }

        private void InizializeMenu()
        {
            this.ContextMenuStrip = new ContextMenuStrip();

            ContextMenuBuilder menuBuilder = new ContextMenuBuilder(this.ContextMenuStrip);
            menuBuilder.BlockEditStripItem(OnAddItemClick, OnEditItemClick, OnDeleteItemClick);

            this.ContextMenuStrip.VisibleChanged += OnMenuVisible;
        }

        protected void OnAddItemClick(object sender, EventArgs e)
        {
            TFlexObjSynchDialog dialog = new TFlexObjSynchDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var obj = _repoObjSynchView.GetById(dialog.Id);
                AddRow(obj);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            int id = (int)row.Tag;

            TFlexObjSynchDialog dialog = new TFlexObjSynchDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var obj = _repoObjSynchView.GetById(dialog.Id);
                UpdateRow(row, obj);
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
                    SEPO_TFLEX_OBJ_SYNCH synch = _repoObjSynch.GetQuery().Where(x => x.ID == id).FirstOrDefault();
                    _repoObjSynch.Delete(synch);

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