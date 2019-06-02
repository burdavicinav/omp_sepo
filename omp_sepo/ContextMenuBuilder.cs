using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ui_lib;

namespace omp_sepo
{
    public class ContextMenuBuilder : IContextMenuBuilder
    {
        public ContextMenuStrip Menu { get; set; }

        public ContextMenuBuilder(ContextMenuStrip menu)
        {
            this.Menu = menu;
        }

        public ToolStripItem NewToolStripItem(EventHandler onClick)
        {
            ToolStripItem item = this.Menu.Items.Add("Добавить", omp_sepo.Properties.Resources.add_green, onClick);
            item.Name = "Add";

            return item;
        }

        public ToolStripItem EditToolStripItem(EventHandler onClick)
        {
            ToolStripItem item = this.Menu.Items.Add("Изменить", omp_sepo.Properties.Resources.edit_pencil, onClick);
            item.Name = "Edit";

            return item;
        }

        public ToolStripItem DeleteToolStripItem(EventHandler onClick)
        {
            ToolStripItem item = this.Menu.Items.Add("Удалить", omp_sepo.Properties.Resources.delete, onClick);
            item.Name = "Delete";

            return item;
        }

        public void BlockEditStripItem(EventHandler onAdd, EventHandler onEdit, EventHandler onDelete)
        {
            NewToolStripItem(onAdd);
            EditToolStripItem(onEdit);
            DeleteToolStripItem(onDelete);
        }
    }
}
