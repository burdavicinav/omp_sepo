using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexDocsSignDialog : Form
    {
        private IRepository<SEPO_TFLEX_SIGN_DOCS> signDocsRepo;

        private DialogType dialogType;

        public SEPO_TFLEX_SIGN_DOCS DocSign { get; set; }

        public TFlexDocsSignDialog(IRepository<SEPO_TFLEX_SIGN_DOCS> repo)
        {
            signDocsRepo = repo;

            InitializeComponent();

            signBox.TextChanged += SectionBox_TextChanged;
            okButton.Enabled = false;

            okButton.Click += OkButton_Click;
        }

        public TFlexDocsSignDialog() : this(new Repository<SEPO_TFLEX_SIGN_DOCS>())
        {
            dialogType = DialogType.Add;
        }

        public TFlexDocsSignDialog(SEPO_TFLEX_SIGN_DOCS docSign, IRepository<SEPO_TFLEX_SIGN_DOCS> repo) : this(repo)
        {
            dialogType = DialogType.Edit;
            DocSign = docSign;

            signBox.Text = DocSign.SIGN;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_SIGN_DOCS sign = new SEPO_TFLEX_SIGN_DOCS
                {
                    SIGN = signBox.Text
                };

                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        signDocsRepo.Create(sign);
                        DocSign = sign;

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
            else
            {
                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        DocSign.SIGN = signBox.Text;
                        signDocsRepo.Update(DocSign);

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
            okButton.Enabled = (signBox.Text != String.Empty);
        }
    }
}