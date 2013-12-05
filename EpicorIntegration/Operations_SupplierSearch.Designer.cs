namespace Epicor_Integration
{
    partial class Operations_SupplierSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operations_SupplierSearch));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.select_btn = new System.Windows.Forms.Button();
            this.clear_btn = new System.Windows.Forms.Button();
            this.search_btn = new System.Windows.Forms.Button();
            this.idnum_txt = new System.Windows.Forms.TextBox();
            this.name_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SupplyGrid = new System.Windows.Forms.DataGridView();
            this.VName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorNumVendorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SupplyGrid)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.cancel_btn);
            this.splitContainer1.Panel1.Controls.Add(this.select_btn);
            this.splitContainer1.Panel1.Controls.Add(this.clear_btn);
            this.splitContainer1.Panel1.Controls.Add(this.search_btn);
            this.splitContainer1.Panel1.Controls.Add(this.idnum_txt);
            this.splitContainer1.Panel1.Controls.Add(this.name_txt);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SupplyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(506, 390);
            this.splitContainer1.SplitterDistance = 81;
            this.splitContainer1.TabIndex = 0;
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(419, 37);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 7;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // select_btn
            // 
            this.select_btn.Location = new System.Drawing.Point(419, 10);
            this.select_btn.Name = "select_btn";
            this.select_btn.Size = new System.Drawing.Size(75, 23);
            this.select_btn.TabIndex = 6;
            this.select_btn.Text = "Select";
            this.select_btn.UseVisualStyleBackColor = true;
            this.select_btn.Click += new System.EventHandler(this.select_btn_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.clear_btn.Location = new System.Drawing.Point(229, 36);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(75, 23);
            this.clear_btn.TabIndex = 5;
            this.clear_btn.Text = "Clear";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(229, 9);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(75, 23);
            this.search_btn.TabIndex = 4;
            this.search_btn.Text = "Search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // idnum_txt
            // 
            this.idnum_txt.Location = new System.Drawing.Point(79, 38);
            this.idnum_txt.Name = "idnum_txt";
            this.idnum_txt.Size = new System.Drawing.Size(144, 20);
            this.idnum_txt.TabIndex = 3;
            this.idnum_txt.TextChanged += new System.EventHandler(this.idnum_txt_TextChanged);
            // 
            // name_txt
            // 
            this.name_txt.Location = new System.Drawing.Point(79, 12);
            this.name_txt.Name = "name_txt";
            this.name_txt.Size = new System.Drawing.Size(144, 20);
            this.name_txt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // SupplyGrid
            // 
            this.SupplyGrid.AllowUserToAddRows = false;
            this.SupplyGrid.AllowUserToDeleteRows = false;
            this.SupplyGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SupplyGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SupplyGrid.AutoGenerateColumns = global::Epicor_Integration.Properties.Settings.Default.AutoGenCol;
            this.SupplyGrid.BackgroundColor = System.Drawing.Color.White;
            this.SupplyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SupplyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VName,
            this.VendorNumVendorID,
            this.Address1,
            this.Address2,
            this.Address3,
            this.City,
            this.ZIP,
            this.State,
            this.Country,
            this.VendorNum,
            this.PhoneNum});
            this.SupplyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplyGrid.Location = new System.Drawing.Point(0, 0);
            this.SupplyGrid.Name = "SupplyGrid";
            this.SupplyGrid.ReadOnly = true;
            this.SupplyGrid.RowHeadersVisible = false;
            this.SupplyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SupplyGrid.ShowCellErrors = false;
            this.SupplyGrid.ShowCellToolTips = false;
            this.SupplyGrid.ShowEditingIcon = false;
            this.SupplyGrid.ShowRowErrors = false;
            this.SupplyGrid.Size = new System.Drawing.Size(506, 305);
            this.SupplyGrid.TabIndex = 0;
            // 
            // VName
            // 
            this.VName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VName.DataPropertyName = "Name";
            this.VName.HeaderText = "Name";
            this.VName.Name = "VName";
            this.VName.ReadOnly = true;
            this.VName.Width = 60;
            // 
            // VendorNumVendorID
            // 
            this.VendorNumVendorID.DataPropertyName = "VendorNumVendorID";
            this.VendorNumVendorID.HeaderText = "Vendor ID";
            this.VendorNumVendorID.Name = "VendorNumVendorID";
            this.VendorNumVendorID.ReadOnly = true;
            // 
            // Address1
            // 
            this.Address1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Address1.DataPropertyName = "Address1";
            this.Address1.HeaderText = "Address";
            this.Address1.Name = "Address1";
            this.Address1.ReadOnly = true;
            this.Address1.Width = 70;
            // 
            // Address2
            // 
            this.Address2.DataPropertyName = "Address2";
            this.Address2.HeaderText = "Address2";
            this.Address2.Name = "Address2";
            this.Address2.ReadOnly = true;
            this.Address2.Visible = false;
            // 
            // Address3
            // 
            this.Address3.DataPropertyName = "Address3";
            this.Address3.HeaderText = "Address3";
            this.Address3.Name = "Address3";
            this.Address3.ReadOnly = true;
            this.Address3.Visible = false;
            // 
            // City
            // 
            this.City.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.City.DataPropertyName = "City";
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            this.City.Width = 49;
            // 
            // ZIP
            // 
            this.ZIP.DataPropertyName = "ZIP";
            this.ZIP.HeaderText = "Zip";
            this.ZIP.Name = "ZIP";
            this.ZIP.ReadOnly = true;
            // 
            // State
            // 
            this.State.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 57;
            // 
            // Country
            // 
            this.Country.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Country.DataPropertyName = "Country";
            this.Country.HeaderText = "Country";
            this.Country.Name = "Country";
            this.Country.ReadOnly = true;
            this.Country.Width = 68;
            // 
            // VendorNum
            // 
            this.VendorNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VendorNum.DataPropertyName = "VendorNum";
            this.VendorNum.HeaderText = "ID Number";
            this.VendorNum.Name = "VendorNum";
            this.VendorNum.ReadOnly = true;
            this.VendorNum.Width = 77;
            // 
            // PhoneNum
            // 
            this.PhoneNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PhoneNum.DataPropertyName = "PhoneNum";
            this.PhoneNum.HeaderText = "Phone Number";
            this.PhoneNum.Name = "PhoneNum";
            this.PhoneNum.ReadOnly = true;
            // 
            // Operations_SupplierSearch
            // 
            this.AcceptButton = this.search_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.clear_btn;
            this.ClientSize = new System.Drawing.Size(506, 390);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Operations_SupplierSearch";
            this.Text = "Supplier Purchase Point Search";
            this.Load += new System.EventHandler(this.Operations_SupplierSearch_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SupplyGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button select_btn;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.TextBox idnum_txt;
        private System.Windows.Forms.TextBox name_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView SupplyGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn VName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorNumVendorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address3;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Country;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNum;
    }
}