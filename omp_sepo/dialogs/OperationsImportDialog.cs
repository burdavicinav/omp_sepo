using imp_exp;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo.dialogs
{
    public partial class OperationsImportDialog : Form
    {
        private IViewRepository<TECHOPERATION_TYPES> _repoOperTypes;

        private void LoadOperTypes()
        {
            var types = _repoOperTypes.GetQuery()
                .Select(x => new { Key = x.CODE, Value = x.NAME })
                .OrderBy(x => x.Value)
                .ToList();

            types.Add(new { Key = -1, Value = String.Empty });

            operTypeBox.ValueMember = "Key";
            operTypeBox.DisplayMember = "Value";
            operTypeBox.DataSource = new BindingSource(types, null);
        }

        public OperationsImportDialog(IViewRepository<TECHOPERATION_TYPES> repo)
        {
            _repoOperTypes = repo;

            InitializeComponent();
            LoadOperTypes();

            fileBox.TextValueChanged += ChangeAcceptEnable;
            fileDopBox.TextValueChanged += ChangeAcceptEnable;
            operTypeBox.SelectedValueChanged += ChangeOperTypeValue;

            okButton.Enabled = false;
            TaskAccept = false;
        }

        public OperationsImportDialog() : this(new ViewRepository<TECHOPERATION_TYPES>())
        {
        }

        private void ChangeOperTypeValue(object sender, EventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void ChangeAcceptEnable(object sender, TextChangedEventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private bool TaskAccept { get; set; }

        private bool IsCorrectData()
        {
            return (fileBox.TextValue != String.Empty &&
                fileDopBox.TextValue != String.Empty &&
                operTypeBox.SelectedValue != null
                );
        }

        private void Task()
        {
            try
            {
                int? opertype = ((int)operTypeBox.SelectedValue == -1) ? null : (int?)operTypeBox.SelectedValue;

                TechnologyOperationsManager import = new TechnologyOperationsManager();
                import.LoadFromXml(fileBox.TextValue, fileDopBox.TextValue);
                import.Load(opertype);

                TaskAccept = true;

                MessageBox.Show(
                    "Операция успешно завершена!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            catch (Exception exc)
            {
                MessageBox.Show(
                    exc.Message + " " + exc.StackTrace,
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            finally
            {
                okButton.Enabled = true;
                cancelButton.Enabled = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (TaskAccept)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                okButton.Enabled = false;
                cancelButton.Enabled = false;

                Thread thread = new Thread(Task);
                thread.Start();
            }
        }
    }
}