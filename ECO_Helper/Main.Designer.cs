namespace ECO_Helper
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.addrev_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnum_txt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.revcomments_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.eco_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.desc_txt = new System.Windows.Forms.TextBox();
            this.useswrev_chk = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rev_txt = new System.Windows.Forms.TextBox();
            this.item_btn = new System.Windows.Forms.Button();
            this.ops_btn = new System.Windows.Forms.Button();
            this.bill_btn = new System.Windows.Forms.Button();
            this.getdetails_btn = new System.Windows.Forms.Button();
            this.browse_btn = new System.Windows.Forms.Button();
            this.filedir_txt = new System.Windows.Forms.TextBox();
            this.checkin_btn = new System.Windows.Forms.Button();
            this.approved_btn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gid_cbo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedout_chk = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.legacy_chk = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // addrev_btn
            // 
            this.addrev_btn.Location = new System.Drawing.Point(230, 12);
            this.addrev_btn.Name = "addrev_btn";
            this.addrev_btn.Size = new System.Drawing.Size(90, 23);
            this.addrev_btn.TabIndex = 0;
            this.addrev_btn.Text = "Add Revision";
            this.addrev_btn.UseVisualStyleBackColor = true;
            this.addrev_btn.Click += new System.EventHandler(this.addrev_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Part Number";
            // 
            // pnum_txt
            // 
            this.pnum_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pnum_txt.Location = new System.Drawing.Point(9, 32);
            this.pnum_txt.Name = "pnum_txt";
            this.pnum_txt.Size = new System.Drawing.Size(131, 20);
            this.pnum_txt.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.revcomments_txt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.eco_txt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.desc_txt);
            this.groupBox1.Controls.Add(this.useswrev_chk);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rev_txt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pnum_txt);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 313);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Revision";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Revision Comments";
            // 
            // revcomments_txt
            // 
            this.revcomments_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.revcomments_txt.Location = new System.Drawing.Point(9, 188);
            this.revcomments_txt.Multiline = true;
            this.revcomments_txt.Name = "revcomments_txt";
            this.revcomments_txt.Size = new System.Drawing.Size(197, 119);
            this.revcomments_txt.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ECO Number";
            // 
            // eco_txt
            // 
            this.eco_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.eco_txt.Location = new System.Drawing.Point(9, 149);
            this.eco_txt.MaxLength = 10;
            this.eco_txt.Name = "eco_txt";
            this.eco_txt.Size = new System.Drawing.Size(100, 20);
            this.eco_txt.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description";
            // 
            // desc_txt
            // 
            this.desc_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.desc_txt.Location = new System.Drawing.Point(9, 110);
            this.desc_txt.MaxLength = 30;
            this.desc_txt.Name = "desc_txt";
            this.desc_txt.Size = new System.Drawing.Size(197, 20);
            this.desc_txt.TabIndex = 7;
            // 
            // useswrev_chk
            // 
            this.useswrev_chk.AutoSize = true;
            this.useswrev_chk.Location = new System.Drawing.Point(60, 71);
            this.useswrev_chk.Name = "useswrev_chk";
            this.useswrev_chk.Size = new System.Drawing.Size(110, 17);
            this.useswrev_chk.TabIndex = 5;
            this.useswrev_chk.Text = "Use SW Revision";
            this.useswrev_chk.UseVisualStyleBackColor = true;
            this.useswrev_chk.CheckedChanged += new System.EventHandler(this.useswrev_chk_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Revision";
            // 
            // rev_txt
            // 
            this.rev_txt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rev_txt.Location = new System.Drawing.Point(9, 71);
            this.rev_txt.MaxLength = 2;
            this.rev_txt.Name = "rev_txt";
            this.rev_txt.Size = new System.Drawing.Size(45, 20);
            this.rev_txt.TabIndex = 4;
            // 
            // item_btn
            // 
            this.item_btn.Enabled = false;
            this.item_btn.Location = new System.Drawing.Point(230, 41);
            this.item_btn.Name = "item_btn";
            this.item_btn.Size = new System.Drawing.Size(90, 23);
            this.item_btn.TabIndex = 4;
            this.item_btn.Text = "Edit Item";
            this.item_btn.UseVisualStyleBackColor = true;
            this.item_btn.Click += new System.EventHandler(this.item_btn_Click);
            // 
            // ops_btn
            // 
            this.ops_btn.Enabled = false;
            this.ops_btn.Location = new System.Drawing.Point(230, 99);
            this.ops_btn.Name = "ops_btn";
            this.ops_btn.Size = new System.Drawing.Size(90, 23);
            this.ops_btn.TabIndex = 5;
            this.ops_btn.Text = "Edit Operations";
            this.ops_btn.UseVisualStyleBackColor = true;
            this.ops_btn.Click += new System.EventHandler(this.ops_btn_Click);
            // 
            // bill_btn
            // 
            this.bill_btn.Enabled = false;
            this.bill_btn.Location = new System.Drawing.Point(230, 128);
            this.bill_btn.Name = "bill_btn";
            this.bill_btn.Size = new System.Drawing.Size(90, 23);
            this.bill_btn.TabIndex = 6;
            this.bill_btn.Text = "Edit Bill of Mat\'l";
            this.bill_btn.UseVisualStyleBackColor = true;
            this.bill_btn.Click += new System.EventHandler(this.bill_btn_Click);
            // 
            // getdetails_btn
            // 
            this.getdetails_btn.Enabled = false;
            this.getdetails_btn.Location = new System.Drawing.Point(230, 70);
            this.getdetails_btn.Name = "getdetails_btn";
            this.getdetails_btn.Size = new System.Drawing.Size(90, 23);
            this.getdetails_btn.TabIndex = 7;
            this.getdetails_btn.Text = "Get Details";
            this.getdetails_btn.UseVisualStyleBackColor = true;
            this.getdetails_btn.Click += new System.EventHandler(this.getdetails_btn_Click);
            // 
            // browse_btn
            // 
            this.browse_btn.Location = new System.Drawing.Point(21, 387);
            this.browse_btn.Name = "browse_btn";
            this.browse_btn.Size = new System.Drawing.Size(75, 23);
            this.browse_btn.TabIndex = 8;
            this.browse_btn.Text = "Browse";
            this.browse_btn.UseVisualStyleBackColor = true;
            this.browse_btn.Click += new System.EventHandler(this.browse_btn_Click);
            // 
            // filedir_txt
            // 
            this.filedir_txt.Location = new System.Drawing.Point(102, 390);
            this.filedir_txt.Name = "filedir_txt";
            this.filedir_txt.Size = new System.Drawing.Size(218, 20);
            this.filedir_txt.TabIndex = 9;
            // 
            // checkin_btn
            // 
            this.checkin_btn.Enabled = false;
            this.checkin_btn.Location = new System.Drawing.Point(230, 348);
            this.checkin_btn.Name = "checkin_btn";
            this.checkin_btn.Size = new System.Drawing.Size(90, 23);
            this.checkin_btn.TabIndex = 10;
            this.checkin_btn.Text = "Check-In";
            this.checkin_btn.UseVisualStyleBackColor = true;
            this.checkin_btn.Click += new System.EventHandler(this.checkin_btn_Click);
            // 
            // approved_btn
            // 
            this.approved_btn.BackColor = System.Drawing.Color.Yellow;
            this.approved_btn.Enabled = false;
            this.approved_btn.ForeColor = System.Drawing.Color.Black;
            this.approved_btn.Location = new System.Drawing.Point(230, 319);
            this.approved_btn.Name = "approved_btn";
            this.approved_btn.Size = new System.Drawing.Size(90, 23);
            this.approved_btn.TabIndex = 11;
            this.approved_btn.Text = "Not Approved";
            this.approved_btn.UseVisualStyleBackColor = false;
            this.approved_btn.Click += new System.EventHandler(this.approved_btn_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "sldasm";
            this.openFileDialog.Filter = "SolidWorks Parts|*.sldprt|SolidWorks Assemblies|*.sldasm|All files|*.*";
            this.openFileDialog.InitialDirectory = "C:\\NORCO_PDM";
            this.openFileDialog.Title = "Find File in Vault";
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetFormToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // resetFormToolStripMenuItem
            // 
            this.resetFormToolStripMenuItem.Name = "resetFormToolStripMenuItem";
            this.resetFormToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.resetFormToolStripMenuItem.Text = "Reset Form";
            this.resetFormToolStripMenuItem.Click += new System.EventHandler(this.resetFormToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gid_cbo
            // 
            this.gid_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gid_cbo.FormattingEnabled = true;
            this.gid_cbo.Location = new System.Drawing.Point(21, 31);
            this.gid_cbo.Name = "gid_cbo";
            this.gid_cbo.Size = new System.Drawing.Size(197, 21);
            this.gid_cbo.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Group ID";
            // 
            // checkedout_chk
            // 
            this.checkedout_chk.AutoSize = true;
            this.checkedout_chk.Location = new System.Drawing.Point(231, 296);
            this.checkedout_chk.Name = "checkedout_chk";
            this.checkedout_chk.Size = new System.Drawing.Size(89, 17);
            this.checkedout_chk.TabIndex = 15;
            this.checkedout_chk.Text = "Checked Out";
            this.checkedout_chk.UseVisualStyleBackColor = true;
            this.checkedout_chk.CheckedChanged += new System.EventHandler(this.checkedout_chk_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 374);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Target Part File";
            // 
            // legacy_chk
            // 
            this.legacy_chk.AutoSize = true;
            this.legacy_chk.Location = new System.Drawing.Point(231, 273);
            this.legacy_chk.Name = "legacy_chk";
            this.legacy_chk.Size = new System.Drawing.Size(80, 17);
            this.legacy_chk.TabIndex = 17;
            this.legacy_chk.Text = "Legacy File";
            this.legacy_chk.UseVisualStyleBackColor = true;
            this.legacy_chk.CheckedChanged += new System.EventHandler(this.legacy_chk_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 421);
            this.ContextMenuStrip = this.RightClickMenu;
            this.Controls.Add(this.legacy_chk);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkedout_chk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gid_cbo);
            this.Controls.Add(this.approved_btn);
            this.Controls.Add(this.checkin_btn);
            this.Controls.Add(this.filedir_txt);
            this.Controls.Add(this.browse_btn);
            this.Controls.Add(this.getdetails_btn);
            this.Controls.Add(this.bill_btn);
            this.Controls.Add(this.ops_btn);
            this.Controls.Add(this.item_btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.addrev_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "ECO Helper";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addrev_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pnum_txt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox revcomments_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox eco_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox desc_txt;
        private System.Windows.Forms.CheckBox useswrev_chk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rev_txt;
        private System.Windows.Forms.Button item_btn;
        private System.Windows.Forms.Button ops_btn;
        private System.Windows.Forms.Button bill_btn;
        private System.Windows.Forms.Button getdetails_btn;
        private System.Windows.Forms.Button browse_btn;
        private System.Windows.Forms.TextBox filedir_txt;
        private System.Windows.Forms.Button checkin_btn;
        private System.Windows.Forms.Button approved_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ComboBox gid_cbo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkedout_chk;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox legacy_chk;
    }
}

