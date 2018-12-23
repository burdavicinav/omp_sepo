namespace omp_sepo.dialogs
{
    partial class FixtureAttachFilesDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.directoryBox = new ui_lib.DirectoryPathBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.detailsBox = new System.Windows.Forms.ComboBox();
            this.fixtureBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fixtureNodesBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Директория:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fixtureNodesBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fixtureBox);
            this.groupBox1.Controls.Add(this.detailsBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 151);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Группа файлов";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(238, 225);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(157, 225);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // directoryBox
            // 
            this.directoryBox.Filter = null;
            this.directoryBox.Location = new System.Drawing.Point(85, 15);
            this.directoryBox.Margin = new System.Windows.Forms.Padding(0);
            this.directoryBox.Name = "directoryBox";
            this.directoryBox.Size = new System.Drawing.Size(228, 20);
            this.directoryBox.TabIndex = 4;
            this.directoryBox.TextValue = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Детали:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Оснастка:";
            // 
            // detailsBox
            // 
            this.detailsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.detailsBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.detailsBox.FormattingEnabled = true;
            this.detailsBox.Location = new System.Drawing.Point(99, 32);
            this.detailsBox.Name = "detailsBox";
            this.detailsBox.Size = new System.Drawing.Size(188, 21);
            this.detailsBox.TabIndex = 2;
            // 
            // fixtureBox
            // 
            this.fixtureBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fixtureBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fixtureBox.FormattingEnabled = true;
            this.fixtureBox.Location = new System.Drawing.Point(99, 66);
            this.fixtureBox.Name = "fixtureBox";
            this.fixtureBox.Size = new System.Drawing.Size(188, 21);
            this.fixtureBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Узлы оснастки:";
            // 
            // fixtureNodesBox
            // 
            this.fixtureNodesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fixtureNodesBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fixtureNodesBox.FormattingEnabled = true;
            this.fixtureNodesBox.Location = new System.Drawing.Point(100, 103);
            this.fixtureNodesBox.Name = "fixtureNodesBox";
            this.fixtureNodesBox.Size = new System.Drawing.Size(187, 21);
            this.fixtureNodesBox.TabIndex = 5;
            // 
            // FixtureAttachFilesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(327, 260);
            this.Controls.Add(this.directoryBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixtureAttachFilesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Присоединенные файлы";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private ui_lib.DirectoryPathBox directoryBox;
        private System.Windows.Forms.ComboBox fixtureBox;
        private System.Windows.Forms.ComboBox detailsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fixtureNodesBox;
        private System.Windows.Forms.Label label4;
    }
}