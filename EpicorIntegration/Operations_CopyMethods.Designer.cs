namespace Epicor_Integration
{
    partial class Operations_CopyMethods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operations_CopyMethods));
            this.RevGrid = new System.Windows.Forms.DataGridView();
            this.part_txt = new System.Windows.Forms.Button();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.pnum_txt = new System.Windows.Forms.TextBox();
            this.desc_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RevisionNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Approved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ApprovedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApprovedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ECO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Effective = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RevShortDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RevGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RevGrid
            // 
            this.RevGrid.AllowUserToAddRows = false;
            this.RevGrid.AllowUserToDeleteRows = false;
            this.RevGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RevGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RevGrid.AutoGenerateColumns = global::Epicor_Integration.Properties.Settings.Default.AutoGenCol;
            this.RevGrid.BackgroundColor = System.Drawing.Color.White;
            this.RevGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RevGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RevisionNum,
            this.Description,
            this.Approved,
            this.ApprovedBy,
            this.ApprovedDate,
            this.ECO,
            this.Effective,
            this.RevShortDesc});
            this.RevGrid.Location = new System.Drawing.Point(12, 95);
            this.RevGrid.Name = "RevGrid";
            this.RevGrid.ReadOnly = true;
            this.RevGrid.RowHeadersVisible = false;
            this.RevGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RevGrid.Size = new System.Drawing.Size(373, 108);
            this.RevGrid.TabIndex = 0;
            // 
            // part_txt
            // 
            this.part_txt.Location = new System.Drawing.Point(6, 19);
            this.part_txt.Name = "part_txt";
            this.part_txt.Size = new System.Drawing.Size(63, 23);
            this.part_txt.TabIndex = 1;
            this.part_txt.Text = "Part";
            this.part_txt.UseVisualStyleBackColor = true;
            this.part_txt.Click += new System.EventHandler(this.part_txt_Click);
            // 
            // ok_btn
            // 
            this.ok_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_btn.Location = new System.Drawing.Point(310, 23);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "Ok";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(310, 57);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 3;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // pnum_txt
            // 
            this.pnum_txt.Location = new System.Drawing.Point(75, 21);
            this.pnum_txt.Name = "pnum_txt";
            this.pnum_txt.Size = new System.Drawing.Size(211, 20);
            this.pnum_txt.TabIndex = 4;
            // 
            // desc_txt
            // 
            this.desc_txt.Location = new System.Drawing.Point(75, 47);
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.ReadOnly = true;
            this.desc_txt.Size = new System.Drawing.Size(211, 20);
            this.desc_txt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Description:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.part_txt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pnum_txt);
            this.groupBox1.Controls.Add(this.desc_txt);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(292, 85);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // RevisionNum
            // 
            this.RevisionNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RevisionNum.DataPropertyName = "RevisionNum";
            this.RevisionNum.HeaderText = "Rev";
            this.RevisionNum.MinimumWidth = 50;
            this.RevisionNum.Name = "RevisionNum";
            this.RevisionNum.ReadOnly = true;
            this.RevisionNum.Width = 52;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Description.DataPropertyName = "RevDescription";
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 85;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 85;
            // 
            // Approved
            // 
            this.Approved.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Approved.DataPropertyName = "Approved";
            this.Approved.HeaderText = "Approved";
            this.Approved.MinimumWidth = 75;
            this.Approved.Name = "Approved";
            this.Approved.ReadOnly = true;
            this.Approved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Approved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Approved.Width = 78;
            // 
            // ApprovedBy
            // 
            this.ApprovedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ApprovedBy.DataPropertyName = "ApprovedBy";
            this.ApprovedBy.HeaderText = "Approved By";
            this.ApprovedBy.MinimumWidth = 100;
            this.ApprovedBy.Name = "ApprovedBy";
            this.ApprovedBy.ReadOnly = true;
            // 
            // ApprovedDate
            // 
            this.ApprovedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ApprovedDate.DataPropertyName = "ApprovedDate";
            this.ApprovedDate.HeaderText = "Approved Date";
            this.ApprovedDate.MinimumWidth = 105;
            this.ApprovedDate.Name = "ApprovedDate";
            this.ApprovedDate.ReadOnly = true;
            this.ApprovedDate.Width = 105;
            // 
            // ECO
            // 
            this.ECO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ECO.DataPropertyName = "ECO";
            this.ECO.HeaderText = "ECO";
            this.ECO.MinimumWidth = 54;
            this.ECO.Name = "ECO";
            this.ECO.ReadOnly = true;
            this.ECO.Width = 54;
            // 
            // Effective
            // 
            this.Effective.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Effective.DataPropertyName = "EffectiveDate";
            this.Effective.HeaderText = "Effective";
            this.Effective.MinimumWidth = 74;
            this.Effective.Name = "Effective";
            this.Effective.ReadOnly = true;
            this.Effective.Width = 74;
            // 
            // RevShortDesc
            // 
            this.RevShortDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RevShortDesc.DataPropertyName = "RevShortDesc";
            this.RevShortDesc.HeaderText = "Short Desc";
            this.RevShortDesc.MinimumWidth = 85;
            this.RevShortDesc.Name = "RevShortDesc";
            this.RevShortDesc.ReadOnly = true;
            this.RevShortDesc.Width = 85;
            // 
            // Operations_CopyMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 216);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.RevGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Operations_CopyMethods";
            this.Text = "Copy Methods From...";
            this.Load += new System.EventHandler(this.Operations_CopyMethods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RevGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView RevGrid;
        private System.Windows.Forms.Button part_txt;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.TextBox pnum_txt;
        private System.Windows.Forms.TextBox desc_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RevisionNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Approved;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApprovedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApprovedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Effective;
        private System.Windows.Forms.DataGridViewTextBoxColumn RevShortDesc;

    }
}