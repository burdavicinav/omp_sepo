using obj_lib;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdFoldersView : TreeView
    {
        public StdFoldersView(decimal id_record)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select id, id_record, f_level, f_table, f_descr from v_sepo_std_tables
                                        where id_record = :id_record";

            command.Parameters.Add("id_record", id_record);

            V_SEPO_STD_TABLES tbl = new V_SEPO_STD_TABLES();

            using (OracleDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                tbl.Id = reader.GetDecimal(0);
                tbl.IdRecord = reader.GetDecimal(1);
                tbl.FLevel = reader.GetDecimal(2);
                tbl.FTable = reader.GetString(3);
                tbl.FDescr = reader.GetString(4);

                TreeNode tbl_node = new TreeNode(tbl.FTable + " " + tbl.FDescr);
                tbl_node.Tag = tbl.FLevel;
                this.Nodes.Add(tbl_node);

                BuildNodes(tbl_node);
            }

            this.ExpandAll();
        }

        private void BuildNodes(TreeNode parent)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = "select f_owner, f_name from sepo_std_folders where f_level = :f_level";

            command.Parameters.Add("f_level", parent.Tag);

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TreeNode node = new TreeNode(parent.Tag + " " + reader.GetString(1));
                    node.Tag = reader.GetDecimal(0);

                    parent.Nodes.Add(node);

                    BuildNodes(node);
                }
            }
        }
    }
}