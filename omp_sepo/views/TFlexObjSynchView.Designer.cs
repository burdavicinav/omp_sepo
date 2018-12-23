namespace omp_sepo.views
{
    partial class TFlexObjSynchView
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
            this.specColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signDocColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boStateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupFileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ompSectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.specColumn,
            this.signDocColumn,
            this.boTypeColumn,
            this.boStateColumn,
            this.groupFileColumn,
            this.ompSectionColumn});
            this.scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.MultiSelect = false;
            this.scene.Name = "scene";
            this.scene.ReadOnly = true;
            this.scene.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.scene.Size = new System.Drawing.Size(671, 354);
            this.scene.TabIndex = 0;
            // 
            // specColumn
            // 
            this.specColumn.HeaderText = "TFlex Спецификация";
            this.specColumn.Name = "specColumn";
            // 
            // signDocColumn
            // 
            this.signDocColumn.HeaderText = "TFlex Обозначение документа";
            this.signDocColumn.Name = "signDocColumn";
            // 
            // boTypeColumn
            // 
            this.boTypeColumn.HeaderText = "Тип БО";
            this.boTypeColumn.Name = "boTypeColumn";
            // 
            // boStateColumn
            // 
            this.boStateColumn.HeaderText = "Статус БО";
            this.boStateColumn.Name = "boStateColumn";
            // 
            // groupFileColumn
            // 
            this.groupFileColumn.HeaderText = "Группа файлов";
            this.groupFileColumn.Name = "groupFileColumn";
            // 
            // ompSectionColumn
            // 
            this.ompSectionColumn.HeaderText = "Раздел спецификации";
            this.ompSectionColumn.Name = "ompSectionColumn";
            // 
            // TFlexObjSynchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scene);
            this.Name = "TFlexObjSynchView";
            this.Size = new System.Drawing.Size(671, 354);
            ((System.ComponentModel.ISupportInitialize)(this.scene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn specColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signDocColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn boTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn boStateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupFileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ompSectionColumn;
    }
}
