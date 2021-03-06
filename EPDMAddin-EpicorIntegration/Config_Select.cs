﻿using EdmLib;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace EPDMEpicorIntegration
{
    public partial class Config_Select : Form
    {
        string StartMethod = "";

        public string SelectedConfig;

        IEdmVault7 Vault;

        EdmCmdData File;

        IEdmFile5 Part;

        public string _SearchTerm = null;

        public string SearchTerm
        {
            get { return _SearchTerm; }
            set { _SearchTerm = value; }
        }

        public Config_Select(IEdmVault7 vault, EdmCmdData file)
        {
            InitializeComponent();

            this.SizeChanged += Config_Select_SizeChanged;

            Vault = vault;

            File = file;

            StartMethod = "file";
        }

        public Config_Select(IEdmVault7 vault, IEdmFile5 part, string SearchPart)
        {
            InitializeComponent();

            this.SizeChanged += Config_Select_SizeChanged;

            Vault = vault;

            Part = part;

            SearchTerm = SearchPart;

            StartMethod = "part";
        }

        void Config_Select_SizeChanged(object sender, EventArgs e)
        {
            cancel_btn.Location = new Point(this.Width - 104, cancel_btn.Location.Y);

            config_cbo.Size = new Size(this.Width - 41, config_cbo.Height);

            pnum_txt.Size = new Size(this.Width - 41, pnum_txt.Size.Height);
        }

        private void select_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

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
                IEdmFile5 part;

                if (StartMethod == "file")
                    part = (IEdmFile5)Vault.GetObject(EdmObjectType.EdmObject_File, File.mlObjectID1);
                else
                    part = Part;

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
            #region No selectable part numbers

            if (config_cbo.Items.Count == 0)
            {
                this.DialogResult = DialogResult.Cancel;

                MessageBox.Show("There are no properties to determine the part number in this file.  Please correct this and try again.\n\nIf the properties are located in the \"@\" Configuration move them to another configuration.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }

            #endregion

            if (SearchTerm != null)
            {
                for (int i = 0; i < config_cbo.Items.Count; i++)
                {
                    config_cbo.SelectedIndex = i;

                    IEdmEnumeratorVariable5 var = Part.GetEnumeratorVariable();

                    object number = "";

                    var.GetVar("Number", config_cbo.Text, out number);

                    if (number != null)
                    {
                        if (number.ToString() == SearchTerm)
                        {
                            this.DialogResult = DialogResult.OK;

                            SelectedConfig = config_cbo.Text;

                            this.Close();
                        }
                    }
                }
            }
            else
                if (config_cbo.Items.Count == 1)
                {
                    config_cbo.SelectedIndex = 0;

                    SelectedConfig = config_cbo.Text;

                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
        }
    }
}
