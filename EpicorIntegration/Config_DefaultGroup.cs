using System;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class Config_DefaultGroup : Form
    {
        public Config_DefaultGroup()
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

        private void Config_DefaultGroup_Load(object sender, EventArgs e)
        {

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ecogroup = gid_cbo.Text;

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void gid_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gid_desc.Text = gid_cbo.SelectedValue.ToString();
        }
    }
}
