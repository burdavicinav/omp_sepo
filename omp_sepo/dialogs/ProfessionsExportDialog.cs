using imp_exp;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public class ProfessionsExportDialog : ExportDialog
    {
        public ProfessionsExportDialog() : base()
        {
        }

        public override void Run(object sender, EventArgs e)
        {
            base.Run(sender, e);

            try
            {
                IExportManager export = new ProfessionsManager();
                export.ExportToXml(this.File);

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