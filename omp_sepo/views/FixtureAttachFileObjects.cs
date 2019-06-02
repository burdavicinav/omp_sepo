using obj_lib;
using obj_lib.Entities;
using omp_sepo.dialogs;
using System;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class FixtureAttachFileObjects : UserControl
    {
        public FixtureAttachFileObjects()
        {
            InitializeComponent();

            InizializeMenu();
            RefrechData();
        }

        private void RefrechData()
        {
            var session = Module.OpenSession();

            foreach (var item in session.QueryOver<V_SEPO_FIXTURE_AF_OBJECTS>().List())
            {
                AddRow(item);
            }
        }

        private DataGridViewRow AddRow(V_SEPO_FIXTURE_AF_OBJECTS item)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(
                scene,
                item.ID_TYPE,
                item.TYPENAME,
                item.FILEGROUP,
                item.OWNER
                );

            row.Tag = item.ID;

            scene.Rows.Add(row);

            return row;
        }

        private void UpdateRow(DataGridViewRow row, V_SEPO_FIXTURE_AF_OBJECTS item)
        {
            row.Cells[0].Value = item.ID_TYPE;
            row.Cells[1].Value = item.TYPENAME;
            row.Cells[2].Value = item.FILEGROUP;
            row.Cells[3].Value = item.OWNER;
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
            SepoFixtureAfObjectsDialog dialog = new SepoFixtureAfObjectsDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var session = Module.OpenSession();
                var obj = session.Get<V_SEPO_FIXTURE_AF_OBJECTS>(dialog.Id);
                AddRow(obj);
            }
        }

        protected void OnEditItemClick(object sender, EventArgs e)
        {
            DataGridViewRow row = scene.SelectedRows[0];
            int id = (int)row.Tag;

            SepoFixtureAfObjectsDialog dialog = new SepoFixtureAfObjectsDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var session = Module.OpenSession();
                var obj = session.Get<V_SEPO_FIXTURE_AF_OBJECTS>(dialog.Id);
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
                    var session = Module.OpenSession();

                    SEPO_FIXTURE_AF_OBJECTS obj = session.Get<SEPO_FIXTURE_AF_OBJECTS>(id);
                    session.Delete(obj);

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