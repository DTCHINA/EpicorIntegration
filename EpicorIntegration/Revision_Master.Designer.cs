namespace Epicor_Integration
{
    partial class Revision_Master
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Revision_Master));
            this.savebtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gid_desc = new System.Windows.Forms.TextBox();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.Searchtxt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.econum_txt = new System.Windows.Forms.TextBox();
            this.comments_txt = new System.Windows.Forms.TextBox();
            this.newrev_txt = new System.Windows.Forms.TextBox();
            this.currev_txt = new System.Windows.Forms.TextBox();
            this.revdesc_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkout_chk = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // savebtn
            // 
            this.savebtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.savebtn.Location = new System.Drawing.Point(263, 262);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(75, 23);
            this.savebtn.TabIndex = 7;
            this.savebtn.Text = "&Save";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(344, 262);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 8;
            this.cancelbtn.Text = "&Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gid_desc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ECO Group";
            // 
            // gid_desc
            // 
            this.gid_desc.Location = new System.Drawing.Point(6, 19);
            this.gid_desc.Name = "gid_desc";
            this.gid_desc.ReadOnly = true;
            this.gid_desc.Size = new System.Drawing.Size(181, 20);
            this.gid_desc.TabIndex = 0;
            // 
            // Searchbtn
            // 
            this.Searchbtn.Location = new System.Drawing.Point(6, 19);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(75, 23);
            this.Searchbtn.TabIndex = 38;
            this.Searchbtn.TabStop = false;
            this.Searchbtn.Text = "&Search";
            this.Searchbtn.UseVisualStyleBackColor = true;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // Searchtxt
            // 
            this.Searchtxt.Location = new System.Drawing.Point(87, 21);
            this.Searchtxt.Name = "Searchtxt";
            this.Searchtxt.Size = new System.Drawing.Size(100, 20);
            this.Searchtxt.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.econum_txt);
            this.groupBox2.Controls.Add(this.comments_txt);
            this.groupBox2.Controls.Add(this.newrev_txt);
            this.groupBox2.Controls.Add(this.currev_txt);
            this.groupBox2.Controls.Add(this.revdesc_txt);
            this.groupBox2.Controls.Add(this.Searchbtn);
            this.groupBox2.Controls.Add(this.Searchtxt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 187);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Part";
            // 
            // econum_txt
            // 
            this.econum_txt.Location = new System.Drawing.Point(9, 112);
            this.econum_txt.Name = "econum_txt";
            this.econum_txt.Size = new System.Drawing.Size(178, 20);
            this.econum_txt.TabIndex = 4;
            // 
            // comments_txt
            // 
            this.comments_txt.Location = new System.Drawing.Point(206, 40);
            this.comments_txt.Multiline = true;
            this.comments_txt.Name = "comments_txt";
            this.comments_txt.Size = new System.Drawing.Size(178, 101);
            this.comments_txt.TabIndex = 5;
            // 
            // newrev_txt
            // 
            this.newrev_txt.Location = new System.Drawing.Point(87, 73);
            this.newrev_txt.Name = "newrev_txt";
            this.newrev_txt.Size = new System.Drawing.Size(100, 20);
            this.newrev_txt.TabIndex = 3;
            // 
            // currev_txt
            // 
            this.currev_txt.Location = new System.Drawing.Point(87, 47);
            this.currev_txt.Name = "currev_txt";
            this.currev_txt.Size = new System.Drawing.Size(100, 20);
            this.currev_txt.TabIndex = 2;
            // 
            // revdesc_txt
            // 
            this.revdesc_txt.Location = new System.Drawing.Point(9, 151);
            this.revdesc_txt.Name = "revdesc_txt";
            this.revdesc_txt.Size = new System.Drawing.Size(375, 20);
            this.revdesc_txt.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "ECO Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Comments:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "New Revision:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Cur Revision:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Revision Description:";
            // 
            // checkout_chk
            // 
            this.checkout_chk.AutoSize = true;
            this.checkout_chk.Checked = true;
            this.checkout_chk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkout_chk.Location = new System.Drawing.Point(320, 33);
            this.checkout_chk.Name = "checkout_chk";
            this.checkout_chk.Size = new System.Drawing.Size(99, 17);
            this.checkout_chk.TabIndex = 100;
            this.checkout_chk.TabStop = false;
            this.checkout_chk.Text = "Check Out Part";
            this.checkout_chk.UseVisualStyleBackColor = true;
            // 
            // Revision_Master
            // 
            this.AcceptButton = this.Searchbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(431, 299);
            this.Controls.Add(this.checkout_chk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.cancelbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Revision_Master";
            this.Text = "Add Item Revision";
            this.Load += new System.EventHandler(this.Revision_Master_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gid_desc;
        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.TextBox Searchtxt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox currev_txt;
        private System.Windows.Forms.TextBox revdesc_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newrev_txt;
        private System.Windows.Forms.CheckBox checkout_chk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox comments_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox econum_txt;
    }
}