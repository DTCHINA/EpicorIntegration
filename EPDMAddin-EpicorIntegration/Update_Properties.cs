using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EdmLib;
using EpicorIntegration;

namespace EPDMAddin_EpicorIntegration
{
    public partial class Update_Properties : Form
    {
        public class ConfigListItem : IComparable<ConfigListItem>
        {
            public string DisplayName { get; set; }
            public string ConfigName { get; set; }
            public string PartNumber { get; set; }

            public int CompareTo(ConfigListItem other)
            {
                return this.PartNumber.CompareTo(other.PartNumber);
            }

            public override string ToString()
            {
                return this.DisplayName;
            }
        }

        public Update_Properties(IEdmVault7 vault, EdmCmdData file)
        {
            InitializeComponent();

            filename_txt.Text = file.mbsStrData1;

            IEdmFolder5 parentfolder;

            IEdmFile5 Part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmFile5 Part2 = vault.GetFileFromPath(file.mbsStrData1, out parentfolder);

            #region CheckOut Status of file

            if (Part.IsLocked)
            {
                IEdmUserMgr5 Mgr = (IEdmUserMgr5)vault;
                
                //Get Current User Name
                IEdmUser5 You = Mgr.GetLoggedInUser();

                //Get User that has part checked out
                IEdmUser5 ByWho = Part.LockedByUser;

                if (You.ID != ByWho.ID)
                {
                    //Check Part out
                    DialogResult Dr = MessageBox.Show("File must be checked out to continue.\n\nCheck out file now?", "Check Out File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Dr == DialogResult.Yes)
                    {
                        Part.LockFile(file.mlObjectID3, this.Handle.ToInt32(), (int)EdmLockFlag.EdmLock_Simple);
                    }
                    else
                        this.Close();
                }
            }
            else
            {
                //Check Part out
                DialogResult Dr = MessageBox.Show("File must be checked out to continue.\n\nCheck out file now?", "Check Out File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Dr == DialogResult.Yes)
                {
                    Part.LockFile(file.mlObjectID3, this.Handle.ToInt32(), (int)EdmLockFlag.EdmLock_Simple);
                }
                else
                    this.Close();
            }

            #endregion

            #region Fill configurations

            EdmStrLst5 Configs = Part.GetConfigurations();

            IEdmPos5 pos = Configs.GetHeadPosition();

            for (int i = 0; i < Configs.Count; i++)
            {
                string ConfigToAdd = Configs.GetNext(pos);

                IEdmEnumeratorVariable5 var = Part.GetEnumeratorVariable();

                object number;

                var.GetVar("Number", ConfigToAdd, out number);

                if (ConfigToAdd != "@" && number != null && number.ToString() != "")
                {
                    ConfigListItem item = new ConfigListItem();

                    item.PartNumber = number.ToString();

                    item.ConfigName = ConfigToAdd;

                    item.DisplayName = ConfigToAdd + " - " + number;

                    Config_List.Items.Add(item);
                }
            }

            #endregion
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {

        }

        private void Update_Properties_Load(object sender, EventArgs e)
        {

        }
    }
}
