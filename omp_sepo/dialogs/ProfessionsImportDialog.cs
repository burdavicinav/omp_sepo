using imp_exp;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public class ProfessionsImportDialog : ImportDialog
    {
        public ProfessionsImportDialog() : base()
        {
        }

        public override void Run(object sender, EventArgs e)
        {
            base.Run(sender, e);

            try
            {
                imp_exp.Module.Connection = omp_sepo.Module.Connection;

                IImportManager import = new ProfessionsManager();
                import.LoadFromXml(this.File);
                import.Load();

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