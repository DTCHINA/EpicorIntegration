namespace Epicor_Integration
{
    partial class Warehouse_Master
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Warehouse_Master));
            this.WhseDataGrid = new System.Windows.Forms.DataGridView();
            this.WarehouseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarehouseCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.done_btn = new System.Windows.Forms.Button();
            this.rem_btn = new System.Windows.Forms.Button();
            this.add_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.partnum_txt = new System.Windows.Forms.TextBox();
            this.plant_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.whse_cbo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.WhseDataGrid)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WhseDataGrid
            // 
            this.WhseDataGrid.AllowUserToAddRows = false;
            this.WhseDataGrid.AllowUserToDeleteRows = false;
            this.WhseDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.WhseDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.WhseDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.WhseDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WhseDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WarehouseName,
            this.WarehouseCode});
            this.WhseDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WhseDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.WhseDataGrid.Location = new System.Drawing.Point(0, 0);
            this.WhseDataGrid.MultiSelect = false;
            this.WhseDataGrid.Name = "WhseDataGrid";
            this.WhseDataGrid.ReadOnly = true;
            this.WhseDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.WhseDataGrid.RowHeadersVisible = false;
            this.WhseDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.WhseDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.WhseDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WhseDataGrid.Size = new System.Drawing.Size(277, 154);
            this.WhseDataGrid.TabIndex = 37;
            // 
            // WarehouseName
            // 
            this.WarehouseName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WarehouseName.DataPropertyName = "WarehouseName";
            this.WarehouseName.HeaderText = "Warehouse Name";
            this.WarehouseName.Name = "WarehouseName";
            this.WarehouseName.ReadOnly = true;
            // 
            // WarehouseCode
            // 
            this.WarehouseCode.DataPropertyName = "WarehouseCode";
            this.WarehouseCode.HeaderText = "WarehouseCode";
            this.WarehouseCode.Name = "WarehouseCode";
            this.WarehouseCode.ReadOnly = true;
            this.WarehouseCode.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.done_btn);
            this.splitContainer1.Panel1.Controls.Add(this.rem_btn);
            this.splitContainer1.Panel1.Controls.Add(this.add_btn);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.whse_cbo);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.WhseDataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(277, 329);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 38;
            // 
            // done_btn
            // 
            this.done_btn.Location = new System.Drawing.Point(182, 131);
            this.done_btn.Name = "done_btn";
            this.done_btn.Size = new System.Drawing.Size(76, 23);
            this.done_btn.TabIndex = 42;
            this.done_btn.Text = "&Done";
            this.done_btn.UseVisualStyleBackColor = true;
            this.done_btn.Click += new System.EventHandler(this.done_btn_Click);
            // 
            // rem_btn
            // 
            this.rem_btn.Enabled = false;
            this.rem_btn.Location = new System.Drawing.Point(100, 131);
            this.rem_btn.Name = "rem_btn";
            this.rem_btn.Size = new System.Drawing.Size(76, 23);
            this.rem_btn.TabIndex = 41;
            this.rem_btn.Text = "&Remove";
            this.rem_btn.UseVisualStyleBackColor = true;
            this.rem_btn.Click += new System.EventHandler(this.rem_btn_Click);
            // 
            // add_btn
            // 
            this.add_btn.Location = new System.Drawing.Point(18, 131);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(76, 23);
            this.add_btn.TabIndex = 40;
            this.add_btn.Text = "&Add";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.partnum_txt);
            this.groupBox1.Controls.Add(this.plant_txt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(246, 75);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Part Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Plant:";
            // 
            // partnum_txt
            // 
            this.partnum_txt.Location = new System.Drawing.Point(10, 32);
            this.partnum_txt.Name = "partnum_txt";
            this.partnum_txt.ReadOnly = true;
            this.partnum_txt.Size = new System.Drawing.Size(100, 20);
            this.partnum_txt.TabIndex = 29;
            // 
            // plant_txt
            // 
            this.plant_txt.Location = new System.Drawing.Point(116, 32);
            this.plant_txt.Name = "plant_txt";
            this.plant_txt.ReadOnly = true;
            this.plant_txt.Size = new System.Drawing.Size(116, 20);
            this.plant_txt.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Warehouse:";
            // 
            // whse_cbo
            // 
            this.whse_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.whse_cbo.FormattingEnabled = true;
            this.whse_cbo.Location = new System.Drawing.Point(18, 104);
            this.whse_cbo.Name = "whse_cbo";
            this.whse_cbo.Size = new System.Drawing.Size(240, 21);
            this.whse_cbo.TabIndex = 37;
            // 
            // Warehouse_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 329);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Warehouse_Master";
            this.Text = "Add Warehouse to Item";
            this.Load += new System.EventHandler(this.Warehouse_Master_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WhseDataGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView WhseDataGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button done_btn;
        private System.Windows.Forms.Button rem_btn;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox partnum_txt;
        private System.Windows.Forms.TextBox plant_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox whse_cbo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseCode;


    }
}