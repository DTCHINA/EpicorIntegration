using EdmLib;
using EpicorIntegration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace EPDMAddin_EpicorIntegration
{
    [Guid("9e974a5f-3bd9-4d32-9976-44efa09d6ee7"), ComVisible(true)]
    public class SWEPDMAddin : IEdmAddIn5
    {

        void EdmLib.IEdmAddIn5.GetAddInInfo(ref EdmAddInInfo poInfo, IEdmVault5 poVault, IEdmCmdMgr5 poCmdMgr)
        {
            //Fill in the AddIn's description
            poInfo.mbsAddInName = "EPDMAddin_EpicorIntegration";
            poInfo.mbsCompany = "Norco Ind.";
            poInfo.mbsDescription = "Epicor Integration Enterprise PDM Add-in";
            poInfo.mlAddInVersion = 1;

            //Minimum Conisio version needed for .Net Add-Ins is 6.4
            poInfo.mlRequiredVersionMajor = 6;
            poInfo.mlRequiredVersionMinor = 4;

            poCmdMgr.AddCmd(1, "Epicor Integration\\Add/Update Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to create/update Item in Epicor", 0, 0);
            poCmdMgr.AddCmd(2, "Epicor Integration\\Add/Update OOM & BOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to create/update OOM and BOM in Epicor", 0, 0);
            poCmdMgr.AddCmd(3, "Epicor Integration\\Add Revision", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to add revision to Item in Epicor", 0, 0);
            poCmdMgr.AddCmd(4, "Epicor Integration\\Check Out Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Checks out Item in Epicor (Not Enterprise PDM)", 0, 0);
            poCmdMgr.AddCmd(5, "Epicor Integration\\Check In/Approve Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to Approve and Check In Item to Epicor", 0, 0);
            poCmdMgr.AddCmd(-1, "Epicor Integration\\Add-in Configuration", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to configure Epicor Integration Add-in", 0, 0);

            //uncomment this line to add all hooks
            AddAllHooks(poCmdMgr);
        }

        public bool HaveUpToDateItemRef(IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            IEdmFolder5 path = vault.RootFolder;

            string LocalPath = Part.GetLocalPath(path.ID);

            long Local = Part.GetLocalVersionNo(LocalPath);

            int Server = Part.CurrentVersion;

            if (Local == Server)
                retval = true;

            return retval;
        }

        public bool UpdateItemRef(IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            if (!HaveUpToDateItemRef(Part,vault))
            {
                DialogResult dr = MessageBox.Show("This requires that you get the latest version of this file. Continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    Part.GetFileCopy(0, 0, 0, (int)EdmGetFlag.EdmGet_RefsVerLatest, "");

                    retval = true;
                }
                else
                    retval = false;
            }   
            return retval;
        }

        public string DetermineConfig(IEdmFile5 Part)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config = new Config_Select();

            pos = list.GetHeadPosition();

            for (int i = 0; i < list.Count; i++)
            {

                config.config_cbo.Items.Add(list.GetNext(pos));

            }

            config.config_cbo.SelectedIndex = 0;

            config.ShowDialog();

            if (config.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "";

            return retval;        
        }

        public void GetItemInfo()
        {

        }

        void EdmLib.IEdmAddIn5.OnCmd(ref EdmCmd poCmd, ref System.Array ppoData)
        {
            Debug.Print("Command Type: " + poCmd.meCmdType.ToString() + "\n  " + System.DateTime.Now.ToString());

            IEdmVault5 edmVault = poCmd.mpoVault as IEdmVault5;

            try
            {
                switch (poCmd.meCmdType)
                {
                    case EdmCmdType.EdmCmd_CardButton:
                        break;
                    case EdmCmdType.EdmCmd_CardInput:
                        break;
                    case EdmCmdType.EdmCmd_CardListSrc:
                        break;
                    case EdmCmdType.EdmCmd_InstallAddIn:
                        break;
                    case EdmCmdType.EdmCmd_Menu:
                        switch(poCmd.mlCmdID)
                        {
                            case 1:            
                                EdmCmdData[] Temp = (EdmCmdData[])ppoData;

                                IEdmVault7 vault = (IEdmVault7)poCmd.mpoVault;

                                DataHelper helper = new DataHelper();

                                foreach (EdmCmdData file in Temp)
                                {
                                    IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

                                    if (UpdateItemRef(part, vault))
                                    {
                                        IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

                                        object value;

                                        var.GetVar("Number",DetermineConfig(part), out value);

                                        if (value == null)
                                            break;

                                        //EpicorIntegration.Item_Master item = new Item_Master(value.ToString(), "", 0);

                                        //item.ShowDialog();
                                    }
                                }
                                //Item_Master
                                break;
                            case 2:
                                //Bill_Master
                                break;
                            case 3:
                                //Revision_Master
                                break;
                            case 4:
                                //CheckOut_Master
                                break;
                            case 5:
                                //CheckIn_Master
                                break;
                            case -1:
                                Config conf = new Config();
                                conf.ShowDialog();
                                break;
                            default:
                                break;
                        }                    
                        break;
                    case EdmCmdType.EdmCmd_PostAdd:
                        break;
                    case EdmCmdType.EdmCmd_PostAddFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostCopy:
                        break;
                    case EdmCmdType.EdmCmd_PostCopyFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostDelete:
                        break;
                    case EdmCmdType.EdmCmd_PostDeleteFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostGet:
                        break;
                    case EdmCmdType.EdmCmd_PostLock:
                        break;
                    case EdmCmdType.EdmCmd_PostMove:
                        break;
                    case EdmCmdType.EdmCmd_PostMoveFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostRename:
                        break;
                    case EdmCmdType.EdmCmd_PostRenameFolder:
                        break;
                    case EdmCmdType.EdmCmd_PostShare:
                        break;
                    case EdmCmdType.EdmCmd_PostState:
                        break;
                    case EdmCmdType.EdmCmd_PostUndoLock:
                        break;
                    case EdmCmdType.EdmCmd_PostUnlock:
                        break;
                    case EdmCmdType.EdmCmd_PreAdd:
                        break;
                    case EdmCmdType.EdmCmd_PreAddFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreCopy:
                        break;
                    case EdmCmdType.EdmCmd_PreCopyFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreDelete:
                        break;
                    case EdmCmdType.EdmCmd_PreDeleteFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreGet:
                        break;
                    case EdmCmdType.EdmCmd_PreLock:
                        break;
                    case EdmCmdType.EdmCmd_PreMove:
                        break;
                    case EdmCmdType.EdmCmd_PreMoveFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreRename:
                        break;
                    case EdmCmdType.EdmCmd_PreRenameFolder:
                        break;
                    case EdmCmdType.EdmCmd_PreShare:
                        break;
                    case EdmCmdType.EdmCmd_PreState:
                        break;
                    case EdmCmdType.EdmCmd_PreUndoLock:
                        break;
                    case EdmCmdType.EdmCmd_PreUnlock:
                        break;
                    case EdmCmdType.EdmCmd_SerialNo:
                        break;
                    case EdmCmdType.EdmCmd_UninstallAddIn:
                        break;
                    default:
                        break;
                }
            }
            catch (COMException exp)
            {
                string errorName, errorDesc;
                edmVault.GetErrorString(exp.ErrorCode, out errorName, out errorDesc);
                edmVault.MsgBox(0, errorDesc, EdmMBoxType.EdmMbt_OKOnly, errorName);
            }
        }

        void AddAllHooks(IEdmCmdMgr5 poCmdMgr)
        {
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardButton, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardInput, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_CardListSrc, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_InstallAddIn, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_Menu, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostAdd, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostAddFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostCopy, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostCopyFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostDelete, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostDeleteFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostGet, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostMove, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostMoveFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostRename, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostRenameFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostShare, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostState, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostUndoLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PostUnlock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreAdd, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreAddFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreCopy, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreCopyFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreDelete, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreDeleteFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreGet, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreMove, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreMoveFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreRename, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreRenameFolder, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreShare, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreState, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreUndoLock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_PreUnlock, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_SerialNo, null);

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_UninstallAddIn, null);
        }
    }


}
