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

            poCmdMgr.AddCmd(2, "Epicor Integration\\Add Item,OOM & BOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to add an Item, a revision, an OOM and BOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(3, "Epicor Integration\\Check Out Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Checks out Item in Epicor (Not Enterprise PDM)", 0, 0);

            poCmdMgr.AddCmd(4, "Epicor Integration\\Add Revision", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to add revision to Item in Epicor", 0, 0);

            poCmdMgr.AddCmd(5, "Epicor Integration\\Add/Update OOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to create/update OOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(6, "Epicor Integration\\Add/Update BOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to create/update BOM in Epicor", 0, 0);
            
            poCmdMgr.AddCmd(7, "Epicor Integration\\Check In/Approve Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles, "", "Launches a dialog to Approve and Check In Item to Epicor", 0, 0);
            
            poCmdMgr.AddCmd(-1, "Epicor Integration\\Add-in Configuration", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Launches a dialog to configure Epicor Integration Add-in", 0, 0);

            //uncomment this line to add all hooks
            AddAllHooks(poCmdMgr);
        }

        public bool HaveUpToDateItemRef(IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            IEdmSearch5 search = vault.CreateSearch();

            search.FileName = Part.Name;

            IEdmSearchResult5 result = search.GetFirstResult();

            string LocalPath = Part.GetLocalPath(result.ParentFolderID);

            long Local = Part.GetLocalVersionNo(LocalPath);

            int Server = Part.CurrentVersion;

            if (Local == Server)
                retval = true;

            return retval;
        }

        public DialogResult CheckOutPart(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            var.GetVar("Number", selected_config, out partnum_val);

            CheckOut_Master CM = new CheckOut_Master(partnum_val.ToString());

            CM.ShowDialog();

            return CM.DialogResult;
        }

        public DialogResult CheckInPart(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            var.GetVar("Number", selected_config, out partnum_val);

            CheckIn_Master CM = new CheckIn_Master(partnum_val.ToString());

            CM.ShowDialog();

            return CM.DialogResult;
        }

        public bool UpdateItemRef(IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            if (!HaveUpToDateItemRef(Part, vault))
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
            else
                retval = true;

            return retval;
        }

        public string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault, EdmCmdData file)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config = new Config_Select(vault, file);

            pos = list.GetHeadPosition();

            for (int i = 0; i < list.Count; i++)
            {

                config.config_cbo.Items.Add(list.GetNext(pos));

            }

            config.config_cbo.SelectedIndex = 0;

            config.ShowDialog();

            retval = config.SelectedConfig;

            if (config.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "";

            return retval;        
        }

        public DialogResult AddRevision(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            object rev_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            var.GetVar("Number", selected_config, out partnum_val);

            var.GetVar("Revision", selected_config, out rev_val);

            Revision_Master RM = new Revision_Master(partnum_val.ToString(), rev_val.ToString(), "", "");

            RM.ShowDialog();

            return RM.DialogResult;
        }

        public DialogResult AddOOM(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            object rev_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            var.GetVar("Number", selected_config, out partnum_val);

            var.GetVar("Revision", selected_config, out rev_val);

            Operations_Master OM = new Operations_Master(partnum_val.ToString(), rev_val.ToString());

            OM.ShowDialog();

            return OM.DialogResult;
        }

        public DialogResult AddBill(IEdmVault7 vault, EdmCmdData file)
        {
            IEdmEnumeratorVariable5 var;

            string selected_config;

            IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            if (UpdateItemRef(part, vault))
            {
                var = part.GetEnumeratorVariable();

                selected_config = DetermineConfig(part, vault, file);

                IEdmBomMgr BomMgr = (IEdmBomMgr)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);

                Array LayoutVal;

                Array BomVal = Array.CreateInstance(typeof(EdmBomInfo), 1);

                Array ColumnVal;

                BomMgr.GetBomLayouts(out LayoutVal);

                IEdmBomMgr EdmBomMgr = (IEdmBomMgr)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);

                Array BomLayouts;

                EdmBomMgr.GetBomLayouts(out BomLayouts);

                EdmBomView BomView = part.GetComputedBOM(1, 0, selected_config, 2);

                BomView.GetRows(out BomVal);

                BomView.GetColumns(out ColumnVal);

                EdmBomColumn ColVal = (EdmBomColumn)ColumnVal.GetValue(0);

                List<string> BillQty = new List<string>();

                List<string> BillNumbers = new List<string>();

                List<string> BillLevel = new List<string>();

                string ParentNumber = "";

                for (int i = 0; i < BomVal.Length; i++)
                {
                    IEdmBomCell bominfo = (IEdmBomCell)BomVal.GetValue(i);

                    object Value;

                    object CompVal;

                    string Config;

                    bool RO;

                    int itemlevel = bominfo.GetTreeLevel();

                    if (itemlevel == 0)
                    {
                        bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out Value, out CompVal, out Config, out RO);

                        ParentNumber = Value.ToString();
                    }

                    if (itemlevel != 1)
                    {
                        EdmBomColumnType ColType = EdmBomColumnType.EdmBomCol_RefCount;

                        bominfo.GetVar(0, ColType, out Value, out CompVal, out Config, out RO);

                        BillQty.Add(Value.ToString());

                        ColType = EdmBomColumnType.EdmBomCol_PartNumber;

                        bominfo.GetVar(0, ColType, out Value, out CompVal, out Config, out RO);

                        BillNumbers.Add(Value.ToString());
                    }
                }

                Bill_Master BM = new Bill_Master(BillNumbers, BillQty, ParentNumber);

                BM.ShowDialog();

                return BM.DialogResult;
            }
            return DialogResult.Cancel;
        }

        public DialogResult GetItemInfo(IEdmVault7 vault, EdmCmdData file)
        {                           
            IEdmFile5 part;
 
            IEdmEnumeratorVariable5 var;

            string selected_config;

            object partnum_val;

            object desc_val;

            object weight_val;
                         
            part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            if (UpdateItemRef(part, vault))
            {
                var = part.GetEnumeratorVariable();

                decimal weight_fallback = 0;

                selected_config = DetermineConfig(part,vault,file);

                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Description", selected_config, out desc_val);

                //Weight is typically @ config
                var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val == null)
                    var.GetVar("NetWeight", "@", out weight_val);

                if (weight_val != null)
                    decimal.TryParse(weight_val.ToString(), out weight_fallback);

                if (partnum_val != null)
                {
                    EpicorIntegration.Item_Master item = new Item_Master(partnum_val.ToString(), desc_val.ToString(), weight_fallback);

                    item.ShowDialog();

                    return item.DialogResult;
                }
                else
                {
                    MessageBox.Show("Part number was a null value!\n\nEnsure that custom properties are completely filled out.", "Missing Properties!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    return DialogResult.Cancel;
                }
            }
            else
                return DialogResult.Cancel;
        }

        public bool ValidSelection(EdmCmdData file)
        {
            string file_ext = file.mbsStrData1.Substring(file.mbsStrData1.LastIndexOf('.') + 1).ToUpper();

            if (file_ext != "SLDASM" && file_ext != "SLDPRT")
            {
                MessageBox.Show("Must be run on SolidWorks Parts/Assemblies.  Cannot continue!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else
                return true;
        }

        void EdmLib.IEdmAddIn5.OnCmd(ref EdmCmd poCmd, ref System.Array ppoData)
        {
            Debug.Print("Command Type: " + poCmd.meCmdType.ToString() + "\n  " + System.DateTime.Now.ToString());

            IEdmVault5 edmVault = poCmd.mpoVault as IEdmVault5;

            EdmCmdData[] Temp = (EdmCmdData[])ppoData;

            IEdmVault7 vault = (IEdmVault7)poCmd.mpoVault;

            DataHelper helper = new DataHelper();

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
                                #region Item Master

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        GetItemInfo(vault, file);
                                }
                                #endregion
                                break;
                            case 2:
                                #region Add Item/Rev/OOM/BOM

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                    {
                                        if (GetItemInfo(vault, file) == DialogResult.Cancel)
                                            break;

                                        if (AddRevision(vault, file) == DialogResult.Cancel)
                                            break;

                                        if (AddOOM(vault, file) == DialogResult.Cancel)
                                            break;

                                        if (AddBill(vault, file) == DialogResult.Cancel)
                                            break;

                                        if (CheckInPart(vault, file) == DialogResult.Cancel)
                                            break;
                                    }
                                }

                                #endregion
                                break;
                            case 3:
                                #region CheckOut_Master

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        CheckOutPart(vault, file);
                                }
                                #endregion
                                break;
                            case 4:
                                #region Add Revision

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        AddRevision(vault, file);
                                }

                                #endregion
                                break;
                            case 5:
                                #region OOM_Master

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        AddOOM(vault, file);
                                }

                                #endregion
                                break;
                            case 6:
                                #region Bill Master

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        AddBill(vault, file);
                                }
                                #endregion
                                break;
                            case 7:
                                #region CheckIn_Master

                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                        CheckInPart(vault, file);
                                }
                                #endregion
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
