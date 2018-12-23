namespace omp_sepo.dialogs
{
    public class ExportDialog : ExpImpDialog
    {
        public ExportDialog() : base()
        {
            Text = "Экспорт";
            fileBox.Mode = ui_lib.UiFileMode.Save;
        }
    }
}