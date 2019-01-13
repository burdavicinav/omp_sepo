using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexSpecSectionDialog : Form
    {
        private IRepository<SEPO_TFLEX_SPEC_SECTIONS> sectionsRepo;

        private DialogType dialogType;

        public SEPO_TFLEX_SPEC_SECTIONS TFlexSpecSection { get; set; }

        public TFlexSpecSectionDialog(IRepository<SEPO_TFLEX_SPEC_SECTIONS> repo)
        {
            sectionsRepo = repo;

            InitializeComponent();

            sectionBox.TextChanged += SectionBox_TextChanged;
            okButton.Enabled = false;

            okButton.Click += OkButton_Click;
        }

        public TFlexSpecSectionDialog() : this(new Repository<SEPO_TFLEX_SPEC_SECTIONS>())
        {
            dialogType = DialogType.Add;
        }

        public TFlexSpecSectionDialog(SEPO_TFLEX_SPEC_SECTIONS section)
            : this(new Repository<SEPO_TFLEX_SPEC_SECTIONS>())
        {
            dialogType = DialogType.Edit;
            TFlexSpecSection = section;

            sectionBox.Text = section.SECTION_;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_SPEC_SECTIONS section = new SEPO_TFLEX_SPEC_SECTIONS
                {
                    SECTION_ = sectionBox.Text
                };

                using (UnitOfWork transaction = new UnitOfWork())
                {
                    try
                    {
                        sectionsRepo.Create(section);

                        transaction.Commit();

                        TFlexSpecSection = section;
                        DialogResult = DialogResult.OK;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                TFlexSpecSection.SECTION_ = sectionBox.Text;

                using (UnitOfWork transaction = new UnitOfWork())
                {
                    try
                    {
                        sectionsRepo.Update(TFlexSpecSection);

                        transaction.Commit();

                        DialogResult = DialogResult.OK;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void SectionBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = (sectionBox.Text != String.Empty);
        }
    }
}