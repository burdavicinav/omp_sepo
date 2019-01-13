using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class FixtureImportFilesDialog : Form
    {
        public FixtureImportFilesDialog()
        {
            InitializeComponent();

            okButton.Enabled = false;

            osnAllBox.TextValueChanged += OnChangeValueBox;
            osnDetBox.TextValueChanged += OnChangeValueBox;
            osnDocsBox.TextValueChanged += OnChangeValueBox;
            osnSeBox.TextValueChanged += OnChangeValueBox;
            osnSostavBox.TextValueChanged += OnChangeValueBox;
            osnSpBox.TextValueChanged += OnChangeValueBox;

            okButton.Click += OnClick;
        }

        protected void OnChangeValueBox(object sender, EventArgs args)
        {
            okButton.Enabled = (
                osnAllBox.TextValue != String.Empty &&
                osnDetBox.TextValue != String.Empty &&
                osnDocsBox.TextValue != String.Empty &&
                osnSeBox.TextValue != String.Empty &&
                osnSostavBox.TextValue != String.Empty &&
                osnSpBox.TextValue != String.Empty
                );
        }

        protected void OnClick(object sender, EventArgs args)
        {
            try
            {
                imp_exp.FixtureManager mgr = new imp_exp.FixtureManager();
                mgr.LoadFromFiles(
                    osnAllBox.TextValue,
                    osnDetBox.TextValue,
                    osnDocsBox.TextValue,
                    osnSeBox.TextValue,
                    osnSostavBox.TextValue,
                    osnSpBox.TextValue
                    );

                MessageBox.Show(
                    "Операция успешно завершена!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                DialogResult = DialogResult.OK;
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
        }
    }
}