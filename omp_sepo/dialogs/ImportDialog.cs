namespace omp_sepo.dialogs
{
    public class ImportDialog : ExpImpDialog
    {
        public ImportDialog() : base()
        {
            Text = "Импорт";
            fileBox.Mode = ui_lib.UiFileMode.Open;
        }
    }
}