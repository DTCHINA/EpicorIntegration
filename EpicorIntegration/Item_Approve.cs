using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor.Mfg.BO;

namespace EpicorIntegration
{
    public partial class Item_Approve : Form
    {
        public bool Approved = false;

        public Item_Approve()
        {
            InitializeComponent();

            gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

            gid_cbo.ValueMember = "Description";

            gid_cbo.DisplayMember = "GroupID";

            if (Properties.Settings.Default.ecogroup != "" || Properties.Settings.Default.ecogroup != null)
            {
                gid_cbo.Text = Properties.Settings.Default.ecogroup;
            }
        }

        private void approval_btn_Click(object sender, EventArgs e)
        {
            Approved = !Approved;

            if (Approved)
            {
                approval_btn.Text = "Approved";

                approval_btn.BackColor = Color.GreenYellow;
            }
            else
            {
                approval_btn.Text = "Not Approved";

                approval_btn.BackColor = Color.Yellow;
            }
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

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            EngWorkBench EngWb = new EngWorkBench(DataList.EpicConn);

            EngWorkBenchDataSet ds = new EngWorkBenchDataSet();

            ds = EngWb.GetDatasetForTree(gid_cbo.Text, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

            DataList.EpicClose();

            DataList.ApprovePart(ds, gid_cbo.Text, partnumber_txt.Text, rev_txt.Text);
        }

        void PartTextChanged_Reset()
        {
            PartTextChanged.Stop();

            PartTextChanged.Start();
        }

        private void partnumber_txt_TextChanged(object sender, EventArgs e)
        {
            PartTextChanged.Enabled = true;

            PartTextChanged_Reset();
        }

        private void gid_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gid_desc.Text = gid_cbo.SelectedValue.ToString();
        }
    }
}
