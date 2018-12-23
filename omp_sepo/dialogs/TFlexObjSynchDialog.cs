using obj_lib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexObjSynchDialog : Form
    {
        private DialogType dialogType;

        private bool IsCorrect()
        {
            return (sectionBox.SelectedValue != null
                && boTypeBox.SelectedValue != null
                && boStateBox.SelectedValue != null
                && ompSectionBox.SelectedValue != null);
        }

        public decimal Id { get; set; }

        public void RefreshData()
        {
            sectionBox.DisplayMember = "SECTION_";
            sectionBox.ValueMember = "ID";
            sectionBox.DataSource = Module.Context.SEPO_TFLEX_SPEC_SECTIONS.ToList();

            signDocBox.DisplayMember = "SIGN";
            signDocBox.ValueMember = "ID";

            var list = Module.Context.SEPO_TFLEX_SIGN_DOCS.Select(
                x => new { SIGN = x.SIGN, ID = x.ID }).ToList();

            list.Add(new { SIGN = String.Empty, ID = (decimal)0 });

            signDocBox.DataSource = list;

            boTypeBox.DisplayMember = "BOTYPENAME";
            boTypeBox.ValueMember = "BOTYPE";
            boTypeBox.DataSource = Module.Context.V_SEPO_TFLEX_BO_TYPES.Select(
                x => new { BOTYPE = x.BOTYPE, BOTYPENAME = x.BOTYPENAME }).OrderBy(
                x => x.BOTYPENAME).ToList();

            ompSectionBox.DisplayMember = "SECTION";
            ompSectionBox.ValueMember = "CODE";
            ompSectionBox.DataSource = Module.Context.SPC_SECTIONS.Select(
                x => new { SECTION = x.NAME, CODE = x.CODE }).ToList();
        }

        public TFlexObjSynchDialog()
        {
            InitializeComponent();

            dialogType = DialogType.Add;

            okButton.Click += OkButton_Click;
            sectionBox.SelectedValueChanged += OnSelectedValueChanged;
            boTypeBox.SelectedValueChanged += OnBoTypeChanged;
            boTypeBox.SelectedValueChanged += OnSelectedValueChanged;
            boStateBox.SelectedValueChanged += OnSelectedValueChanged;
            ompSectionBox.SelectedValueChanged += OnSelectedValueChanged;

            RefreshData();
        }

        public TFlexObjSynchDialog(decimal id) : this()
        {
            dialogType = DialogType.Edit;
            Id = id;

            try
            {
                SEPO_TFLEX_OBJ_SYNCH item =
                    Module.Context.SEPO_TFLEX_OBJ_SYNCH.Where(x => x.ID == id).FirstOrDefault();

                sectionBox.SelectedValue = item.TFLEX_SECTION;
                signDocBox.SelectedValue = (item.TFLEX_DOCSIGN == null) ? 0 : item.TFLEX_DOCSIGN;
                boTypeBox.SelectedValue = item.OMP_BOTYPE;
                boStateBox.SelectedValue = item.OMP_BOSTATE;
                groupFileBox.SelectedValue = (item.OMP_FILEGROUP == null) ? 0 : item.OMP_FILEGROUP;
                ompSectionBox.SelectedValue = item.OMP_SECTION;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_TFLEX_OBJ_SYNCH synch = new SEPO_TFLEX_OBJ_SYNCH();

                synch.TFLEX_SECTION = (decimal)sectionBox.SelectedValue;

                if ((decimal)signDocBox.SelectedValue != 0)
                {
                    synch.TFLEX_DOCSIGN = (decimal)signDocBox.SelectedValue;
                }

                synch.OMP_BOTYPE = (decimal)boTypeBox.SelectedValue;
                synch.OMP_BOSTATE = (decimal)boStateBox.SelectedValue;

                if ((decimal)groupFileBox.SelectedValue != 0)
                {
                    synch.OMP_FILEGROUP = (decimal)groupFileBox.SelectedValue;
                }

                synch.OMP_SECTION = (decimal)ompSectionBox.SelectedValue;

                Module.Context.SEPO_TFLEX_OBJ_SYNCH.Add(synch);

                try
                {
                    Module.Context.SaveChanges();

                    Id = synch.ID;
                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);

                    Module.Context.SEPO_TFLEX_OBJ_SYNCH.Remove(synch);
                }
            }
            else
            {
                SEPO_TFLEX_OBJ_SYNCH item =
                    Module.Context.SEPO_TFLEX_OBJ_SYNCH.Where(x => x.ID == Id).FirstOrDefault();

                item.TFLEX_SECTION = (decimal)sectionBox.SelectedValue;

                if ((decimal)signDocBox.SelectedValue != 0)
                {
                    item.TFLEX_DOCSIGN = (decimal)signDocBox.SelectedValue;
                }
                else
                {
                    item.TFLEX_DOCSIGN = null;
                }

                item.OMP_BOTYPE = (decimal)boTypeBox.SelectedValue;
                item.OMP_BOSTATE = (decimal)boStateBox.SelectedValue;

                if ((decimal)groupFileBox.SelectedValue != 0)
                {
                    item.OMP_FILEGROUP = (decimal)groupFileBox.SelectedValue;
                }
                else
                {
                    item.OMP_FILEGROUP = null;
                }

                item.OMP_SECTION = (decimal)ompSectionBox.SelectedValue;

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

        private void OnSelectedValueChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrect();
        }

        private void OnBoTypeChanged(object sender, EventArgs e)
        {
            if (boTypeBox.SelectedValue == null) return;

            decimal boType = (decimal)boTypeBox.SelectedValue;

            boStateBox.DisplayMember = "BOSTATENAME";
            boStateBox.ValueMember = "BOSTATE";
            boStateBox.DataSource = Module.Context.BUSINESSOBJ_STATES.Where(
                x => x.BOTYPE == boType).Select(
                x => new { BOSTATENAME = x.NAME, BOSTATE = x.CODE }).ToList();

            groupFileBox.DisplayMember = "GROUPNAME";
            groupFileBox.ValueMember = "GROUPCODE";

            var list = Module.Context.ATTACHMENTS_GROUPS.Where(
                x => x.BOTYPE == boType).Select(
                x => new { GROUPNAME = x.NAME, GROUPCODE = x.CODE }).ToList();

            list.Add(new { GROUPNAME = String.Empty, GROUPCODE = (decimal)0 });

            groupFileBox.DataSource = list;
        }
    }
}