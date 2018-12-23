namespace omp_sepo.views
{
    partial class StdSchemesForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.schemesView = new omp_sepo.views.StdSchemesListView();
            this.attrsView = new omp_sepo.views.StdAttrsListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.schemesView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.attrsView);
            this.splitContainer1.Size = new System.Drawing.Size(366, 198);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 0;
            // 
            // schemesView
            // 
            this.schemesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemesView.Form = null;
            this.schemesView.Location = new System.Drawing.Point(0, 0);
            this.schemesView.Name = "schemesView";
            this.schemesView.Size = new System.Drawing.Size(366, 122);
            this.schemesView.TabIndex = 0;
            this.schemesView.UseCompatibleStateImageBehavior = false;
            // 
            // attrsView
            // 
            this.attrsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attrsView.Location = new System.Drawing.Point(0, 0);
            this.attrsView.Name = "attrsView";
            this.attrsView.Size = new System.Drawing.Size(366, 72);
            this.attrsView.TabIndex = 0;
            this.attrsView.UseCompatibleStateImageBehavior = false;
            // 
            // StdSchemesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 198);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StdSchemesForm";
            this.Text = "Схемы атрибутов";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private StdSchemesListView schemesView;
        private StdAttrsListView attrsView;
    }
}