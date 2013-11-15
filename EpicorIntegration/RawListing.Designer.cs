namespace EpicorIntegration
{
    partial class RawListing
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawListing));
            this.MajorHoriz = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RawListGrid = new System.Windows.Forms.DataGridView();
            this.eNGDataDataSet = new EpicorIntegration.ENGDataDataSet();
            this.rawlistingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            //this.rawlistingTableAdapter = new EpicorIntegration.ENGDataDataSetTableAdapters.rawlistingTableAdapter();
            this.matnumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MajorHoriz.Panel1.SuspendLayout();
            this.MajorHoriz.Panel2.SuspendLayout();
            this.MajorHoriz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RawListGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rawlistingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MajorHoriz
            // 
            this.MajorHoriz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MajorHoriz.Location = new System.Drawing.Point(0, 0);
            this.MajorHoriz.Name = "MajorHoriz";
            this.MajorHoriz.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MajorHoriz.Panel1
            // 
            this.MajorHoriz.Panel1.Controls.Add(this.groupBox1);
            // 
            // MajorHoriz.Panel2
            // 
            this.MajorHoriz.Panel2.Controls.Add(this.RawListGrid);
            this.MajorHoriz.Size = new System.Drawing.Size(496, 344);
            this.MajorHoriz.SplitterDistance = 154;
            this.MajorHoriz.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // RawListGrid
            // 
            this.RawListGrid.AllowUserToAddRows = false;
            this.RawListGrid.AllowUserToDeleteRows = false;
            this.RawListGrid.AutoGenerateColumns = false;
            this.RawListGrid.BackgroundColor = System.Drawing.Color.White;
            this.RawListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RawListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.matnumDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn});
            this.RawListGrid.DataSource = this.rawlistingBindingSource;
            this.RawListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RawListGrid.GridColor = System.Drawing.Color.Black;
            this.RawListGrid.Location = new System.Drawing.Point(0, 0);
            this.RawListGrid.Name = "RawListGrid";
            this.RawListGrid.ReadOnly = true;
            this.RawListGrid.RowHeadersVisible = false;
            this.RawListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RawListGrid.Size = new System.Drawing.Size(496, 186);
            this.RawListGrid.TabIndex = 0;
            // 
            // eNGDataDataSet
            // 
            this.eNGDataDataSet.DataSetName = "ENGDataDataSet";
            this.eNGDataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rawlistingBindingSource
            // 
            this.rawlistingBindingSource.DataMember = "rawlisting";
            this.rawlistingBindingSource.DataSource = this.eNGDataDataSet;
            // 
            // rawlistingTableAdapter
            // 
            //this.rawlistingTableAdapter.ClearBeforeFill = true;
            // 
            // matnumDataGridViewTextBoxColumn
            // 
            this.matnumDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.matnumDataGridViewTextBoxColumn.DataPropertyName = "matnum";
            this.matnumDataGridViewTextBoxColumn.HeaderText = "Part Number";
            this.matnumDataGridViewTextBoxColumn.Name = "matnumDataGridViewTextBoxColumn";
            this.matnumDataGridViewTextBoxColumn.ReadOnly = true;
            this.matnumDataGridViewTextBoxColumn.Width = 91;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Material Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // RawListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 344);
            this.Controls.Add(this.MajorHoriz);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RawListing";
            this.Text = "Raw Material Listing";
            this.Load += new System.EventHandler(this.RawListing_Load);
            this.MajorHoriz.Panel1.ResumeLayout(false);
            this.MajorHoriz.Panel2.ResumeLayout(false);
            this.MajorHoriz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RawListGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rawlistingBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MajorHoriz;
        private System.Windows.Forms.GroupBox groupBox1;
        private ENGDataDataSet eNGDataDataSet;
        private System.Windows.Forms.BindingSource rawlistingBindingSource;
        private System.Windows.Forms.DataGridView RawListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn matnumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;

    }
}