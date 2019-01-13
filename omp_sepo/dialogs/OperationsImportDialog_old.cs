using imp_exp;
using System;
using System.Windows.Forms;

namespace omp_sepo.dialogs
{
    public class OperationsImportDialog_old : ImportDialog
    {
        public OperationsImportDialog_old() : base()
        {
            fileBox.Mode = ui_lib.UiFileMode.Open;
        }

        public override void Run(object sender, EventArgs e)
        {
            try
            {
                fileBox.Filter = Extension;

                IImportManager import = new TechnologyOperationsManager();
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