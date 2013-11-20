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

namespace EpicorIntegration
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

        public IEdmFile5 Part { get; set; }

        public IEdmVault7 Vault { get; set; }

        public Update_Properties(IEdmVault7 vault, EdmCmdData file)
        {
            InitializeComponent();

            Vault = vault;

            filename_txt.Text = file.mbsStrData1;

            Part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

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

            FillConfigs();

            if (Config_List.Items.Count == 0)
            {
                DialogResult dr = MessageBox.Show("Could not find valid configurations. If the properties are set in the '@' configuration but not the default (or more) this can happen.  Do you want to try and auto-correct this?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dr == DialogResult.No)
                    this.Close();
                else
                {
                    AutoCorrect();

                    FillConfigs();
                }
            }

            #endregion
        }

        public void AutoCorrect()
        {
            EdmStrLst5 Configs = Part.GetConfigurations();

            if (Configs.Count > 2)
            {
                MessageBox.Show("Too many configurations!  This process must be done manually", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }

            IEdmEnumeratorVariable8 var = (IEdmEnumeratorVariable8)Part.GetEnumeratorVariable();

            IEdmPos5 pos = Configs.GetHeadPosition();

            string NotAtName = "";

            for (int i = 0; i < Configs.Count; i++)
            {
                NotAtName = Configs.GetNext(pos);

                if (NotAtName != "@")
                    break;
            }
            if (NotAtName != "")
            {
                object RetVal;

                var.GetVar("Number", "@", out RetVal);

                var.SetVar("Number", NotAtName, RetVal);

                var.Flush();

                var.CloseFile(true);
            }
            else
            {
                MessageBox.Show("Could not find configurations.  Something is very wrong.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                this.Close();
            }


        }

        public void FillConfigs()
        {
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
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            #region What properties are we updating

            bool Desc = false;

            bool Rev = false;

            bool Group = false;

            bool Class = false;

            bool Type = false;

            for (int i = 0; i < Properties_List.Items.Count; i++)
            {
                if (Properties_List.GetItemChecked(i))
                {
                    object PropertiesItem = Properties_List.Items[i];

                    if (PropertiesItem.ToString() == "Description")
                        Desc = true;

                    if (PropertiesItem.ToString() == "Revision")
                        Rev = true;

                    if (PropertiesItem.ToString() == "Product Group")
                        Group = true;

                    if (PropertiesItem.ToString() == "Product Class")
                        Class = true;

                    if (PropertiesItem.ToString() == "Type")
                        Type = true;
                }
            }

            #endregion

            #region fetch and update those properties

            IEdmEnumeratorVariable8 var = (IEdmEnumeratorVariable8)Part.GetEnumeratorVariable();

            for (int i = 0; i < Config_List.Items.Count; i++)
            {
                if (Config_List.GetItemChecked(i))
                {
                    ConfigListItem ConfItem = (ConfigListItem)Config_List.Items[i];

                    if (Rev)
                    {
                        object Revision = DataList.GetCurrentRev(ConfItem.PartNumber);

                        var.SetVar("Revision", ConfItem.ConfigName, Revision);
                    }

                    if (Desc)
                    {
                        object Description = DataList.GetCurrentDesc(ConfItem.PartNumber);

                        var.SetVar("Description", ConfItem.ConfigName, Description);
                    }

                    if (Type)
                    {
                        object _Type = DataList.GetType(ConfItem.PartNumber);

                        var.SetVar("Type", ConfItem.ConfigName, _Type);
                    }

                    if (Class)
                    {
                        object _Class = DataList.GetClass(ConfItem.PartNumber);

                        var.SetVar("Class", ConfItem.ConfigName, _Class);
                    }

                    if (Group)
                    {
                        object _Group = DataList.GetGroup(ConfItem.PartNumber);

                        var.SetVar("Product", ConfItem.ConfigName, _Group);
                    }
                }
            }

            #endregion

            var.Flush();

            var.CloseFile(true);

            this.Close();
        }

        private void Update_Properties_Load(object sender, EventArgs e)
        {

        }
    }
}
