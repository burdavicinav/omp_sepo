using System;
using System.Threading;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public partial class StepsImportDialog : Form
    {
        public StepsImportDialog()
        {
            InitializeComponent();

            execProgress.Style = ProgressBarStyle.Blocks;
            execProgress.Value = 0;

            okButton.Enabled = false;
            TaskAccept = false;

            fileBox.TextValueChanged += OnFileBoxChanged;
            fileDopBox.TextValueChanged += OnFileBoxChanged;
        }

        private bool TaskAccept { get; set; }

        private bool IsCorrectData()
        {
            return (fileBox.TextValue != String.Empty && fileDopBox.TextValue != String.Empty);
        }

        private void OnFileBoxChanged(object sender, ui_lib.TextChangedEventArgs e)
        {
            okButton.Enabled = IsCorrectData();
        }

        private void Task()
        {
            try
            {
                imp_exp.Module.Connection = omp_sepo.Module.Connection;
                imp_exp.TechnologyStepsManager mngr = new imp_exp.TechnologyStepsManager();

                mngr.LoadFromXml(
                    fileBox.TextValue,
                    fileDopBox.TextValue
                    );

                mngr.Load((isClassifyBox.Checked) ? 0 : 1);

                TaskAccept = true;

                this.Invoke(new ThreadStart(delegate
                {
                    execProgress.Style = ProgressBarStyle.Blocks;
                    execProgress.Value = execProgress.Maximum;
                }));

                MessageBox.Show(
                    "Операция успешно завершена!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            catch (Exception exc)
            {
                this.Invoke(new ThreadStart(delegate
                {
                    execProgress.Style = ProgressBarStyle.Blocks;
                    execProgress.Value = 0;
                }));

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
                execProgress.Style = ProgressBarStyle.Marquee;
                execProgress.MarqueeAnimationSpeed = 30;

                Thread thread = new Thread(Task);
                thread.Start();
            }
        }
    }
}