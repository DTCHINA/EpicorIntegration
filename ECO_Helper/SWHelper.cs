using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdmLib;
using Epicor_Integration;
using System.Windows.Forms;
using System.ComponentModel;
using EPDM_EPICOR_LIB;

namespace ECO_Helper
{
    public class SWHelper
    {
        Waiting BWForm = new Waiting("Retrieving Bill of Materials from SolidWorks...");

        BackgroundWorker BW = new BackgroundWorker();

        public List<BillItem> Bill = new List<BillItem>();

        private decimal Weight = 0;

        private decimal Area = 0;

        private string ParentNumber = "";

        public string GetCurrentRevision(string path, string partnumber)
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

        public string GetCurrentDescription(string path, string partnumber)
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

        public decimal GetCurrentWeight(string path, string partnumber)
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

        public IEdmVault7 LogInVault()
        {
            EdmVault5 vault = new EdmVault5();

            vault.LoginAuto("NORCO_PDM", 0);

            return vault;
        }

        public string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault, string partnumber)
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
        public void GetBill(string partpath, string partnumber)
        {
            Bill.Clear();

            BW.WorkerReportsProgress = true;

            string selected_config;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile7 part = (IEdmFile7)vault.GetFileFromPath(partpath, out ppoRetParentFolder);

            selected_config = DetermineConfig(part, vault, partnumber);

            IEdmEnumeratorVariable5 var;

            if (selected_config != "")
            {
                BW.DoWork += BW_DoWorkAssy;

                object[] args = new object[3];

                args[0] = part;

                args[1] = vault;

                args[2] = selected_config;

                BW.RunWorkerCompleted += BW_RunWorkerCompleted;

                BW.RunWorkerAsync(args);

                BWForm.ShowDialog();

                ProcessBill(vault);

                BW.DoWork -= BW_DoWorkAssy;
            }
        }

        void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BWForm.Close();
        }

        public IEdmFile7 FindPartinVault(IEdmVault7 vault, string SearchPart, string Config)
        {
            string selected_config = Config;

            object partnum_val = "";

            IEdmSearch5 Search = vault.CreateSearch();

            Search.Clear();

            Search.FileName = SearchPart;

            //Search.AddVariable("Number", SearchPart);

            //Search.AddVariable("Number", SearchPart);

            Search.FindFolders = false;

            Search.FindFiles = true;

            IEdmSearchResult5 Result = Search.GetFirstResult();

            if (Result != null)
            {
                IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, Result.ID);

                return part;
            }
            else
            {
                Config = null;

                return null;
            }
        }

        void BW_DoWorkAssy(object sender, DoWorkEventArgs e)
        {
            try
            {

                object weight_val = "0";

                object area_val = "0";

                object[] args = new object[3];

                args = (object[])e.Argument;

                IEdmVault7 vault = (IEdmVault7)args[1];

                IEdmFile7 part = (IEdmFile7)args[0];

                string selected_config = args[2].ToString();

                IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

                try
                {
                    var.GetVar("NetWeight", selected_config, out weight_val);

                    Weight = decimal.Parse(weight_val.ToString());

                    var.GetVar("SurfaceArea", selected_config, out area_val);

                    Area = decimal.Parse(area_val.ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\nWeight or Area could not be retrieved"); Weight = 0; Area = 0; }

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

                List<string> BillLevel = new List<string>();

                string PConfig = "";

                for (int i = 0; i < BomVal.Length; i++)
                {
                    IEdmBomCell bominfo = (IEdmBomCell)BomVal.GetValue(i);

                    object QtyValue;

                    object PnumValue;

                    object FnameValue;

                    object ConfValue;

                    object ParentConfig;

                    object Name;

                    object CompVal;

                    string Config;

                    bool RO;

                    int itemlevel = bominfo.GetTreeLevel();

                    if (itemlevel == 0)
                    {
                        bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out PnumValue, out CompVal, out Config, out RO);

                        ParentNumber = PnumValue.ToString();

                        bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Configuration, out ParentConfig, out CompVal, out Config, out RO);

                        PConfig = ParentConfig.ToString();
                    }

                    if (itemlevel == 1)
                    {

                        bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_RefCount, out QtyValue, out CompVal, out Config, out RO);

                        bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out PnumValue, out CompVal, out Config, out RO);

                        if (PnumValue.ToString() == PConfig)
                        {
                            bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Configuration, out ConfValue, out CompVal, out Config, out RO);

                            bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Name, out FnameValue, out CompVal, out Config, out RO);


                            IEdmFile7 subpart = FindPartinVault(vault, FnameValue.ToString(), ConfValue.ToString());

                            EdmBomView SubBomView = subpart.GetComputedBOM(1, 0, ConfValue.ToString(), 1);

                            Array SubBomVal = Array.CreateInstance(typeof(EdmBomInfo), 1);

                            Array SubColumnVal;

                            SubBomView.GetRows(out SubBomVal);

                            SubBomView.GetColumns(out SubColumnVal);

                            for (int j = 0; j < SubBomVal.Length; j++)
                            {
                                IEdmBomCell subbominfo = (IEdmBomCell)SubBomVal.GetValue(j);

                                int subitemlevel = subbominfo.GetTreeLevel();

                                if (subitemlevel == 1)
                                {
                                    object QtyValue2;

                                    subbominfo.GetVar(0, EdmBomColumnType.EdmBomCol_RefCount, out QtyValue2, out CompVal, out Config, out RO);

                                    subbominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out PnumValue, out CompVal, out Config, out RO);

                                    if (PnumValue == "")
                                        subbominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Configuration, out PnumValue, out CompVal, out Config, out RO);

                                    subbominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Name, out Name, out CompVal, out Config, out RO);

                                    BillItem Item = new BillItem();

                                    QtyValue2 = decimal.Parse(QtyValue.ToString()) * decimal.Parse(QtyValue2.ToString());

                                    Item.Qty = QtyValue2.ToString();

                                    Item.PartNumber = PnumValue.ToString();

                                    Bill.Add(Item);
                                }
                            }
                        }
                        else
                        {
                            BillItem Item = new BillItem();

                            Item.Qty = QtyValue.ToString();

                            Item.PartNumber = PnumValue.ToString();

                            Bill.Add(Item);
                        }

                    }

                    bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out PnumValue, out CompVal, out Config, out RO);

                    bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Name, out Name, out CompVal, out Config, out RO);

                    System.Diagnostics.Debug.Print(PnumValue.ToString() + "\t" + itemlevel.ToString() + "\t" + Name.ToString());

                }
                #endregion

                BW.ReportProgress(100);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
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

        public IEdmFile7 FindPartinVault(IEdmVault7 vault, string SearchPart, out string Config)
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

        public bool HaveUpToDateItemRef(IEdmFile5 Part, IEdmVault7 vault, string Path)
        {
            bool retval = false;

            long Local = Part.GetLocalVersionNo(Path);

            int Server = Part.CurrentVersion;

            if (Local == Server)
                retval = true;

            return retval;
        }

        public bool UpdateItemRef(IEdmFile5 Part, IEdmVault7 vault, string Path)
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

        public DialogResult GetItemInfobyPath(IEdmVault7 vault, IEdmFile7 Part, string Path)
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

        public List<string> ProcessBill(IEdmVault7 vault, List<string> BillNumbers, List<string> _BillQty, out List<string> BillQty, string Path)
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

                        DialogResult Dr = GetItemInfobyPath(vault, Part, Path);

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

        public void ProcessBill(IEdmVault7 vault)//, List<string> BillNumbers, List<string> _BillQty, out List<string> BillQty)
        {
            for (int i = 0; i < Bill.Count; i++)
            {
                if (DataList.PartExists(Bill[i].ToString()))
                { /*Part exists and we are good*/}
                else
                {
                    if (PartExistsSW(vault, Bill[i].ToString()))
                    {
                        //Get it, Add it
                        string Config = null;

                        IEdmFile7 Part = FindPartinVault(vault, Bill[i].ToString(), out Config);

                        DialogResult Dr = GetItemInfo(vault, Part, "");

                        if (Dr == DialogResult.Cancel)
                            Bill.RemoveAt(i);
                    }
                    else
                    {
                        DialogResult Dr = MessageBox.Show(Bill[i].ToString() + " was not found in the vault/Epicor database.  Do you want to manually add it?", "Missing Item!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                        if (Dr == DialogResult.Yes)
                        {
                            //Blank item master
                            Item_Master IM = new Item_Master(Bill[i].ToString(), "", 0);

                            IM.ShowDialog();

                            if (IM.DialogResult == DialogResult.Cancel)
                                Bill.RemoveAt(i);
                        }
                        else
                        {
                            //Remove from the bill to proceed
                            Bill.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public DialogResult GetItemInfo(IEdmVault7 vault, IEdmFile7 Part, string selected_config)
        {
            IEdmEnumeratorVariable5 var;

            object partnum_val;

            object desc_val = "";

            object weight_val;

            object product_val;

            object class_val;

            object type_val;

            object planner_val;

            if (UpdateItemRef(Part, vault))
            {
                try
                {
                    var = Part.GetEnumeratorVariable();

                    decimal weight_fallback = 0;

                    if (selected_config == "" || selected_config == null)
                        selected_config = DetermineConfig(Part, vault, "");
                    if (selected_config != "")
                    {
                        var.GetVar("Number", selected_config, out partnum_val);

                        if (partnum_val.ToString().Contains("201"))
                        {
                            DialogResult DR = MessageBox.Show("Part number identified as a frame.  Do you want to use the Customer/Model instead of SolidWorks description custom property?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (DR == DialogResult.Yes)
                            {
                                object cust_val;
                                var.GetVar("Customer", selected_config, out cust_val);

                                object model_val;
                                var.GetVar("Model", selected_config, out model_val);

                                desc_val = "FRAME " + cust_val.ToString() + " " + model_val.ToString();
                            }
                            else
                                var.GetVar("Description", selected_config, out desc_val);
                        }
                        else
                            var.GetVar("Description", selected_config, out desc_val);

                        var.GetVar("Brand", selected_config, out planner_val);

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nPlease check the datacard to ensure that all fields are filled in the file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return DialogResult.Cancel;
                }
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

        public bool HaveUpToDateItemRef(IEdmFile5 Part, IEdmVault7 vault)
        {
            bool retval = false;

            long Local = Part.GetLocalVersionNo(Part.GetLocalPath(0));

            int Server = Part.CurrentVersion;

            if (Local == Server)
                retval = true;

            return retval;
        }

        public List<BillItem> CombineBill(List<BillItem> Bill)
        {
            for (int i = 0; i < Bill.Count; i++)
            {
                for (int j = 0; j < Bill.Count; j++)
                {
                    if (i != j)
                    {
                        if (Bill[i].PartNumber == Bill[j].PartNumber)
                        {
                            Bill[i].Qty = (decimal.Parse(Bill[j].Qty) + decimal.Parse(Bill[i].Qty)).ToString();

                            Bill.RemoveAt(j);
                        }
                    }
                }
            }

            return Bill;
        }
    }
}
