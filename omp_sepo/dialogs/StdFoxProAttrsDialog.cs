using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StdFoxProAttrsDialog : Form
    {
        private IRepository<SEPO_STD_FOXPRO_ATTRS> attrsRepo;

        public DialogType DialogType { get; private set; }

        public SEPO_STD_FOXPRO_ATTRS Attr { get; set; }

        public StdFoxProAttrsDialog(IRepository<SEPO_STD_FOXPRO_ATTRS> repo)
        {
            attrsRepo = repo;

            InitializeComponent();

            DialogType = DialogType.Add;
            this.Text = "Создание атрибута";
            SetData();
            okButton.Enabled = IsCorrectData();
            okButton.Click += OnOkClicked;

            shortnameBox.TextChanged += OnDataChanged;
            nameBox.TextChanged += OnDataChanged;
            typeBox.SelectedValueChanged += OnDataChanged;
        }

        public StdFoxProAttrsDialog() : this(new Repository<SEPO_STD_FOXPRO_ATTRS>())
        {
        }

        public StdFoxProAttrsDialog(SEPO_STD_FOXPRO_ATTRS attr, IRepository<SEPO_STD_FOXPRO_ATTRS> repo)
            : this(repo)

        {
            DialogType = DialogType.Edit;
            this.Text = "Редактирование атрибута";
            Attr = attr;
        }

        private void OnDataChanged(object sender, EventArgs args)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void SetData()
        {
            var type = new[] {
                new { id = (short)1, name = "Строка" },
                new { id = (short)2, name = "Число" },
                new { id = (short)3, name = "Целое число" },
                new { id = (short)4, name = "Дата" }
                };

            typeBox.ValueMember = "id";
            typeBox.DisplayMember = "name";
            typeBox.DataSource = type;
        }

        private bool IsCorrectData()
        {
            return shortnameBox.Text != String.Empty
                && nameBox.Text != String.Empty
                && typeBox.SelectedValue != null;
        }

        private void OnOkClicked(object sender, EventArgs args)
        {
            if (DialogType == DialogType.Add)
            {
                try
                {
                    SEPO_STD_FOXPRO_ATTRS attr = new SEPO_STD_FOXPRO_ATTRS();

                    attr.NAME = nameBox.Text;
                    attr.SHORTNAME = shortnameBox.Text;
                    attr.TYPE_ = (short)typeBox.SelectedValue;

                    attrsRepo.Create(attr);
                    Attr = attr;

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                try
                {
                    Attr.SHORTNAME = shortnameBox.Text;
                    Attr.NAME = nameBox.Text;
                    Attr.TYPE_ = (short)typeBox.SelectedValue;

                    attrsRepo.Update(Attr);

                    DialogResult = DialogResult.OK;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}