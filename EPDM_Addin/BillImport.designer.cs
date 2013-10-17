namespace SWAddin
{
    partial class BillImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillImport));
            this.WorkingGrid = new System.Windows.Forms.DataGridView();
            this.jobmatlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKERAppDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKER_AppDataSet = new SWAddin.DEKKER_AppDataSet();
            this.confirmbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.jobBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jobmatlTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jobmatlTableAdapter();
            this.jobTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jobTableAdapter();
            this.itemTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.itemTableAdapter();
            this.dEKKER_AppDataSet1 = new SWAddin.DEKKER_AppDataSet();
            this.jobrouteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jobrouteTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jobrouteTableAdapter();
            this.jrt_schTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jrt_schTableAdapter();
            this.jrtresourcegroupTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jrtresourcegroupTableAdapter();
            this.wcTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.wcTableAdapter();
            this.sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matl_qty_conv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_m = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.WorkingGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobmatlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKERAppDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobrouteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkingGrid
            // 
            this.WorkingGrid.AllowUserToDeleteRows = false;
            this.WorkingGrid.BackgroundColor = System.Drawing.Color.White;
            this.WorkingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WorkingGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sequence,
            this.item,
            this.matl_qty_conv,
            this.Sqty,
            this.Description,
            this.u_m});
            this.WorkingGrid.Location = new System.Drawing.Point(12, 12);
            this.WorkingGrid.MultiSelect = false;
            this.WorkingGrid.Name = "WorkingGrid";
            this.WorkingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WorkingGrid.Size = new System.Drawing.Size(651, 269);
            this.WorkingGrid.TabIndex = 0;
            // 
            // jobmatlBindingSource
            // 
            this.jobmatlBindingSource.DataSource = this.dEKKERAppDataSetBindingSource;
            // 
            // dEKKERAppDataSetBindingSource
            // 
            this.dEKKERAppDataSetBindingSource.DataMember = "wc";
            this.dEKKERAppDataSetBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // dEKKER_AppDataSet
            // 
            this.dEKKER_AppDataSet.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // confirmbtn
            // 
            this.confirmbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.confirmbtn.Location = new System.Drawing.Point(453, 287);
            this.confirmbtn.Name = "confirmbtn";
            this.confirmbtn.Size = new System.Drawing.Size(102, 23);
            this.confirmbtn.TabIndex = 1;
            this.confirmbtn.Text = "Confirm Changes";
            this.confirmbtn.UseVisualStyleBackColor = true;
            this.confirmbtn.Click += new System.EventHandler(this.confirmbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(561, 287);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(102, 23);
            this.cancelbtn.TabIndex = 2;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // jobBindingSource
            // 
            this.jobBindingSource.DataSource = this.dEKKERAppDataSetBindingSource;
            // 
            // jobmatlTableAdapter
            // 
            this.jobmatlTableAdapter.ClearBeforeFill = true;
            // 
            // jobTableAdapter
            // 
            this.jobTableAdapter.ClearBeforeFill = true;
            // 
            // itemTableAdapter
            // 
            this.itemTableAdapter.ClearBeforeFill = true;
            // 
            // dEKKER_AppDataSet1
            // 
            this.dEKKER_AppDataSet1.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // jobrouteBindingSource
            // 
            this.jobrouteBindingSource.DataMember = "jobroute";
            this.jobrouteBindingSource.DataSource = this.dEKKER_AppDataSet1;
            // 
            // jobrouteTableAdapter
            // 
            this.jobrouteTableAdapter.ClearBeforeFill = true;
            // 
            // jrt_schTableAdapter
            // 
            this.jrt_schTableAdapter.ClearBeforeFill = true;
            // 
            // jrtresourcegroupTableAdapter
            // 
            this.jrtresourcegroupTableAdapter.ClearBeforeFill = true;
            // 
            // wcTableAdapter
            // 
            this.wcTableAdapter.ClearBeforeFill = true;
            // 
            // sequence
            // 
            this.sequence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sequence.DataPropertyName = "sequence";
            this.sequence.FillWeight = 30F;
            this.sequence.HeaderText = "";
            this.sequence.Name = "sequence";
            this.sequence.ReadOnly = true;
            // 
            // item
            // 
            this.item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item.DataPropertyName = "item";
            this.item.FillWeight = 90F;
            this.item.HeaderText = "Item";
            this.item.Name = "item";
            // 
            // matl_qty_conv
            // 
            this.matl_qty_conv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.matl_qty_conv.DataPropertyName = "matl_qty_conv";
            this.matl_qty_conv.FillWeight = 50F;
            this.matl_qty_conv.HeaderText = "Qty";
            this.matl_qty_conv.Name = "matl_qty_conv";
            // 
            // Sqty
            // 
            this.Sqty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sqty.FillWeight = 60F;
            this.Sqty.HeaderText = "System Qty";
            this.Sqty.Name = "Sqty";
            this.Sqty.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.FillWeight = 120F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // u_m
            // 
            this.u_m.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.u_m.DataPropertyName = "u_m";
            this.u_m.FillWeight = 45F;
            this.u_m.HeaderText = "U/M";
            this.u_m.Name = "u_m";
            this.u_m.ReadOnly = true;
            // 
            // BillImport
            // 
            this.AcceptButton = this.confirmbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(675, 322);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.confirmbtn);
            this.Controls.Add(this.WorkingGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(691, 360);
            this.Name = "BillImport";
            this.Text = "Importing Bill for:";
            this.Load += new System.EventHandler(this.BillImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WorkingGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobmatlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKERAppDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobrouteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView WorkingGrid;
        private System.Windows.Forms.Button confirmbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.BindingSource dEKKERAppDataSetBindingSource;
        private DEKKER_AppDataSet dEKKER_AppDataSet;
        private System.Windows.Forms.BindingSource jobmatlBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jobmatlTableAdapter jobmatlTableAdapter;
        private System.Windows.Forms.BindingSource jobBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jobTableAdapter jobTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.itemTableAdapter itemTableAdapter;
        private DEKKER_AppDataSet dEKKER_AppDataSet1;
        private System.Windows.Forms.BindingSource jobrouteBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jobrouteTableAdapter jobrouteTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jrt_schTableAdapter jrt_schTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jrtresourcegroupTableAdapter jrtresourcegroupTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.wcTableAdapter wcTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn matl_qty_conv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_m;
    }
}