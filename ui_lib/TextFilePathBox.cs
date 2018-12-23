using System;
using System.Windows.Forms;

namespace ui_lib
{
    public enum UiFileMode { Open, Save }

    public class TextFilePathBox : TextRangeBox
    {
        public delegate void TextChangedEventHandler(object s, TextChangedEventArgs e);

        public event TextChangedEventHandler TextValueChanged;

        public UiFileMode Mode { get; set; }

        public string Filter { get; set; }

        public TextFilePathBox()
            : base()
        {
            textBox.ReadOnly = true;
        }

        protected override void OnClickRange(object sender, EventArgs args)
        {
            FileDialog dialog;

            if (Mode == UiFileMode.Save)
            {
                dialog = new SaveFileDialog();
            }
            else
            {
                dialog = new OpenFileDialog();
            }

            dialog.Filter = Filter;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextChangedEventArgs e = new TextChangedEventArgs();
                e.Old = TextValue;

                TextValue = dialog.FileName;

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