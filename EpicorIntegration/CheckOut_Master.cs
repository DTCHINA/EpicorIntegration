using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class CheckOut_Master : Form
    {
        public CheckOut_Master()
        {
            InitializeComponent();

            gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

            gid_cbo.ValueMember = "Description";

            gid_cbo.DisplayMember = "GroupID";

            if (Properties.Settings.Default.ecogroup != "" || Properties.Settings.Default.ecogroup != null)
            {
                gid_cbo.Text = Properties.Settings.Default.ecogroup;
            }

            partnumber_txt.TextChanged += partnumber_txt_TextChanged;
        }

        public CheckOut_Master(string partnumber)
        {
            InitializeComponent();

            gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

            gid_cbo.ValueMember = "Description";

            gid_cbo.DisplayMember = "GroupID";

            if (Properties.Settings.Default.ecogroup != "" || Properties.Settings.Default.ecogroup != null)
            {
                gid_cbo.Text = Properties.Settings.Default.ecogroup;
            }

            partnumber_txt.Text = partnumber;

            partnumber_txt.TextChanged += partnumber_txt_TextChanged;
        }

        void PartTextChanged_Reset()
        {
            PartTextChanged.Stop();

            PartTextChanged.Start();
        }

        void partnumber_txt_TextChanged(object sender, EventArgs e)
        {
            PartTextChanged.Enabled = true;

            PartTextChanged_Reset();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            DataList.CheckOutPart(gid_cbo.Text, partnumber_txt.Text, rev_txt.Text);

            this.Close();
        }

        private void CheckOut_Master_Load(object sender, EventArgs e)
        {
            rev_txt.Text = DataList.GetCurrentRev(partnumber_txt.Text);

            if (rev_txt.Text == "")
            {
                MessageBox.Show("Revision cannot be blank/null.  You must make a revision before checking an item out.\n\nThis form will now close.", "Stop!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }
        }

        private void gid_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gid_desc.Text = gid_cbo.SelectedValue.ToString();
        }

        private void currev_chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (currev_chk.Checked)
                {
                    rev_txt.Text = DataList.GetCurrentRev(partnumber_txt.Text);

                    rev_txt.Enabled = false;

                    rev_txt.ReadOnly = true;
                }
                else
                {
                    rev_txt.Enabled = true;

                    rev_txt.ReadOnly = false;
                }
            }
            catch { }
        }

        private void PartTextChanged_Tick(object sender, EventArgs e)
        {
            if (currev_chk.Checked)
            {
                rev_txt.Text = DataList.GetCurrentRev(partnumber_txt.Text);

                rev_txt.Enabled = false;

                PartTextChanged.Enabled = false;
            }
        }
    }
}
