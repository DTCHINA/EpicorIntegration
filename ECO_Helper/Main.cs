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
using EdmLib;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;


namespace ECO_Helper
{
    public partial class Main : Form
    {


        public Main()
        {
            InitializeComponent();

            checkedout_chk.Click += checkedout_chk_Click;

            pnum_txt.Leave += pnum_txt_Leave;

            rev_txt.Leave += rev_txt_Leave;
        }

        void AlreadyCheckedOut()
        {
            if (rev_txt.Text != "" && rev_txt.Text != null && pnum_txt.Text != "" && pnum_txt.Text != null)
            {
                //Get Part info, if checked out by GID set flag and enable buttons
                try
                {
                    Part Part = new Part(DataList.EpicConn);

                    PartDataSet Pdata = Part.GetByID(pnum_txt.Text);

                    int lastrow = 0;

                    for (int i = 0; i < Pdata.Tables["PartRev"].Rows.Count; i++)
                    {
                        if (Pdata.Tables["PartRev"].Rows[i]["RevisionNum"].ToString() == rev_txt.Text)
                            lastrow = i;
                    }

                    if (Pdata.Tables["PartRev"].Rows[lastrow]["ECOGroup"].ToString() != "" && Pdata.Tables["PartRev"].Rows[lastrow]["ECOGroup"].ToString() != null)
                    {
                        desc_txt.Text = Pdata.Tables["PartRev"].Rows[lastrow]["RevShortDesc"].ToString();

                        revcomments_txt.Text = Pdata.Tables["PartRev"].Rows[lastrow]["RevDescription"].ToString();

                        eco_txt.Text = Pdata.Tables["PartRev"].Rows[lastrow]["ECO"].ToString();

                        checkedout_chk.Checked = true;

                        addrev_btn.Enabled = false;

                        useswrev_chk.Enabled = false;

                        item_btn.Enabled = true;

                        getdetails_btn.Enabled = true;

                        ops_btn.Enabled = true;

                        bill_btn.Enabled = true;

                        approved_btn.Enabled = true;

                        rev_txt.ReadOnly = true;

                        pnum_txt.ReadOnly = true;
                    }
                }
                catch { }
            }
        }

        void rev_txt_Leave(object sender, EventArgs e)
        {
            AlreadyCheckedOut();
        }

        void pnum_txt_Leave(object sender, EventArgs e)
        {
            AlreadyCheckedOut();
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

                DataList.ApprovePart(gid_cbo.Text, pnum_txt.Text, rev_txt.Text);
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

            DataList.CheckOutPart(gid_cbo.Text, pnum_txt.Text, rev_txt.Text);

            useswrev_chk.Enabled = !valid;

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
            try
            {
                gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

                gid_cbo.ValueMember = "Description";

                gid_cbo.DisplayMember = "GroupID";

                if (Epicor_Integration.Properties.Settings.Default.ecogroup != "" || Epicor_Integration.Properties.Settings.Default.ecogroup != null)
                {
                    gid_cbo.Text = Epicor_Integration.Properties.Settings.Default.ecogroup;
                }
            }
            catch
            {
                Config Confg = new Config();

                Confg.ShowDialog();

                Application.Restart();
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
            SWHelper.GetBill(filedir_txt.Text, pnum_txt.Text);
        }

        private void checkin_btn_Click(object sender, EventArgs e)
        {
            try
            {
                DataList.CheckInPart(gid_cbo.Text, pnum_txt.Text, rev_txt.Text);

                DataList.UnApproveOldRevisions(gid_cbo.Text, pnum_txt.Text, rev_txt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not check in part\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void checkedout_chk_CheckedChanged(object sender, EventArgs e)
        {
            if (checkedout_chk.Checked)
            {
                gid_cbo.Enabled = false;
            }
        }

        private void resetFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gid_cbo.Enabled = true;

            pnum_txt.ReadOnly = false;

            rev_txt.ReadOnly = false;

            pnum_txt.Text = "";

            rev_txt.Text = "";

            desc_txt.Text = "";

            eco_txt.Text = "";

            revcomments_txt.Text = "";

            filedir_txt.Text = "";
            
            checkedout_chk.Checked = false;

            addrev_btn.Enabled = true;

            useswrev_chk.Enabled = true;

            item_btn.Enabled = false;

            getdetails_btn.Enabled = false;

            ops_btn.Enabled = false;

            bill_btn.Enabled = false;

            approved_btn.Enabled = false;

            checkin_btn.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
