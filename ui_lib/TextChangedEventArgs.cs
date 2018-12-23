using System;

namespace ui_lib
{
    public class TextChangedEventArgs : EventArgs
    {
        public string Old { get; set; }

        public string New { get; set; }

        public TextChangedEventArgs() : base()
        {
        }
    }
}