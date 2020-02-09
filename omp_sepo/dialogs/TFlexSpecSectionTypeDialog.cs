using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexSpecSectionTypeDialog : Form
    {
        private IRepository<SEPO_TFLEX_SECTION_TYPES> typesRepo;

        private DialogType dialogType;

        public SEPO_TFLEX_SPEC_SECTIONS TFlexSpecSection { get; set; }

        public SEPO_TFLEX_SECTION_TYPES TFlexSectionType { get; set; }

        public TFlexSpecSectionTypeDialog(IRepository<SEPO_TFLEX_SECTION_TYPES> repo)
        {
            InitializeComponent();

            typesRepo = repo;

            signBox.DataSource = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            nameBox.TextChanged += SectionBox_TextChanged;
            okButton.Enabled = false;

            okButton.Click += OkButton_Click;
        }

        public TFlexSpecSectionTypeDialog(SEPO_TFLEX_SPEC_SECTIONS section)
            : this(new Repository<SEPO_TFLEX_SECTION_TYPES>())
        {
            dialogType = DialogType.Add;

            TFlexSpecSection = section;
        }

        public TFlexSpecSectionTypeDialog(SEPO_TFLEX_SECTION_TYPES type)
            : this(new Repository<SEPO_TFLEX_SECTION_TYPES>())
        {
            dialogType = DialogType.Edit;
            TFlexSectionType = type;
            TFlexSpecSection = type.ID_SECTION;

            signBox.SelectedItem = type.SIGN;
            nameBox.Text = type.NAME;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_SECTION_TYPES type = new SEPO_TFLEX_SECTION_TYPES
                {
                    ID_SECTION = TFlexSpecSection,
                    SIGN = (int)signBox.SelectedValue,
                    NAME = nameBox.Text
                };

                using (UnitOfWork transaction = new UnitOfWork())
                {
                    try
                    {
                        typesRepo.Create(type);

                        transaction.Commit();

                        TFlexSectionType = type;
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
                TFlexSectionType.SIGN = (int)signBox.SelectedValue;
                TFlexSectionType.NAME = nameBox.Text;

                using (UnitOfWork transaction = new UnitOfWork())

                {
                    try
                    {
                        typesRepo.Update(TFlexSectionType);

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
            okButton.Enabled = (nameBox.Text != String.Empty);
        }
    }
}