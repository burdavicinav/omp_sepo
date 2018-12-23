using obj_lib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexDocsSignDialog : Form
    {
        private DialogType dialogType;

        public decimal Id { get; set; }

        public TFlexDocsSignDialog()
        {
            InitializeComponent();

            dialogType = DialogType.Add;

            signBox.TextChanged += SectionBox_TextChanged;
            okButton.Enabled = false;

            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_SIGN_DOCS sign = new SEPO_TFLEX_SIGN_DOCS
                {
                    SIGN = signBox.Text
                };

                Module.Context.SEPO_TFLEX_SIGN_DOCS.Add(sign);

                try
                {
                    Module.Context.SaveChanges();

                    Id = sign.ID;

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);

                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Remove(sign);
                }
            }
            else
            {
                SEPO_TFLEX_SIGN_DOCS item =
                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Where(x => x.ID == Id).FirstOrDefault();

                item.SIGN = signBox.Text;

                try
                {
                    Module.Context.SaveChanges();

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void SectionBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = (signBox.Text != String.Empty);
        }

        public TFlexDocsSignDialog(decimal id) : this()
        {
            dialogType = DialogType.Edit;
            Id = id;

            try
            {
                SEPO_TFLEX_SIGN_DOCS item =
                    Module.Context.SEPO_TFLEX_SIGN_DOCS.Where(x => x.ID == id).FirstOrDefault();

                signBox.Text = item.SIGN;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}