using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Windows.Forms;
using obj_lib.Entities;

namespace omp_sepo.views
{
    public partial class TpImportOwnersView : UserControl
    {
        public TpImportOwnersView()
        {
            InitializeComponent();

            var owners = obj_lib.Module.OpenSession()
                .Query<OWNER_NAME>()
                .Select(x => new { ID = x.OWNER, x.NAME })
                .OrderBy(x => x.NAME)
                .ToList();

            owners.Add(new { ID = -1, NAME = String.Empty });

            ownerColumn.ValueMember = "ID";
            ownerColumn.DisplayMember = "NAME";
            ownerColumn.DataSource = owners;

            foreach (var obj in obj_lib.Module.OpenSession().QueryOver<V_SEPO_TP_WORKSHOP_OWNER>().List())
            {
                DataGridViewRow row = new DataGridViewRow();

                row.Tag = obj.ID;
                row.CreateCells(ownersView, obj.WSSIGN, (obj.OWNER.HasValue) ? obj.OWNER : -1);

                ownersView.Rows.Add(row);
            }

            ownersView.CellEndEdit += OnCellEndEdit;
        }

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = ownersView.Rows[e.RowIndex];
            object currentValue = row.Cells[e.ColumnIndex].Value;

            int id = (int)row.Tag;

            NHibernate.ISession session = obj_lib.Module.OpenSession();

            SEPO_TP_WORKSHOP_OWNER dbRow = session.Get<SEPO_TP_WORKSHOP_OWNER>(id);
            dbRow.OWNER = (int?)currentValue;

            session.Update(dbRow);
            session.Flush();
        }
    }
}