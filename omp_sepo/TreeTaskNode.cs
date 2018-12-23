using System.Windows.Forms;

namespace omp_sepo
{
    public enum TreeTaskNodeType { File, Folder }

    public class TreeTaskNode : TreeNode
    {
        public TreeTaskNode() : base()
        {
        }

        public TreeTaskNode(string text, TreeTaskNodeType type = TreeTaskNodeType.Folder) : base(text)
        {
            Type = type;
        }

        public TreeTaskNodeType Type { get; set; }
    }
}