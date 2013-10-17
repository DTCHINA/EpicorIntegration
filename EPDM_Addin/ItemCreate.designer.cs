namespace SWAddin
{
    partial class ItemCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemCreate));
            this.itemtxt = new System.Windows.Forms.TextBox();
            this.desctxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.drawnumtxt = new System.Windows.Forms.TextBox();
            this.revtrack = new System.Windows.Forms.CheckBox();
            this.ecn = new System.Windows.Forms.CheckBox();
            this.umcbo = new System.Windows.Forms.ComboBox();
            this.umBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKER_AppDataSet = new SWAddin.DEKKER_AppDataSet();
            this.dEKKERAppDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.typecbo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.prodcodecbo = new System.Windows.Forms.ComboBox();
            this.prodcodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.sourcecbo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.costmethodcbo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.costtypecbo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.overviewtxt = new System.Windows.Forms.TextBox();
            this.phantom = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.familycodecbo = new System.Windows.Forms.ComboBox();
            this.famcodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sntrack = new System.Windows.Forms.CheckBox();
            this.assignsn = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.weighttxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lead = new System.Windows.Forms.TextBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.continuebtn = new System.Windows.Forms.Button();
            this.u_mTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.u_mTableAdapter();
            this.prodcodeTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.prodcodeTableAdapter();
            this.label14 = new System.Windows.Forms.Label();
            this.abccbo = new System.Windows.Forms.ComboBox();
            this.famcodeTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.famcodeTableAdapter();
            this.itemTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.itemTableAdapter();
            this.itemvendTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.itemvendTableAdapter();
            //this.m2MDATA01DataSet = new SWAddin.M2MDATA01DataSet();
            //this.inmastTableAdapter = new SWAddin.M2MDATA01DataSetTableAdapters.inmastTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.umBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKERAppDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodcodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.famcodeBindingSource)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.m2MDATA01DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // itemtxt
            // 
            this.itemtxt.Location = new System.Drawing.Point(12, 25);
            this.itemtxt.Name = "itemtxt";
            this.itemtxt.Size = new System.Drawing.Size(238, 20);
            this.itemtxt.TabIndex = 0;
            // 
            // desctxt
            // 
            this.desctxt.Location = new System.Drawing.Point(12, 64);
            this.desctxt.Name = "desctxt";
            this.desctxt.Size = new System.Drawing.Size(336, 20);
            this.desctxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Item:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Drawing Number:";
            // 
            // drawnumtxt
            // 
            this.drawnumtxt.Location = new System.Drawing.Point(12, 103);
            this.drawnumtxt.Name = "drawnumtxt";
            this.drawnumtxt.Size = new System.Drawing.Size(132, 20);
            this.drawnumtxt.TabIndex = 4;
            // 
            // revtrack
            // 
            this.revtrack.AutoSize = true;
            this.revtrack.Location = new System.Drawing.Point(250, 90);
            this.revtrack.Name = "revtrack";
            this.revtrack.Size = new System.Drawing.Size(98, 17);
            this.revtrack.TabIndex = 6;
            this.revtrack.Text = "Revision Track";
            this.revtrack.UseVisualStyleBackColor = true;
            // 
            // ecn
            // 
            this.ecn.AutoSize = true;
            this.ecn.Location = new System.Drawing.Point(250, 113);
            this.ecn.Name = "ecn";
            this.ecn.Size = new System.Drawing.Size(48, 17);
            this.ecn.TabIndex = 7;
            this.ecn.Text = "ECN";
            this.ecn.UseVisualStyleBackColor = true;
            // 
            // umcbo
            // 
            this.umcbo.DataSource = this.umBindingSource;
            this.umcbo.DisplayMember = "u_m";
            this.umcbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.umcbo.FormattingEnabled = true;
            this.umcbo.Location = new System.Drawing.Point(367, 25);
            this.umcbo.Name = "umcbo";
            this.umcbo.Size = new System.Drawing.Size(155, 21);
            this.umcbo.TabIndex = 8;
            this.umcbo.ValueMember = "u_m";
            // 
            // umBindingSource
            // 
            this.umBindingSource.DataMember = "u_m";
            this.umBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // dEKKER_AppDataSet
            // 
            this.dEKKER_AppDataSet.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dEKKERAppDataSetBindingSource
            // 
            this.dEKKERAppDataSetBindingSource.DataMember = "itemvend";
            this.dEKKERAppDataSetBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "U/M:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type:";
            // 
            // typecbo
            // 
            this.typecbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typecbo.FormattingEnabled = true;
            this.typecbo.Items.AddRange(new object[] {
            "Material",
            "Tool",
            "Fixture",
            "Other"});
            this.typecbo.Location = new System.Drawing.Point(367, 65);
            this.typecbo.Name = "typecbo";
            this.typecbo.Size = new System.Drawing.Size(155, 21);
            this.typecbo.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(366, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Product Code:";
            // 
            // prodcodecbo
            // 
            this.prodcodecbo.DataSource = this.prodcodeBindingSource;
            this.prodcodecbo.DisplayMember = "description";
            this.prodcodecbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prodcodecbo.FormattingEnabled = true;
            this.prodcodecbo.Location = new System.Drawing.Point(367, 145);
            this.prodcodecbo.Name = "prodcodecbo";
            this.prodcodecbo.Size = new System.Drawing.Size(155, 21);
            this.prodcodecbo.TabIndex = 14;
            this.prodcodecbo.ValueMember = "product_code";
            // 
            // prodcodeBindingSource
            // 
            this.prodcodeBindingSource.DataMember = "prodcode";
            this.prodcodeBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(366, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Source:";
            // 
            // sourcecbo
            // 
            this.sourcecbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourcecbo.FormattingEnabled = true;
            this.sourcecbo.Items.AddRange(new object[] {
            "Purchased",
            "Manufactured",
            "Transferred"});
            this.sourcecbo.Location = new System.Drawing.Point(367, 105);
            this.sourcecbo.Name = "sourcecbo";
            this.sourcecbo.Size = new System.Drawing.Size(155, 21);
            this.sourcecbo.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(364, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Cost Method:";
            // 
            // costmethodcbo
            // 
            this.costmethodcbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.costmethodcbo.FormattingEnabled = true;
            this.costmethodcbo.Items.AddRange(new object[] {
            "Specific",
            "FIFO",
            "Standard",
            "Average",
            "LIFO"});
            this.costmethodcbo.Location = new System.Drawing.Point(365, 265);
            this.costmethodcbo.Name = "costmethodcbo";
            this.costmethodcbo.Size = new System.Drawing.Size(155, 21);
            this.costmethodcbo.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(364, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Cost Type:";
            // 
            // costtypecbo
            // 
            this.costtypecbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.costtypecbo.Enabled = false;
            this.costtypecbo.FormattingEnabled = true;
            this.costtypecbo.Items.AddRange(new object[] {
            "Actual",
            "Standard"});
            this.costtypecbo.Location = new System.Drawing.Point(365, 225);
            this.costtypecbo.Name = "costtypecbo";
            this.costtypecbo.Size = new System.Drawing.Size(155, 21);
            this.costtypecbo.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Overview";
            // 
            // overviewtxt
            // 
            this.overviewtxt.Location = new System.Drawing.Point(15, 146);
            this.overviewtxt.Multiline = true;
            this.overviewtxt.Name = "overviewtxt";
            this.overviewtxt.Size = new System.Drawing.Size(333, 100);
            this.overviewtxt.TabIndex = 21;
            // 
            // phantom
            // 
            this.phantom.AutoSize = true;
            this.phantom.Location = new System.Drawing.Point(15, 265);
            this.phantom.Name = "phantom";
            this.phantom.Size = new System.Drawing.Size(91, 17);
            this.phantom.TabIndex = 22;
            this.phantom.Text = "Phantom Flag";
            this.phantom.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(364, 289);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Family Code:";
            // 
            // familycodecbo
            // 
            this.familycodecbo.DataSource = this.famcodeBindingSource;
            this.familycodecbo.DisplayMember = "family_code";
            this.familycodecbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.familycodecbo.FormattingEnabled = true;
            this.familycodecbo.Location = new System.Drawing.Point(365, 305);
            this.familycodecbo.Name = "familycodecbo";
            this.familycodecbo.Size = new System.Drawing.Size(155, 21);
            this.familycodecbo.TabIndex = 23;
            this.familycodecbo.ValueMember = "family_code";
            // 
            // famcodeBindingSource
            // 
            this.famcodeBindingSource.DataMember = "famcode";
            this.famcodeBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // sntrack
            // 
            this.sntrack.AutoSize = true;
            this.sntrack.Location = new System.Drawing.Point(15, 288);
            this.sntrack.Name = "sntrack";
            this.sntrack.Size = new System.Drawing.Size(77, 17);
            this.sntrack.TabIndex = 25;
            this.sntrack.Text = "S/N Track";
            this.sntrack.UseVisualStyleBackColor = true;
            // 
            // assignsn
            // 
            this.assignsn.AutoSize = true;
            this.assignsn.Location = new System.Drawing.Point(15, 311);
            this.assignsn.Name = "assignsn";
            this.assignsn.Size = new System.Drawing.Size(106, 17);
            this.assignsn.TabIndex = 26;
            this.assignsn.Text = "Preassign Serials";
            this.assignsn.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(253, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Weight:";
            // 
            // weighttxt
            // 
            this.weighttxt.Location = new System.Drawing.Point(256, 25);
            this.weighttxt.Name = "weighttxt";
            this.weighttxt.Size = new System.Drawing.Size(92, 20);
            this.weighttxt.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 249);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Fixed Lead:";
            // 
            // lead
            // 
            this.lead.Location = new System.Drawing.Point(256, 265);
            this.lead.Name = "lead";
            this.lead.Size = new System.Drawing.Size(92, 20);
            this.lead.TabIndex = 29;
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(456, 346);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 31;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // continuebtn
            // 
            this.continuebtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continuebtn.Location = new System.Drawing.Point(375, 346);
            this.continuebtn.Name = "continuebtn";
            this.continuebtn.Size = new System.Drawing.Size(75, 23);
            this.continuebtn.TabIndex = 32;
            this.continuebtn.Text = "Continue";
            this.continuebtn.UseVisualStyleBackColor = true;
            this.continuebtn.Click += new System.EventHandler(this.continuebtn_Click);
            // 
            // u_mTableAdapter
            // 
            this.u_mTableAdapter.ClearBeforeFill = true;
            // 
            // prodcodeTableAdapter
            // 
            this.prodcodeTableAdapter.ClearBeforeFill = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(366, 169);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "ABC Code:";
            // 
            // abccbo
            // 
            this.abccbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.abccbo.FormattingEnabled = true;
            this.abccbo.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.abccbo.Location = new System.Drawing.Point(367, 185);
            this.abccbo.Name = "abccbo";
            this.abccbo.Size = new System.Drawing.Size(155, 21);
            this.abccbo.TabIndex = 33;
            // 
            // famcodeTableAdapter
            // 
            this.famcodeTableAdapter.ClearBeforeFill = true;
            // 
            // itemTableAdapter
            // 
            this.itemTableAdapter.ClearBeforeFill = true;
            // 
            // itemvendTableAdapter
            // 
            this.itemvendTableAdapter.ClearBeforeFill = true;
            // 
            // m2MDATA01DataSet
            // 
            //this.m2MDATA01DataSet.DataSetName = "M2MDATA01DataSet";
            //this.m2MDATA01DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // inmastTableAdapter
            // 
            //this.inmastTableAdapter.ClearBeforeFill = true;
            // 
            // ItemCreate
            // 
            this.AcceptButton = this.continuebtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(543, 381);
            this.Controls.Add(this.abccbo);
            this.Controls.Add(this.continuebtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.lead);
            this.Controls.Add(this.weighttxt);
            this.Controls.Add(this.assignsn);
            this.Controls.Add(this.sntrack);
            this.Controls.Add(this.familycodecbo);
            this.Controls.Add(this.phantom);
            this.Controls.Add(this.overviewtxt);
            this.Controls.Add(this.costmethodcbo);
            this.Controls.Add(this.costtypecbo);
            this.Controls.Add(this.prodcodecbo);
            this.Controls.Add(this.sourcecbo);
            this.Controls.Add(this.typecbo);
            this.Controls.Add(this.umcbo);
            this.Controls.Add(this.ecn);
            this.Controls.Add(this.revtrack);
            this.Controls.Add(this.drawnumtxt);
            this.Controls.Add(this.desctxt);
            this.Controls.Add(this.itemtxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ItemCreate";
            this.Text = "Enter Item for: ";
            this.Load += new System.EventHandler(this.ItemCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.umBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKERAppDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodcodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.famcodeBindingSource)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.m2MDATA01DataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox itemtxt;
        private System.Windows.Forms.TextBox desctxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox drawnumtxt;
        private System.Windows.Forms.CheckBox revtrack;
        private System.Windows.Forms.CheckBox ecn;
        private System.Windows.Forms.ComboBox umcbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox typecbo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox prodcodecbo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox sourcecbo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox costmethodcbo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox costtypecbo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox overviewtxt;
        private System.Windows.Forms.CheckBox phantom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox familycodecbo;
        private System.Windows.Forms.CheckBox sntrack;
        private System.Windows.Forms.CheckBox assignsn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox weighttxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox lead;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button continuebtn;
        private System.Windows.Forms.BindingSource dEKKERAppDataSetBindingSource;
        private DEKKER_AppDataSet dEKKER_AppDataSet;
        private System.Windows.Forms.BindingSource umBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.u_mTableAdapter u_mTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.prodcodeTableAdapter prodcodeTableAdapter;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox abccbo;
        private SWAddin.DEKKER_AppDataSetTableAdapters.famcodeTableAdapter famcodeTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.itemTableAdapter itemTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.itemvendTableAdapter itemvendTableAdapter;
        private System.Windows.Forms.BindingSource prodcodeBindingSource;
        private System.Windows.Forms.BindingSource famcodeBindingSource;
        //private M2MDATA01DataSet m2MDATA01DataSet;
        //private SWAddin.M2MDATA01DataSetTableAdapters.inmastTableAdapter inmastTableAdapter;
    }
}