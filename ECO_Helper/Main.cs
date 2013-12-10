using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor_Integration;


namespace ECO_Helper
{
    public partial class Main : Form
    {


        public Main()
        {
            InitializeComponent();

            checkedout_chk.Click += checkedout_chk_Click;

            pnum_txt.Leave += pnum_txt_Leave;
        }

        void pnum_txt_Leave(object sender, EventArgs e)
        {
            //Get Part info, if checked out by GID set flag and enable buttons

            MessageBox.Show("unfinished");
        }

        void checkedout_chk_Click(object sender, EventArgs e)
        {
            checkedout_chk.Checked = !checkedout_chk.Checked;
        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = pnum_txt.Text;

            openFileDialog.ShowDialog();

            filedir_txt.Text = openFileDialog.FileName;
        }

        private void approved_btn_Click(object sender, EventArgs e)
        {
            if (approved_btn.Text == "Approved")
            {
                approved_btn.Text = "Not Approved";

                approved_btn.BackColor = Color.Yellow;
            }
            else
            {
                approved_btn.Text = "Approved";

                approved_btn.BackColor = Color.GreenYellow;
            }

            checkin_btn.Enabled = (approved_btn.Text == "Approved");
        }

        private void useswrev_chk_CheckedChanged(object sender, EventArgs e)
        {
            if ((filedir_txt.Text == "" || filedir_txt.Text == null) && useswrev_chk.Checked)
                browse_btn_Click(browse_btn, null);

            rev_txt.ReadOnly = useswrev_chk.Checked;

            rev_txt.Text = SWHelper.GetCurrentRevision(filedir_txt.Text, pnum_txt.Text);
        }

        private void addrev_btn_Click(object sender, EventArgs e)
        {
            string CurrentRev = DataList.GetCurrentRev(pnum_txt.Text);

            bool valid = DataList.CreatePartRevision(pnum_txt.Text, CurrentRev, rev_txt.Text, desc_txt.Text, revcomments_txt.Text, eco_txt.Text);
                
            addrev_btn.Enabled = !valid;

            item_btn.Enabled = valid;

            getdetails_btn.Enabled = valid;

            ops_btn.Enabled = valid;

            bill_btn.Enabled = valid;

            approved_btn.Enabled = valid;

            checkedout_chk.Checked = valid;
        }

        private void item_btn_Click(object sender, EventArgs e)
        {
            string desc = SWHelper.GetCurrentDescription(filedir_txt.Text, pnum_txt.Text);
    
            decimal weight = SWHelper.GetCurrentWeight(filedir_txt.Text, pnum_txt.Text);

            Item_Master IM = new Item_Master(pnum_txt.Text, desc, weight);

            IM.ShowDialog();

            IM.Dispose();
        }

        private void getdetails_btn_Click(object sender, EventArgs e)
        {
            Operations_CopyMethods OpCopy = new Operations_CopyMethods(pnum_txt.Text);

            OpCopy.ShowDialog();

            DataList.GetDetailsFromMethods(gid_cbo.Text, pnum_txt.Text, rev_txt.Text, OpCopy.retPart, OpCopy.retRev);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

            gid_cbo.ValueMember = "Description";

            gid_cbo.DisplayMember = "GroupID";

            if (Epicor_Integration.Properties.Settings.Default.ecogroup != "" || Epicor_Integration.Properties.Settings.Default.ecogroup != null)
            {
                gid_cbo.Text = Epicor_Integration.Properties.Settings.Default.ecogroup;
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config Confg = new Config();

            Confg.ShowDialog();
        }

        private void ops_btn_Click(object sender, EventArgs e)
        {
            Operations_Master Ops = new Operations_Master(pnum_txt.Text, rev_txt.Text);

            Ops.ShowDialog();

            Ops.Dispose();
        }

        private void bill_btn_Click(object sender, EventArgs e)
        {

        }

        private void checkin_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
