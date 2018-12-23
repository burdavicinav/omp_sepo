namespace omp_sepo.dialogs
{
    partial class FixtureImportFilesDialog
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
            this.osnSpBox = new ui_lib.TextFilePathBox();
            this.osnSostavBox = new ui_lib.TextFilePathBox();
            this.osnSeBox = new ui_lib.TextFilePathBox();
            this.osnDocsBox = new ui_lib.TextFilePathBox();
            this.osnDetBox = new ui_lib.TextFilePathBox();
            this.osnAllBox = new ui_lib.TextFilePathBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.osnSpBox);
            this.groupBox1.Controls.Add(this.osnSostavBox);
            this.groupBox1.Controls.Add(this.osnSeBox);
            this.groupBox1.Controls.Add(this.osnDocsBox);
            this.groupBox1.Controls.Add(this.osnDetBox);
            this.groupBox1.Controls.Add(this.osnAllBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Файлы";
            // 
            // osnSpBox
            // 
            this.osnSpBox.Filter = "(*.csv)|*.csv";
            this.osnSpBox.Location = new System.Drawing.Point(73, 193);
            this.osnSpBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnSpBox.Mode = ui_lib.UiFileMode.Open;
            this.osnSpBox.Name = "osnSpBox";
            this.osnSpBox.Size = new System.Drawing.Size(170, 20);
            this.osnSpBox.TabIndex = 21;
            this.osnSpBox.TextValue = "";
            // 
            // osnSostavBox
            // 
            this.osnSostavBox.Filter = "(*.csv)|*.csv";
            this.osnSostavBox.Location = new System.Drawing.Point(73, 162);
            this.osnSostavBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnSostavBox.Mode = ui_lib.UiFileMode.Open;
            this.osnSostavBox.Name = "osnSostavBox";
            this.osnSostavBox.Size = new System.Drawing.Size(170, 20);
            this.osnSostavBox.TabIndex = 20;
            this.osnSostavBox.TextValue = "";
            // 
            // osnSeBox
            // 
            this.osnSeBox.Filter = "(*.csv)|*.csv";
            this.osnSeBox.Location = new System.Drawing.Point(73, 128);
            this.osnSeBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnSeBox.Mode = ui_lib.UiFileMode.Open;
            this.osnSeBox.Name = "osnSeBox";
            this.osnSeBox.Size = new System.Drawing.Size(170, 20);
            this.osnSeBox.TabIndex = 19;
            this.osnSeBox.TextValue = "";
            // 
            // osnDocsBox
            // 
            this.osnDocsBox.Filter = "(*.csv)|*.csv";
            this.osnDocsBox.Location = new System.Drawing.Point(73, 94);
            this.osnDocsBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnDocsBox.Mode = ui_lib.UiFileMode.Open;
            this.osnDocsBox.Name = "osnDocsBox";
            this.osnDocsBox.Size = new System.Drawing.Size(170, 20);
            this.osnDocsBox.TabIndex = 18;
            this.osnDocsBox.TextValue = "";
            // 
            // osnDetBox
            // 
            this.osnDetBox.Filter = "(*.csv)|*.csv";
            this.osnDetBox.Location = new System.Drawing.Point(73, 59);
            this.osnDetBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnDetBox.Mode = ui_lib.UiFileMode.Open;
            this.osnDetBox.Name = "osnDetBox";
            this.osnDetBox.Size = new System.Drawing.Size(170, 20);
            this.osnDetBox.TabIndex = 17;
            this.osnDetBox.TextValue = "";
            // 
            // osnAllBox
            // 
            this.osnAllBox.Filter = "(*.csv)|*.csv";
            this.osnAllBox.Location = new System.Drawing.Point(73, 26);
            this.osnAllBox.Margin = new System.Windows.Forms.Padding(0);
            this.osnAllBox.Mode = ui_lib.UiFileMode.Open;
            this.osnAllBox.Name = "osnAllBox";
            this.osnAllBox.Size = new System.Drawing.Size(170, 20);
            this.osnAllBox.TabIndex = 16;
            this.osnAllBox.TextValue = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "osn_sp:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "osn_sostav:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "osn_se:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "osn_docs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "osn_det:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "osn_all:";
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(285, 17);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(285, 46);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // FixtureImportFilesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(372, 267);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixtureImportFilesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Импорт оснастки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private ui_lib.TextFilePathBox osnSpBox;
        private ui_lib.TextFilePathBox osnSostavBox;
        private ui_lib.TextFilePathBox osnSeBox;
        private ui_lib.TextFilePathBox osnDocsBox;
        private ui_lib.TextFilePathBox osnDetBox;
        private ui_lib.TextFilePathBox osnAllBox;
    }
}