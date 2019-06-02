namespace omp_sepo.views
{
    partial class FixtureAttachFileObjects
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
            this.idTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileGroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.idTypeColumn,
            this.typeNameColumn,
            this.fileGroupColumn,
            this.ownerColumn});
            this.scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.MultiSelect = false;
            this.scene.Name = "scene";
            this.scene.ReadOnly = true;
            this.scene.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.scene.Size = new System.Drawing.Size(474, 257);
            this.scene.TabIndex = 0;
            // 
            // idTypeColumn
            // 
            this.idTypeColumn.HeaderText = "Тип";
            this.idTypeColumn.Name = "idTypeColumn";
            this.idTypeColumn.ReadOnly = true;
            // 
            // typeNameColumn
            // 
            this.typeNameColumn.HeaderText = "Наименование типа";
            this.typeNameColumn.Name = "typeNameColumn";
            this.typeNameColumn.ReadOnly = true;
            // 
            // fileGroupColumn
            // 
            this.fileGroupColumn.HeaderText = "Группа файлов";
            this.fileGroupColumn.Name = "fileGroupColumn";
            this.fileGroupColumn.ReadOnly = true;
            // 
            // ownerColumn
            // 
            this.ownerColumn.HeaderText = "Владелец";
            this.ownerColumn.Name = "ownerColumn";
            this.ownerColumn.ReadOnly = true;
            // 
            // FixtureAttachFileObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scene);
            this.Name = "FixtureAttachFileObjects";
            this.Size = new System.Drawing.Size(474, 257);
            ((System.ComponentModel.ISupportInitialize)(this.scene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileGroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerColumn;
    }
}
