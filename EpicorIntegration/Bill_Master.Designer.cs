namespace Epicor_Integration
{
    partial class Bill_Master
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bill_Master));
            this.MajorHorizSplit = new System.Windows.Forms.SplitContainer();
            this.weight = new System.Windows.Forms.NumericUpDown();
            this.area = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.parentrev_txt = new System.Windows.Forms.TextBox();
            this.gid_txt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reseq_btn = new System.Windows.Forms.Button();
            this.saveandclose_btn = new System.Windows.Forms.Button();
            this.newbtn = new System.Windows.Forms.Button();
            this.removebtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.savebtn = new System.Windows.Forms.Button();
            this.parentdesc_txt = new System.Windows.Forms.TextBox();
            this.parent_txt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.factor_btn = new System.Windows.Forms.Button();
            this.PullAsAsm_chk = new System.Windows.Forms.CheckBox();
            this.addraw = new System.Windows.Forms.Button();
            this.copy_btn = new System.Windows.Forms.Button();
            this.ViewAsAsm_chk = new System.Windows.Forms.CheckBox();
            this.uom_cbo = new System.Windows.Forms.ComboBox();
            this.qty_num = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.desc_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.partnum_txt = new System.Windows.Forms.TextBox();
            this.ops_cbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.findpart_btn = new System.Windows.Forms.Button();
            this.mtlseq_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BillDataGrid = new System.Windows.Forms.DataGridView();
            this.MtlSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MtlPartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MtlPartNumPartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelatedOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewAsAsm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PullAsAsm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OpDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartTimer = new System.Windows.Forms.Timer(this.components);
            this.EnableNew = new System.Windows.Forms.Timer(this.components);
            this.RawMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TemplateMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sheetCoilUsageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eNGDataDataSet = new Epicor_Integration.ENGDataDataSet();
            this.sheetCoil_UsageTableAdapter = new Epicor_Integration.ENGDataDataSetTableAdapters.SheetCoil_UsageTableAdapter();
            this.MajorHorizSplit.Panel1.SuspendLayout();
            this.MajorHorizSplit.Panel2.SuspendLayout();
            this.MajorHorizSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.area)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qty_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetCoilUsageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // MajorHorizSplit
            // 
            this.MajorHorizSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MajorHorizSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MajorHorizSplit.Location = new System.Drawing.Point(0, 0);
            this.MajorHorizSplit.Name = "MajorHorizSplit";
            this.MajorHorizSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MajorHorizSplit.Panel1
            // 
            this.MajorHorizSplit.Panel1.Controls.Add(this.weight);
            this.MajorHorizSplit.Panel1.Controls.Add(this.area);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label10);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label9);
            this.MajorHorizSplit.Panel1.Controls.Add(this.parentrev_txt);
            this.MajorHorizSplit.Panel1.Controls.Add(this.gid_txt);
            this.MajorHorizSplit.Panel1.Controls.Add(this.groupBox2);
            this.MajorHorizSplit.Panel1.Controls.Add(this.parentdesc_txt);
            this.MajorHorizSplit.Panel1.Controls.Add(this.parent_txt);
            this.MajorHorizSplit.Panel1.Controls.Add(this.groupBox1);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label8);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label7);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label2);
            this.MajorHorizSplit.Panel1.Controls.Add(this.label1);
            // 
            // MajorHorizSplit.Panel2
            // 
            this.MajorHorizSplit.Panel2.Controls.Add(this.BillDataGrid);
            this.MajorHorizSplit.Size = new System.Drawing.Size(684, 466);
            this.MajorHorizSplit.SplitterDistance = 262;
            this.MajorHorizSplit.TabIndex = 1;
            // 
            // weight
            // 
            this.weight.BackColor = System.Drawing.Color.White;
            this.weight.DecimalPlaces = 3;
            this.weight.InterceptArrowKeys = false;
            this.weight.Location = new System.Drawing.Point(257, 64);
            this.weight.Name = "weight";
            this.weight.ReadOnly = true;
            this.weight.Size = new System.Drawing.Size(62, 20);
            this.weight.TabIndex = 16;
            // 
            // area
            // 
            this.area.BackColor = System.Drawing.Color.White;
            this.area.DecimalPlaces = 3;
            this.area.InterceptArrowKeys = false;
            this.area.Location = new System.Drawing.Point(325, 64);
            this.area.Name = "area";
            this.area.ReadOnly = true;
            this.area.Size = new System.Drawing.Size(62, 20);
            this.area.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Area:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(254, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Weight:";
            // 
            // parentrev_txt
            // 
            this.parentrev_txt.BackColor = System.Drawing.Color.White;
            this.parentrev_txt.Location = new System.Drawing.Point(158, 64);
            this.parentrev_txt.Name = "parentrev_txt";
            this.parentrev_txt.ReadOnly = true;
            this.parentrev_txt.Size = new System.Drawing.Size(82, 20);
            this.parentrev_txt.TabIndex = 10;
            // 
            // gid_txt
            // 
            this.gid_txt.BackColor = System.Drawing.Color.White;
            this.gid_txt.Location = new System.Drawing.Point(15, 64);
            this.gid_txt.Name = "gid_txt";
            this.gid_txt.ReadOnly = true;
            this.gid_txt.Size = new System.Drawing.Size(82, 20);
            this.gid_txt.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reseq_btn);
            this.groupBox2.Controls.Add(this.saveandclose_btn);
            this.groupBox2.Controls.Add(this.newbtn);
            this.groupBox2.Controls.Add(this.removebtn);
            this.groupBox2.Controls.Add(this.cancelbtn);
            this.groupBox2.Controls.Add(this.savebtn);
            this.groupBox2.Location = new System.Drawing.Point(584, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(88, 239);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // reseq_btn
            // 
            this.reseq_btn.Location = new System.Drawing.Point(6, 74);
            this.reseq_btn.Name = "reseq_btn";
            this.reseq_btn.Size = new System.Drawing.Size(75, 23);
            this.reseq_btn.TabIndex = 16;
            this.reseq_btn.Text = "Re-Seq.";
            this.reseq_btn.UseVisualStyleBackColor = true;
            this.reseq_btn.Click += new System.EventHandler(this.reseq_btn_Click);
            // 
            // saveandclose_btn
            // 
            this.saveandclose_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveandclose_btn.Location = new System.Drawing.Point(7, 181);
            this.saveandclose_btn.Name = "saveandclose_btn";
            this.saveandclose_btn.Size = new System.Drawing.Size(75, 23);
            this.saveandclose_btn.TabIndex = 15;
            this.saveandclose_btn.Text = "Save/Close";
            this.saveandclose_btn.UseVisualStyleBackColor = true;
            this.saveandclose_btn.Click += new System.EventHandler(this.saveandclose_btn_Click);
            // 
            // newbtn
            // 
            this.newbtn.Location = new System.Drawing.Point(6, 16);
            this.newbtn.Name = "newbtn";
            this.newbtn.Size = new System.Drawing.Size(75, 23);
            this.newbtn.TabIndex = 0;
            this.newbtn.Text = "&Add";
            this.newbtn.UseVisualStyleBackColor = true;
            this.newbtn.Click += new System.EventHandler(this.newbtn_Click);
            // 
            // removebtn
            // 
            this.removebtn.Location = new System.Drawing.Point(6, 45);
            this.removebtn.Name = "removebtn";
            this.removebtn.Size = new System.Drawing.Size(75, 23);
            this.removebtn.TabIndex = 1;
            this.removebtn.Text = "&Remove";
            this.removebtn.UseVisualStyleBackColor = true;
            this.removebtn.Click += new System.EventHandler(this.removebtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(6, 210);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 2;
            this.cancelbtn.Text = "&Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // savebtn
            // 
            this.savebtn.Location = new System.Drawing.Point(6, 103);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(75, 23);
            this.savebtn.TabIndex = 3;
            this.savebtn.Text = "&Save";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // parentdesc_txt
            // 
            this.parentdesc_txt.BackColor = System.Drawing.Color.White;
            this.parentdesc_txt.Location = new System.Drawing.Point(158, 25);
            this.parentdesc_txt.Name = "parentdesc_txt";
            this.parentdesc_txt.ReadOnly = true;
            this.parentdesc_txt.Size = new System.Drawing.Size(229, 20);
            this.parentdesc_txt.TabIndex = 3;
            // 
            // parent_txt
            // 
            this.parent_txt.BackColor = System.Drawing.Color.White;
            this.parent_txt.Location = new System.Drawing.Point(15, 25);
            this.parent_txt.Name = "parent_txt";
            this.parent_txt.ReadOnly = true;
            this.parent_txt.Size = new System.Drawing.Size(137, 20);
            this.parent_txt.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.factor_btn);
            this.groupBox1.Controls.Add(this.PullAsAsm_chk);
            this.groupBox1.Controls.Add(this.addraw);
            this.groupBox1.Controls.Add(this.copy_btn);
            this.groupBox1.Controls.Add(this.ViewAsAsm_chk);
            this.groupBox1.Controls.Add(this.uom_cbo);
            this.groupBox1.Controls.Add(this.qty_num);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.desc_txt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.partnum_txt);
            this.groupBox1.Controls.Add(this.ops_cbo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.findpart_btn);
            this.groupBox1.Controls.Add(this.mtlseq_txt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // factor_btn
            // 
            this.factor_btn.Location = new System.Drawing.Point(143, 124);
            this.factor_btn.Name = "factor_btn";
            this.factor_btn.Size = new System.Drawing.Size(46, 20);
            this.factor_btn.TabIndex = 37;
            this.factor_btn.Text = "Factor Qty";
            this.factor_btn.UseVisualStyleBackColor = true;
            this.factor_btn.Click += new System.EventHandler(this.factor_btn_Click);
            // 
            // PullAsAsm_chk
            // 
            this.PullAsAsm_chk.AutoSize = true;
            this.PullAsAsm_chk.Location = new System.Drawing.Point(292, 125);
            this.PullAsAsm_chk.Name = "PullAsAsm_chk";
            this.PullAsAsm_chk.Size = new System.Drawing.Size(105, 17);
            this.PullAsAsm_chk.TabIndex = 15;
            this.PullAsAsm_chk.Text = "Pull As Assembly";
            this.PullAsAsm_chk.UseVisualStyleBackColor = true;
            this.PullAsAsm_chk.Click += new System.EventHandler(this.PullAsAsm_chk_Click);
            // 
            // addraw
            // 
            this.addraw.Location = new System.Drawing.Point(485, 16);
            this.addraw.Name = "addraw";
            this.addraw.Size = new System.Drawing.Size(75, 23);
            this.addraw.TabIndex = 11;
            this.addraw.Text = "Add &Raw";
            this.addraw.UseVisualStyleBackColor = true;
            this.addraw.Click += new System.EventHandler(this.addraw_Click);
            // 
            // copy_btn
            // 
            this.copy_btn.Location = new System.Drawing.Point(485, 42);
            this.copy_btn.Name = "copy_btn";
            this.copy_btn.Size = new System.Drawing.Size(75, 23);
            this.copy_btn.TabIndex = 14;
            this.copy_btn.Text = "&Add From...";
            this.copy_btn.UseVisualStyleBackColor = true;
            this.copy_btn.Click += new System.EventHandler(this.copy_btn_Click);
            // 
            // ViewAsAsm_chk
            // 
            this.ViewAsAsm_chk.AutoSize = true;
            this.ViewAsAsm_chk.Location = new System.Drawing.Point(292, 99);
            this.ViewAsAsm_chk.Name = "ViewAsAsm_chk";
            this.ViewAsAsm_chk.Size = new System.Drawing.Size(111, 17);
            this.ViewAsAsm_chk.TabIndex = 10;
            this.ViewAsAsm_chk.Text = "View As Assembly";
            this.ViewAsAsm_chk.UseVisualStyleBackColor = true;
            this.ViewAsAsm_chk.Click += new System.EventHandler(this.ViewAsAsm_chk_Click);
            // 
            // uom_cbo
            // 
            this.uom_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uom_cbo.FormattingEnabled = true;
            this.uom_cbo.Location = new System.Drawing.Point(195, 123);
            this.uom_cbo.Name = "uom_cbo";
            this.uom_cbo.Size = new System.Drawing.Size(91, 21);
            this.uom_cbo.TabIndex = 2;
            this.uom_cbo.Enter += new System.EventHandler(this.uom_cbo_Enter);
            this.uom_cbo.Leave += new System.EventHandler(this.uom_cbo_Leave);
            // 
            // qty_num
            // 
            this.qty_num.DecimalPlaces = 2;
            this.qty_num.Location = new System.Drawing.Point(75, 124);
            this.qty_num.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.qty_num.Name = "qty_num";
            this.qty_num.Size = new System.Drawing.Size(62, 20);
            this.qty_num.TabIndex = 9;
            this.qty_num.Enter += new System.EventHandler(this.qty_num_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Qty.";
            // 
            // desc_txt
            // 
            this.desc_txt.BackColor = System.Drawing.Color.White;
            this.desc_txt.Location = new System.Drawing.Point(75, 71);
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.ReadOnly = true;
            this.desc_txt.Size = new System.Drawing.Size(211, 20);
            this.desc_txt.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Description:";
            // 
            // partnum_txt
            // 
            this.partnum_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.partnum_txt.Location = new System.Drawing.Point(75, 45);
            this.partnum_txt.Name = "partnum_txt";
            this.partnum_txt.Size = new System.Drawing.Size(211, 20);
            this.partnum_txt.TabIndex = 5;
            this.partnum_txt.Enter += new System.EventHandler(this.partnum_txt_Enter);
            this.partnum_txt.Leave += new System.EventHandler(this.partnum_txt_Leave);
            // 
            // ops_cbo
            // 
            this.ops_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ops_cbo.FormattingEnabled = true;
            this.ops_cbo.Location = new System.Drawing.Point(75, 97);
            this.ops_cbo.Name = "ops_cbo";
            this.ops_cbo.Size = new System.Drawing.Size(211, 21);
            this.ops_cbo.TabIndex = 4;
            this.ops_cbo.Enter += new System.EventHandler(this.ops_cbo_Enter);
            this.ops_cbo.Leave += new System.EventHandler(this.ops_cbo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Operation:";
            // 
            // findpart_btn
            // 
            this.findpart_btn.Location = new System.Drawing.Point(7, 43);
            this.findpart_btn.Name = "findpart_btn";
            this.findpart_btn.Size = new System.Drawing.Size(62, 23);
            this.findpart_btn.TabIndex = 2;
            this.findpart_btn.Text = "Part";
            this.findpart_btn.UseVisualStyleBackColor = true;
            this.findpart_btn.Click += new System.EventHandler(this.findpart_btn_Click);
            // 
            // mtlseq_txt
            // 
            this.mtlseq_txt.Location = new System.Drawing.Point(75, 19);
            this.mtlseq_txt.Name = "mtlseq_txt";
            this.mtlseq_txt.ReadOnly = true;
            this.mtlseq_txt.Size = new System.Drawing.Size(62, 20);
            this.mtlseq_txt.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mtl Seq:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(155, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Parent Revision:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Group ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parent Part:";
            // 
            // BillDataGrid
            // 
            this.BillDataGrid.AllowUserToAddRows = false;
            this.BillDataGrid.AllowUserToDeleteRows = false;
            this.BillDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BillDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.BillDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.BillDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BillDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MtlSeq,
            this.QtyPer,
            this.MtlPartNum,
            this.MtlPartNumPartDescription,
            this.RelatedOperation,
            this.UOMCode,
            this.ViewAsAsm,
            this.PullAsAsm,
            this.OpDesc});
            this.BillDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.BillDataGrid.Location = new System.Drawing.Point(0, 0);
            this.BillDataGrid.MultiSelect = false;
            this.BillDataGrid.Name = "BillDataGrid";
            this.BillDataGrid.RowHeadersVisible = false;
            this.BillDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.BillDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.BillDataGrid.ShowCellErrors = false;
            this.BillDataGrid.ShowCellToolTips = false;
            this.BillDataGrid.ShowEditingIcon = false;
            this.BillDataGrid.ShowRowErrors = false;
            this.BillDataGrid.Size = new System.Drawing.Size(684, 200);
            this.BillDataGrid.TabIndex = 1;
            this.BillDataGrid.VirtualMode = true;
            this.BillDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BillDataGrid_CellContentClick);
            // 
            // MtlSeq
            // 
            this.MtlSeq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MtlSeq.DataPropertyName = "MtlSeq";
            this.MtlSeq.HeaderText = "Seq.";
            this.MtlSeq.Name = "MtlSeq";
            this.MtlSeq.ReadOnly = true;
            this.MtlSeq.Width = 54;
            // 
            // QtyPer
            // 
            this.QtyPer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QtyPer.DataPropertyName = "QtyPer";
            this.QtyPer.HeaderText = "Qty";
            this.QtyPer.Name = "QtyPer";
            this.QtyPer.ReadOnly = true;
            this.QtyPer.Width = 48;
            // 
            // MtlPartNum
            // 
            this.MtlPartNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MtlPartNum.DataPropertyName = "MtlPartNum";
            this.MtlPartNum.HeaderText = "Part Number";
            this.MtlPartNum.Name = "MtlPartNum";
            this.MtlPartNum.ReadOnly = true;
            this.MtlPartNum.Width = 91;
            // 
            // MtlPartNumPartDescription
            // 
            this.MtlPartNumPartDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MtlPartNumPartDescription.DataPropertyName = "MtlPartNumPartDescription";
            this.MtlPartNumPartDescription.HeaderText = "Description";
            this.MtlPartNumPartDescription.MinimumWidth = 85;
            this.MtlPartNumPartDescription.Name = "MtlPartNumPartDescription";
            this.MtlPartNumPartDescription.ReadOnly = true;
            // 
            // RelatedOperation
            // 
            this.RelatedOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.RelatedOperation.DataPropertyName = "RelatedOperation";
            this.RelatedOperation.HeaderText = "Opr.";
            this.RelatedOperation.Name = "RelatedOperation";
            this.RelatedOperation.ReadOnly = true;
            this.RelatedOperation.Width = 52;
            // 
            // UOMCode
            // 
            this.UOMCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UOMCode.DataPropertyName = "UOMCode";
            this.UOMCode.HeaderText = "UOM Code";
            this.UOMCode.Name = "UOMCode";
            this.UOMCode.ReadOnly = true;
            this.UOMCode.Width = 85;
            // 
            // ViewAsAsm
            // 
            this.ViewAsAsm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ViewAsAsm.DataPropertyName = "ViewAsAsm";
            this.ViewAsAsm.HeaderText = "View As Assembly";
            this.ViewAsAsm.Name = "ViewAsAsm";
            this.ViewAsAsm.ReadOnly = true;
            this.ViewAsAsm.Width = 88;
            // 
            // PullAsAsm
            // 
            this.PullAsAsm.DataPropertyName = "PullAsAsm";
            this.PullAsAsm.HeaderText = "Pull As Assembly";
            this.PullAsAsm.Name = "PullAsAsm";
            this.PullAsAsm.ReadOnly = true;
            // 
            // OpDesc
            // 
            this.OpDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OpDesc.DataPropertyName = "OpDesc";
            this.OpDesc.HeaderText = "Operation";
            this.OpDesc.Name = "OpDesc";
            this.OpDesc.ReadOnly = true;
            this.OpDesc.Width = 78;
            // 
            // PartTimer
            // 
            this.PartTimer.Interval = 500;
            this.PartTimer.Tick += new System.EventHandler(this.PartTimer_Tick);
            // 
            // EnableNew
            // 
            this.EnableNew.Enabled = true;
            this.EnableNew.Tick += new System.EventHandler(this.EnableNew_Tick);
            // 
            // RawMenu
            // 
            this.RawMenu.Name = "RawMenu";
            this.RawMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // TemplateMenu
            // 
            this.TemplateMenu.Name = "TemplateMenu";
            this.TemplateMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // sheetCoilUsageBindingSource
            // 
            this.sheetCoilUsageBindingSource.DataMember = "SheetCoil_Usage";
            this.sheetCoilUsageBindingSource.DataSource = this.eNGDataDataSet;
            // 
            // eNGDataDataSet
            // 
            this.eNGDataDataSet.DataSetName = "ENGDataDataSet";
            this.eNGDataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sheetCoil_UsageTableAdapter
            // 
            this.sheetCoil_UsageTableAdapter.ClearBeforeFill = true;
            // 
            // Bill_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(684, 466);
            this.Controls.Add(this.MajorHorizSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(570, 504);
            this.Name = "Bill_Master";
            this.Text = "Add Materials To...";
            this.Load += new System.EventHandler(this.Bill_Master_Load);
            this.MajorHorizSplit.Panel1.ResumeLayout(false);
            this.MajorHorizSplit.Panel1.PerformLayout();
            this.MajorHorizSplit.Panel2.ResumeLayout(false);
            this.MajorHorizSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.area)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qty_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetCoilUsageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MajorHorizSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox parentdesc_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox parent_txt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown qty_num;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox desc_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox partnum_txt;
        private System.Windows.Forms.ComboBox ops_cbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button findpart_btn;
        private System.Windows.Forms.TextBox mtlseq_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView BillDataGrid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button copy_btn;
        private System.Windows.Forms.Button newbtn;
        private System.Windows.Forms.Button removebtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox parentrev_txt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox gid_txt;
        private System.Windows.Forms.ComboBox uom_cbo;
        private System.Windows.Forms.CheckBox ViewAsAsm_chk;
        private System.Windows.Forms.Timer PartTimer;
        private System.Windows.Forms.Button saveandclose_btn;
        private System.Windows.Forms.Timer EnableNew;
        private System.Windows.Forms.Button addraw;
        private System.Windows.Forms.ContextMenuStrip RawMenu;
        private System.Windows.Forms.ContextMenuStrip TemplateMenu;
        private System.Windows.Forms.CheckBox PullAsAsm_chk;
        private System.Windows.Forms.Button reseq_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MtlSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn MtlPartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn MtlPartNumPartDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelatedOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ViewAsAsm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PullAsAsm;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpDesc;
        private System.Windows.Forms.NumericUpDown area;
        private System.Windows.Forms.NumericUpDown weight;
        private System.Windows.Forms.Button factor_btn;
        private System.Windows.Forms.BindingSource sheetCoilUsageBindingSource;
        private ENGDataDataSet eNGDataDataSet;
        private ENGDataDataSetTableAdapters.SheetCoil_UsageTableAdapter sheetCoil_UsageTableAdapter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;

    }
}