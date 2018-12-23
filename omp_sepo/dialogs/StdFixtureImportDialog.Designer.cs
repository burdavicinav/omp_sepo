namespace omp_sepo.dialogs
{
    partial class StdFixtureImportDialog
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
            this.enumPath = new ui_lib.TextFilePathBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stdFixturePath = new ui_lib.TextFilePathBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDirectoryPath = new ui_lib.DirectoryPathBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.entitiesFilePath = new ui_lib.TextFilePathBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tpFilePrefix = new System.Windows.Forms.TextBox();
            this.tpFilePath = new ui_lib.TextFilePathBox();
            this.groupDceButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupTpButton = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.logLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enumPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.stdFixturePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Стандартная оснастка";
            // 
            // enumPath
            // 
            this.enumPath.Filter = null;
            this.enumPath.Location = new System.Drawing.Point(111, 62);
            this.enumPath.Margin = new System.Windows.Forms.Padding(0);
            this.enumPath.Mode = ui_lib.UiFileMode.Open;
            this.enumPath.Name = "enumPath";
            this.enumPath.Size = new System.Drawing.Size(243, 20);
            this.enumPath.TabIndex = 5;
            this.enumPath.TextValue = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Перечисления:";
            // 
            // stdFixturePath
            // 
            this.stdFixturePath.Filter = null;
            this.stdFixturePath.Location = new System.Drawing.Point(111, 28);
            this.stdFixturePath.Margin = new System.Windows.Forms.Padding(0);
            this.stdFixturePath.Mode = ui_lib.UiFileMode.Open;
            this.stdFixturePath.Name = "stdFixturePath";
            this.stdFixturePath.Size = new System.Drawing.Size(243, 20);
            this.stdFixturePath.TabIndex = 1;
            this.stdFixturePath.TextValue = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Оснастка:";
            // 
            // tpDirectoryPath
            // 
            this.tpDirectoryPath.Filter = null;
            this.tpDirectoryPath.Location = new System.Drawing.Point(111, 117);
            this.tpDirectoryPath.Margin = new System.Windows.Forms.Padding(0);
            this.tpDirectoryPath.Name = "tpDirectoryPath";
            this.tpDirectoryPath.Size = new System.Drawing.Size(243, 20);
            this.tpDirectoryPath.TabIndex = 3;
            this.tpDirectoryPath.TextValue = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Директория ТП:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(307, 346);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(226, 346);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.entitiesFilePath);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tpFilePrefix);
            this.groupBox2.Controls.Add(this.tpFilePath);
            this.groupBox2.Controls.Add(this.groupDceButton);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tpDirectoryPath);
            this.groupBox2.Controls.Add(this.groupTpButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(12, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 191);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Технологические процессы";
            // 
            // entitiesFilePath
            // 
            this.entitiesFilePath.Filter = null;
            this.entitiesFilePath.Location = new System.Drawing.Point(111, 88);
            this.entitiesFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.entitiesFilePath.Mode = ui_lib.UiFileMode.Open;
            this.entitiesFilePath.Name = "entitiesFilePath";
            this.entitiesFilePath.Size = new System.Drawing.Size(243, 20);
            this.entitiesFilePath.TabIndex = 13;
            this.entitiesFilePath.TextValue = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Сущности:";
            // 
            // tpFilePrefix
            // 
            this.tpFilePrefix.Location = new System.Drawing.Point(111, 146);
            this.tpFilePrefix.Name = "tpFilePrefix";
            this.tpFilePrefix.ReadOnly = true;
            this.tpFilePrefix.Size = new System.Drawing.Size(93, 20);
            this.tpFilePrefix.TabIndex = 11;
            this.tpFilePrefix.Text = "TC";
            // 
            // tpFilePath
            // 
            this.tpFilePath.Filter = null;
            this.tpFilePath.Location = new System.Drawing.Point(111, 58);
            this.tpFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.tpFilePath.Mode = ui_lib.UiFileMode.Open;
            this.tpFilePath.Name = "tpFilePath";
            this.tpFilePath.Size = new System.Drawing.Size(243, 20);
            this.tpFilePath.TabIndex = 6;
            this.tpFilePath.TextValue = "";
            // 
            // groupDceButton
            // 
            this.groupDceButton.AutoSize = true;
            this.groupDceButton.Enabled = false;
            this.groupDceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupDceButton.Location = new System.Drawing.Point(157, 29);
            this.groupDceButton.Name = "groupDceButton";
            this.groupDceButton.Size = new System.Drawing.Size(47, 17);
            this.groupDceButton.TabIndex = 9;
            this.groupDceButton.Text = "ДСЕ";
            this.groupDceButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(6, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Префикс файлов:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Список ТП:";
            // 
            // groupTpButton
            // 
            this.groupTpButton.AutoSize = true;
            this.groupTpButton.Checked = true;
            this.groupTpButton.Enabled = false;
            this.groupTpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupTpButton.Location = new System.Drawing.Point(111, 29);
            this.groupTpButton.Name = "groupTpButton";
            this.groupTpButton.Size = new System.Drawing.Size(39, 17);
            this.groupTpButton.TabIndex = 8;
            this.groupTpButton.TabStop = true;
            this.groupTpButton.Text = "ТП";
            this.groupTpButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(6, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Группировка:";
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(12, 328);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(0, 13);
            this.logLabel.TabIndex = 5;
            // 
            // StdFixtureImportDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(394, 381);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StdFixtureImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Стандартная оснастка: загрузка файлов";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ui_lib.DirectoryPathBox tpDirectoryPath;
        private System.Windows.Forms.Label label2;
        private ui_lib.TextFilePathBox stdFixturePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private ui_lib.TextFilePathBox enumPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton groupDceButton;
        private System.Windows.Forms.RadioButton groupTpButton;
        private ui_lib.TextFilePathBox tpFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tpFilePrefix;
        private ui_lib.TextFilePathBox entitiesFilePath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label logLabel;
    }
}