using System;
using System.Windows.Forms;

namespace ui_lib
{
    public partial class TextRangeBox : UserControl
    {
        public string TextValue
        {
            get
            {
                return textBox.Text;
            }

            set
            {
                textBox.Text = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (textBox.Text == String.Empty);
            }
        }

        public TextRangeBox()
        {
            InitializeComponent();

            rangeBox.Click += OnClickRange;
        }

        protected virtual void OnClickRange(object sender, EventArgs args)
        {
        }
    }
}