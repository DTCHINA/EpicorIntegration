namespace EpicorIntegration
{
    partial class Item_Approve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Item_Approve));
            this.SuspendLayout();
            // 
            // Item_Approve
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "Item_Approve";
            this.Load += new System.EventHandler(this.Item_Approve_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox currev_chk;
        private System.Windows.Forms.TextBox rev_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox partnumber_txt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gid_desc;
        private System.Windows.Forms.ComboBox gid_cbo;
        private System.Windows.Forms.Button approval_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button confirm_btn;
        private System.Windows.Forms.Timer PartTextChanged;
    }
}