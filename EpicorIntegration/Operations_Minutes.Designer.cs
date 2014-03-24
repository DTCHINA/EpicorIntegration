namespace Epicor_Integration
{
    partial class Operations_Minutes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operations_Minutes));
            this.label1 = new System.Windows.Forms.Label();
            this.operation_cbo = new System.Windows.Forms.ComboBox();
            this.type_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.efficiency_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.per_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minpc_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.seconds_txt = new System.Windows.Forms.TextBox();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.epicorMinutesTableAdapter = new Epicor_Integration.ENGDataDataSetTableAdapters.EpicorMinutesTableAdapter();
            this.engDataDataSet = new Epicor_Integration.ENGDataDataSet();
            this.epicorMinutesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mult_txt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rails_chk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.engDataDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epicorMinutesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operations";
            // 
            // operation_cbo
            // 
            this.operation_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operation_cbo.FormattingEnabled = true;
            this.operation_cbo.Location = new System.Drawing.Point(12, 25);
            this.operation_cbo.Name = "operation_cbo";
            this.operation_cbo.Size = new System.Drawing.Size(211, 21);
            this.operation_cbo.TabIndex = 1;
            this.operation_cbo.SelectedIndexChanged += new System.EventHandler(this.operation_cbo_SelectedIndexChanged);
            // 
            // type_txt
            // 
            this.type_txt.Location = new System.Drawing.Point(12, 65);
            this.type_txt.Name = "type_txt";
            this.type_txt.ReadOnly = true;
            this.type_txt.Size = new System.Drawing.Size(211, 20);
            this.type_txt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Efficiency";
            // 
            // efficiency_txt
            // 
            this.efficiency_txt.Location = new System.Drawing.Point(12, 104);
            this.efficiency_txt.Name = "efficiency_txt";
            this.efficiency_txt.ReadOnly = true;
            this.efficiency_txt.Size = new System.Drawing.Size(53, 20);
            this.efficiency_txt.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Per";
            // 
            // per_txt
            // 
            this.per_txt.Location = new System.Drawing.Point(71, 104);
            this.per_txt.Name = "per_txt";
            this.per_txt.ReadOnly = true;
            this.per_txt.Size = new System.Drawing.Size(152, 20);
            this.per_txt.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Minutes/PC";
            // 
            // minpc_txt
            // 
            this.minpc_txt.Location = new System.Drawing.Point(12, 191);
            this.minpc_txt.Name = "minpc_txt";
            this.minpc_txt.ReadOnly = true;
            this.minpc_txt.Size = new System.Drawing.Size(211, 20);
            this.minpc_txt.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Seconds";
            // 
            // seconds_txt
            // 
            this.seconds_txt.Location = new System.Drawing.Point(12, 143);
            this.seconds_txt.Name = "seconds_txt";
            this.seconds_txt.Size = new System.Drawing.Size(53, 20);
            this.seconds_txt.TabIndex = 10;
            this.seconds_txt.TextChanged += new System.EventHandler(this.seconds_txt_TextChanged);
            // 
            // ok_btn
            // 
            this.ok_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_btn.Location = new System.Drawing.Point(251, 160);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 12;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(251, 189);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 13;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // epicorMinutesTableAdapter
            // 
            this.epicorMinutesTableAdapter.ClearBeforeFill = true;
            // 
            // engDataDataSet
            // 
            this.engDataDataSet.DataSetName = "ENGDataDataSet";
            this.engDataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // epicorMinutesBindingSource
            // 
            this.epicorMinutesBindingSource.DataMember = "EpicorMinutes";
            this.epicorMinutesBindingSource.DataSource = this.engDataDataSet;
            // 
            // mult_txt
            // 
            this.mult_txt.Location = new System.Drawing.Point(74, 143);
            this.mult_txt.Name = "mult_txt";
            this.mult_txt.Size = new System.Drawing.Size(53, 20);
            this.mult_txt.TabIndex = 14;
            this.mult_txt.Text = "1";
            this.mult_txt.TextChanged += new System.EventHandler(this.mult_txt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Multiplier";
            // 
            // rails_chk
            // 
            this.rails_chk.AutoSize = true;
            this.rails_chk.Location = new System.Drawing.Point(133, 145);
            this.rails_chk.Name = "rails_chk";
            this.rails_chk.Size = new System.Drawing.Size(49, 17);
            this.rails_chk.TabIndex = 16;
            this.rails_chk.Text = "Rails";
            this.rails_chk.UseVisualStyleBackColor = true;
            this.rails_chk.CheckedChanged += new System.EventHandler(this.rails_chk_CheckedChanged);
            // 
            // Operations_Minutes
            // 
            this.AcceptButton = this.ok_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(338, 224);
            this.Controls.Add(this.rails_chk);
            this.Controls.Add(this.mult_txt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.seconds_txt);
            this.Controls.Add(this.minpc_txt);
            this.Controls.Add(this.per_txt);
            this.Controls.Add(this.efficiency_txt);
            this.Controls.Add(this.type_txt);
            this.Controls.Add(this.operation_cbo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Operations_Minutes";
            this.Text = "Operations: Minutes per Operation";
            this.Load += new System.EventHandler(this.Operations_Minutes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.engDataDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epicorMinutesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox operation_cbo;
        private System.Windows.Forms.TextBox type_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox efficiency_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox per_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox minpc_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox seconds_txt;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
        private ENGDataDataSetTableAdapters.EpicorMinutesTableAdapter epicorMinutesTableAdapter;
        private ENGDataDataSet engDataDataSet;
        private System.Windows.Forms.BindingSource epicorMinutesBindingSource;
        private System.Windows.Forms.TextBox mult_txt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox rails_chk;
    }
}