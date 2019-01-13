using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class TFlexObjSynchDialog : Form
    {
        private IViewRepository<SEPO_TFLEX_SPEC_SECTIONS> _repoSections;

        private IViewRepository<SEPO_TFLEX_SIGN_DOCS> _repoSignDocs;

        private IViewRepository<V_SEPO_TFLEX_BO_TYPES> _repoBoTypes;

        private IViewRepository<SPC_SECTIONS> _repoOmpSections;

        private IRepository<SEPO_TFLEX_OBJ_SYNCH> _repoObjSynch;

        private IViewRepository<BUSINESSOBJ_STATES> _repoBoStates;

        private IViewRepository<ATTACHMENTS_GROUPS> _repoAttachGroups;

        private DialogType dialogType;

        private void InizializeRepo(
            IViewRepository<SEPO_TFLEX_SPEC_SECTIONS> repoSections,
            IViewRepository<SEPO_TFLEX_SIGN_DOCS> repoSignDocs,
            IViewRepository<V_SEPO_TFLEX_BO_TYPES> repoBoTypes,
            IViewRepository<SPC_SECTIONS> repoOmpSections,
            IRepository<SEPO_TFLEX_OBJ_SYNCH> repoObjSynch,
            IViewRepository<BUSINESSOBJ_STATES> repoBoStates,
            IViewRepository<ATTACHMENTS_GROUPS> repoAttachGroups)
        {
            _repoSections = repoSections;
            _repoSignDocs = repoSignDocs;
            _repoBoTypes = repoBoTypes;
            _repoOmpSections = repoOmpSections;
            _repoObjSynch = repoObjSynch;
            _repoBoStates = repoBoStates;
            _repoAttachGroups = repoAttachGroups;
        }

        private bool IsCorrect()
        {
            return (sectionBox.SelectedValue != null
                && boTypeBox.SelectedValue != null
                && boStateBox.SelectedValue != null
                && ompSectionBox.SelectedValue != null);
        }

        public int Id { get; set; }

        public void RefreshData()
        {
            sectionBox.DisplayMember = "SECTION_";
            sectionBox.ValueMember = "ID";
            sectionBox.DataSource = _repoSections.GetQuery().ToList();

            signDocBox.DisplayMember = "SIGN";
            signDocBox.ValueMember = "ID";

            var list = _repoSignDocs.GetQuery().Select(
                x => new { x.SIGN, x.ID }).ToList();

            list.Add(new { SIGN = string.Empty, ID = 0 });

            signDocBox.DataSource = list;

            boTypeBox.DisplayMember = "BOTYPENAME";
            boTypeBox.ValueMember = "BOTYPE";
            boTypeBox.DataSource = _repoBoTypes.GetQuery().Select(
                x => new { x.BOTYPENAME, x.BOTYPE }).OrderBy(
                x => x.BOTYPENAME).ToList();

            ompSectionBox.DisplayMember = "SECTION";
            ompSectionBox.ValueMember = "CODE";
            ompSectionBox.DataSource = _repoOmpSections.GetQuery().Select(
                x => new { SECTION = x.NAME, x.CODE }).OrderBy(x => x.SECTION).ToList();
        }

        public TFlexObjSynchDialog(
            IViewRepository<SEPO_TFLEX_SPEC_SECTIONS> repoSections,
            IViewRepository<SEPO_TFLEX_SIGN_DOCS> repoSignDocs,
            IViewRepository<V_SEPO_TFLEX_BO_TYPES> repoBoTypes,
            IViewRepository<SPC_SECTIONS> repoOmpSections,
            IRepository<SEPO_TFLEX_OBJ_SYNCH> repoObjSynch,
            IViewRepository<BUSINESSOBJ_STATES> repoBoStates,
            IViewRepository<ATTACHMENTS_GROUPS> repoAttachGroups)
        {
            InitializeComponent();

            InizializeRepo(
                repoSections,
                repoSignDocs,
                repoBoTypes,
                repoOmpSections,
                repoObjSynch,
                repoBoStates,
                repoAttachGroups);

            dialogType = DialogType.Add;

            okButton.Click += OkButton_Click;
            sectionBox.SelectedValueChanged += OnSelectedValueChanged;
            boTypeBox.SelectedValueChanged += OnBoTypeChanged;
            boTypeBox.SelectedValueChanged += OnSelectedValueChanged;
            boStateBox.SelectedValueChanged += OnSelectedValueChanged;
            ompSectionBox.SelectedValueChanged += OnSelectedValueChanged;

            RefreshData();
        }

        public TFlexObjSynchDialog() : this(
            new ViewRepository<SEPO_TFLEX_SPEC_SECTIONS>(),
            new ViewRepository<SEPO_TFLEX_SIGN_DOCS>(),
            new ViewRepository<V_SEPO_TFLEX_BO_TYPES>(),
            new ViewRepository<SPC_SECTIONS>(),
            new Repository<SEPO_TFLEX_OBJ_SYNCH>(),
            new ViewRepository<BUSINESSOBJ_STATES>(),
            new ViewRepository<ATTACHMENTS_GROUPS>())
        {
        }

        public TFlexObjSynchDialog(int id) : this()
        {
            dialogType = DialogType.Edit;
            Id = id;

            try
            {
                SEPO_TFLEX_OBJ_SYNCH item = _repoObjSynch.GetQuery().Where(x => x.ID == id).FirstOrDefault();

                sectionBox.SelectedValue = item.TFLEX_SECTION.ID;
                signDocBox.SelectedValue = (item.TFLEX_DOCSIGN == null) ? 0 : item.TFLEX_DOCSIGN.ID;
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
                SEPO_TFLEX_OBJ_SYNCH synch = new SEPO_TFLEX_OBJ_SYNCH
                {
                    TFLEX_SECTION = _repoSections.GetById(sectionBox.SelectedValue)
                };

                if ((int)signDocBox.SelectedValue != 0)
                {
                    synch.TFLEX_DOCSIGN = _repoSignDocs.GetById(signDocBox.SelectedValue);
                }

                synch.OMP_BOTYPE = (int)boTypeBox.SelectedValue;
                synch.OMP_BOSTATE = (int)boStateBox.SelectedValue;

                if ((int)groupFileBox.SelectedValue != 0)
                {
                    synch.OMP_FILEGROUP = (int)groupFileBox.SelectedValue;
                }

                synch.OMP_SECTION = (int)ompSectionBox.SelectedValue;

                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        _repoObjSynch.Create(synch);

                        transaction.Commit();

                        Id = synch.ID;
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
                SEPO_TFLEX_OBJ_SYNCH item = _repoObjSynch.GetQuery().Where(x => x.ID == Id).FirstOrDefault();

                item.TFLEX_SECTION = _repoSections.GetById(sectionBox.SelectedValue);

                if ((int)signDocBox.SelectedValue != 0)
                {
                    item.TFLEX_DOCSIGN = _repoSignDocs.GetById(signDocBox.SelectedValue);
                }
                else
                {
                    item.TFLEX_DOCSIGN = null;
                }

                item.OMP_BOTYPE = (int)boTypeBox.SelectedValue;
                item.OMP_BOSTATE = (int)boStateBox.SelectedValue;

                if ((int)groupFileBox.SelectedValue != 0)
                {
                    item.OMP_FILEGROUP = (int)groupFileBox.SelectedValue;
                }
                else
                {
                    item.OMP_FILEGROUP = null;
                }

                item.OMP_SECTION = (int)ompSectionBox.SelectedValue;

                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        _repoObjSynch.Update(item);

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

        private void OnSelectedValueChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrect();
        }

        private void OnBoTypeChanged(object sender, EventArgs e)
        {
            if (boTypeBox.SelectedValue == null)
            {
                return;
            }

            int boType = (int)boTypeBox.SelectedValue;

            boStateBox.DisplayMember = "BOSTATENAME";
            boStateBox.ValueMember = "BOSTATE";
            boStateBox.DataSource = _repoBoStates.GetQuery().Where(
                x => x.BOTYPE == boType).Select(
                x => new { BOSTATENAME = x.NAME, BOSTATE = x.CODE }).ToList();

            groupFileBox.DisplayMember = "GROUPNAME";
            groupFileBox.ValueMember = "GROUPCODE";

            var list = _repoAttachGroups.GetQuery().Where(
                x => x.BOTYPE == boType).Select(
                x => new { GROUPNAME = x.NAME, GROUPCODE = x.CODE }).ToList();

            list.Add(new { GROUPNAME = string.Empty, GROUPCODE = 0 });

            groupFileBox.DataSource = list;
        }
    }
}