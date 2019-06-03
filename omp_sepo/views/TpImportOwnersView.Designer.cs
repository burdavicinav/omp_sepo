namespace omp_sepo.views
{
    partial class TpImportOwnersView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ownersView = new System.Windows.Forms.DataGridView();
            this.wsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ownersView)).BeginInit();
            this.SuspendLayout();
            // 
            // ownersView
            // 
            this.ownersView.AllowUserToAddRows = false;
            this.ownersView.AllowUserToDeleteRows = false;
            this.ownersView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ownersView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ownersView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wsColumn,
            this.ownerColumn});
            this.ownersView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ownersView.Location = new System.Drawing.Point(0, 0);
            this.ownersView.Name = "ownersView";
            this.ownersView.Size = new System.Drawing.Size(397, 218);
            this.ownersView.TabIndex = 0;
            // 
            // wsColumn
            // 
            this.wsColumn.HeaderText = "Цех";
            this.wsColumn.Name = "wsColumn";
            this.wsColumn.ReadOnly = true;
            // 
            // ownerColumn
            // 
            this.ownerColumn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ownerColumn.HeaderText = "Владелец";
            this.ownerColumn.Name = "ownerColumn";
            // 
            // TpImportOwnersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ownersView);
            this.Name = "TpImportOwnersView";
            this.Size = new System.Drawing.Size(397, 218);
            ((System.ComponentModel.ISupportInitialize)(this.ownersView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ownersView;
        private System.Windows.Forms.DataGridViewTextBoxColumn wsColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ownerColumn;
    }
}
