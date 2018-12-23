using obj_lib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexSpecSectionDialog : Form
    {
        private DialogType dialogType;

        public decimal Id { get; set; }

        public TFlexSpecSectionDialog()
        {
            InitializeComponent();

            dialogType = DialogType.Add;

            sectionBox.TextChanged += SectionBox_TextChanged;
            okButton.Enabled = false;

            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_SPEC_SECTIONS section = new SEPO_TFLEX_SPEC_SECTIONS
                {
                    SECTION_ = sectionBox.Text
                };

                Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Add(section);

                try
                {
                    Module.Context.SaveChanges();

                    Id = section.ID;

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);

                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Remove(section);
                }
            }
            else
            {
                SEPO_TFLEX_SPEC_SECTIONS item =
                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Where(x => x.ID == Id).FirstOrDefault();

                item.SECTION_ = sectionBox.Text;

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
            okButton.Enabled = (sectionBox.Text != String.Empty);
        }

        public TFlexSpecSectionDialog(decimal id) : this()
        {
            dialogType = DialogType.Edit;
            Id = id;

            try
            {
                SEPO_TFLEX_SPEC_SECTIONS item =
                    Module.Context.SEPO_TFLEX_SPEC_SECTIONS.Where(x => x.ID == id).FirstOrDefault();

                sectionBox.Text = item.SECTION_;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}