using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class LoadFixtureDialog : Form
    {
        public string Sign
        {
            get
            {
                return signBox.Text;
            }
        }

        public LoadFixtureDialog()
        {
            InitializeComponent();
        }
    }
}