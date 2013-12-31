using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Epicor_Integration
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();

            SeverPort.KeyDown += SeverPort_KeyDown;

            SeverPort.KeyPress += SeverPort_KeyPress;

            SeverPort.Leave += SeverPort_Leave;

            List<MtlReseqType> MtlReseq = new List<MtlReseqType>();

            MtlReseq.Add(new MtlReseqType("Material","Material"));

            MtlReseq.Add(new MtlReseqType("Part Number","Part"));

            MtlReseq.Add (new MtlReseqType("Find Number","Item"));

            BindingSource bind = new BindingSource();

            bind.DataSource = MtlReseq;

            mtlreseq_cbo.DataSource = bind;

            mtlreseq_cbo.DisplayMember = "Name";

            mtlreseq_cbo.ValueMember = "Arg";

            mtlreseq_cbo.SelectedIndex = 0;

            if (Properties.Settings.Default.mtlreqtype != null)
                mtlreseq_cbo.SelectedValue = Properties.Settings.Default.mtlreqtype;
        }

        void SeverPort_Leave(object sender, EventArgs e)
        {
            if (double.Parse(SeverPort.Text) > 65535)
                MessageBox.Show("Please enter an acceptable port (1-65535)", "Error!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        void SeverPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ServerPortValidKey)
                e.Handled = true;
        }

        public bool ServerPortValidKey = new bool();

        void SeverPort_KeyDown(object sender, KeyEventArgs e)
        {
            ServerPortValidKey = NonNumberFilter(e);
        }

        bool NonNumberFilter(KeyEventArgs e)
        {

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                                return true;
                }
            }
            if (Control.ModifierKeys == Keys.Shift)
                return true;
            return false;
        
        }

        private void logininfobtn_click(object sender, EventArgs e)
        {
            EpicLogin LoginInfoForm = new EpicLogin();

            LoginInfoForm.ShowDialog();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            //Save information then close

            Properties.Settings.Default.mtlreqtype = mtlreseq_cbo.SelectedValue.ToString();

            Properties.Settings.Default.svrname = ServerName.Text;
            
            Properties.Settings.Default.svrport = SeverPort.Text;

            Properties.Settings.Default.Save();

            this.Close();

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            ServerName.Text = Properties.Settings.Default.svrname;

            SeverPort.Text = Properties.Settings.Default.svrport;

            mtlreseq_cbo.SelectedValue = Properties.Settings.Default.mtlreqtype;
        }

        private void ecogp_btn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.validated)
            {
                Config_DefaultGroup DefGroup = new Config_DefaultGroup();

                DefGroup.ShowDialog();
            }
            else
            {
                MessageBox.Show("Must validate login before attempting!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void opsmin_btn_Click(object sender, EventArgs e)
        {
            Config_OpsMins ConfOps = new Config_OpsMins();

            ConfOps.ShowDialog();

            ConfOps.Dispose();
        }

        private void ServerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void SeverPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void ServerDataChanged()
        {
            DialogResult dr = MessageBox.Show("Server information changed.  For these settings to take effect please restart Windows Explorer.\nDo you want to restart Explorer now?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

            if (dr == DialogResult.Yes)
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.MainModule.ModuleName == "explorer.exe")
                    {
                        p.Kill();
                    }
                }
                Process.Start("explorer.exe");
            }
        }

    }
}
