using obj_lib.Entities;
using obj_lib.Repositories;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo
{
    public class MainForm : MdiFormBase
    {
        private Splitter splitter1;
        private StatusStrip statusStrip1;
        private TreeTaskView treeTaskView;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.treeTaskView = new omp_sepo.TreeTaskView();
            this.SuspendLayout();
            //
            // windowTab
            //
            this.windowTab.Location = new System.Drawing.Point(270, 24);
            this.windowTab.Size = new System.Drawing.Size(808, 21);
            //
            // splitter1
            //
            this.splitter1.Location = new System.Drawing.Point(270, 45);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 459);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            //
            // statusStrip1
            //
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1078, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            //
            // treeTaskView
            //
            this.treeTaskView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeTaskView.Location = new System.Drawing.Point(0, 24);
            this.treeTaskView.Name = "treeTaskView";
            this.treeTaskView.Size = new System.Drawing.Size(270, 480);
            this.treeTaskView.TabIndex = 4;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1078, 526);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeTaskView);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.treeTaskView, 0);
            this.Controls.SetChildIndex(this.windowTab, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetMainMenu()
        {
            ToolStripMenuItem fileMenuItem = new ToolStripMenuItem("&Файл");

            ToolStripMenuItem closeMenuItem = new ToolStripMenuItem("&Закрыть");
            closeMenuItem.Click += delegate { this.Close(); };

            ToolStripMenuItem helpMenuItem = new ToolStripMenuItem("&Помощь");

            ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem("&О программе");
            aboutMenuItem.Click += delegate
            {
                new AboutBox().ShowDialog();
            };

            fileMenuItem.DropDownItems.Add(closeMenuItem);
            helpMenuItem.DropDownItems.Add(aboutMenuItem);

            mainMenu.Items.Add(fileMenuItem);
            mainMenu.Items.Add(helpMenuItem);
        }

        public MainForm()
        {
            InitializeComponent();

            Text = Program.Settings.ProgramName;

            treeTaskView.Init();

            treeTaskView.LoadData(
                new ViewRepository<SEPO_TASK_FOLDER_LIST>(),
                new ViewRepository<SEPO_TASK_LIST>());

            treeTaskView.ExpandAll();

            SetMainMenu();
        }
    }
}