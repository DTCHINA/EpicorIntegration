using EdmLib;
using Epicor_Integration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using EPDM_EPICOR_LIB;
using System.ComponentModel;
using System.Reflection;

namespace EPDMEpicorIntegration
{
    //Release GUID
    [Guid("9e974a5f-3bd9-4d32-9976-44efa09d6ee7"), ComVisible(true)]
 
    //Test GUID
    //[Guid("194D5C17-3B13-40EA-B695-15E502AA6412"), ComVisible(true)]
    
    public class Addin : IEdmAddIn5
    {
        public int AssemblyVersion
        {
            get
            {
                string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                ver = ver.Replace(".", "0");

                return int.Parse(ver);
            }
        }

        Waiting BWForm { get; set; }

        BackgroundWorker BW = new BackgroundWorker();

        IEdmVault7 vault_ { get; set; }

        EdmCmdData file_ { get; set; }

        double[] Mins = new double[8];

        private List<BillItem> Bill { get; set; }

        private decimal Weight { get; set; }

        private decimal Area { get; set; }

        private string ParentNumber { get; set; }

        void IEdmAddIn5.GetAddInInfo(ref EdmAddInInfo poInfo, IEdmVault5 poVault, IEdmCmdMgr5 poCmdMgr)
        {
            string MenuName;

            bool test = false;

            //test = true;

            if (!test)
            {
                poInfo.mbsAddInName = "EpicorIntegration";

                MenuName = "Epicor Integration";
            }
            else
            {
                poInfo.mbsAddInName = "Epicor-Integration";

                MenuName = "Debug";
            }
            
            poInfo.mbsCompany = "Norco Industries";
            poInfo.mbsDescription = "Epicor Integration Enterprise PDM Add-in";
            poInfo.mlAddInVersion = (int)2015032500;//(int)AssemblyVersion;

            //Minimum Conisio version needed for .Net Add-Ins is 6.4
            poInfo.mlRequiredVersionMajor = 6;
            poInfo.mlRequiredVersionMinor = 4;

            poCmdMgr.AddCmd(1, MenuName + "\\Part Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update Item in Epicor", 0, 0); ;

            poCmdMgr.AddCmd(4, MenuName + "\\Add Revision", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to add revision to Item in Epicor", 0, 0);

            poCmdMgr.AddCmd(5, MenuName + "\\Operation Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update OOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(6, MenuName + "\\Bill Master", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to create/update BOM in Epicor", 0, 0);

            poCmdMgr.AddCmd(7, MenuName + "\\Check In/Approve Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to Approve and Check In Item to Epicor", 0, 0);

            poCmdMgr.AddCmd(-2, MenuName + "\\Calculate Assembly Minutes", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to Approve and Check In Item to Epicor", 0, 0);

            poCmdMgr.AddCmd(3, MenuName + "\\Check Out Item", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Checks out Item in Epicor (Not Enterprise PDM)", 0, 0);

            poCmdMgr.AddCmd(0, MenuName + "\\RevCompare", (int)EdmMenuFlags.EdmMenu_Nothing + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to compare previous revisions to the current SolidWorks bill", 0, 0);

            poCmdMgr.AddCmd(-1, MenuName + "\\Add-in Configuration", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Launches a dialog to configure Epicor Integration Add-in", 0, 0);

            poCmdMgr.AddCmd(-10, MenuName + "\\Update Properties from Epicor", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to update file properties from current Epicor values", 0, 0);

            poCmdMgr.AddCmd(-100, MenuName + "\\Templates", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Launches a dialog to Add/Edit/Update Templates", 0, 0);

            //poCmdMgr.AddCmd(-101, MenuName + "\\Search", (int)EdmMenuFlags.EdmMenu_Nothing, "", "Temp", 0, 0);

            poCmdMgr.AddCmd(2, MenuName + "\\Add Item,OOM & BOM", (int)EdmMenuFlags.EdmMenu_OnlyFiles + (int)EdmMenuFlags.EdmMenu_MustHaveSelection + (int)EdmMenuFlags.EdmMenu_OnlySingleSelection, "", "Launches a dialog to add an Item, a revision, an OOM and BOM in Epicor", 0, 0);
            
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_Menu, null);

            Bill = new List<BillItem>();
        }

        void IEdmAddIn5.OnCmd(ref EdmCmd poCmd, ref System.Array ppoData)
        {
            try
            {
                BW.RunWorkerCompleted += BW_RunWorkerCompleted;

                BW.WorkerSupportsCancellation = true;

                Debug.Print("Command Type: " + poCmd.meCmdType.ToString() + "\n  " + System.DateTime.Now.ToString());

                IEdmVault5 edmVault = poCmd.mpoVault as IEdmVault5;

                EdmCmdData[] Temp = (EdmCmdData[])ppoData;

                IEdmVault7 vault = (IEdmVault7)poCmd.mpoVault;

                vault_ = vault;

                try
                {
                    file_ = Temp[0];
                }
                catch { }

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
                                            GetItemInfo(vault, file, "");
                                    }
                                    #endregion
                                    break;
                                case 2:
                                    #region Add Item/Rev/OOM/BOM

                                    string config;

                                    foreach (EdmCmdData file in Temp)
                                    {
                                        if (ValidSelection(file))
                                        {
                                            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

                                            string selected_config = DetermineConfig(part, vault, file, "");

                                            if (GetItemInfo(vault, file, selected_config) == DialogResult.Cancel)
                                                break;

                                            if (AddRevision(vault, file, selected_config) == DialogResult.Cancel)
                                                break;

                                            if (AddOOM(vault, file, selected_config) == DialogResult.Cancel)
                                                break;

                                            #region Bill Master

                                            BWForm = new Waiting("Retrieving Bill of Materials from SolidWorks...");

                                            ParentNumber = "";

                                            Area = 0;

                                            Weight = 0;

                                            Bill.Clear();

                                                string sconfig;

                                                AddBill(vault, file, "", out sconfig);

                                                if (sconfig == null)
                                                    break;

                                                Bill.Sort((x, y) => x.PartNumber.CompareTo(y.PartNumber));

                                                CombineBill();

                                                Bill_Master BM = new Bill_Master(Bill, ParentNumber, "", Weight, Area);

                                            #endregion

                                            if (BM.ShowDialog() == DialogResult.Cancel)
                                                break;

                                            if (CheckInPart(vault, file, selected_config) == DialogResult.Cancel)
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
                                            AddRevision(vault, file, "");
                                    }

                                    #endregion
                                    break;
                                case 5:
                                    #region OOM_Master

                                    foreach (EdmCmdData file in Temp)
                                    {
                                        if (ValidSelection(file))
                                            AddOOM(vault, file, "");
                                    }

                                    #endregion
                                    break;
                                case 6:
                                    #region Bill Master

                                    BWForm = new Waiting("Retrieving Bill of Materials from SolidWorks...");

                                    foreach (EdmCmdData file in Temp)
                                    {
                                        ParentNumber = "";

                                        Area = 0;

                                        Weight = 0;

                                        try
                                        {

                                            //BW = null;

                                            //BW = new BackgroundWorker();

                                            Bill.Clear();

                                            string sconfig;

                                            AddBill(vault, file, "", out sconfig);

                                            if (sconfig == null)
                                                break;

                                            Bill.Sort((x, y) => x.PartNumber.CompareTo(y.PartNumber));

                                            CombineBill();

                                            Bill_Master BM = new Bill_Master(Bill, ParentNumber, "", Weight, Area);

                                            BM.ShowDialog();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                    #endregion
                                    break;
                                case 7:
                                    #region CheckIn_Master

                                    foreach (EdmCmdData file in Temp)
                                    {
                                        if (ValidSelection(file))
                                            CheckInPart(vault, file, "");
                                    }
                                    #endregion
                                    break;
                                case 0:
                                    #region RevCompare

                                    BWForm = new Waiting("Retrieving Bill of Materials from SolidWorks...");

                                    foreach (EdmCmdData file in Temp)
                                    {
                                        try
                                        {
                                            string file_ext = file.mbsStrData1.Substring(file.mbsStrData1.LastIndexOf('.') + 1).ToUpper();
                                            if (file_ext == "SLDASM")
                                            {
                                                if (file.mlObjectID1 != null)
                                                {
                                                    System.Data.DataSet DS = new System.Data.DataSet();

                                                    DS.Tables.Add("PartMtl");

                                                    DS.Tables[0].Columns.Add("MtlPartNum");

                                                    DS.Tables[0].Columns.Add("QtyPer");

                                                    Bill.Clear();

                                                    ParentNumber = "";

                                                    string rconfig;

                                                    AddBill(vault, file, "", out rconfig);

                                                    if (rconfig == null)
                                                        break;

                                                    try
                                                    {
                                                        Bill.Sort((x, y) => x.PartNumber.CompareTo(y.PartNumber));

                                                        CombineBill();

                                                        foreach (BillItem item in Bill)
                                                        {
                                                            System.Data.DataRow dr = DS.Tables[0].NewRow();

                                                            dr["MtlPartNum"] = item.PartNumber;

                                                            dr["QtyPer"] = item.Qty;

                                                            DS.Tables[0].Rows.Add(dr);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    { MessageBox.Show(ex.Message); }

                                                    try
                                                    {
                                                        RevCompare RC = new RevCompare(DS, ParentNumber);

                                                        RC.ShowDialog();
                                                    }
                                                    catch (Exception ex)
                                                    { MessageBox.Show(ex.Message); }
                                                }
                                                else
                                                {
                                                    RevCompare RC = new RevCompare();

                                                    RC.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("This function cannot be used on Parts.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                    break;
                                    #endregion
                                case -2:
                                    #region Op Minutes
                                    foreach (EdmCmdData file in Temp)
                                    {
                                        try
                                        {
                                            Mins[0] = 0; //Huck
                                            Mins[1] = 0; //Tec
                                            Mins[2] = 0; //Bolt
                                            Mins[3] = 0; //Rivet
                                            Mins[4] = 0; //Band
                                            Mins[5] = 0; //Heat
                                            Mins[6] = 0; //Spring
                                            Mins[7] = 0; //Zhooks

                                            BW = null;

                                            BW = new BackgroundWorker();

                                            BW.WorkerSupportsCancellation = true;

                                            CalcMins(vault, file, "");

                                            Epicor_Integration.Operations_Minutes OpsMins = new Operations_Minutes(Mins[0].ToString(), Mins[1].ToString(), Mins[2].ToString(), Mins[3].ToString(), Mins[4].ToString(), Mins[5].ToString(), Mins[6].ToString(), Mins[7].ToString());

                                            OpsMins.Show();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                    break;
                                    #endregion
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
                                case - 101:
                                    SearchPart SP = new SearchPart();

                                    SP.ShowDialog();

                                    break;
                                default:
                                    break;
                            } break;
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
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BWForm.Close();
        }

        void BW_DoWorkAssy(object sender, DoWorkEventArgs e)
        {
            List<BillItem> WorkingBill = new List<BillItem>();

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

                        //MessageBox.Show(PnumValue.ToString());

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

                                WorkingBill.Add(Item);
                            }
                        }
                    }
                    else
                    {
                        BillItem Item = new BillItem();

                        Item.Qty = QtyValue.ToString();

                        Item.PartNumber = PnumValue.ToString();

                        WorkingBill.Add(Item);
                    }

                }

                bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out PnumValue, out CompVal, out Config, out RO);

                bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_Name, out Name, out CompVal, out Config, out RO);

                Debug.Print(PnumValue.ToString() + "\t" + itemlevel.ToString() + "\t" + Name.ToString());

            }
            #endregion

            Bill = WorkingBill;

            if (BW.IsBusy)
                try
                {
                    BW.CancelAsync();
                }
                catch (Exception ex)
                { Debug.Print(ex.Message); }
        }

        void BW_DoWorkCalc(object sender, DoWorkEventArgs e)
        {
            object weight_val = "0";

            object area_val = "0";

            object[] args = new object[3];

            args = (object[])e.Argument;

            IEdmVault7 vault = (IEdmVault7)args[1];

            IEdmFile7 part = (IEdmFile7)args[0];

            string selected_config = args[2].ToString();

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

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

                object CalcType;

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

                        EdmBomView SubBomView = subpart.GetComputedBOM(1, 0, ConfValue.ToString(), 2);

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

                Debug.Print(PnumValue.ToString() + "\t" + itemlevel.ToString() + "\t" + Name.ToString());

            }
            #endregion

            if (BW.IsBusy)
                BW.CancelAsync();
        }

        void BW_DoWorkDrawing(object sender, DoWorkEventArgs e)
        {
            object[] args = new object [2];

            args = (object[])e.Argument ;

            IEdmVault7 vault = (IEdmVault7)args[1];

            IEdmFile7 part = (IEdmFile7)args[0];

            Array bom_array;

            part.GetDerivedBOMs(out bom_array);

            EdmBomInfo[] BOMs = (EdmBomInfo[])bom_array;

            IEdmBom bom = (IEdmBom)vault.GetObject(EdmObjectType.EdmObject_BOM, BOMs[BOMs.GetUpperBound(0)].mlBomID);

            EdmBomView bomView = bom.GetView();

            Array BomVal;

            bomView.GetRows(out BomVal);

            Bill = null;

            List<string> BillLevel = new List<string>();

            ParentNumber = BOMs[0].mbsBomName;

            ParentNumber = ParentNumber.Substring(0, BOMs[0].mbsBomName.IndexOf(".SLDDRW"));

            for (int i = 0; i < BomVal.Length; i++)
            {
                IEdmBomCell bominfo = (IEdmBomCell)BomVal.GetValue(i);

                object Value;

                object Val_Qty;

                object CompVal;

                string Config;

                bool RO;

                bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_RefCount, out Val_Qty, out CompVal, out Config, out RO);

                bominfo.GetVar(0, EdmBomColumnType.EdmBomCol_PartNumber, out Value, out CompVal, out Config, out RO);

                if (!Value.ToString().Contains("PART"))
                {
                    try
                    {
                        double QtyInt = double.Parse(Val_Qty.ToString());

                        BillItem Item = new BillItem();

                        Item.PartNumber = Value.ToString();

                        Item.Qty = Val_Qty.ToString();

                        Bill.Add(Item);
                    }
                    catch { }
                }
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

            string selected_config = DetermineConfig(part, vault, file, "");

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

        public DialogResult CheckInPart(IEdmVault7 vault, EdmCmdData file,string selected_config)
        {
            object partnum_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            if (selected_config == null || selected_config == "")
                selected_config = DetermineConfig(part, vault, file, "");

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

        public string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault,EdmCmdData file, string SearchTerm)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config;

            if (SearchTerm != "")
                config = new Config_Select(vault, Part, SearchTerm);
            else
                config = new Config_Select(vault, file);

            for (int i = 0; i < list.Count; i++)
            {
                string name = list.GetNext(pos);

                    if (name != "@")
                    {
                        object number;

                        object area;

                        object mass;

                        IEdmEnumeratorVariable5 var = Part.GetEnumeratorVariable();

                        var.GetVar("Number", name, out number);

                        var.GetVar("NetWeight", name, out mass);

                        var.GetVar("SurfaceArea", name, out area);

                        if (number != null && mass != null && area != null)
                        {
                            config.config_cbo.Items.Add(name);
                        }
                    }
            }

            config.config_cbo.SelectedIndex = 0;

            config.ShowDialog();

            retval = config.SelectedConfig;

            if (config.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "";

            return retval;
        }

        public DialogResult AddRevision(IEdmVault7 vault, EdmCmdData file, string selected_config)
        {
            object partnum_val;

            object rev_val;

            DialogResult RetVal = DialogResult.Cancel;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();
            if (selected_config == null || selected_config == "")
                selected_config = DetermineConfig(part, vault, file, "");

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

        public DialogResult AddOOM(IEdmVault7 vault, EdmCmdData file, string selected_config)
        {
            object partnum_val;

            object rev_val;

            IEdmFile5 part = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            IEdmEnumeratorVariable5 var = part.GetEnumeratorVariable();

            DialogResult RetVal = DialogResult.Cancel;
            if (selected_config == null || selected_config == "")
                selected_config = DetermineConfig(part, vault, file, "");

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

        public void ProcessBill(IEdmVault7 vault, EdmCmdData file)//, List<string> BillNumbers, List<string> _BillQty, out List<string> BillQty)
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

                        DialogResult Dr = GetItemInfo(file, vault, Part, "");

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

        public bool CheckBill(List<string> BillNumbers, IEdmVault7 vault, EdmCmdData file)
        {
            for (int i = 0; i < BillNumbers.Count; i++)
            {
                bool exists = DataList.PartExists(BillNumbers[i]);

                if (!exists)
                {
                    string Config;

                    IEdmFile7 Part = FindPartinVault(vault, BillNumbers[i], out Config);

                    if (Part != null)
                    {
                        try
                        {
                            GetItemInfo(file, vault, Part, "");
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

        public void AddBill(IEdmVault7 vault, EdmCmdData file,string selected_config, out string config)
        {
            config = null;

            IEdmEnumeratorVariable5 var;

            IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            string file_ext = file.mbsStrData1.Substring(file.mbsStrData1.LastIndexOf('.') + 1).ToUpper();

            if (file_ext != "SLDDRW" || UpdateItemRef(file, part, vault))
            {
                var = part.GetEnumeratorVariable();

                selected_config = DetermineConfig(part, vault, file, "");

                config = selected_config;
            }

            #region Drawing Bill
            if (file_ext == "SLDDRW")
            {
                config = "DRAWING";

                BW.DoWork += BW_DoWorkDrawing;

                object[] args = new object[2];

                args[0] = part;

                args[1] = vault;

                BW.RunWorkerAsync(args);

                BWForm.ShowDialog();

                ProcessBill(vault, file);

                BW.DoWork -= BW_DoWorkDrawing;
            }
            #endregion
            else
            {
                    if (selected_config == "")
                    {
                        config = null;
                        return; 
                    }
                        BW.DoWork += BW_DoWorkAssy;

                        object[] args = new object[3];

                        args[0] = part;

                        args[1] = vault;

                        args[2] = selected_config;

                        try
                        {
                            BW.RunWorkerAsync(args);
                        }
                        catch {

                            if (BW.IsBusy == true)
                            {
                                BW.CancelAsync();

                                while (!BW.CancellationPending)
                                {
                                    BW.RunWorkerAsync(args);
                                }
                            }
                        
                        }
                        BWForm.ShowDialog();

                        ProcessBill(vault, file);

                        BW.DoWork -= BW_DoWorkAssy;
            }
        }

        public void CalcMins(IEdmVault7 vault, EdmCmdData file, string selected_config)
        {
            IEdmEnumeratorVariable5 var;

            IEdmFile7 part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            string file_ext = file.mbsStrData1.Substring(file.mbsStrData1.LastIndexOf('.') + 1).ToUpper();

            if (UpdateItemRef(file, part, vault))
            {
                var = part.GetEnumeratorVariable();

                selected_config = DetermineConfig(part, vault, file, "");
            }

            if (selected_config != "")
            {
                BW.DoWork += BW_DoWorkCalc;

                object[] args = new object[3];

                args[0] = part;

                args[1] = vault;

                args[2] = selected_config;

                BW.RunWorkerAsync(args);

                BWForm.ShowDialog();

                foreach (BillItem item in Bill)
                {
                    string Config = null;

                    object val = null;

                    IEdmFile7 Part = FindPartinVault(vault, item.ToString(), Config);

                    EdmStrLst5 list = Part.GetConfigurations();

                    IEdmPos5 pos = list.GetHeadPosition();

                    Config = list.GetNext(pos);

                    Config = list.GetNext(pos);

                    if (Config == "PreviewCfg")
                        Config = list.GetNext(pos);

                    var = Part.GetEnumeratorVariable();

                    var.GetVar("HW Code", Config, out val);

                    if (val != null)
                    {
                        if (val.ToString() == "HUCK")
                            Mins[0] += double.Parse(item.Qty);

                        if (val.ToString() == "TEC")
                            Mins[1] += double.Parse(item.Qty);

                        if (val.ToString() == "BOLT")
                            Mins[2] += double.Parse(item.Qty);

                        if (val.ToString() == "RIVET")
                            Mins[3] += double.Parse(item.Qty);

                        if (val.ToString() == "BAND")
                            Mins[4] += double.Parse(item.Qty);

                        if (val.ToString() == "HEAT")
                            Mins[5] += double.Parse(item.Qty);

                        if (val.ToString() == "SPRING")
                            Mins[6] += double.Parse(item.Qty);

                        if (val.ToString() == "ZHOOK")
                            Mins[7] += double.Parse(item.Qty);
                    }
                }

                BW.DoWork -= BW_DoWorkCalc;
            }
        }

        public string GetItemProperty(string PartNumber, string PropertyName)
        {
            object property = "";

            try
            {
                string Config;

                IEdmFile7 Part = FindPartinVault(vault_, PartNumber, out Config);

                if (UpdateItemRef(file_, Part, vault_))
                {
                    IEdmEnumeratorVariable5 var = Part.GetEnumeratorVariable();

                    var.GetVar(PropertyName, Config, out property);
                }

                if (property.ToString() == "" || property == null)
                {
                    Config = DetermineConfig(Part, vault_, file_, "");

                    IEdmEnumeratorVariable5 var = Part.GetEnumeratorVariable();

                    var.GetVar(PropertyName, Config, out property);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nPlease check the datacard to ensure that all fields are filled in the file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return property.ToString();

        }

        public DialogResult GetItemInfo(EdmCmdData file, IEdmVault7 vault, IEdmFile7 Part, string selected_config)
        {
            IEdmEnumeratorVariable5 var;  

            object partnum_val;

            object desc_val = "";

            object weight_val;

            object product_val;

            object class_val;

            object type_val;

            object planner_val;

            if (UpdateItemRef(file,Part, vault))
            {
                try
                {
                    var = Part.GetEnumeratorVariable();

                    decimal weight_fallback = 0;

                    if (selected_config == "" || selected_config == null)
                        selected_config = DetermineConfig(Part, vault, file, "");
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
         
        public DialogResult GetItemInfo(IEdmVault7 vault, EdmCmdData file, string selected_config)
        {                           
            IEdmFile7 Part = (IEdmFile7)vault.GetObject(EdmObjectType.EdmObject_File, file.mlObjectID1);

            return GetItemInfo(file, vault, Part, selected_config);
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

        public IEdmFile7 FindPartinVault(IEdmVault7 vault, string SearchPart,out string Config)
        {
            EdmCmdData file;

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
                    selected_config = DetermineConfig(part, vault, file_, SearchPart);
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

                    selected_config = DetermineConfig(part, vault, file_, SearchPart);

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

        public void CombineBill()
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
        }
    }
}
