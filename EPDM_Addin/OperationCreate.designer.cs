namespace SWAddin
{
    partial class OperationCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationCreate));
            this.OpGrid = new System.Windows.Forms.DataGridView();
            this.Ops = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.wcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKER_AppDataSet = new SWAddin.DEKKER_AppDataSet();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Setup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.continuebtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.wcTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.wcTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.OpGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wcBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // OpGrid
            // 
            this.OpGrid.BackgroundColor = System.Drawing.Color.White;
            this.OpGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OpGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ops,
            this.WC,
            this.Hours,
            this.Setup});
            this.OpGrid.Location = new System.Drawing.Point(12, 12);
            this.OpGrid.Name = "OpGrid";
            this.OpGrid.Size = new System.Drawing.Size(410, 204);
            this.OpGrid.TabIndex = 0;
            // 
            // Ops
            // 
            this.Ops.HeaderText = "Operation:";
            this.Ops.Name = "Ops";
            this.Ops.ReadOnly = true;
            this.Ops.Width = 75;
            // 
            // WC
            // 
            this.WC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WC.DataSource = this.wcBindingSource;
            this.WC.DisplayMember = "wc";
            this.WC.FillWeight = 110F;
            this.WC.HeaderText = "WC:";
            this.WC.Name = "WC";
            this.WC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.WC.ValueMember = "wc";
            // 
            // wcBindingSource
            // 
            this.wcBindingSource.DataMember = "wc";
            this.wcBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // dEKKER_AppDataSet
            // 
            this.dEKKER_AppDataSet.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Hours
            // 
            this.Hours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Hours.HeaderText = "Hours:";
            this.Hours.Name = "Hours";
            // 
            // Setup
            // 
            this.Setup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Setup.HeaderText = "Setup:";
            this.Setup.Name = "Setup";
            // 
            // continuebtn
            // 
            this.continuebtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continuebtn.Location = new System.Drawing.Point(268, 222);
            this.continuebtn.Name = "continuebtn";
            this.continuebtn.Size = new System.Drawing.Size(75, 23);
            this.continuebtn.TabIndex = 1;
            this.continuebtn.Text = "Continue";
            this.continuebtn.UseVisualStyleBackColor = true;
            this.continuebtn.Click += new System.EventHandler(this.continuebtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(349, 222);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 2;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // wcTableAdapter
            // 
            this.wcTableAdapter.ClearBeforeFill = true;
            // 
            // OperationCreate
            // 
            this.AcceptButton = this.continuebtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(436, 255);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.continuebtn);
            this.Controls.Add(this.OpGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OperationCreate";
            this.Text = "Create Operations for:";
            this.Load += new System.EventHandler(this.OperationCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OpGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wcBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView OpGrid;
        private System.Windows.Forms.Button continuebtn;
        private System.Windows.Forms.Button cancelbtn;
        private DEKKER_AppDataSet dEKKER_AppDataSet;
        private System.Windows.Forms.BindingSource wcBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.wcTableAdapter wcTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ops;
        private System.Windows.Forms.DataGridViewComboBoxColumn WC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Setup;
    }
}