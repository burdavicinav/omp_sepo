using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdAttrsListView : ListView
    {
        private IViewRepository<V_SEPO_STD_ATTRS> attrsRepo;

        public StdAttrsListView(IViewRepository<V_SEPO_STD_ATTRS> repo)
        {
            attrsRepo = repo;
        }

        public StdAttrsListView() : this(new ViewRepository<V_SEPO_STD_ATTRS>())
        {
        }

        public void ViewSettings()
        {
            this.View = View.Details;
            this.MultiSelect = false;
            this.FullRowSelect = true;
            this.GridLines = true;

            this.Columns.Add("Таблица", -2);
            this.Columns.Add("Поле", -2);
            this.Columns.Add("Тип данных", -2);
            this.Columns.Add("Тип атрибута", -2);
            this.Columns.Add("Формула", -2);
            this.Columns.Add("Наименование", -2);
            this.Columns.Add("Наименование в КИС \"Омега\"", -2);
            this.Columns.Add("Тип в КИС \"Омега\"", -2);
            this.Columns.Add("Перечисление");
        }

        private void AddItem(V_SEPO_STD_ATTRS item)
        {
            ListViewItem vitem = new ListViewItem();
            vitem.Tag = item.ID_ATTR;

            vitem.Text = item.TNAME;
            vitem.SubItems.Add(item.FIELD);
            vitem.SubItems.Add(item.F_DATATYPE);
            vitem.SubItems.Add(item.F_ENTERMODE);
            vitem.SubItems.Add(item.F_DATA);
            vitem.SubItems.Add(item.ATTR_NAME);
            vitem.SubItems.Add(item.OMP_NAME);

            string omp_type = String.Empty;
            switch ((int)item.OMP_TYPE)
            {
                case 1:
                    omp_type = "Строка";
                    break;

                case 2:
                    omp_type = "Число";
                    break;

                case 3:
                    omp_type = "Целое число";
                    break;

                case 10:
                    omp_type = "Перечисление";
                    break;

                default:
                    omp_type = String.Empty;
                    break;
            }

            vitem.SubItems.Add(omp_type);
            vitem.SubItems.Add(item.ENUM_NAME);

            this.Items.Add(vitem);
        }

        public void UpdateScene(decimal id_record = -1)
        {
            this.Items.Clear();

            var attrs = attrsRepo.GetQuery();

            if (id_record != -1)
            {
                attrs = attrs.Where(x => x.ID_RECORD == id_record);
            }

            foreach (var attr in attrs)
            {
                AddItem(attr);
            }
        }
    }
}