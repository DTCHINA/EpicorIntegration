namespace TopLevelReport
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.report_btn = new System.Windows.Forms.Button();
            this.searchterm_txt = new System.Windows.Forms.TextBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.loading_img = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopLevel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RevisionNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MtlPartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumPartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.report_btn);
            this.splitContainer1.Panel1.Controls.Add(this.searchterm_txt);
            this.splitContainer1.Panel1.Controls.Add(this.search_btn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.loading_img);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(481, 554);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Part Number:";
            // 
            // report_btn
            // 
            this.report_btn.Enabled = false;
            this.report_btn.Location = new System.Drawing.Point(227, 7);
            this.report_btn.Name = "report_btn";
            this.report_btn.Size = new System.Drawing.Size(83, 23);
            this.report_btn.TabIndex = 5;
            this.report_btn.Text = "Show Report";
            this.report_btn.UseVisualStyleBackColor = true;
            this.report_btn.Click += new System.EventHandler(this.report_btn_Click);
            // 
            // searchterm_txt
            // 
            this.searchterm_txt.Location = new System.Drawing.Point(81, 9);
            this.searchterm_txt.Name = "searchterm_txt";
            this.searchterm_txt.Size = new System.Drawing.Size(100, 20);
            this.searchterm_txt.TabIndex = 4;
            // 
            // search_btn
            // 
            this.search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_btn.ForeColor = System.Drawing.SystemColors.Control;
            this.search_btn.Image = global::TopLevelReport.Properties.Resources.Search_24;
            this.search_btn.Location = new System.Drawing.Point(187, 3);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(34, 30);
            this.search_btn.TabIndex = 3;
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // loading_img
            // 
            this.loading_img.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loading_img.Image = global::TopLevelReport.Properties.Resources.ajax_loader;
            this.loading_img.Location = new System.Drawing.Point(129, 148);
            this.loading_img.Name = "loading_img";
            this.loading_img.Size = new System.Drawing.Size(200, 200);
            this.loading_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loading_img.TabIndex = 1;
            this.loading_img.TabStop = false;
            this.loading_img.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = global::TopLevelReport.Properties.Settings.Default.False;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNum,
            this.TopLevel,
            this.RevisionNum,
            this.MtlPartNum,
            this.PartNumPartDescription});
            this.dataGridView1.DataBindings.Add(new System.Windows.Forms.Binding("AutoGenerateColumns", global::TopLevelReport.Properties.Settings.Default, "False", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(481, 516);
            this.dataGridView1.TabIndex = 0;
            // 
            // PartNum
            // 
            this.PartNum.DataPropertyName = "PartMtl.PartNum";
            this.PartNum.HeaderText = "Part Number";
            this.PartNum.Name = "PartNum";
            this.PartNum.ReadOnly = true;
            // 
            // TopLevel
            // 
            this.TopLevel.DataPropertyName = "TopLevel";
            this.TopLevel.HeaderText = "Top Level";
            this.TopLevel.Name = "TopLevel";
            this.TopLevel.ReadOnly = true;
            this.TopLevel.TrueValue = "1";
            // 
            // RevisionNum
            // 
            this.RevisionNum.DataPropertyName = "PartMtl.RevisionNum";
            this.RevisionNum.HeaderText = "Revision";
            this.RevisionNum.Name = "RevisionNum";
            this.RevisionNum.ReadOnly = true;
            // 
            // MtlPartNum
            // 
            this.MtlPartNum.DataPropertyName = "PartMtl.MtlPartNum";
            this.MtlPartNum.HeaderText = "Sub-Part";
            this.MtlPartNum.Name = "MtlPartNum";
            this.MtlPartNum.ReadOnly = true;
            // 
            // PartNumPartDescription
            // 
            this.PartNumPartDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNumPartDescription.DataPropertyName = "Part.PartDescription";
            this.PartNumPartDescription.HeaderText = "Top Level Description";
            this.PartNumPartDescription.Name = "PartNumPartDescription";
            this.PartNumPartDescription.ReadOnly = true;
            // 
            // main
            // 
            this.AcceptButton = this.search_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 554);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.Text = "Top Level Report";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button report_btn;
        private System.Windows.Forms.TextBox searchterm_txt;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNum;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TopLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn RevisionNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn MtlPartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumPartDescription;
        private System.Windows.Forms.PictureBox loading_img;

    }
}

