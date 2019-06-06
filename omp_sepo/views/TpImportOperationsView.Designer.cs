namespace omp_sepo.views
{
    partial class TpImportOperationsView
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
            this.operationsView = new System.Windows.Forms.DataGridView();
            this.operCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ompCodeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.operationsView)).BeginInit();
            this.SuspendLayout();
            // 
            // operationsView
            // 
            this.operationsView.AllowUserToAddRows = false;
            this.operationsView.AllowUserToDeleteRows = false;
            this.operationsView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.operationsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operationsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operCodeColumn,
            this.operNameColumn,
            this.ompCodeColumn});
            this.operationsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operationsView.Location = new System.Drawing.Point(0, 0);
            this.operationsView.MultiSelect = false;
            this.operationsView.Name = "operationsView";
            this.operationsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.operationsView.Size = new System.Drawing.Size(758, 261);
            this.operationsView.TabIndex = 0;
            // 
            // operCodeColumn
            // 
            this.operCodeColumn.HeaderText = "Код операции";
            this.operCodeColumn.Name = "operCodeColumn";
            this.operCodeColumn.ReadOnly = true;
            // 
            // operNameColumn
            // 
            this.operNameColumn.HeaderText = "Наименование операции";
            this.operNameColumn.Name = "operNameColumn";
            this.operNameColumn.ReadOnly = true;
            this.operNameColumn.Width = 200;
            // 
            // ompCodeColumn
            // 
            this.ompCodeColumn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ompCodeColumn.HeaderText = "КИС \"Омега\": Операция";
            this.ompCodeColumn.Name = "ompCodeColumn";
            this.ompCodeColumn.Width = 300;
            // 
            // TpImportOperationsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.operationsView);
            this.Name = "TpImportOperationsView";
            this.Size = new System.Drawing.Size(758, 261);
            ((System.ComponentModel.ISupportInitialize)(this.operationsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView operationsView;
        private System.Windows.Forms.DataGridViewTextBoxColumn operCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ompCodeColumn;
    }
}
