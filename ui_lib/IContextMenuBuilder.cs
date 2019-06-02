using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ui_lib
{
    public interface IContextMenuBuilder
    {
        ToolStripItem NewToolStripItem(EventHandler onClick);

        ToolStripItem EditToolStripItem(EventHandler onClick);

        ToolStripItem DeleteToolStripItem(EventHandler onClick);

        void BlockEditStripItem(EventHandler onAdd, EventHandler onEdit, EventHandler onDelete);
    }
}
