using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class Operations_Minutes : Form
    {
        public decimal RetVal { get; set; }

        public Operations_Minutes()
        {
            InitializeComponent();
        }

        private void Operations_Minutes_Load(object sender, EventArgs e)
        {
            this.epicorMinutesTableAdapter.Fill(this.engDataDataSet.EpicorMinutes);

            operation_cbo.DataSource = engDataDataSet.Tables[1];

            operation_cbo.DisplayMember = "Name";
        }

        private void operation_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowid = operation_cbo.SelectedIndex;

            type_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Type"].ToString();

            efficiency_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Efficiency"].ToString();

            per_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Per"].ToString();

            seconds_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Seconds"].ToString();

            minpc_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["MP"].ToString();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
        }

        private void seconds_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            try
            {
                RetVal = decimal.Parse(minpc_txt.Text);
            }
            catch { MessageBox.Show("Value is not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
        }

        private void mult_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }

        void CalculateVals()
        {
            string MP = null;

            double railfactor = 1;

            if (rails_chk.Checked)
                railfactor = .5;

            try
            {
                MP = ((double.Parse(seconds_txt.Text) / double.Parse(efficiency_txt.Text) / 60) * (double.Parse(mult_txt.Text)) * railfactor).ToString();
            }
            catch { MP = "Error!"; }
            finally { minpc_txt.Text = MP; }
        }

        private void rails_chk_CheckedChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }
    }
}
