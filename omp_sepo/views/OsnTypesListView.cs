using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class OsnTypesListView : ListView
    {
        private IViewRepository<V_SEPO_OSN_TYPES> typesRepo;

        private void AddItem(V_SEPO_OSN_TYPES tps)
        {
            ListViewItem item = new ListViewItem(tps.SHORTNAME);
            item.SubItems.Add(tps.OMP_NAME);
            item.Tag = tps.ID;

            Items.Add(item);
        }

        private void SelectObjects()
        {
            foreach (var type in typesRepo.GetQuery().OrderBy(x => x.SHORTNAME))
            {
                AddItem(type);
            }
        }

        public OsnTypesListView(IViewRepository<V_SEPO_OSN_TYPES> repo)
        {
            typesRepo = repo;
        }

        public OsnTypesListView() : this(new ViewRepository<V_SEPO_OSN_TYPES>())
        {
            this.View = View.Details;
            this.GridLines = true;
            this.MultiSelect = false;
            this.FullRowSelect = true;

            this.Columns.Add("Тип Search", -2);
            this.Columns.Add("Тип КИС Омега", -2);

            SelectObjects();

            this.MouseDoubleClick += OsnTypesListView_MouseDoubleClick;
        }

        private void OsnTypesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem row = this.SelectedItems[0];
            decimal id = (decimal)row.Tag;

            OsnTypesDialog dialog = new OsnTypesDialog(id);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                row.SubItems[1].Text = dialog.OmpName;
            }
        }
    }
}