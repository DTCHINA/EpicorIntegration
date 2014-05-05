namespace Epicor_Integration
{
    partial class Item_SheetFactor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Item_SheetFactor));
            this.partnumber_cbo = new System.Windows.Forms.ComboBox();
            this.sheetCoilUsageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eNGDataDataSet = new Epicor_Integration.ENGDataDataSet();
            this.gauge_cbo = new System.Windows.Forms.ComboBox();
            this.gaugeRefBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.width_cbo = new System.Windows.Forms.ComboBox();
            this.density_cbo = new System.Windows.Forms.ComboBox();
            this.length_cbo = new System.Windows.Forms.ComboBox();
            this.type_cbo = new System.Windows.Forms.ComboBox();
            this.material_cbo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.weight_num = new System.Windows.Forms.NumericUpDown();
            this.sheetcoilinput_lbl = new System.Windows.Forms.Label();
            this.factor_txt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.factoredweight_txt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reset_btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.accept_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.gaugeRefTableAdapter = new Epicor_Integration.ENGDataDataSetTableAdapters.GaugeRefTableAdapter();
            this.sheetCoil_UsageTableAdapter = new Epicor_Integration.ENGDataDataSetTableAdapters.SheetCoil_UsageTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sheetCoilUsageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaugeRefBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weight_num)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // partnumber_cbo
            // 
            this.partnumber_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partnumber_cbo.FormattingEnabled = true;
            this.partnumber_cbo.Location = new System.Drawing.Point(6, 32);
            this.partnumber_cbo.Name = "partnumber_cbo";
            this.partnumber_cbo.Size = new System.Drawing.Size(121, 21);
            this.partnumber_cbo.TabIndex = 0;
            this.partnumber_cbo.SelectedIndexChanged += new System.EventHandler(this.partnumber_cbo_SelectedIndexChanged);
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
            // gauge_cbo
            // 
            this.gauge_cbo.BackColor = System.Drawing.Color.White;
            this.gauge_cbo.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.gaugeRefBindingSource, "gauge", true));
            this.gauge_cbo.DataSource = this.gaugeRefBindingSource;
            this.gauge_cbo.DisplayMember = "gauge";
            this.gauge_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.gauge_cbo.Enabled = false;
            this.gauge_cbo.FormattingEnabled = true;
            this.gauge_cbo.Location = new System.Drawing.Point(6, 112);
            this.gauge_cbo.Name = "gauge_cbo";
            this.gauge_cbo.Size = new System.Drawing.Size(121, 20);
            this.gauge_cbo.TabIndex = 1;
            this.gauge_cbo.ValueMember = "nominal";
            // 
            // gaugeRefBindingSource
            // 
            this.gaugeRefBindingSource.DataMember = "GaugeRef";
            this.gaugeRefBindingSource.DataSource = this.eNGDataDataSet;
            // 
            // width_cbo
            // 
            this.width_cbo.BackColor = System.Drawing.Color.White;
            this.width_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.width_cbo.Enabled = false;
            this.width_cbo.FormattingEnabled = true;
            this.width_cbo.Location = new System.Drawing.Point(133, 72);
            this.width_cbo.Name = "width_cbo";
            this.width_cbo.Size = new System.Drawing.Size(121, 20);
            this.width_cbo.TabIndex = 2;
            // 
            // density_cbo
            // 
            this.density_cbo.BackColor = System.Drawing.Color.White;
            this.density_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.density_cbo.Enabled = false;
            this.density_cbo.FormattingEnabled = true;
            this.density_cbo.Location = new System.Drawing.Point(6, 152);
            this.density_cbo.Name = "density_cbo";
            this.density_cbo.Size = new System.Drawing.Size(121, 20);
            this.density_cbo.TabIndex = 3;
            // 
            // length_cbo
            // 
            this.length_cbo.BackColor = System.Drawing.Color.White;
            this.length_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.length_cbo.Enabled = false;
            this.length_cbo.FormattingEnabled = true;
            this.length_cbo.Location = new System.Drawing.Point(133, 112);
            this.length_cbo.Name = "length_cbo";
            this.length_cbo.Size = new System.Drawing.Size(121, 20);
            this.length_cbo.TabIndex = 4;
            // 
            // type_cbo
            // 
            this.type_cbo.BackColor = System.Drawing.Color.White;
            this.type_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.type_cbo.Enabled = false;
            this.type_cbo.FormattingEnabled = true;
            this.type_cbo.Location = new System.Drawing.Point(133, 32);
            this.type_cbo.Name = "type_cbo";
            this.type_cbo.Size = new System.Drawing.Size(121, 20);
            this.type_cbo.TabIndex = 5;
            // 
            // material_cbo
            // 
            this.material_cbo.BackColor = System.Drawing.Color.White;
            this.material_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.material_cbo.Enabled = false;
            this.material_cbo.FormattingEnabled = true;
            this.material_cbo.Location = new System.Drawing.Point(6, 72);
            this.material_cbo.Name = "material_cbo";
            this.material_cbo.Size = new System.Drawing.Size(121, 20);
            this.material_cbo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Part Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Material";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Gauge";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Length";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Density";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Type";
            // 
            // weight_num
            // 
            this.weight_num.DecimalPlaces = 4;
            this.weight_num.Location = new System.Drawing.Point(6, 32);
            this.weight_num.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            458752});
            this.weight_num.Name = "weight_num";
            this.weight_num.Size = new System.Drawing.Size(86, 20);
            this.weight_num.TabIndex = 14;
            // 
            // sheetcoilinput_lbl
            // 
            this.sheetcoilinput_lbl.AutoSize = true;
            this.sheetcoilinput_lbl.Location = new System.Drawing.Point(6, 16);
            this.sheetcoilinput_lbl.Name = "sheetcoilinput_lbl";
            this.sheetcoilinput_lbl.Size = new System.Drawing.Size(73, 13);
            this.sheetcoilinput_lbl.TabIndex = 15;
            this.sheetcoilinput_lbl.Text = "Qty Per Sheet";
            // 
            // factor_txt
            // 
            this.factor_txt.Location = new System.Drawing.Point(133, 32);
            this.factor_txt.Name = "factor_txt";
            this.factor_txt.ReadOnly = true;
            this.factor_txt.Size = new System.Drawing.Size(100, 20);
            this.factor_txt.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Factor";
            // 
            // factoredweight_txt
            // 
            this.factoredweight_txt.Location = new System.Drawing.Point(133, 71);
            this.factoredweight_txt.Name = "factoredweight_txt";
            this.factoredweight_txt.ReadOnly = true;
            this.factoredweight_txt.Size = new System.Drawing.Size(100, 20);
            this.factoredweight_txt.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(133, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Factored Weight";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reset_btn);
            this.groupBox1.Controls.Add(this.partnumber_cbo);
            this.groupBox1.Controls.Add(this.gauge_cbo);
            this.groupBox1.Controls.Add(this.density_cbo);
            this.groupBox1.Controls.Add(this.length_cbo);
            this.groupBox1.Controls.Add(this.width_cbo);
            this.groupBox1.Controls.Add(this.type_cbo);
            this.groupBox1.Controls.Add(this.material_cbo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 189);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // reset_btn
            // 
            this.reset_btn.Location = new System.Drawing.Point(133, 150);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(121, 23);
            this.reset_btn.TabIndex = 14;
            this.reset_btn.Text = "Reset";
            this.reset_btn.UseVisualStyleBackColor = true;
            this.reset_btn.Click += new System.EventHandler(this.reset_btn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.weight_num);
            this.groupBox2.Controls.Add(this.factor_txt);
            this.groupBox2.Controls.Add(this.factoredweight_txt);
            this.groupBox2.Controls.Add(this.sheetcoilinput_lbl);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(12, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 104);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // accept_btn
            // 
            this.accept_btn.Location = new System.Drawing.Point(123, 317);
            this.accept_btn.Name = "accept_btn";
            this.accept_btn.Size = new System.Drawing.Size(75, 23);
            this.accept_btn.TabIndex = 22;
            this.accept_btn.Text = "Accept";
            this.accept_btn.UseVisualStyleBackColor = true;
            this.accept_btn.Click += new System.EventHandler(this.accept_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(204, 317);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 23;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // gaugeRefTableAdapter
            // 
            this.gaugeRefTableAdapter.ClearBeforeFill = true;
            // 
            // sheetCoil_UsageTableAdapter
            // 
            this.sheetCoil_UsageTableAdapter.ClearBeforeFill = true;
            // 
            // Item_SheetFactor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(290, 363);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.accept_btn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Item_SheetFactor";
            this.Text = "Sheet/Coil Usage";
            this.Load += new System.EventHandler(this.Operations_SheetFactor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sheetCoilUsageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaugeRefBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weight_num)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ENGDataDataSet eNGDataDataSet;
        private ENGDataDataSetTableAdapters.GaugeRefTableAdapter gaugeRefTableAdapter;
        private ENGDataDataSetTableAdapters.SheetCoil_UsageTableAdapter sheetCoil_UsageTableAdapter;
        private System.Windows.Forms.ComboBox partnumber_cbo;
        private System.Windows.Forms.ComboBox gauge_cbo;
        private System.Windows.Forms.ComboBox width_cbo;
        private System.Windows.Forms.ComboBox density_cbo;
        private System.Windows.Forms.ComboBox length_cbo;
        private System.Windows.Forms.ComboBox type_cbo;
        private System.Windows.Forms.ComboBox material_cbo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown weight_num;
        private System.Windows.Forms.Label sheetcoilinput_lbl;
        private System.Windows.Forms.TextBox factor_txt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox factoredweight_txt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button accept_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.BindingSource sheetCoilUsageBindingSource;
        private System.Windows.Forms.BindingSource gaugeRefBindingSource;
        private System.Windows.Forms.Button reset_btn;
    }
}