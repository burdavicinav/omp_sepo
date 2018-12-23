using obj_lib;
using Oracle.DataAccess.Client;
using System;
using System.Text;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdAttrsListView : ListView
    {
        public StdAttrsListView()
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
            vitem.Tag = item.IdAttr;

            vitem.Text = item.TName;
            vitem.SubItems.Add(item.Field);
            vitem.SubItems.Add(item.FDataType);
            vitem.SubItems.Add(item.FEnterMode);
            vitem.SubItems.Add(item.FData);
            vitem.SubItems.Add(item.AttrName);
            vitem.SubItems.Add(item.OmpName);

            string omp_type = String.Empty;
            switch ((int)item.OmpType)
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
            vitem.SubItems.Add(item.EnumName);

            this.Items.Add(vitem);
        }

        public void UpdateScene(decimal id_record = -1)
        {
            this.Items.Clear();

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;

            StringBuilder sb = new StringBuilder(
                @"select id_attr, id_table, tname, field, f_datatype, f_entermode, f_data,
                    attr_name, omp_name, omp_type, id_enum, enum_name, id_record
                  from v_sepo_std_attrs where 1=1");

            if (id_record != -1)
            {
                sb.Append(" and id_record = :id_record");

                command.Parameters.Add("id_record", id_record);
            }

            sb.Append(" order by tname, field");

            command.CommandText = sb.ToString();

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    V_SEPO_STD_ATTRS item = new V_SEPO_STD_ATTRS();

                    item.IdAttr = reader.GetDecimal(0);
                    item.IdTable = reader.GetDecimal(1);
                    item.TName = reader.GetString(2);
                    item.Field = reader.GetString(3);
                    item.FDataType = reader.GetString(4);
                    item.FEnterMode = reader.GetString(5);
                    if (!reader.IsDBNull(6)) item.FData = reader.GetString(6);
                    item.AttrName = reader.GetString(7);
                    item.OmpName = reader.GetString(8);
                    item.OmpType = reader.GetDecimal(9);
                    if (!reader.IsDBNull(10)) item.IdEnum = reader.GetDecimal(10);
                    if (!reader.IsDBNull(11)) item.EnumName = reader.GetString(11);
                    item.IdRecord = reader.GetDecimal(12);

                    AddItem(item);
                }
            }
        }
    }
}