using EdmLib;
using Epicor_Integration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EPDMEpicorIntegration
{
    //Release GUID
    [Guid("9e974a5f-3bd9-4d32-9976-44efa09d6ee7"), ComVisible(true)]
 
    //Test GUID
    //[Guid("194D5C17-3B13-40EA-B695-15E502AA6412"), ComVisible(true)]

    public class SWEPDMAddin : IEdmAddIn5
    {
        void IEdmAddIn5.GetAddInInfo(ref EdmAddInInfo poInfo, IEdmVault5 poVault, IEdmCmdMgr5 poCmdMgr)
        {
            //Fill in the AddIn's description

            poInfo.mbsAddInName = "Epicor-Integration";

            //poInfo.mbsAddInName = "EpicorIntegration";

            poInfo.mbsCompany = "Norco Ind.";
            poInfo.mbsDescription = "Epicor Integration Enterprise PDM Add-in";
            poInfo.mlAddInVersion = 1;

            //Minimum Conisio version needed for .Net Add-Ins is 6.4
            poInfo.mlRequiredVersionMajor = 6;
            poInfo.mlRequiredVersionMinor = 4;


            string MenuName = "Epicor Integration";

            //string MenuName = "Debug";

            poCmdMgr.AddCmd(1, MenuName + "\\Part Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update Item in Epicor", 0, 0); ;

            poCmdMgr.AddCmd(4, MenuName + "\\Add Revision", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to add revision to Item in Epicor", 0, 0);

            poCmdMgr.AddCmd(5, MenuName + "\\Operation Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update OOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(6, MenuName + "\\Bill Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update BOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(7, MenuName + "\\Check In/Approve Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to Approve and Check In Item to Epicor", 0, 0);

            poCmdMgr.AddCmd(3, MenuName + "\\Check Out Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Checks out Item in Epicor (Not Enterprise PDM)", 0, 0);

            poCmdMgr.AddCmd(-1, MenuName + "\\Add-in Configuration", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Launches a dialog to configure Epicor Integration Add-in", 0, 0);

            poCmdMgr.AddCmd(-10, MenuName + "\\Update Properties from Epicor", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to update file properties from current Epicor values", 0, 0);

            poCmdMgr.AddCmd(-100, MenuName + "\\Templates", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Launches a dialog to Add/Edit/Update Templates", 0, 0);

            //poCmdMgr.AddCmd(2, MenuName + "\\Add Item,OOM & BOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to add an Item, a revision, an OOM and BOM in Epicor", 0, 0);
            
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_Menu, null);
        }

        void IEdmAddIn5.OnCmd(ref EdmCmd poCmd, ref System.Array ppoData)
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
                    case EdmCmdType.EdmCmd_Menu:
                        switch (poCmd.mlCmdID)
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
                            case -10:
                                foreach (EdmCmdData file in Temp)
                                {
                                    if (ValidSelection(file))
                                    {
                                        Update_Properties Update = new Update_Properties(vault, file);

                                        Update.ShowDialog();
                                    }
                                }
                                break;
                            case -100:
                                Template_Master TM = new Template_Master();

                                TM.ShowDialog();

                                break;
                            default:
                                break;
                        }
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

        public bool HaveUpToDateItemRef(EdmCmdData file,IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            long Local = Part.GetLocalVersionNo(file.mlObjectID3);

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

            if (selected_config != "")
            {
                CheckOut_Master CM = new CheckOut_Master(partnum_val.ToString());

                CM.ShowDialog();

                return CM.DialogResult;
            }
            else
                return DialogResult.Cancel;
        }

        public DialogResult CheckInPart(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            var.GetVar("Number", selected_config, out partnum_val);

            if (selected_config != "")
            {
                Item_Approve CM = new Item_Approve(partnum_val.ToString());

                CM.ShowDialog();

                return CM.DialogResult;
            }
            else
                return DialogResult.Cancel;
        }

        /// <summary>
        /// Syncs local version of a file with the current version in the vault
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="vault"></param>
        /// <returns></returns>
        public bool UpdateItemRef(EdmCmdData file,IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            if (!HaveUpToDateItemRef(file,Part, vault))
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
                string itemtoadd = list.GetNext(pos);

                if (itemtoadd != "@")
                    config.config_cbo.Items.Add(itemtoadd);
            }

            config.config_cbo.SelectedIndex = 0;

            config.ShowDialog();

            retval = config.SelectedConfig;

            if (config.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "";

            return retval;        
        }

        public string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault, string SearchTerm)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config = new Config_Select(vault, Part,SearchTerm);

            //pos = list.GetHeadPosition();

            for (int i = 0; i < list.Count; i++)
            {
                string name = list.GetNext(pos);

                if (name != "@")
                    config.config_cbo.Items.Add(name);
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

            DialogResult RetVal = DialogResult.Cancel;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            string selected_config = DetermineConfig(part,vault,file);

            if (selected_config != "")
            {
                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Revision", selected_config, out rev_val);

                if (rev_val == null)
                {
                    MessageBox.Show("Item has not been completed and released.  Please make certain that the item has a revision before attempting to add a revision in Epicor.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    Revision_Master RM = new Revision_Master(partnum_val.ToString(), rev_val.ToString(), "", "");

                    RM.ShowDialog();

                    RetVal = RM.DialogResult;
                }
            }

            return RetVal;
        }

        public DialogResult AddOOM(IEdmVault7 vault, EdmCmdData file)
        {
            object partnum_val;

            object rev_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            DialogResult RetVal = DialogResult.Cancel;

            string selected_config = DetermineConfig(part,vault,file);

            if (selected_config != "")
            {
                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Revision", selected_config, out rev_val);

                if (rev_val == null)
                {
                    MessageBox.Show("Revision cannot be null.  Check that custom properties are filled out in the selected configuration", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return DialogResult.Cancel;
                }

                Operations_Master OM = new Operations_Master(partnum_val.ToString(), rev_val.ToString());

                OM.ShowDialog();

                RetVal = OM.DialogResult;
            }

            return RetVal;
        }

        public bool OntheList(string PartNumber)
        {
            List<RawMaterial> SafeItems = new List<RawMaterial>();

            SafeItems.AddRange(DataList.GetCoils());

            SafeItems.AddRange(DataList.GetEcoat());

            SafeItems.AddRange(DataList.GetSheets());

            for (int i = 0; i < SafeItems.Count; i++)
            {
                if (SafeItems[i].part_number == PartNumber)
                    return true;
            }

            return false;
        }

        public List<string> ProcessBill(IEdmVault7 vault, EdmCmdData file, List<string> BillNumbers, List<string> _BillQty, out List<string> BillQty)
        {
            for (int i = 0; i < BillNumbers.Count; i++)
            {
                if (DataList.PartExists(BillNumbers[i]))
                {/*Part exists and we are good*/}
                else
                {
                    if (PartExistsSW(vault, BillNumbers[i]))
                    {
                        //Get it, Add it
                        string Config = null;

                        IEdmFile7 Part = FindPartinVault(vault, file, BillNumbers[i], out Config);

                        DialogResult Dr = GetItemInfo(file,vault, Part);

                        if (Dr == DialogResult.Cancel)
                            BillNumbers.RemoveAt(i);
                    }
                    else
                    {
                        DialogResult Dr = MessageBox.Show(BillNumbers[i] + " was not found in the vault/Epicor database.  Do you want to manually add it?", "Missing Item!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                        if (Dr == DialogResult.Yes)
                        {
                            //Blank item master
                            Item_Master IM = new Item_Master(BillNumbers[i], "", 0);

                            IM.ShowDialog();

                            if (IM.DialogResult == DialogResult.Cancel)
                                BillNumbers.RemoveAt(i);
                        }
                        else
                        {
                            //Remove from the bill to proceed
                            BillNumbers.RemoveAt(i);
                        }
                    }
                }
            }

            BillQty = _BillQty;

            return BillNumbers;
        }

        public bool CheckBill(List<string> BillNumbers, IEdmVault7 vault, EdmCmdData file)
        {
            for (int i = 0; i < BillNumbers.Count; i++)
            {
                bool exists = DataList.PartExists(BillNumbers[i]);

                if (!exists)
                {
                    string Config;

                    IEdmFile7 Part = FindPartinVault(vault, file, BillNumbers[i], out Config);

                    if (Part != null)
                    {
                        try
                        {
                            GetItemInfo(file, vault, Part);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\nAn error has occured with this item. Check its properties to ensure no errors and try again","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        DialogResult DR = MessageBox.Show("File not found in vault.\nDo you want to manually add this item?", "Part Not Found!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (DR == DialogResult.Yes)
                        {
                            Item_Master IM = new Item_Master();

                            IM.ShowDialog();
                        }
                        else
                            return false;
                    }
                }
            }
            return true;
        }

        public DialogResult AddBill(IEdmVault7 vault, EdmCmdData file)
        {
            IEdmEnumeratorVariable5 var;

            string selected_config;

            IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            //Get the lastest version to continue
            if (UpdateItemRef(file,part, vault))
            {
                var = part.GetEnumeratorVariable();

                selected_config = DetermineConfig(part, vault, file);


                if (selected_config != "")
                {
                    #region Fill Bill

                    IEdmBomMgr BomMgr = (IEdmBomMgr)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);

                    Array LayoutVal;

                    Array BomVal = Array.CreateInstance(typeof(EdmBomInfo), 1);

                    Array ColumnVal;

                    BomMgr.GetBomLayouts(out LayoutVal);

                    IEdmBomMgr EdmBomMgr = (IEdmBomMgr)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);

                    Array BomLayouts;

                    EdmBomMgr.GetBomLayouts(out BomLayouts);

                    //selected_config = something;

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

                        if (itemlevel == 1)
                        {
                            EdmBomColumnType ColType = EdmBomColumnType.EdmBomCol_RefCount;

                            bominfo.GetVar(0, ColType, out Value, out CompVal, out Config, out RO);

                            BillQty.Add(Value.ToString());

                            ColType = EdmBomColumnType.EdmBomCol_PartNumber;

                            bominfo.GetVar(0, ColType, out Value, out CompVal, out Config, out RO);

                            BillNumbers.Add(Value.ToString());
                        }
                    }
                    #endregion

                    ProcessBill(vault, file, BillNumbers, BillQty,out BillQty);
                    
                    Bill_Master BM = new Bill_Master(BillNumbers, BillQty, ParentNumber, "");

                    BM.ShowDialog();

                    return BM.DialogResult;
                }
            }
            return DialogResult.Cancel;
        }

        public DialogResult GetItemInfo(EdmCmdData file, IEdmVault7 vault, IEdmFile7 Part)
        {
            IEdmEnumeratorVariable5 var;

            string selected_config;

            object partnum_val;

            object desc_val;

            object weight_val;

            object product_val;

            object class_val;

            object type_val;

            if (UpdateItemRef(file,Part, vault))
            {
                var = Part.GetEnumeratorVariable();

                decimal weight_fallback = 0;

                selected_config = DetermineConfig(Part, vault, null);

                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Description", selected_config, out desc_val);

                var.GetVar("Product", selected_config, out product_val);

                var.GetVar("Class", selected_config, out class_val);

                var.GetVar("Type", selected_config, out type_val);

                //Weight is typically @ config
                var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val == null)
                    var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val != null)
                    decimal.TryParse(weight_val.ToString(), out weight_fallback);

                if (product_val == null)
                    product_val = "";

                if (class_val == null)
                    class_val = "";

                if (desc_val == null)
                    desc_val = "";

                if (partnum_val != null)
                {
                    Epicor_Integration.Item_Master item = new Item_Master(partnum_val.ToString(), desc_val.ToString(), weight_fallback, product_val.ToString(), class_val.ToString(), type_val.ToString());

                    item.ShowDialog();

                    return item.DialogResult;
                }
                else
                {
                    //Not necessary anymore
                    //MessageBox.Show("Part number was a null value!\n\nEnsure that custom properties are completely filled out.", "Missing Properties!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    return DialogResult.Cancel;
                }
            }
            else
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

            object product_val;

            object class_val;

            object type_val;
                         
            part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            if (UpdateItemRef(file,part, vault))
            {
                var = part.GetEnumeratorVariable();

                decimal weight_fallback = 0;

                selected_config = DetermineConfig(part,vault,file);

                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Description", selected_config, out desc_val);

                var.GetVar("Product", selected_config, out product_val);

                var.GetVar("Class", selected_config, out class_val);

                var.GetVar("Type", selected_config, out type_val);

                //Weight is typically @ config
                var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val == null)
                    var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val != null)
                    decimal.TryParse(weight_val.ToString(), out weight_fallback);

                if (class_val == null)
                    class_val = "";

                if (type_val == null)
                    type_val = "";

                if (product_val == null)
                    product_val = "";

                if (desc_val == null)
                    desc_val = "";

                if (partnum_val != null)
                {
                    Epicor_Integration.Item_Master item = new Item_Master(partnum_val.ToString(), desc_val.ToString(), weight_fallback,product_val.ToString(),class_val.ToString(),type_val.ToString());

                    item.ShowDialog();

                    return item.DialogResult;
                }
                else
                {
                    //Not necessary anymore
                    //MessageBox.Show("Part number was a null value!\n\nEnsure that custom properties are completely filled out.", "Missing Properties!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    return DialogResult.Cancel;
                }
            }
            else
                return DialogResult.Cancel;
        }

        public bool PartExistsSW(IEdmVault7 vault, string SearchPart)
        {
            object partnum_val = "";

            IEdmSearch5 Search = vault.CreateSearch();

            Search.Clear();

            //Search.FileName = SearchPart;

            Search.AddVariable("Number", SearchPart);

            Search.FindFolders = false;

            Search.FindFiles = true;

            IEdmSearchResult5 Result = Search.GetFirstResult();

            if (Result != null)
                return true;
            else
                return false;
        }

        public IEdmFile7 FindPartinVault(IEdmVault7 vault, EdmCmdData file, string SearchPart,out string Config)
        {
            string selected_config = null;

            object partnum_val = "";

            IEdmSearch5 Search = vault.CreateSearch();

            Search.Clear();
            
            //Search.FileName = SearchPart;

            Search.AddVariable("Number", SearchPart);

            Search.FindFolders = false;

            Search.FindFiles = true;

            IEdmSearchResult5 Result = Search.GetFirstResult();

            if (Result != null)
            {
                IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, Result.ID);

                IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

                DialogResult DR = MessageBox.Show("A component not entered into Epicor has been located in the vault.\nThe system will attempt to select the correct configuration if necessary.\n\nSearch Part Number: " + SearchPart, "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (DR == DialogResult.OK)
                    selected_config = DetermineConfig(part, vault, SearchPart);
                else
                {
                    Config = null;

                    return null;
                }

                var.GetVar("Number", selected_config, out partnum_val);
                
                while (partnum_val.ToString() != SearchPart)
                {
                    Result = Search.GetNextResult();

                    part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, Result.ID);

                    var = part.GetEnumeratorVariable();

                    selected_config = DetermineConfig(part, vault, SearchPart);

                    var.GetVar("Number", selected_config, out partnum_val);
                }

                Config = selected_config;

                return part;
            }
            else
            {
                Config = null;

                return null;
            }
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
    }
}
