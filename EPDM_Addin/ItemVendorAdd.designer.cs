namespace SWAddin
{
    partial class ItemVendorAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemVendorAdd));
            this.label1 = new System.Windows.Forms.Label();
            this.vendorcbo = new System.Windows.Forms.ComboBox();
            this.vendaddrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKER_AppDataSet = new SWAddin.DEKKER_AppDataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.vendoritemtxt = new System.Windows.Forms.TextBox();
            this.u_mcbo = new System.Windows.Forms.ComboBox();
            this.umBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.leadtxt = new System.Windows.Forms.TextBox();
            this.Quotedate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.continuebtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.costtxt = new System.Windows.Forms.TextBox();
            this.vendaddrTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.vendaddrTableAdapter();
            this.u_mTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.u_mTableAdapter();
            this.itemvendTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.itemvendTableAdapter();
            this.itemvendpriceTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.itemvendpriceTableAdapter();
            this.quotedchk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.vendaddrBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.umBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor:";
            // 
            // vendorcbo
            // 
            this.vendorcbo.DataSource = this.vendaddrBindingSource;
            this.vendorcbo.DisplayMember = "name";
            this.vendorcbo.FormattingEnabled = true;
            this.vendorcbo.Location = new System.Drawing.Point(12, 25);
            this.vendorcbo.Name = "vendorcbo";
            this.vendorcbo.Size = new System.Drawing.Size(207, 21);
            this.vendorcbo.TabIndex = 1;
            this.vendorcbo.ValueMember = "vend_num";
            // 
            // vendaddrBindingSource
            // 
            this.vendaddrBindingSource.DataMember = "vendaddr";
            this.vendaddrBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // dEKKER_AppDataSet
            // 
            this.dEKKER_AppDataSet.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "U/M:";
            // 
            // vendoritemtxt
            // 
            this.vendoritemtxt.Location = new System.Drawing.Point(12, 65);
            this.vendoritemtxt.Name = "vendoritemtxt";
            this.vendoritemtxt.Size = new System.Drawing.Size(287, 20);
            this.vendoritemtxt.TabIndex = 3;
            // 
            // u_mcbo
            // 
            this.u_mcbo.DataSource = this.umBindingSource;
            this.u_mcbo.DisplayMember = "u_m";
            this.u_mcbo.FormattingEnabled = true;
            this.u_mcbo.Location = new System.Drawing.Point(225, 25);
            this.u_mcbo.Name = "u_mcbo";
            this.u_mcbo.Size = new System.Drawing.Size(74, 21);
            this.u_mcbo.TabIndex = 4;
            this.u_mcbo.ValueMember = "u_m";
            // 
            // umBindingSource
            // 
            this.umBindingSource.DataMember = "u_m";
            this.umBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Vendor Item:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Lead Time:";
            // 
            // leadtxt
            // 
            this.leadtxt.Enabled = false;
            this.leadtxt.Location = new System.Drawing.Point(12, 143);
            this.leadtxt.Name = "leadtxt";
            this.leadtxt.Size = new System.Drawing.Size(81, 20);
            this.leadtxt.TabIndex = 6;
            // 
            // Quotedate
            // 
            this.Quotedate.Enabled = false;
            this.Quotedate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Quotedate.Location = new System.Drawing.Point(192, 143);
            this.Quotedate.Name = "Quotedate";
            this.Quotedate.Size = new System.Drawing.Size(106, 20);
            this.Quotedate.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Quote Date:";
            // 
            // continuebtn
            // 
            this.continuebtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continuebtn.Location = new System.Drawing.Point(12, 169);
            this.continuebtn.Name = "continuebtn";
            this.continuebtn.Size = new System.Drawing.Size(75, 23);
            this.continuebtn.TabIndex = 10;
            this.continuebtn.Text = "Continue";
            this.continuebtn.UseVisualStyleBackColor = true;
            this.continuebtn.Click += new System.EventHandler(this.continuebtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(223, 169);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 11;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cost:";
            // 
            // costtxt
            // 
            this.costtxt.Location = new System.Drawing.Point(12, 104);
            this.costtxt.Name = "costtxt";
            this.costtxt.Size = new System.Drawing.Size(81, 20);
            this.costtxt.TabIndex = 12;
            // 
            // vendaddrTableAdapter
            // 
            this.vendaddrTableAdapter.ClearBeforeFill = true;
            // 
            // u_mTableAdapter
            // 
            this.u_mTableAdapter.ClearBeforeFill = true;
            // 
            // itemvendTableAdapter
            // 
            this.itemvendTableAdapter.ClearBeforeFill = true;
            // 
            // itemvendpriceTableAdapter
            // 
            this.itemvendpriceTableAdapter.ClearBeforeFill = true;
            // 
            // quotedchk
            // 
            this.quotedchk.AutoSize = true;
            this.quotedchk.Location = new System.Drawing.Point(192, 104);
            this.quotedchk.Name = "quotedchk";
            this.quotedchk.Size = new System.Drawing.Size(61, 17);
            this.quotedchk.TabIndex = 14;
            this.quotedchk.Text = "Quoted";
            this.quotedchk.UseVisualStyleBackColor = true;
            this.quotedchk.CheckedChanged += new System.EventHandler(this.quotedchk_CheckedChanged);
            // 
            // ItemVendorAdd
            // 
            this.AcceptButton = this.continuebtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(310, 205);
            this.Controls.Add(this.quotedchk);
            this.Controls.Add(this.costtxt);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.continuebtn);
            this.Controls.Add(this.Quotedate);
            this.Controls.Add(this.leadtxt);
            this.Controls.Add(this.u_mcbo);
            this.Controls.Add(this.vendoritemtxt);
            this.Controls.Add(this.vendorcbo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ItemVendorAdd";
            this.Text = "Add Vendor for:";
            this.Load += new System.EventHandler(this.ItemVendorAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vendaddrBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.umBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox vendorcbo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vendoritemtxt;
        private System.Windows.Forms.ComboBox u_mcbo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox leadtxt;
        private System.Windows.Forms.DateTimePicker Quotedate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button continuebtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox costtxt;
        private DEKKER_AppDataSet dEKKER_AppDataSet;
        private System.Windows.Forms.BindingSource vendaddrBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.vendaddrTableAdapter vendaddrTableAdapter;
        private System.Windows.Forms.BindingSource umBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.u_mTableAdapter u_mTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.itemvendTableAdapter itemvendTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.itemvendpriceTableAdapter itemvendpriceTableAdapter;
        private System.Windows.Forms.CheckBox quotedchk;
    }
}