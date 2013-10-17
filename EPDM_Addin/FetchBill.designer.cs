namespace SWAddin
{
    partial class FetchBill
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
            this.RefGrid = new System.Windows.Forms.DataGridView();
            this.jobmatlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEKKER_AppDataSet = new SWAddin.DEKKER_AppDataSet();
            this.jobmatlTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jobmatlTableAdapter();
            this.jobTableAdapter = new SWAddin.DEKKER_AppDataSetTableAdapters.jobTableAdapter();
            this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matl_qty_conv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_m = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RefGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobmatlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // RefGrid
            // 
            this.RefGrid.AllowUserToAddRows = false;
            this.RefGrid.AllowUserToDeleteRows = false;
            this.RefGrid.AutoGenerateColumns = false;
            this.RefGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RefGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item,
            this.matl_qty_conv,
            this.u_m,
            this.sequence});
            this.RefGrid.DataSource = this.jobmatlBindingSource;
            this.RefGrid.Location = new System.Drawing.Point(12, 12);
            this.RefGrid.Name = "RefGrid";
            this.RefGrid.ReadOnly = true;
            this.RefGrid.Size = new System.Drawing.Size(450, 238);
            this.RefGrid.TabIndex = 0;
            // 
            // jobmatlBindingSource
            // 
            this.jobmatlBindingSource.DataMember = "jobmatl";
            this.jobmatlBindingSource.DataSource = this.dEKKER_AppDataSet;
            // 
            // dEKKER_AppDataSet
            // 
            this.dEKKER_AppDataSet.DataSetName = "DEKKER_AppDataSet";
            this.dEKKER_AppDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // jobmatlTableAdapter
            // 
            this.jobmatlTableAdapter.ClearBeforeFill = true;
            // 
            // jobTableAdapter
            // 
            this.jobTableAdapter.ClearBeforeFill = true;
            // 
            // item
            // 
            this.item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item.DataPropertyName = "item";
            this.item.HeaderText = "item";
            this.item.Name = "item";
            this.item.ReadOnly = true;
            // 
            // matl_qty_conv
            // 
            this.matl_qty_conv.DataPropertyName = "matl_qty_conv";
            this.matl_qty_conv.HeaderText = "matl_qty_conv";
            this.matl_qty_conv.Name = "matl_qty_conv";
            this.matl_qty_conv.ReadOnly = true;
            // 
            // u_m
            // 
            this.u_m.DataPropertyName = "u_m";
            this.u_m.HeaderText = "u_m";
            this.u_m.Name = "u_m";
            this.u_m.ReadOnly = true;
            // 
            // sequence
            // 
            this.sequence.DataPropertyName = "sequence";
            this.sequence.HeaderText = "sequence";
            this.sequence.Name = "sequence";
            this.sequence.ReadOnly = true;
            // 
            // FetchBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 262);
            this.Controls.Add(this.RefGrid);
            this.Name = "FetchBill";
            this.Text = "FetchBill";
            this.Load += new System.EventHandler(this.FetchBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RefGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobmatlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEKKER_AppDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView RefGrid;
        private DEKKER_AppDataSet dEKKER_AppDataSet;
        private System.Windows.Forms.BindingSource jobmatlBindingSource;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jobmatlTableAdapter jobmatlTableAdapter;
        private SWAddin.DEKKER_AppDataSetTableAdapters.jobTableAdapter jobTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn matl_qty_conv;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_m;
        private System.Windows.Forms.DataGridViewTextBoxColumn sequence;
    }
}