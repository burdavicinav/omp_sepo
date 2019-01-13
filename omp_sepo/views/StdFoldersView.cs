using obj_lib.Entities;
using obj_lib.Repositories;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public class StdFoldersView : TreeView
    {
        private IViewRepository<V_SEPO_STD_TABLES> tablesRepo;

        private IViewRepository<SEPO_STD_FOLDERS> foldersRepo;

        public StdFoldersView(
            decimal id_record,
            IViewRepository<V_SEPO_STD_TABLES> trepo,
            IViewRepository<SEPO_STD_FOLDERS> frepo)
        {
            tablesRepo = trepo;
            foldersRepo = frepo;

            foreach (var table in tablesRepo.GetQuery().Where(x => x.ID_RECORD == id_record))
            {
                TreeNode tbl_node = new TreeNode(table.F_TABLE + " " + table.F_DESCR);
                tbl_node.Tag = table.F_LEVEL;
                this.Nodes.Add(tbl_node);

                BuildNodes(tbl_node);
            }

            this.ExpandAll();
        }

        public StdFoldersView(decimal id_record)
            : this(id_record, new ViewRepository<V_SEPO_STD_TABLES>(), new ViewRepository<SEPO_STD_FOLDERS>())
        {
        }

        private void BuildNodes(TreeNode parent)
        {
            foreach (var folder in foldersRepo.GetQuery().Where(x => x.F_LEVEL == (int)parent.Tag))
            {
                TreeNode node = new TreeNode(parent.Tag + " " + folder.F_NAME);
                node.Tag = folder.F_OWNER;

                parent.Nodes.Add(node);

                BuildNodes(node);
            }
        }
    }
}