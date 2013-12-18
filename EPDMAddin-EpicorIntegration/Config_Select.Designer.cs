namespace EPDM_EpicorIntegration
{
    partial class Config_Select
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config_Select));
            this.select_btn = new System.Windows.Forms.Button();
            this.config_cbo = new System.Windows.Forms.ComboBox();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnum_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // select_btn
            // 
            this.select_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.select_btn.Location = new System.Drawing.Point(12, 78);
            this.select_btn.Name = "select_btn";
            this.select_btn.Size = new System.Drawing.Size(75, 23);
            this.select_btn.TabIndex = 0;
            this.select_btn.Text = "&Select";
            this.select_btn.UseVisualStyleBackColor = true;
            this.select_btn.Click += new System.EventHandler(this.select_btn_Click);
            // 
            // config_cbo
            // 
            this.config_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.config_cbo.FormattingEnabled = true;
            this.config_cbo.Location = new System.Drawing.Point(12, 51);
            this.config_cbo.Name = "config_cbo";
            this.config_cbo.Size = new System.Drawing.Size(222, 21);
            this.config_cbo.TabIndex = 1;
            this.config_cbo.SelectedIndexChanged += new System.EventHandler(this.config_cbo_SelectedIndexChanged);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(159, 78);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 0;
            this.cancel_btn.Text = "&Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Part Number";
            // 
            // pnum_txt
            // 
            this.pnum_txt.BackColor = System.Drawing.Color.White;
            this.pnum_txt.Location = new System.Drawing.Point(12, 25);
            this.pnum_txt.Name = "pnum_txt";
            this.pnum_txt.ReadOnly = true;
            this.pnum_txt.Size = new System.Drawing.Size(222, 20);
            this.pnum_txt.TabIndex = 3;
            // 
            // Config_Select
            // 
            this.AcceptButton = this.select_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(247, 112);
            this.Controls.Add(this.pnum_txt);
            this.Controls.Add(this.config_cbo);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.select_btn);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 150);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(263, 150);
            this.Name = "Config_Select";
            this.Text = "Select Configuration:";
            this.Load += new System.EventHandler(this.Config_Select_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button select_btn;
        private System.Windows.Forms.Button cancel_btn;
        public System.Windows.Forms.ComboBox config_cbo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pnum_txt;
    }
}