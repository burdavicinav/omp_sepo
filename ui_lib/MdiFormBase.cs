using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ui_lib
{
    public partial class MdiFormBase : Form, IMdiForm
    {
        private Dictionary<TabPage, Form> childs;

        private ToolStripMenuItem windowMenuItem;

        private ToolStripMenuItem
            closeAllWindowsItem,
            cascadeWindowsItem,
            tileHorizontalWindowsItem,
            tileVerticalWindowsItem,
            arrangeWindowsItem;

        private ContextMenuStrip menuTab;
        private int indexLastTab;

        private void SetWindowsMenu()
        {
            windowMenuItem = new ToolStripMenuItem("&Окно...");
            windowMenuItem.DropDownOpening +=
                new EventHandler(OnWindowsMenuItemDropDownOpening);
            windowMenuItem.DropDownItemClicked +=
                new ToolStripItemClickedEventHandler(OnWindowsMenuDropDownItemClicked);

            cascadeWindowsItem = new ToolStripMenuItem("&Каскад");
            cascadeWindowsItem.Click += new EventHandler(OnCascadeWindowItems);

            tileHorizontalWindowsItem = new ToolStripMenuItem("&Горизонтально");
            tileHorizontalWindowsItem.Click += new EventHandler(OnTileHorizontalWindowItems);

            tileVerticalWindowsItem = new ToolStripMenuItem("&Вертикально");
            tileVerticalWindowsItem.Click += new EventHandler(OnTileVerticalWindowItems);

            arrangeWindowsItem = new ToolStripMenuItem("&Упорядочить");
            arrangeWindowsItem.Click += new EventHandler(OnArrangeWindowItems);

            closeAllWindowsItem = new ToolStripMenuItem("&Закрыть все");
            closeAllWindowsItem.Click += new EventHandler(OnCloseAllWindowItems);
        }

        private void OnWindowTabsMouseUp(object sender, MouseEventArgs args)
        {
            if (args.Button == System.Windows.Forms.MouseButtons.Right)
            {
                menuTab.Show(windowTab, args.X, args.Y);
                for (int i = 0; i < windowTab.TabPages.Count; i++)
                {
                    Rectangle rect = windowTab.GetTabRect(i);
                    if (rect.Contains(args.Location))
                    {
                        indexLastTab = i;
                        break;
                    }
                }
            }
        }

        private void OnMenuTabControlCloseClick(object sender, EventArgs args)
        {
            TabPage currentPage = windowTab.TabPages[indexLastTab];

            Form form = childs.Where(x => x.Key == currentPage).First().Value;
            form.Close();
        }

        private void OnWindowTabsSelect(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex >= 0)
            {
                Form form = childs.Where(x => x.Key == e.TabPage).First().Value;
                form.Activate();
            }
        }

        private void OnWindowTabsDrawItem(object sender, DrawItemEventArgs args)
        {
            Rectangle tabArea = windowTab.GetTabRect(args.Index);

            Graphics g = args.Graphics;
            Pen p = new Pen(Color.White);
            Font font = new Font("Arial", 8.0f);

            SolidBrush brush;
            if (args.Index % 3 == 0)
            {
                brush = new SolidBrush(Color.LightYellow);
            }
            else if (args.Index % 3 == 1)
            {
                brush = new SolidBrush(Color.LightGreen);
            }
            else
            {
                brush = new SolidBrush(Color.LightBlue);
            }

            g.FillRectangle(brush, tabArea);

            brush = new SolidBrush(Color.Black);
            g.DrawString(
                windowTab.TabPages[args.Index].Text,
                font,
                brush,
                tabArea);
        }

        private void OnMdiChildActivate(object sender, EventArgs e)
        {
            Form form = this.ActiveMdiChild;
            if (form != null)
            {
                TabPage page = childs.Where(x => x.Value == form).First().Key;
                windowTab.SelectedTab = page;
            }
            else
            {
                mainMenu.Items.Remove(windowMenuItem);
            }
        }

        private void OnWindowsMenuItemDropDownOpening(object sender, EventArgs e)
        {
            windowMenuItem.DropDownItems.Clear();

            windowMenuItem.DropDownItems.Add(cascadeWindowsItem);
            windowMenuItem.DropDownItems.Add(tileHorizontalWindowsItem);
            windowMenuItem.DropDownItems.Add(tileVerticalWindowsItem);
            windowMenuItem.DropDownItems.Add(arrangeWindowsItem);
            windowMenuItem.DropDownItems.Add(new ToolStripSeparator());

            int k = 0;
            foreach (Form child in this.MdiChildren)
            {
                k += 1;
                ToolStripMenuItem item;
                //if (k < 10)
                {
                    item = new ToolStripMenuItem(k.ToString() + ". " + child.Text);

                    item.Checked = (child == this.ActiveMdiChild);
                    item.Tag = child;
                    windowMenuItem.DropDownItems.Add(item);
                }
                //else
                //{
                //    item = new ToolStripMenuItem("Все...");
                //    item.Tag = null;
                //    windowMenuItem.DropDownItems.Add(item);

                //    break;
                //}
            }

            windowMenuItem.DropDownItems.Add(new ToolStripSeparator());
            windowMenuItem.DropDownItems.Add(closeAllWindowsItem);
        }

        private void OnWindowsMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs args)
        {
            if (args.ClickedItem.Tag != null)
            {
                ((Form)args.ClickedItem.Tag).Activate();
            }
        }

        private void OnCloseAllWindowItems(object sender, EventArgs args)
        {
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }
        }

        private void OnCascadeWindowItems(object sender, EventArgs args)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void OnTileHorizontalWindowItems(object sender, EventArgs args)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void OnTileVerticalWindowItems(object sender, EventArgs args)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void OnArrangeWindowItems(object sender, EventArgs args)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void OnCloseMdiChild(object sender, FormClosedEventArgs args)
        {
            TabPage page = childs.Where(x => x.Value == sender).First().Key;
            windowTab.TabPages.Remove(page);
            childs.Remove(page);
        }

        private void StartWindowShow()
        {
            Form welcomeChild = new Form();
            AddChild("WelcomePage", welcomeChild);
        }

        public MdiFormBase()
        {
            InitializeComponent();

            windowTab.Selected += OnWindowTabsSelect;
            windowTab.MouseUp += OnWindowTabsMouseUp;
            windowTab.DrawItem += OnWindowTabsDrawItem;
            this.MdiChildActivate += OnMdiChildActivate;

            childs = new Dictionary<TabPage, Form>();
            menuTab = new ContextMenuStrip();
            menuTab.Items.Add("Закрыть", null,
                new EventHandler(OnMenuTabControlCloseClick)).Name = "Close";

            SetWindowsMenu();

            //StartWindowShow();
            //StartWindowShow();
        }

        public void AddChild(string name, Form child, bool isfullscreen = false)
        {
            child.Text = name;

            if (isfullscreen)
            {
                child.WindowState = FormWindowState.Maximized;
            }

            if (childs.Count == 0)
            {
                mainMenu.Items.Add(windowMenuItem);
            }

            TabPage newPage = new TabPage(name);
            windowTab.TabPages.Add(newPage);

            childs.Add(newPage, child);
            child.MdiParent = this;
            child.Show();

            child.FormClosed += OnCloseMdiChild;
        }

        public void AddChild(string name, Control view, bool isfullscreen = false)
        {
            Form child = new Form();

            if (isfullscreen)
            {
                child.WindowState = FormWindowState.Maximized;
            }

            view.Dock = DockStyle.Fill;
            child.Controls.Add(view);

            AddChild(name, child);
        }
    }
}