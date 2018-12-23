using System;
using System.Windows.Forms;

namespace ui_lib
{
    public class DirectoryPathBox : TextRangeBox
    {
        public delegate void TextChangedEventHandler(object s, TextChangedEventArgs e);

        public event TextChangedEventHandler TextValueChanged;

        public string Filter { get; set; }

        public DirectoryPathBox()
            : base()
        {
            textBox.ReadOnly = true;
        }

        protected override void OnClickRange(object sender, EventArgs args)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextChangedEventArgs e = new TextChangedEventArgs();
                e.Old = TextValue;

                TextValue = dialog.SelectedPath;

                e.New = TextValue;

                //TextValueChanged?.Invoke(this, e);
                if (TextValueChanged != null)
                {
                    TextValueChanged(this, e);
                }
            }
        }
    }
}