using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using obj_lib.Entities;
using NHibernate.Linq;

namespace omp_sepo.views
{
    public partial class TpImportOperationsView : UserControl
    {
        public TpImportOperationsView()
        {
            InitializeComponent();

            var operations = obj_lib.Module.OpenSession()
                .Query<TECHNOLOGY_OPERATIONS>()
                .Select(x => new { ID = x.CODE, NAME = x.OPERCODE + "-" + x.VARIANTCODE + " " + x.NAME })
                .OrderBy(x => x.NAME)
                .ToList();

            operations.Add(new { ID = -1, NAME = String.Empty });

            ompCodeColumn.ValueMember = "ID";
            ompCodeColumn.DisplayMember = "NAME";
            ompCodeColumn.DataSource = operations;

            IList<V_SEPO_TECH_OPER_LINKS> objects;

            if (obj_lib.Module.OpenSession().QueryOver<V_SEPO_TECH_OPER_LINKS>().RowCount() == 0)
            {
                objects = obj_lib.Module.OpenSession()
                    .GetNamedQuery("UpdateTechOperLinks")
                    .List<V_SEPO_TECH_OPER_LINKS>();
            }
            else
            {
                objects = obj_lib.Module.OpenSession().QueryOver<V_SEPO_TECH_OPER_LINKS>().List();
            }

            foreach (var obj in objects)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.Tag = obj.ID;
                row.CreateCells(operationsView, obj.OPERCODE, obj.OPERNAME, (obj.OMP_ID.HasValue) ? obj.OMP_ID : -1);

                operationsView.Rows.Add(row);
            }

            operationsView.CellEndEdit += OnCellEndEdit;
        }

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = operationsView.Rows[e.RowIndex];
            object currentValue = row.Cells[e.ColumnIndex].Value;

            int id = (int)row.Tag;

            NHibernate.ISession session = obj_lib.Module.OpenSession();

            SEPO_TECH_OPER_LINKS dbRow = session.Get<SEPO_TECH_OPER_LINKS>(id);
            dbRow.OMPID = (int?)currentValue;

            session.Update(dbRow);
            session.Flush();
        }
    }
}