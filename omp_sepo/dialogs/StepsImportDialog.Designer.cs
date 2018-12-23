namespace omp_sepo.dialogs
{
    partial class StepsImportDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isClassifyBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.execProgress = new System.Windows.Forms.ProgressBar();
            this.fileBox = new ui_lib.TextFilePathBox();
            this.fileDopBox = new ui_lib.TextFilePathBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fileDopBox);
            this.groupBox1.Controls.Add(this.fileBox);
            this.groupBox1.Controls.Add(this.isClassifyBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // isClassifyBox
            // 
            this.isClassifyBox.AutoSize = true;
            this.isClassifyBox.Checked = true;
            this.isClassifyBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isClassifyBox.Location = new System.Drawing.Point(9, 86);
            this.isClassifyBox.Name = "isClassifyBox";
            this.isClassifyBox.Size = new System.Drawing.Size(246, 17);
            this.isClassifyBox.TabIndex = 6;
            this.isClassifyBox.Text = "Исключить каталоги \"Из классификатора\"";
            this.isClassifyBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Файл доп.:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Файл:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(197, 151);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(116, 151);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // execProgress
            // 
            this.execProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.execProgress.Location = new System.Drawing.Point(13, 128);
            this.execProgress.Name = "execProgress";
            this.execProgress.Size = new System.Drawing.Size(259, 17);
            this.execProgress.TabIndex = 3;
            // 
            // fileBox
            // 
            this.fileBox.Filter = "(*.xml)|*.xml";
            this.fileBox.Location = new System.Drawing.Point(68, 16);
            this.fileBox.Margin = new System.Windows.Forms.Padding(0);
            this.fileBox.Mode = ui_lib.UiFileMode.Open;
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(178, 20);
            this.fileBox.TabIndex = 7;
            this.fileBox.TextValue = "";
            // 
            // fileDopBox
            // 
            this.fileDopBox.Filter = "(*.xml)|*.xml";
            this.fileDopBox.Location = new System.Drawing.Point(68, 52);
            this.fileDopBox.Margin = new System.Windows.Forms.Padding(0);
            this.fileDopBox.Mode = ui_lib.UiFileMode.Open;
            this.fileDopBox.Name = "fileDopBox";
            this.fileDopBox.Size = new System.Drawing.Size(178, 20);
            this.fileDopBox.TabIndex = 8;
            this.fileDopBox.TextValue = "";
            // 
            // StepsImportDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.execProgress);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StepsImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Импорт переходов";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ProgressBar execProgress;
        private System.Windows.Forms.CheckBox isClassifyBox;
        private ui_lib.TextFilePathBox fileDopBox;
        private ui_lib.TextFilePathBox fileBox;
    }
}