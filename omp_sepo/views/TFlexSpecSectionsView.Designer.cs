namespace omp_sepo.views
{
    partial class TFlexSpecSectionsView
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
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.typesScene = new System.Windows.Forms.DataGridView();
            this.typeSignColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scene)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typesScene)).BeginInit();
            this.SuspendLayout();
            // 
            // scene
            // 
            this.scene.AllowUserToAddRows = false;
            this.scene.AllowUserToDeleteRows = false;
            this.scene.BackgroundColor = System.Drawing.SystemColors.Control;
            this.scene.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scene.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn});
            this.scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.MultiSelect = false;
            this.scene.Name = "scene";
            this.scene.ReadOnly = true;
            this.scene.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.scene.Size = new System.Drawing.Size(296, 190);
            this.scene.TabIndex = 0;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Наименование";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scene);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.typesScene);
            this.splitContainer1.Size = new System.Drawing.Size(296, 380);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 1;
            // 
            // typesScene
            // 
            this.typesScene.AllowUserToAddRows = false;
            this.typesScene.AllowUserToDeleteRows = false;
            this.typesScene.BackgroundColor = System.Drawing.SystemColors.Control;
            this.typesScene.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.typesScene.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeSignColumn,
            this.typeNameColumn});
            this.typesScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typesScene.Location = new System.Drawing.Point(0, 0);
            this.typesScene.MultiSelect = false;
            this.typesScene.Name = "typesScene";
            this.typesScene.ReadOnly = true;
            this.typesScene.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.typesScene.Size = new System.Drawing.Size(296, 186);
            this.typesScene.TabIndex = 1;
            // 
            // typeSignColumn
            // 
            this.typeSignColumn.HeaderText = "Обозначение";
            this.typeSignColumn.Name = "typeSignColumn";
            this.typeSignColumn.ReadOnly = true;
            // 
            // typeNameColumn
            // 
            this.typeNameColumn.HeaderText = "Наименование";
            this.typeNameColumn.Name = "typeNameColumn";
            this.typeNameColumn.ReadOnly = true;
            // 
            // TFlexSpecSectionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TFlexSpecSectionsView";
            this.Size = new System.Drawing.Size(296, 380);
            ((System.ComponentModel.ISupportInitialize)(this.scene)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.typesScene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView typesScene;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeSignColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameColumn;
    }
}
