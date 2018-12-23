using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class ExpImpDialog : Form
    {
        private void OnCheckedTypeFile(object sender, EventArgs e)
        {
            fileBox.Filter = Extension;
        }

        public string Extension
        {
            get
            {
                string ext = String.Empty;

                if (xmlButton.Checked)
                {
                    ext = "(*.xml)|*.xml";
                }
                else if (csvButton.Checked)
                {
                    ext = "(*.csv)|*.csv";
                }

                return ext;
            }
        }

        public string File
        {
            get
            {
                return fileBox.TextValue;
            }

            protected set
            {
                fileBox.TextValue = value;
            }
        }

        public bool IsRun
        {
            get
            {
                return okButton.Enabled;
            }
        }

        public ExpImpDialog()

        {
            InitializeComponent();
            okButton.Enabled = false;

            fileBox.Filter = Extension;
            fileBox.TextValueChanged += delegate
            {
                okButton.Enabled = (fileBox.TextValue != String.Empty);
            };

            xmlButton.CheckedChanged += OnCheckedTypeFile;
            csvButton.CheckedChanged += OnCheckedTypeFile;
        }

        virtual public void Run(object sender, EventArgs e)
        {
        }
    }
}