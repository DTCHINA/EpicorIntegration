using EdmLib;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace EPDMAddin_EpicorIntegration
{
    public partial class Config_Select : Form
    {
        public string SelectedConfig;

        IEdmVault7 Vault;

        EdmCmdData File;

        public Config_Select(IEdmVault7 vault, EdmCmdData file)
        {
            InitializeComponent();

            this.SizeChanged += Config_Select_SizeChanged;

            Vault = vault;

            File = file;
        }

        void Config_Select_SizeChanged(object sender, EventArgs e)
        {
            cancel_btn.Location = new Point(this.Width - 104, cancel_btn.Location.Y);

            config_cbo.Size = new Size(this.Width - 41, config_cbo.Height);

            pnum_txt.Size = new Size(this.Width - 41, pnum_txt.Size.Height);
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
            try
            {
                IEdmFile5 part = (IEdmFile5)Vault.GetObject(EdmObjectType.EdmObject_File, File.mlObjectID1);

                IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

                object number;

                var.GetVar("Number", config_cbo.Text, out number);

                pnum_txt.Text = "";

                if (number != null)
                    pnum_txt.Text = number.ToString();
            }
            catch { }
        }

        private void Config_Select_Load(object sender, EventArgs e)
        {
            if (config_cbo.Items.Count == 2 && config_cbo.Items[0].ToString() == "@" && config_cbo.Items[1].ToString() == "Default")
            {
                SelectedConfig = "@";

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}
