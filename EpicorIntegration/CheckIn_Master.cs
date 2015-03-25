﻿using System;
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
    public partial class CheckIn_Master : Form
    {
        public CheckIn_Master(string partnumber)
        {
            InitializeComponent();

            partnumber_txt.Text = partnumber;

            gid_cbo.DataSource = DataList.GroupIDDataSet().Tables[0];

            gid_cbo.ValueMember = "Description";

            gid_cbo.DisplayMember = "GroupID";

            if (Properties.Settings.Default.ecogroup != "" || Properties.Settings.Default.ecogroup != null)
            {
                gid_cbo.Text = Properties.Settings.Default.ecogroup;
            }
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            try { DataList.CheckInPart(gid_cbo.Text, partnumber_txt.Text, rev_txt.Text); }
            catch (Exception ex) 
            { 
                //Error handled in Datalist.CheckInPart now
                //MessageBox.Show("Could not check in part\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            finally { this.Close(); }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void partnumber_txt_TextChanged(object sender, EventArgs e)
        {
            PartTextChanged.Enabled = true;

            PartTextChanged_Reset();
        }

        void PartTextChanged_Reset()
        {
            PartTextChanged.Stop();

            PartTextChanged.Start();
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

        private void CheckIn_Master_Load(object sender, EventArgs e)
        {
            string Message;

            if (!DataList.PartCheckOutStatus(gid_cbo.Text, partnumber_txt.Text, rev_txt.Text, out Message))
            {
                MessageBox.Show("Part must be checked out by selected Group ID to continue.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                if (Message != "Checked Out by GroupID")
                {
                    MessageBox.Show("Part must be checked out by selected Group ID to continue.\n\n" + Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                }
            }
        }
    }
}
