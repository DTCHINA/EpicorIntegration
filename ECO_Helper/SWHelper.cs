using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdmLib;
using Epicor_Integration;
using System.Windows.Forms;

namespace ECO_Helper
{
    public static class SWHelper
    {
        public static string GetCurrentRevision(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("Revision", selected_config, out rev_val);

            return rev_val.ToString();
        }

        public static string GetCurrentDescription(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("Description", selected_config, out rev_val);

            return rev_val.ToString();
        }

        public static decimal GetCurrentWeight(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("NetWeight", selected_config, out rev_val);

            return decimal.Parse(rev_val.ToString());
        }

        public static IEdmVault7 LogInVault()
        {
            EdmVault5 vault = new EdmVault5();

            vault.LoginAuto("NORCO_PDM", 0);

            return vault;
        }

        public static string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault, string partnumber)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config = new Config_Select(vault, Part, partnumber);

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

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <param name="partpath"></param>
        /// <param name="partnumber"></param>
        /// <param name="rev"></param>
        public static void GetBill(string partpath, string partnumber,string rev)
        {
            string selected_config;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile7 part = (IEdmFile7)vault.GetFileFromPath(partpath, out ppoRetParentFolder);

            selected_config = DetermineConfig(part, vault, partnumber);

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

                ProcessBill(vault, BillNumbers, BillQty, out BillQty, partpath);

                //Bill_Master BM = new Bill_Master(Bill, BillNumbers, BillQty, ParentNumber, rev, 0, 0);

                //BM.ShowDialog();
            }
        }

        public static bool PartExistsSW(IEdmVault7 vault, string SearchPart)
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

        public static IEdmFile7 FindPartinVault(IEdmVault7 vault, string SearchPart, out string Config)
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

        public static bool HaveUpToDateItemRef(IEdmFile5 Part, IEdmVault7 vault, string Path)
        {
            bool retval = false;

            long Local = Part.GetLocalVersionNo(Path);//file.mlObjectID3);

            int Server = Part.CurrentVersion;

            if (Local == Server)
                retval = true;

            return retval;
        }

        public static bool UpdateItemRef(IEdmFile5 Part, IEdmVault7 vault, string Path)
        {
            bool retval = false;

            if (!HaveUpToDateItemRef(Part, vault, Path))
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

        public static DialogResult GetItemInfo(IEdmVault7 vault, IEdmFile7 Part, string Path)
        {
            IEdmEnumeratorVariable5 var;

            string selected_config;

            object partnum_val;

            object desc_val;

            object weight_val;

            object product_val;

            object class_val;

            object type_val;

            object planner_val;

            if (UpdateItemRef(Part, vault, Path))
            {
                var = Part.GetEnumeratorVariable();

                decimal weight_fallback = 0;

                selected_config = DetermineConfig(Part, vault, null);

                var.GetVar("Number", selected_config, out partnum_val);

                var.GetVar("Description", selected_config, out desc_val);

                var.GetVar("Product", selected_config, out product_val);

                var.GetVar("Class", selected_config, out class_val);

                var.GetVar("Type", selected_config, out type_val);

                var.GetVar("Planner", selected_config, out planner_val);

                //Weight is typically @ config
                var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val == null)
                    var.GetVar("NetWeight", selected_config, out weight_val);

                if (weight_val != null)
                    decimal.TryParse(weight_val.ToString(), out weight_fallback);

                if (partnum_val != null)
                {
                    Epicor_Integration.Item_Master item = new Item_Master(partnum_val.ToString(), desc_val.ToString(), weight_fallback, product_val.ToString(), class_val.ToString(), type_val.ToString(), planner_val.ToString());

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

        public static List<string> ProcessBill(IEdmVault7 vault, List<string> BillNumbers, List<string> _BillQty, out List<string> BillQty, string Path)
        {
            for (int i = 0; i < BillNumbers.Count; i++)
            {
                if (DataList.PartExists(BillNumbers[i]))
                { /*Part exists and we are good*/}
                else
                {
                    if (PartExistsSW(vault, BillNumbers[i]))
                    {
                        //Get it, Add it
                        string Config = null;

                        IEdmFile7 Part = FindPartinVault(vault, BillNumbers[i], out Config);

                        DialogResult Dr = GetItemInfo(vault, Part, Path);

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
    }
}
