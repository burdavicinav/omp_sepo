namespace omp_sepo.dialogs
{
    partial class OperationsImportDialog
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
            this.fileDopBox = new ui_lib.TextFilePathBox();
            this.fileBox = new ui_lib.TextFilePathBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.operTypeBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.operTypeBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.fileDopBox);
            this.groupBox1.Controls.Add(this.fileBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // fileDopBox
            // 
            this.fileDopBox.Filter = "(*.xml)|*.xml";
            this.fileDopBox.Location = new System.Drawing.Point(104, 45);
            this.fileDopBox.Margin = new System.Windows.Forms.Padding(0);
            this.fileDopBox.Mode = ui_lib.UiFileMode.Open;
            this.fileDopBox.Name = "fileDopBox";
            this.fileDopBox.Size = new System.Drawing.Size(179, 20);
            this.fileDopBox.TabIndex = 3;
            this.fileDopBox.TextValue = "";
            // 
            // fileBox
            // 
            this.fileBox.Filter = "(*.xml)|*.xml";
            this.fileBox.Location = new System.Drawing.Point(104, 16);
            this.fileBox.Margin = new System.Windows.Forms.Padding(0);
            this.fileBox.Mode = ui_lib.UiFileMode.Open;
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(179, 20);
            this.fileBox.TabIndex = 2;
            this.fileBox.TextValue = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Коды каталогов:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Операции:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(234, 129);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(153, 129);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Тип операции:";
            // 
            // operTypeBox
            // 
            this.operTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operTypeBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.operTypeBox.FormattingEnabled = true;
            this.operTypeBox.Location = new System.Drawing.Point(104, 76);
            this.operTypeBox.Name = "operTypeBox";
            this.operTypeBox.Size = new System.Drawing.Size(179, 21);
            this.operTypeBox.TabIndex = 5;
            // 
            // OperationsImportDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(321, 164);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperationsImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Импорт технологических операций";
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
        private ui_lib.TextFilePathBox fileDopBox;
        private ui_lib.TextFilePathBox fileBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox operTypeBox;
    }
}