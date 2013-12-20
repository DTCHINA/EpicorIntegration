namespace Epicor_Integration
{
    partial class Item_Approve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Item_Approve));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currev_chk = new System.Windows.Forms.CheckBox();
            this.rev_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.partnumber_txt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gid_desc = new System.Windows.Forms.TextBox();
            this.gid_cbo = new System.Windows.Forms.ComboBox();
            this.approval_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.confirm_btn = new System.Windows.Forms.Button();
            this.PartTextChanged = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.currev_chk);
            this.groupBox2.Controls.Add(this.rev_txt);
            this.groupBox2.Controls.Add(this.partnumber_txt);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 125);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Part";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Part Number:";
            // 
            // currev_chk
            // 
            this.currev_chk.AutoSize = true;
            this.currev_chk.Checked = true;
            this.currev_chk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.currev_chk.Location = new System.Drawing.Point(6, 97);
            this.currev_chk.Name = "currev_chk";
            this.currev_chk.Size = new System.Drawing.Size(104, 17);
            this.currev_chk.TabIndex = 2;
            this.currev_chk.Text = "Current Revision";
            this.currev_chk.UseVisualStyleBackColor = true;
            this.currev_chk.CheckedChanged += new System.EventHandler(this.currev_chk_CheckedChanged);
            // 
            // rev_txt
            // 
            this.rev_txt.Enabled = false;
            this.rev_txt.Location = new System.Drawing.Point(6, 71);
            this.rev_txt.Name = "rev_txt";
            this.rev_txt.ReadOnly = true;
            this.rev_txt.Size = new System.Drawing.Size(128, 20);
            this.rev_txt.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Revision:";
            // 
            // partnumber_txt
            // 
            this.partnumber_txt.Location = new System.Drawing.Point(6, 32);
            this.partnumber_txt.Name = "partnumber_txt";
            this.partnumber_txt.Size = new System.Drawing.Size(128, 20);
            this.partnumber_txt.TabIndex = 5;
            this.partnumber_txt.TextChanged += new System.EventHandler(this.partnumber_txt_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gid_desc);
            this.groupBox1.Controls.Add(this.gid_cbo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 83);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ECO Group";
            // 
            // gid_desc
            // 
            this.gid_desc.Location = new System.Drawing.Point(6, 46);
            this.gid_desc.Name = "gid_desc";
            this.gid_desc.Size = new System.Drawing.Size(186, 20);
            this.gid_desc.TabIndex = 1;
            // 
            // gid_cbo
            // 
            this.gid_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gid_cbo.FormattingEnabled = true;
            this.gid_cbo.Location = new System.Drawing.Point(6, 19);
            this.gid_cbo.Name = "gid_cbo";
            this.gid_cbo.Size = new System.Drawing.Size(186, 21);
            this.gid_cbo.TabIndex = 0;
            this.gid_cbo.SelectedIndexChanged += new System.EventHandler(this.gid_cbo_SelectedIndexChanged);
            // 
            // approval_btn
            // 
            this.approval_btn.BackColor = System.Drawing.Color.Yellow;
            this.approval_btn.Location = new System.Drawing.Point(12, 232);
            this.approval_btn.Name = "approval_btn";
            this.approval_btn.Size = new System.Drawing.Size(202, 23);
            this.approval_btn.TabIndex = 42;
            this.approval_btn.Text = "Not Approved";
            this.approval_btn.UseVisualStyleBackColor = false;
            this.approval_btn.Click += new System.EventHandler(this.approval_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(139, 261);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 44;
            this.cancel_btn.Text = "&Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // confirm_btn
            // 
            this.confirm_btn.Location = new System.Drawing.Point(12, 261);
            this.confirm_btn.Name = "confirm_btn";
            this.confirm_btn.Size = new System.Drawing.Size(75, 23);
            this.confirm_btn.TabIndex = 43;
            this.confirm_btn.Text = "Con&firm";
            this.confirm_btn.UseVisualStyleBackColor = true;
            this.confirm_btn.Click += new System.EventHandler(this.confirm_btn_Click);
            // 
            // PartTextChanged
            // 
            this.PartTextChanged.Interval = 500;
            this.PartTextChanged.Tick += new System.EventHandler(this.PartTextChanged_Tick);
            // 
            // Item_Approve
            // 
            this.AcceptButton = this.confirm_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(227, 296);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.confirm_btn);
            this.Controls.Add(this.approval_btn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Item_Approve";
            this.Text = "Approve Item";
            this.Load += new System.EventHandler(this.Item_Approve_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox currev_chk;
        private System.Windows.Forms.TextBox rev_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox partnumber_txt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gid_desc;
        private System.Windows.Forms.ComboBox gid_cbo;
        private System.Windows.Forms.Button approval_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button confirm_btn;
        private System.Windows.Forms.Timer PartTextChanged;
    }
}