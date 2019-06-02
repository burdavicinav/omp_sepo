using NHibernate.Linq;
using obj_lib;
using obj_lib.Entities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class SepoFixtureAfObjectsDialog : Form
    {
        private DialogType dialogType;

        public int Id { get; set; }

        public SepoFixtureAfObjectsDialog()
        {
            InitializeComponent();

            dialogType = DialogType.Add;

            okButton.Click += OkButton_Click;
            boTypeBox.SelectedValueChanged += OnBoTypeChanged;

            RefreshData();
        }

        public SepoFixtureAfObjectsDialog(int id) : this()
        {
            dialogType = DialogType.Edit;
            Id = id;

            try
            {
                var session = obj_lib.Module.OpenSession();

                var item = session.Get<SEPO_FIXTURE_AF_OBJECTS>(id);

                boTypeBox.SelectedValue = item.ID_TYPE;
                groupFileBox.SelectedValue = (item.ID_FILE_GROUP == null) ? 0 : item.ID_FILE_GROUP;
                ownerBox.SelectedValue = (item.ID_OWNER == null) ? -1 : item.ID_OWNER;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void RefreshData()
        {
            var session = obj_lib.Module.OpenSession();

            boTypeBox.DisplayMember = "BOTYPENAME";
            boTypeBox.ValueMember = "BOTYPE";
            boTypeBox.DataSource = session.Query<V_SEPO_TFLEX_BO_TYPES>()
                .Select(x => new { x.BOTYPENAME, x.BOTYPE })
                .OrderBy(x => x.BOTYPENAME)
                .ToList();

            var ownerList = session.Query<OWNER_NAME>()
                .Select(x => new { NAME = x.NAME, CODE = x.OWNER })
                .OrderBy(x => x.NAME)
                .ToList();

            ownerList.Add(new { NAME = "<Не задано>", CODE = -1 });

            ownerBox.DisplayMember = "NAME";
            ownerBox.ValueMember = "CODE";
            ownerBox.DataSource = ownerList;
        }

        private void OnBoTypeChanged(object sender, EventArgs e)
        {
            if (boTypeBox.SelectedValue == null)
            {
                return;
            }

            int boType = (int)boTypeBox.SelectedValue;

            var session = obj_lib.Module.OpenSession();

            var groupList = session.Query<ATTACHMENTS_GROUPS>()
                .Where(x => x.BOTYPE == boType)
                .Select(x => new { x.NAME, x.CODE })
                .ToList();

            groupList.Add(new { NAME = "<Не задано>", CODE = 0 });

            groupFileBox.DisplayMember = "NAME";
            groupFileBox.ValueMember = "CODE";
            groupFileBox.DataSource = groupList;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (dialogType == DialogType.Add)
            {
                SEPO_FIXTURE_AF_OBJECTS obj = new SEPO_FIXTURE_AF_OBJECTS();

                obj.ID_TYPE = (int)boTypeBox.SelectedValue;

                if ((int)groupFileBox.SelectedValue != 0)
                {
                    obj.ID_FILE_GROUP = (int)groupFileBox.SelectedValue;
                }

                if ((int)ownerBox.SelectedValue != -1)
                {
                    obj.ID_OWNER = (int)ownerBox.SelectedValue;
                }

                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        var session = obj_lib.Module.OpenSession();

                        session.Save(obj);

                        transaction.Commit();

                        Id = obj.ID;
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
                var session = obj_lib.Module.OpenSession();

                SEPO_FIXTURE_AF_OBJECTS obj = session.Get<SEPO_FIXTURE_AF_OBJECTS>(Id);

                obj.ID_TYPE = (int)boTypeBox.SelectedValue;

                if ((int)groupFileBox.SelectedValue != 0)
                {
                    obj.ID_FILE_GROUP = (int)groupFileBox.SelectedValue;
                }
                else
                {
                    obj.ID_FILE_GROUP = null;
                }

                if ((int)ownerBox.SelectedValue != -1)
                {
                    obj.ID_OWNER = (int)ownerBox.SelectedValue;
                }
                else
                {
                    obj.ID_OWNER = null;
                }

                using (var transaction = new UnitOfWork())
                {
                    try
                    {
                        session.Update(obj);

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
    }
}