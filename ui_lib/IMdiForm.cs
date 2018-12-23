using System.Windows.Forms;

namespace ui_lib
{
    public interface IMdiForm
    {
        void AddChild(string name, Form child, bool isfullscreen = false);

        void AddChild(string name, Control view, bool isfullscreen = false);
    }
}