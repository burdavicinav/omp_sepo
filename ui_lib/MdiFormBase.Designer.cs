namespace ui_lib
{
    public partial class MdiFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.windowTab = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(594, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // windowTab
            // 
            this.windowTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.windowTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowTab.Location = new System.Drawing.Point(0, 24);
            this.windowTab.Name = "windowTab";
            this.windowTab.Padding = new System.Drawing.Point(0, 0);
            this.windowTab.SelectedIndex = 0;
            this.windowTab.Size = new System.Drawing.Size(594, 21);
            this.windowTab.TabIndex = 2;
            // 
            // MdiFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 288);
            this.Controls.Add(this.windowTab);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MdiFormBase";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.TabControl windowTab;
        protected System.Windows.Forms.MenuStrip mainMenu;
    }
}