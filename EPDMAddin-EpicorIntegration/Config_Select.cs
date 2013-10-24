using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EPDMAddin_EpicorIntegration
{
    public partial class Config_Select : Form
    {
        public string SelectedConfig;

        public Config_Select()
        {
            InitializeComponent();
        }

        private void select_btn_Click(object sender, EventArgs e)
        {
            SelectedConfig = config_cbo.Text;

            this.Close();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void config_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Config_Select_Load(object sender, EventArgs e)
        {
            if (config_cbo.Items.Count == 2 && config_cbo.Items[0] == "@" && config_cbo.Items[1] == "Default")
            {
                SelectedConfig = "@";

                this.Close();
            }
        }
    }
}
