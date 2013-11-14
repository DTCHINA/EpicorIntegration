namespace JobStatusByPlanner
{
    partial class main
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PersonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RevisionNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReqDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = global::JobStatusByPlanner.Properties.Settings.Default.dsAGC;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PersonID,
            this.jobnum,
            this.partnum,
            this.RevisionNum,
            this.PartDescription,
            this.StartDate,
            this.DueDate,
            this.ReqDueDate});
            this.dataGridView1.DataBindings.Add(new System.Windows.Forms.Binding("AutoGenerateColumns", global::JobStatusByPlanner.Properties.Settings.Default, "dsAGC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(727, 409);
            this.dataGridView1.TabIndex = 0;
            // 
            // PersonID
            // 
            this.PersonID.DataPropertyName = "PersonID";
            this.PersonID.HeaderText = "Planner";
            this.PersonID.Name = "PersonID";
            this.PersonID.ReadOnly = true;
            // 
            // jobnum
            // 
            this.jobnum.DataPropertyName = "jobnum";
            this.jobnum.HeaderText = "Job Number";
            this.jobnum.Name = "jobnum";
            this.jobnum.ReadOnly = true;
            // 
            // partnum
            // 
            this.partnum.DataPropertyName = "partnum";
            this.partnum.HeaderText = "Part Number";
            this.partnum.Name = "partnum";
            this.partnum.ReadOnly = true;
            // 
            // RevisionNum
            // 
            this.RevisionNum.DataPropertyName = "RevisionNum";
            this.RevisionNum.HeaderText = "Revision";
            this.RevisionNum.Name = "RevisionNum";
            this.RevisionNum.ReadOnly = true;
            // 
            // PartDescription
            // 
            this.PartDescription.DataPropertyName = "PartDescription";
            this.PartDescription.HeaderText = "Part Description";
            this.PartDescription.Name = "PartDescription";
            this.PartDescription.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "StartDate";
            this.StartDate.HeaderText = "StartDate";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            // 
            // DueDate
            // 
            this.DueDate.DataPropertyName = "DueDate";
            this.DueDate.HeaderText = "Due Date";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            // 
            // ReqDueDate
            // 
            this.ReqDueDate.DataPropertyName = "ReqDueDate";
            this.ReqDueDate.HeaderText = "Req Due Date";
            this.ReqDueDate.Name = "ReqDueDate";
            this.ReqDueDate.ReadOnly = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 409);
            this.Controls.Add(this.dataGridView1);
            this.Name = "main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn RevisionNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReqDueDate;
    }
}

