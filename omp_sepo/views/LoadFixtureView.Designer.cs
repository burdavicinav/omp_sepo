namespace omp_sepo.views
{
    partial class LoadFixtureView
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
            this.scene = new System.Windows.Forms.DataGridView();
            this.artColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.revisionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileLoadColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scene)).BeginInit();
            this.SuspendLayout();
            // 
            // scene
            // 
            this.scene.AllowUserToAddRows = false;
            this.scene.AllowUserToDeleteRows = false;
            this.scene.BackgroundColor = System.Drawing.SystemColors.Control;
            this.scene.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scene.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.artColumn,
            this.docColumn,
            this.signColumn,
            this.revisionColumn,
            this.stateColumn,
            this.fileNameColumn,
            this.fileLoadColumn});
            this.scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.Name = "scene";
            this.scene.Size = new System.Drawing.Size(765, 268);
            this.scene.TabIndex = 0;
            // 
            // artColumn
            // 
            this.artColumn.HeaderText = "ART_ID";
            this.artColumn.Name = "artColumn";
            this.artColumn.ReadOnly = true;
            // 
            // docColumn
            // 
            this.docColumn.HeaderText = "DOC_ID";
            this.docColumn.Name = "docColumn";
            this.docColumn.ReadOnly = true;
            // 
            // signColumn
            // 
            this.signColumn.HeaderText = "Обозначение БО";
            this.signColumn.Name = "signColumn";
            this.signColumn.ReadOnly = true;
            // 
            // revisionColumn
            // 
            this.revisionColumn.HeaderText = "№ ревизии";
            this.revisionColumn.Name = "revisionColumn";
            this.revisionColumn.ReadOnly = true;
            // 
            // stateColumn
            // 
            this.stateColumn.HeaderText = "Статус";
            this.stateColumn.Name = "stateColumn";
            this.stateColumn.ReadOnly = true;
            // 
            // fileNameColumn
            // 
            this.fileNameColumn.HeaderText = "Файл";
            this.fileNameColumn.Name = "fileNameColumn";
            this.fileNameColumn.ReadOnly = true;
            // 
            // fileLoadColumn
            // 
            this.fileLoadColumn.HeaderText = "Импорт файла";
            this.fileLoadColumn.Name = "fileLoadColumn";
            this.fileLoadColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fileLoadColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // LoadFixtureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scene);
            this.Name = "LoadFixtureView";
            this.Size = new System.Drawing.Size(765, 268);
            ((System.ComponentModel.ISupportInitialize)(this.scene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn artColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn docColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn revisionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fileLoadColumn;
    }
}
