namespace Epicor_Integration
{
    partial class Config_OpsMins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config_OpsMins));
            this.opminsGrid = new System.Windows.Forms.DataGridView();
            this.epicorMinutesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eNGDataDataSet = new Epicor_Integration.ENGDataDataSet();
            this.epicorMinutesTableAdapter = new Epicor_Integration.ENGDataDataSetTableAdapters.EpicorMinutesTableAdapter();
            this.add_btn = new System.Windows.Forms.Button();
            this.rem_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.type_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.operations_txt = new System.Windows.Forms.TextBox();
            this.efficiency_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.per_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.seconds_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mp_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.row_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ops_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.efficiency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.per = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.opminsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epicorMinutesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // opminsGrid
            // 
            this.opminsGrid.AllowUserToAddRows = false;
            this.opminsGrid.AllowUserToDeleteRows = false;
            this.opminsGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.opminsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.opminsGrid.AutoGenerateColumns = false;
            this.opminsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.opminsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.row_id,
            this.Type,
            this.ops_Name,
            this.efficiency,
            this.seconds,
            this.per,
            this.MP});
            this.opminsGrid.DataSource = this.epicorMinutesBindingSource;
            this.opminsGrid.Location = new System.Drawing.Point(12, 124);
            this.opminsGrid.Name = "opminsGrid";
            this.opminsGrid.ReadOnly = true;
            this.opminsGrid.RowHeadersVisible = false;
            this.opminsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.opminsGrid.Size = new System.Drawing.Size(453, 202);
            this.opminsGrid.TabIndex = 0;
            // 
            // epicorMinutesBindingSource
            // 
            this.epicorMinutesBindingSource.DataMember = "EpicorMinutes";
            this.epicorMinutesBindingSource.DataSource = this.eNGDataDataSet;
            // 
            // eNGDataDataSet
            // 
            this.eNGDataDataSet.DataSetName = "ENGDataDataSet";
            this.eNGDataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // epicorMinutesTableAdapter
            // 
            this.epicorMinutesTableAdapter.ClearBeforeFill = true;
            // 
            // add_btn
            // 
            this.add_btn.Location = new System.Drawing.Point(6, 19);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(75, 23);
            this.add_btn.TabIndex = 6;
            this.add_btn.Text = "Add";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // rem_btn
            // 
            this.rem_btn.Location = new System.Drawing.Point(6, 48);
            this.rem_btn.Name = "rem_btn";
            this.rem_btn.Size = new System.Drawing.Size(75, 23);
            this.rem_btn.TabIndex = 7;
            this.rem_btn.Text = "Remove";
            this.rem_btn.UseVisualStyleBackColor = true;
            this.rem_btn.Click += new System.EventHandler(this.rem_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(6, 77);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 8;
            this.save_btn.Text = "Save";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.add_btn);
            this.groupBox1.Controls.Add(this.save_btn);
            this.groupBox1.Controls.Add(this.rem_btn);
            this.groupBox1.Location = new System.Drawing.Point(376, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(89, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Type";
            // 
            // type_txt
            // 
            this.type_txt.Location = new System.Drawing.Point(12, 25);
            this.type_txt.Name = "type_txt";
            this.type_txt.Size = new System.Drawing.Size(177, 20);
            this.type_txt.TabIndex = 0;
            this.type_txt.Leave += new System.EventHandler(this.type_txt_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Operations";
            // 
            // operations_txt
            // 
            this.operations_txt.Location = new System.Drawing.Point(12, 64);
            this.operations_txt.Name = "operations_txt";
            this.operations_txt.Size = new System.Drawing.Size(177, 20);
            this.operations_txt.TabIndex = 3;
            this.operations_txt.Leave += new System.EventHandler(this.operations_txt_Leave);
            // 
            // efficiency_txt
            // 
            this.efficiency_txt.Location = new System.Drawing.Point(194, 25);
            this.efficiency_txt.Name = "efficiency_txt";
            this.efficiency_txt.Size = new System.Drawing.Size(52, 20);
            this.efficiency_txt.TabIndex = 1;
            this.efficiency_txt.Leave += new System.EventHandler(this.efficiency_txt_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Efficiency";
            // 
            // per_txt
            // 
            this.per_txt.Location = new System.Drawing.Point(252, 25);
            this.per_txt.Name = "per_txt";
            this.per_txt.Size = new System.Drawing.Size(118, 20);
            this.per_txt.TabIndex = 2;
            this.per_txt.Leave += new System.EventHandler(this.per_txt_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Per";
            // 
            // seconds_txt
            // 
            this.seconds_txt.Location = new System.Drawing.Point(195, 64);
            this.seconds_txt.Name = "seconds_txt";
            this.seconds_txt.Size = new System.Drawing.Size(52, 20);
            this.seconds_txt.TabIndex = 4;
            this.seconds_txt.Leave += new System.EventHandler(this.seconds_txt_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Seconds";
            // 
            // mp_txt
            // 
            this.mp_txt.Location = new System.Drawing.Point(252, 64);
            this.mp_txt.Name = "mp_txt";
            this.mp_txt.Size = new System.Drawing.Size(118, 20);
            this.mp_txt.TabIndex = 5;
            this.mp_txt.Leave += new System.EventHandler(this.mp_txt_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Minutes/PC";
            // 
            // row_id
            // 
            this.row_id.DataPropertyName = "row_id";
            this.row_id.HeaderText = "row_id";
            this.row_id.Name = "row_id";
            this.row_id.ReadOnly = true;
            this.row_id.Visible = false;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // ops_Name
            // 
            this.ops_Name.DataPropertyName = "Name";
            this.ops_Name.HeaderText = "Name";
            this.ops_Name.Name = "ops_Name";
            this.ops_Name.ReadOnly = true;
            // 
            // efficiency
            // 
            this.efficiency.DataPropertyName = "Efficiency";
            this.efficiency.HeaderText = "Efficiency";
            this.efficiency.Name = "efficiency";
            this.efficiency.ReadOnly = true;
            // 
            // seconds
            // 
            this.seconds.DataPropertyName = "Seconds";
            this.seconds.HeaderText = "Seconds";
            this.seconds.Name = "seconds";
            this.seconds.ReadOnly = true;
            // 
            // per
            // 
            this.per.DataPropertyName = "Per";
            this.per.HeaderText = "Per";
            this.per.Name = "per";
            this.per.ReadOnly = true;
            // 
            // MP
            // 
            this.MP.DataPropertyName = "MP";
            this.MP.HeaderText = "MP";
            this.MP.Name = "MP";
            this.MP.ReadOnly = true;
            // 
            // Config_OpsMins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 336);
            this.Controls.Add(this.mp_txt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.seconds_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.per_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.efficiency_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.operations_txt);
            this.Controls.Add(this.type_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opminsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config_OpsMins";
            this.Text = "Operation Minutes";
            this.Load += new System.EventHandler(this.Config_OpsMins_Load);
            ((System.ComponentModel.ISupportInitialize)(this.opminsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epicorMinutesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNGDataDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView opminsGrid;
        private ENGDataDataSet eNGDataDataSet;
        private System.Windows.Forms.BindingSource epicorMinutesBindingSource;
        private ENGDataDataSetTableAdapters.EpicorMinutesTableAdapter epicorMinutesTableAdapter;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button rem_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox type_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox operations_txt;
        private System.Windows.Forms.TextBox efficiency_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox per_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox seconds_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mp_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ops_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn efficiency;
        private System.Windows.Forms.DataGridViewTextBoxColumn seconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn per;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP;
    }
}