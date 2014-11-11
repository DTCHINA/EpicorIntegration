namespace Epicor_Integration
{
    partial class RevCompare
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevCompare));
            this.pnum_txt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rev2 = new System.Windows.Forms.ComboBox();
            this.rev1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.desc_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Billgrid = new System.Windows.Forms.DataGridView();
            this.Pnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refresh_btn = new System.Windows.Forms.Button();
            this.toClipboard_btn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.BillPage = new System.Windows.Forms.TabPage();
            this.OpsPage = new System.Windows.Forms.TabPage();
            this.Opsgrid = new System.Windows.Forms.DataGridView();
            this.Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdHrs1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdHrs2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Billgrid)).BeginInit();
            this.tabControl.SuspendLayout();
            this.BillPage.SuspendLayout();
            this.OpsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Opsgrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.RightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnum_txt
            // 
            this.pnum_txt.Location = new System.Drawing.Point(9, 32);
            this.pnum_txt.Name = "pnum_txt";
            this.pnum_txt.Size = new System.Drawing.Size(127, 20);
            this.pnum_txt.TabIndex = 0;
            this.pnum_txt.Leave += new System.EventHandler(this.pnum_txt_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.desc_txt);
            this.groupBox1.Controls.Add(this.pnum_txt);
            this.groupBox1.Controls.Add(this.rev2);
            this.groupBox1.Controls.Add(this.rev1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 103);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Information";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Description";
            // 
            // rev2
            // 
            this.rev2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rev2.FormattingEnabled = true;
            this.rev2.Location = new System.Drawing.Point(228, 31);
            this.rev2.Name = "rev2";
            this.rev2.Size = new System.Drawing.Size(80, 21);
            this.rev2.TabIndex = 2;
            // 
            // rev1
            // 
            this.rev1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rev1.FormattingEnabled = true;
            this.rev1.Location = new System.Drawing.Point(142, 31);
            this.rev1.Name = "rev1";
            this.rev1.Size = new System.Drawing.Size(80, 21);
            this.rev1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Revision 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Revision 1";
            // 
            // desc_txt
            // 
            this.desc_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.desc_txt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.desc_txt.Location = new System.Drawing.Point(9, 71);
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.ReadOnly = true;
            this.desc_txt.Size = new System.Drawing.Size(299, 20);
            this.desc_txt.TabIndex = 100;
            this.desc_txt.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Part Number";
            // 
            // Billgrid
            // 
            this.Billgrid.AllowUserToAddRows = false;
            this.Billgrid.AllowUserToDeleteRows = false;
            this.Billgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Billgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pnum,
            this.Qty1,
            this.Qty2,
            this.Desc});
            this.Billgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Billgrid.Location = new System.Drawing.Point(3, 3);
            this.Billgrid.Name = "Billgrid";
            this.Billgrid.ReadOnly = true;
            this.Billgrid.RowHeadersVisible = false;
            this.Billgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Billgrid.ShowCellErrors = false;
            this.Billgrid.ShowCellToolTips = false;
            this.Billgrid.ShowEditingIcon = false;
            this.Billgrid.ShowRowErrors = false;
            this.Billgrid.Size = new System.Drawing.Size(531, 292);
            this.Billgrid.TabIndex = 2;
            this.Billgrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DisplayGrid_ColumnHeaderMouseClick);
            // 
            // Pnum
            // 
            this.Pnum.DataPropertyName = "Pnum";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Pnum.DefaultCellStyle = dataGridViewCellStyle1;
            this.Pnum.HeaderText = "Part Number";
            this.Pnum.Name = "Pnum";
            this.Pnum.ReadOnly = true;
            // 
            // Qty1
            // 
            this.Qty1.DataPropertyName = "Qty1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Qty1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty1.HeaderText = "Qty (Rev 1)";
            this.Qty1.Name = "Qty1";
            this.Qty1.ReadOnly = true;
            // 
            // Qty2
            // 
            this.Qty2.DataPropertyName = "Qty2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Qty2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Qty2.HeaderText = "Qty (Rev 2)";
            this.Qty2.Name = "Qty2";
            this.Qty2.ReadOnly = true;
            // 
            // Desc
            // 
            this.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Desc.DataPropertyName = "Desc";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Desc.DefaultCellStyle = dataGridViewCellStyle4;
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            // 
            // refresh_btn
            // 
            this.refresh_btn.Enabled = false;
            this.refresh_btn.Location = new System.Drawing.Point(338, 18);
            this.refresh_btn.Name = "refresh_btn";
            this.refresh_btn.Size = new System.Drawing.Size(75, 23);
            this.refresh_btn.TabIndex = 4;
            this.refresh_btn.Text = "Refresh";
            this.refresh_btn.UseVisualStyleBackColor = true;
            this.refresh_btn.Click += new System.EventHandler(this.refresh_btn_Click);
            // 
            // toClipboard_btn
            // 
            this.toClipboard_btn.Enabled = false;
            this.toClipboard_btn.Location = new System.Drawing.Point(338, 47);
            this.toClipboard_btn.Name = "toClipboard_btn";
            this.toClipboard_btn.Size = new System.Drawing.Size(75, 23);
            this.toClipboard_btn.TabIndex = 5;
            this.toClipboard_btn.Text = "to Clipboard";
            this.toClipboard_btn.UseVisualStyleBackColor = true;
            this.toClipboard_btn.Click += new System.EventHandler(this.toExcel_btn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.BillPage);
            this.tabControl.Controls.Add(this.OpsPage);
            this.tabControl.Location = new System.Drawing.Point(12, 121);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(545, 324);
            this.tabControl.TabIndex = 5;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this._TabIndexChanged);
            // 
            // BillPage
            // 
            this.BillPage.Controls.Add(this.Billgrid);
            this.BillPage.Location = new System.Drawing.Point(4, 22);
            this.BillPage.Name = "BillPage";
            this.BillPage.Padding = new System.Windows.Forms.Padding(3);
            this.BillPage.Size = new System.Drawing.Size(537, 298);
            this.BillPage.TabIndex = 0;
            this.BillPage.Text = "Bill of Materials";
            this.BillPage.UseVisualStyleBackColor = true;
            // 
            // OpsPage
            // 
            this.OpsPage.Controls.Add(this.Opsgrid);
            this.OpsPage.Location = new System.Drawing.Point(4, 22);
            this.OpsPage.Name = "OpsPage";
            this.OpsPage.Padding = new System.Windows.Forms.Padding(3);
            this.OpsPage.Size = new System.Drawing.Size(537, 298);
            this.OpsPage.TabIndex = 1;
            this.OpsPage.Text = "Operations";
            this.OpsPage.UseVisualStyleBackColor = true;
            // 
            // Opsgrid
            // 
            this.Opsgrid.AllowUserToAddRows = false;
            this.Opsgrid.AllowUserToDeleteRows = false;
            this.Opsgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Opsgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seq,
            this.Code,
            this.ProdHrs1,
            this.ProdHrs2});
            this.Opsgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Opsgrid.Location = new System.Drawing.Point(3, 3);
            this.Opsgrid.Name = "Opsgrid";
            this.Opsgrid.ReadOnly = true;
            this.Opsgrid.RowHeadersVisible = false;
            this.Opsgrid.Size = new System.Drawing.Size(531, 292);
            this.Opsgrid.TabIndex = 0;
            this.Opsgrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DisplayGrid_ColumnHeaderMouseClick);
            // 
            // Seq
            // 
            this.Seq.DataPropertyName = "Seq";
            this.Seq.HeaderText = "Seq.";
            this.Seq.Name = "Seq";
            this.Seq.ReadOnly = true;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Operation";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // ProdHrs1
            // 
            this.ProdHrs1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ProdHrs1.DataPropertyName = "ProdHrs1";
            this.ProdHrs1.HeaderText = "Production Std 1";
            this.ProdHrs1.Name = "ProdHrs1";
            this.ProdHrs1.ReadOnly = true;
            this.ProdHrs1.Width = 94;
            // 
            // ProdHrs2
            // 
            this.ProdHrs2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ProdHrs2.DataPropertyName = "ProdHrs2";
            this.ProdHrs2.HeaderText = "Production Std 2";
            this.ProdHrs2.Name = "ProdHrs2";
            this.ProdHrs2.ReadOnly = true;
            this.ProdHrs2.Width = 94;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ReportWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkDone);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(64, 17);
            this.status.Text = "Status: Idle";
            // 
            // progress
            // 
            this.progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 16);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // RightClick
            // 
            this.RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
            this.RightClick.Name = "RightClick";
            this.RightClick.Size = new System.Drawing.Size(149, 26);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // RevCompare
            // 
            this.AcceptButton = this.refresh_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 470);
            this.ContextMenuStrip = this.RightClick;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toClipboard_btn);
            this.Controls.Add(this.refresh_btn);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(585, 508);
            this.Name = "RevCompare";
            this.Text = "Rev Compare Tool";
            this.Load += new System.EventHandler(this.RevCompare_Load);
            this.Resize += new System.EventHandler(this.FormResize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Billgrid)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.BillPage.ResumeLayout(false);
            this.OpsPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Opsgrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.RightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void displaygrid_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TextBox pnum_txt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox rev2;
        private System.Windows.Forms.ComboBox rev1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox desc_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Billgrid;
        private System.Windows.Forms.Button refresh_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.Button toClipboard_btn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage BillPage;
        private System.Windows.Forms.TabPage OpsPage;
        private System.Windows.Forms.DataGridView Opsgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdHrs1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdHrs2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ContextMenuStrip RightClick;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
    }
}