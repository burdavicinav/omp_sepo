namespace omp_sepo.dialogs
{
    partial class ExpImpDialog
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.fileBox = new ui_lib.TextFilePathBox();
            this.label2 = new System.Windows.Forms.Label();
            this.csvButton = new System.Windows.Forms.RadioButton();
            this.xmlButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.fileBox);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.csvButton);
            this.groupBox.Controls.Add(this.xmlButton);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(259, 70);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // fileBox
            // 
            this.fileBox.Filter = null;
            this.fileBox.Location = new System.Drawing.Point(63, 42);
            this.fileBox.Margin = new System.Windows.Forms.Padding(0);
            this.fileBox.Mode = ui_lib.UiFileMode.Open;
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(177, 20);
            this.fileBox.TabIndex = 4;
            this.fileBox.TextValue = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Файл:";
            // 
            // csvButton
            // 
            this.csvButton.AutoSize = true;
            this.csvButton.Location = new System.Drawing.Point(144, 14);
            this.csvButton.Name = "csvButton";
            this.csvButton.Size = new System.Drawing.Size(96, 17);
            this.csvButton.TabIndex = 2;
            this.csvButton.Text = "csv (1251, tab)";
            this.csvButton.UseVisualStyleBackColor = true;
            // 
            // xmlButton
            // 
            this.xmlButton.AutoSize = true;
            this.xmlButton.Checked = true;
            this.xmlButton.Location = new System.Drawing.Point(63, 14);
            this.xmlButton.Name = "xmlButton";
            this.xmlButton.Size = new System.Drawing.Size(70, 17);
            this.xmlButton.TabIndex = 1;
            this.xmlButton.TabStop = true;
            this.xmlButton.Text = "xml (utf-8)";
            this.xmlButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(197, 91);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(116, 91);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.Run);
            // 
            // ExpImpDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpImpDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton csvButton;
        private System.Windows.Forms.RadioButton xmlButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        protected ui_lib.TextFilePathBox fileBox;
    }
}