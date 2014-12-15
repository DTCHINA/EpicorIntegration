using Epicor.Mfg.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EPDM_EPICOR_LIB;

namespace Epicor_Integration
{
    public partial class Bill_Master : Form
    {
        #region Variables

        Waiting BWForm = new Waiting("Loading Bill of Materials into Epicor...");

        private bool CheckedOut { get; set; }

        private string CheckedOut_Msg { get; set; }
      
        private EngWorkBench EngWB = new EngWorkBench(DataList.EpicConn);

        private EngWorkBenchDataSet _EngWBDS = new EngWorkBenchDataSet();

        private EngWorkBenchDataSet EngWBDS
        {
            get { return _EngWBDS; }
            set { _EngWBDS = value; }
        }

        List<string> BillOps { get; set; }

        #endregion
        
        public Bill_Master(string ParentNumber, string Rev)
        {
            InitializeComponent();

            BillDataGrid.AutoGenerateColumns = false;

            gid_txt.Text = Properties.Settings.Default.ecogroup;

            parentrev_txt.Text = Rev;

            partnum_txt.Leave += partnum_txt_Leave;

            mtlseq_txt.Text = GetNextSeq().ToString();

            parent_txt.Text = ParentNumber;

            try
            {
                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
            }
            catch
            {
                try
                {
                    MessageBox.Show("You must check part out before continuing.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    CheckOut_Master CO_M = new CheckOut_Master(parent_txt.Text);

                    DialogResult dr = CO_M.ShowDialog();

                    if (dr != DialogResult.Cancel)
                    {
                        EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                        BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
                    }
                    else
                        this.Close();
                }
                catch (Exception ex1) { MessageBox.Show(ex1.Message + "\n\nThis process will now close", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            #region Fill Operations

            EngWBDS.Tables["ECOOpr"].Columns.Add("FullCode", typeof(string), "OprSeq + ' - ' + OpDesc");

            ops_cbo.DataSource = EngWBDS.Tables["ECOOpr"];

            ops_cbo.DisplayMember = "FullCode";

            ops_cbo.ValueMember = "OprSeq";

            ops_cbo.SelectedValue = "10";

            #endregion

            UpdateParentDesc();

            EnableItemDetails();

            this.FormClosing += Bill_Master_FormClosing;

            //Setbill(BillParts, BillQty);
        }

        public Bill_Master(List<BillItem>Bill , string ParentNumber, string Rev, decimal weight_val, decimal area_val)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                weight.Value = weight_val;

                area.Value = area_val;
            }
            catch { }

            BillDataGrid.AutoGenerateColumns = false;

            gid_txt.Text = Properties.Settings.Default.ecogroup;

            if (Rev == "")
                parentrev_txt.Text = DataList.GetCurrentRev(ParentNumber);
            else
                parentrev_txt.Text = Rev;

            partnum_txt.Leave += partnum_txt_Leave;

            mtlseq_txt.Text = GetNextSeq().ToString();

            parent_txt.Text = ParentNumber;

            try
            {
                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
            }
            catch 
            {
                try
                {
                    MessageBox.Show("You must check part out before continuing.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    CheckOut_Master CO_M = new CheckOut_Master(parent_txt.Text);

                    DialogResult dr = CO_M.ShowDialog();

                    if (dr != DialogResult.Cancel)
                    {
                        EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                        BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
                    }
                    else
                        this.Close();
                }
                catch (Exception ex1) {MessageBox .Show (ex1.Message + "\n\nThis process will now close","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            }

            this.FormClosing += Bill_Master_FormClosing;

            BW.RunWorkerAsync(Bill);

            try
            {
                BWForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Bill Master Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);

                BWForm.Dispose();

                BWForm = new Waiting("Loading Bill of Materials into Epicor...");

                BWForm.ShowDialog();
            }
        }

        private void Bill_Master_Load(object sender, EventArgs e)
        {
            BillDataGrid.SelectionChanged += BillDataGrid_SelectionChanged;

            BillDataGrid.ClearSelection();

            try
            {
                BillDataGrid.CurrentCell = BillDataGrid.Rows[0].Cells[0];
            }
            catch { }

            UpdateFormFields();

            #region Close if not checked out
            
            /*
            string Message;

            if (!DataList.PartCheckOutStatus(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, out Message))
            {
                MessageBox.Show("Part must be checked out by selected Group ID to continue.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                if (Message != "Checked Out by GroupID")
                {
                    MessageBox.Show("Part must be checked out by selected Group ID to continue.\n\n" + Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                }
            }*/

            #endregion

            FillRawMenu();

            if (BillDataGrid.Rows.Count == 0)
                addraw.Enabled = false;

            EnableNew.Enabled = true;
        }
                       
        /// <summary>
        /// Finds current safe parts within the current bill of materials and keeps a separate list of them to add after the bom has been updated (Sheets/Coils/E-Coat)
        /// </summary>
        /// <param name="BillParts">Current Epicor Bill of Materials</param>
        /// <param name="BillQty">Current Epicor Bill of Materials Qty</param>
        /// <param name="Bill_Qty">Bill of Materials Qty for only Safe items</param>
        /// <returns>Bill of Materials for only safe items</returns>
        public List<BillItem> SafeParts(List<BillItem> BillParts,List <string> BillOps, List<string> BillUOM, out List<string> BillOpts, out List<string> Bill_Ops, out List<string>Bill_UOM)
        {
            //out List<string> Bill_Qty, , List<string> BillQty

            List<BillItem> RetVal = new List<BillItem>();

            //List<string> RetVal_Qty = new List<string>();

            List<string> RetVal_Ops = new List<string>();

            List<string> RetVal_UOM = new List<string>();

            BillOpts = new List<string>();

            List<RawMaterial> ListtoSave = new List<RawMaterial>();

            ListtoSave.AddRange(DataList.GetCoils());

            ListtoSave.AddRange(DataList.GetEcoat());

            ListtoSave.AddRange(DataList.GetSheets());

            for (int i = 0; i < BillParts.Count; i++)
            {
                string view = (EngWBDS.Tables["ECOMtl"].Rows[i]["ViewAsAsm"].ToString() == "True" ? "1" : "0");

                string pull = (EngWBDS.Tables["ECOMtl"].Rows[i]["PullAsAsm"].ToString() == "True" ? "1" : "0");

                for (int j = 0; j < ListtoSave.Count; j++)
                {
                    if (BillParts[i].PartNumber == ListtoSave[j].part_number)
                    {
                        BillItem Item = new BillItem();

                        Item.PartNumber = BillParts[i].PartNumber;

                        Item.Qty = BillParts[i].Qty;

                        RetVal.Add(Item);

                        //RetVal_Qty .Add(BillParts[i].Qty);

                        BillOpts.Add(view + pull);

                        RetVal_Ops.Add(BillOps[i]);

                        RetVal_UOM.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["UOMCode"].ToString());
                    }
                }
            }

            Bill_Ops = RetVal_Ops;

            //Bill_Qty = RetVal_Qty;

            Bill_UOM = RetVal_UOM;

            return RetVal;
        }
        
        /// <summary>
        /// Sequence to determine if a part is a coil or a sheet based on Type
        /// </summary>
        /// <param name="PartNumber"></param>
        /// <returns>True if part is not a coil/sheet; False if part is coil/sheet</returns>
        public bool KeepRaw(string PartNumber)
        {
            Part Part = new Part(DataList.EpicConn);

            PartDataSet Pdata = new PartDataSet();

            Pdata = Part.GetByID(PartNumber);

            string Type = Pdata.Tables ["Part"].Rows[0]["ClassDescription"].ToString();

            if (Type == "INV COIL" || Type == "INV SHEET")
                return false;
            else
                return true;
        }

        public void AddBillItems(List<string> BillParts, List<string> BillQty)
        {
            for (int i = 0; i < BillParts.Count; i++)
            {
                try
                {
                    EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                    int rowindex = BillDataGrid.Rows.Count - 1;

                    BillDataGrid.ClearSelection();

                    BillDataGrid.CurrentCell = BillDataGrid.Rows[rowindex].Cells[0];

                    qty_num.Value = decimal.Parse(BillQty[i]);

                    ops_cbo.SelectedIndex = 0;

                    partnum_txt.Text = BillParts[i];

                    UpdateDescField();

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"] = ops_cbo.SelectedValue;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"] = 1;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"] = false;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"] = uom_cbo.Text;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["PullAsAsm"] = false;
                }
                catch (Exception Exception) { MessageBox.Show(Exception.Message, "Import Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void SaveChanges()
        {
            try
            {
                string opMessage;

                string opMsgType;

                List<bool> Modded = new List<bool>();

                List<bool> Added = new List<bool>();

                foreach (DataRow DR in EngWBDS.Tables["ECOMtl"].Rows)
                {
                    if (DR.RowState == DataRowState.Modified || DR.RowState == DataRowState.Added)
                    {
                        #region Validate all data
                        if (DR.RowState == DataRowState.Modified)
                        {
                            Modded.Add(true);
                            Added.Add(false);
                        }
                        else
                        {
                            Added.Add(true);
                            Modded.Add(false);
                        }

                        string partnumber = DR["MtlPartNum"].ToString();

                        string ops = DR["RelatedOperation"].ToString();

                        string mtlseq = DR["MtlSeq"].ToString();

                        EngWB.CheckECOMtlMtlPartNum(partnumber, out opMessage, out opMsgType, EngWBDS);

                        EngWB.CheckECOMtlMtlSeqRelatedOperation(int.Parse(mtlseq), int.Parse(ops), "", out opMessage, out opMsgType, EngWBDS);

                        EngWB.ChangeECOMtlQtyPer(EngWBDS);

                        EngWB.ChangeECOMtlRelatedOperation(int.Parse(ops), EngWBDS);

                        //Set changes to accepted to run ECOMtlRelated or it will change all modified rows to equal the last modified rows related operations
                        DR.AcceptChanges();

                        //EngWB.ChangeECOMtlMtlPartNum(EngWBDS);

                        #endregion
                    }
                    else
                    {
                        Modded.Add(false);

                        Added.Add(false);
                    }
                }

                for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
                {
                    if (Modded[i])
                    {
                        EngWBDS.Tables["ECOMtl"].Rows[i].SetModified();
                    }
                    if (Added[i])
                        EngWBDS.Tables["ECOMtl"].Rows[i].SetAdded();
                }
                try
                {
                    EngWB.Update(EngWBDS);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int GetNextSeq()
        {
            int rowcount = BillDataGrid.Rows.Count;

            return ++rowcount * 10;
        }

        private void EnableItemDetails()
        {
            partnum_txt.Enabled = !(BillDataGrid.Rows.Count == 0);

            desc_txt.Enabled = !(BillDataGrid.Rows.Count == 0);

            ops_cbo.Enabled = !(BillDataGrid.Rows.Count == 0);

            qty_num.Enabled = !(BillDataGrid.Rows.Count == 0);

            uom_cbo.Enabled = !(BillDataGrid.Rows.Count == 0);

            ViewAsAsm_chk.Enabled = !(BillDataGrid.Rows.Count == 0);

            findpart_btn.Enabled = !(BillDataGrid.Rows.Count == 0);
        }

        /// <summary>
        /// Retrieves and Updates bill of materials
        /// </summary>
        /// <param name="BillItems"></param>
        /// <param name="BillQty"></param>
        private void Setbill(List<BillItem>Bill)
        {
            List<bool> FindItemEpicor = new List<bool>();

            List<bool> FindItemSldWrks = new List<bool>();

            List<BillItem> AddBack = new List<BillItem>();

            List<string> AddBack_Opts = new List<string>();

            List<string> AddBack_Ops = new List<string>();

            List<string> AddBack_Uom = new List<string>();

            #region Locate items


            //See if the part exists in the SolidWorks data
            for (int i = 0; i < Bill.Count; i++)
            {FindItemSldWrks.Add(false);}

            //See if the part exists in the Epicor data
            for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
            {
                FindItemEpicor.Add(false);

                BillItem Item = new BillItem();

                Item.PartNumber = EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString();

                Item.Qty = EngWBDS.Tables["ECOMtl"].Rows[i]["QtyPer"].ToString();

                AddBack.Add(Item);

                //AddBack.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString());

                //AddBack_Qty.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["QtyPer"].ToString());

                AddBack_Ops.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["RelatedOperation"].ToString());

                AddBack_Uom.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["UOMCode"].ToString());
            }
        

            for (int i = 0; i < Bill.Count; i++)
            {
                for (int j = 0; j < EngWBDS.Tables["ECOMtl"].Rows.Count; j++)
                {
                    string DS_val = EngWBDS.Tables["ECOMtl"].Rows[j]["MtlPartNum"].ToString();

                    if (DS_val == Bill[i].PartNumber)
                        FindItemSldWrks[i] = true;
                }
            }

            for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
            {
                for (int j = 0; j < Bill.Count; j++)
                {
                    if (EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString() == Bill[j].PartNumber)
                        FindItemEpicor[i] = true;
                }
            }
            #endregion

            #region Determine what needs to be saved

            AddBack = SafeParts(AddBack, AddBack_Ops,AddBack_Uom, out AddBack_Opts, out AddBack_Ops, out AddBack_Uom);

            #endregion

            #region Delete Missing Items
            for (int i = EngWBDS.Tables["ECOMtl"].Rows.Count - 1; i > -1; i--)
            {
                if (!FindItemEpicor[i])
                {
                    //Remove items
                    EngWBDS.Tables["ECOMtl"].Rows[i].Delete();

                    EngWB.Update(EngWBDS);

                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
                }
            }
            #endregion

            int rowmod = EngWBDS.Tables["ECOMtl"].Rows.Count;

            #region Add missing items
            
            try
            {
                for (int i = 0; i < Bill.Count; i++)
                {
                    if (!FindItemSldWrks[i])
                    {
                        try
                        {
                            //rowmod = EngWBDS.Tables["ECOMtl"].Rows.Count;
                            //Add item
                            EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count-1]["MtlPartNum"] = Bill[i].PartNumber;

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count-1]["QtyPer"] = Bill[i].Qty;

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count-1]["RelatedOperation"] = ops_cbo.SelectedValue;

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count - 1]["PullAsAsm"] = false;

                            partnum_txt.Text = Bill[i].PartNumber;

                            DataTable ds = DataList.PartUOM(partnum_txt.Text);

                            uom_cbo.DataSource = ds;

                            uom_cbo.DisplayMember = "UOMCode";

                            uom_cbo.ValueMember = "UOMCode";

                            uom_cbo.SelectedIndex = 0;

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count - 1]["UOMCode"] = uom_cbo.SelectedValue;

                            decimal qty_val = decimal.Parse(Bill[i].Qty);

                            EngWBDS.Tables["ECOMtl"].Rows[EngWBDS.Tables["ECOMtl"].Rows.Count - 1]["QtyPer"] = qty_val;

                            EngWB.Update(EngWBDS);

                            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
                        }
                        catch { }
                    }
                }
            }
            catch  { }
            #endregion

            #region Add Saved items

            try
            {
                for (int i = 0; i < AddBack.Count; i++)
                {
                    rowmod = EngWBDS.Tables["ECOMtl"].Rows.Count;
                    //Add item               
                    EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");
                
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["MtlPartNum"] = AddBack[i];

                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = AddBack[i].Qty;

                    //EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = AddBack_Qty[i];

                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["RelatedOperation"] = AddBack_Ops[i];

                    char[] opts = AddBack_Opts[i].ToCharArray();

                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["ViewAsAsm"] = (opts[0] == '1' ? true : false);

                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["PullAsAsm"] = (opts[1] == '1' ? true : false);
      
                    partnum_txt.Text = AddBack[i].PartNumber;
            
                    DataTable ds = DataList.PartUOM(partnum_txt.Text);
           
                    uom_cbo.DataSource = ds;
        
                    uom_cbo.DisplayMember = "UOMCode";
        
                    uom_cbo.ValueMember = "UOMCode";
     
                    uom_cbo.SelectedIndex = 0;
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["UOMCode"] = AddBack_Uom[i];

                    uom_cbo.Text = AddBack_Uom[i];

                    decimal qty_val = decimal.Parse(AddBack[i].Qty);

                    //decimal qty_val = decimal.Parse(AddBack_Qty[i]);
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = qty_val;
      
                    EngWB.Update(EngWBDS);
       
                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
                }
            }
            catch{}

            #endregion

            #region Update per line
            for (int i = 0; i < Bill.Count; i++)
            {
                for (int j = 0; j < EngWBDS.Tables["ECOMtl"].Rows.Count; j++)
                {
                    string EpicValue = EngWBDS.Tables["ECOMtl"].Rows[j]["MtlPartNum"].ToString();

                    string _BillItem = Bill[i].PartNumber;

                    decimal EpicQty = decimal.Parse(EngWBDS.Tables["ECOMtl"].Rows[j]["QtyPer"].ToString());

                    decimal _BillQty = decimal.Parse(Bill[i].Qty);

                    if (EpicValue == _BillItem && EpicQty != _BillQty)
                    {
                        EngWBDS.Tables["ECOMtl"].Rows[j]["QtyPer"] = Bill[i].Qty;
                    }
                }
            }
            #endregion

            try
            {
                EngWB.Update(EngWBDS);

                EngWB.ResequenceMaterials(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, Properties.Settings.Default.mtlreqtype, false, false, false);

                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

                EnableItemDetails();

            }
            catch { }
        }

        #region Update Info

        private void UpdateEntireGrid()
        {
            for (int i = 0; i < BillDataGrid.Rows.Count; i++)
            {
                BillDataGrid.CurrentCell = BillDataGrid[0, i];

                BillDataGrid_SelectionChanged(BillDataGrid, null);
            }
        }

        private void UpdateFormFields()
        {
                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                if (rowindex != -1)
                {
                    mtlseq_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlSeq"].ToString();

                    try
                    {
                        qty_num.Value = decimal.Parse(EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"].ToString());
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }

                    partnum_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"].ToString();

                    desc_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNumPartDescription"].ToString();

                    ops_cbo.SelectedValue = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"].ToString();

                    partnum_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"].ToString();

                    string uom_orig = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"].ToString();

                    UpdateUOM();

                    uom_cbo.Text = uom_orig;

                    ViewAsAsm_chk.Checked = Boolean.Parse(EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"].ToString());

                    PullAsAsm_chk.Checked = Boolean.Parse(EngWBDS.Tables["ECOMtl"].Rows[rowindex]["PullAsAsm"].ToString());
                }
        }

        private void UpdateUOM()
        {
            string uom_orig = uom_cbo.Text;

            DataTable ds = DataList.PartUOM(partnum_txt.Text);

            uom_cbo.DataSource = ds;

            uom_cbo.DisplayMember = "UOMCode";

            uom_cbo.ValueMember = "UOMCode";

            uom_cbo.Text = uom_orig;

            try
            {
                //uom_cbo.Text = EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["UOMCode"].ToString();
            }
            catch { }

        }

        private void UpdateDescField()
        {
            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartListDataSet PartList = new PartListDataSet();

                string WhereClause = "PartNum = '" + partnum_txt.Text + "'";

                int pagesize = 1;

                bool morePages;

                PartList = Part.GetList(WhereClause, pagesize, 0, out morePages);

                DataList.EpicClose();

                desc_txt.Text = PartList.Tables[0].Rows[0]["PartDescription"].ToString();

                Part = null;

                PartList.Dispose();

                PartList = null;
            }
            catch { desc_txt.Text = ""; }
        }

        private void UpdateParentDesc()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "PartNum = '" + parent_txt.Text + "'";

            int pagesize = 1;

            bool morePages;

            PartList = Part.GetList(WhereClause, pagesize, 0, out morePages);

            DataList.EpicClose();

            parentdesc_txt.Text = PartList.Tables[0].Rows[0]["PartDescription"].ToString();

            Part = null;

            PartList.Dispose();

            PartList = null;
        }

        private void UpdateParentRev()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "PartNum = '" + parent_txt.Text + "'";

            int pagesize = 1;

            bool morePages;

            PartList = Part.GetList(WhereClause, pagesize, 0, out morePages);

            DataList.EpicClose();

            parentrev_txt.Text = PartList.Tables[0].Rows[0]["PartRevision"].ToString();

            Part = null;

            PartList.Dispose();

            PartList = null;
        }

        private void FillRawMenu()
        {
            #region Add Coil Menu

            List<RawMaterial> ListtoSave = new List<RawMaterial>();

            ToolStripMenuItem TS_item = new ToolStripMenuItem();

            TS_item.Name = "Coils";

            TS_item.Text = "Coils";

            ListtoSave = DataList.GetCoils();

            ListtoSave.Sort();

            for (int i = 0; i < ListtoSave.Count; i++)
            {
                ToolStripMenuItem TS_sub = new ToolStripMenuItem();

                TS_sub.Name = ListtoSave[i].part_number;

                TS_sub.Text = ListtoSave[i].ToString();

                TS_sub.ToolTipText = ListtoSave[i].part_number;

                TS_item.DropDownItems.Add(TS_sub);

                TS_sub.Click += TS_sub_Click;
            }

            RawMenu.Items.Add(TS_item);

            #endregion

            #region Add Ecoat menu

            ListtoSave = DataList.GetEcoat();

            ToolStripMenuItem TS_item1 = new ToolStripMenuItem();

            TS_item1.Name = "E-Coat";

            TS_item1.Text = "E-Coat";

            ListtoSave.Sort();

            for (int i = 0; i < ListtoSave.Count; i++)
            {
                ToolStripMenuItem TS_sub = new ToolStripMenuItem();

                TS_sub.Name = ListtoSave[i].part_number;

                TS_sub.Text = ListtoSave[i].ToString();

                TS_sub.ToolTipText = ListtoSave[i].part_number;

                TS_item1.DropDownItems.Add(TS_sub);

                TS_sub.Click += TS_sub_Click;
            }

            RawMenu.Items.Add(TS_item1);

            #endregion

            #region Add Sheet menu

            ListtoSave = DataList.GetSheets(); 
            
            ToolStripMenuItem TS_item2 = new ToolStripMenuItem();

            TS_item2.Name = "Sheets";

            TS_item2.Text = "Sheets";

            ListtoSave.Sort();

            for (int i = 0; i < ListtoSave.Count; i++)
            {
                ToolStripMenuItem TS_sub = new ToolStripMenuItem();

                TS_sub.Name = ListtoSave[i].part_number;

                TS_sub.Text = ListtoSave[i].ToString();

                TS_sub.ToolTipText = ListtoSave[i].part_number;

                TS_item2.DropDownItems.Add(TS_sub);

                TS_sub.Click += TS_sub_Click;
            }

            RawMenu.Items.Add(TS_item2);

            #endregion
        }

        #endregion

        #region Changed Values

        void PullAsAsm_chk_Click(object sender, EventArgs e)
        {
            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["PullAsAsm"] = PullAsAsm_chk.Checked;
        }

        void ViewAsAsm_chk_Click(object sender, EventArgs e)
        {
            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["ViewAsAsm"] = ViewAsAsm_chk.Checked;
        }

        void qty_num_Enter(object sender, EventArgs e)
        {
            qty_num.ValueChanged +=qty_num_ValueChanged;
        }

        private void qty_num_ValueChanged(object sender, EventArgs e)
        {
            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["QtyPer"] = qty_num.Value;
        }

        void qty_num_Leave(object sender, EventArgs e)
        {
            qty_num.ValueChanged -= qty_num_ValueChanged;
        }
        
        void ops_cbo_Enter(object sender, EventArgs e)
        {
            ops_cbo.SelectedIndexChanged += ops_cbo_SelectedIndexChanged;
        }

        private void ops_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ops_cbo.Text.Contains("COAT"))
            {
                uom_cbo.Text = "FT2";

                EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["UOMCode"] = uom_cbo.Text;
            }

            if (ops_cbo.Text.Contains("LASER") || ops_cbo.Text.Contains("TURRET"))
            {
                uom_cbo.Text = "LB";

                EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["UOMCode"] = uom_cbo.Text;
            }

            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["RelatedOperation"] = ops_cbo.SelectedValue;

            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["OpDesc"] = EngWBDS.Tables["ECOOpr"].Rows[ops_cbo.SelectedIndex]["OpDesc"];
        }

        void ops_cbo_Leave(object sender, EventArgs e)
        {
            ops_cbo.SelectedIndexChanged -= ops_cbo_SelectedIndexChanged;
        }

        void uom_cbo_Enter(object sender, EventArgs e)
        {
            uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;
        }

        private void uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["UOMCode"] = uom_cbo.Text;
        }

        void uom_cbo_Leave(object sender, EventArgs e)
        {
            uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;
        }

        private void partnum_txt_Enter(object sender, EventArgs e)
        {
            //Add Handle to change grid information
            partnum_txt.TextChanged +=partnum_txt_TextChanged;
        }

        private void partnum_txt_TextChanged(object sender, EventArgs e)
        {
                //Wait for user input to finish
                 PartTimer.Enabled = true;           
        }

        void partnum_txt_Leave(object sender, EventArgs e)
        {
            //Remove Handle to change grid information
            partnum_txt.TextChanged -= partnum_txt_TextChanged;

            PartTimer.Enabled = false;
        }

        private void PartTimer_Tick(object sender, EventArgs e)
        {

            if (DataList.PartExists(partnum_txt.Text))
            {
                try
                {
                    PartTimer.Enabled = false;

                    UpdateDescField();

                    EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["MtlPartNum"] = partnum_txt.Text;

                    EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["MtlPartNumPartDescription"] = desc_txt.Text;

                    //Update UOM list
                    try
                    {
                        DataTable ds = DataList.PartUOM(partnum_txt.Text);

                        uom_cbo.DataSource = ds;

                        uom_cbo.DisplayMember = "UOMCode";

                        uom_cbo.ValueMember = "UOMCode";

                        uom_cbo.Text = DataList.PartIUM(partnum_txt.Text);

                        EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["UOMCode"] = uom_cbo.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Could not retrieve UOM for this part number");
                    }

                    Part part = new Part(DataList.EpicConn);

                    bool morePages;

                    PartListDataSet Pdata = part.GetList("PartNum >= '" + partnum_txt.Text + "'", 100, 0, out morePages);

                    string Type = Pdata.Tables[0].Rows[0]["TypeCode"].ToString();

                    ViewAsAsm_chk.Checked = (Type == "M");

                    EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["ViewAsAsm"] = ViewAsAsm_chk.Checked;

                    bool PullAsAssy = false;

                    bool.TryParse(Pdata.Tables[0].Rows[0]["PhantomBOM"].ToString(), out PullAsAssy);

                    PullAsAsm_chk.Checked = PullAsAssy;

                    EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["PullAsAsm"] = PullAsAsm_chk.Checked;

                    if (desc_txt.Text.Contains("COIL") || desc_txt.Text.Contains("SHEET"))
                    {
                        try
                        {
                            Item_SheetFactor Sheet = new Item_SheetFactor(qty_num.Value, partnum_txt.Text);

                            Sheet.ShowDialog();

                            qty_num.Value = Sheet.FactoredWeight;

                            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["QtyPer"] = qty_num.Value;
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.Message); }
                    }
                }
                catch { desc_txt.Text = ""; }
            }
        }

        #endregion

        #region Control Routines

        void TS_sub_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem TS = (ToolStripMenuItem)sender;

            partnum_txt.Text = TS.ToolTipText;

            PartTimer.Enabled = true;
        }

        private void saveandclose_btn_Click(object sender, EventArgs e)
        {
            SaveChanges();

            this.Close();
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            addraw.Enabled = true;

            try
            {
                BillDataGrid.ClearSelection();

                SaveChanges();

                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

                //Add new line item
                EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                int rowindex = BillDataGrid.Rows.Count - 1;

                BillDataGrid.ClearSelection();

                BillDataGrid.CurrentCell = BillDataGrid.Rows[rowindex].Cells[0];

                qty_num.Value = 1;

                ops_cbo.SelectedIndex = 0;

                partnum_txt.Text = "";

                desc_txt.Text = "";

                PullAsAsm_chk.Checked = false;

                ViewAsAsm_chk.Checked = false;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"] = ops_cbo.SelectedValue;

                EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["OpDesc"] = EngWBDS.Tables["ECOOpr"].Rows[ops_cbo.SelectedIndex]["OpDesc"];

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"] = 1;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"] = false;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"] = uom_cbo.Text;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["PullAsAsm"] = false;

                EnableItemDetails();
            }
            catch { }

            EnableItemDetails();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            BillDataGrid.SelectionChanged -= BillDataGrid_SelectionChanged;

            try
            {
                if (partnum_txt.Text != "")
                    SaveChanges();

                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex].Delete();

                SaveChanges();
            }
            catch
            {
                MessageBox.Show("An error occured removing an item from the bill of materials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Get New Datatree
                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

                if (BillDataGrid.Rows.Count == 0)
                    removebtn.Enabled = false;

                EnableItemDetails();

                BillDataGrid.SelectionChanged += BillDataGrid_SelectionChanged;

        }

        private void copy_btn_Click(object sender, EventArgs e)
        {
            TemplateMenu.Items.Clear();

            DataTable DT = Templates.GetBomTemplates();

            foreach (DataRow Dr in DT.Rows)
            {
                ToolStripMenuItem TS = new ToolStripMenuItem();

                TS.Name = Dr["Name"].ToString();

                TS.Text = Dr["Name"].ToString();

                TS.Click += TS_Click;

                TemplateMenu.Items.Add(TS);
            }

            TemplateMenu.Show(copy_btn, new Point(0, copy_btn.Height));
        }

        void TS_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem TS = (ToolStripMenuItem)sender;

            //retrieve template data
            DataTable DT = new DataTable();

            DT = Templates.GetFullTemplate(TS.Name, "BOM");

            addraw.Enabled = true;

            try
            {
                //line for each row
                foreach (DataRow Dr in DT.Rows)
                {
                    EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                    int row = EngWBDS.Tables["ECOMtl"].Rows.Count - 1;

                    EngWBDS.Tables["ECOMtl"].Rows[row]["MtlPartNum"] = Dr["PropertyValue"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["RelatedOperation"] = Dr["PropertyType"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["MtlPartNumPartDescription"] = DataList.GetCurrentDesc(Dr["PropertyValue"].ToString());

                    EngWBDS.Tables["ECOMtl"].Rows[row]["UOMCode"] = Dr["PropertyUOM"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["QtyPer"] = Dr["PropertyQty"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["ViewAsAsm"] = Dr["PropertyOptions"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["PullAsAsm"] = Dr["PropertyOptions1"].ToString();

                    if (Dr["PropertyOptions5"].ToString() != null || Dr["PropertyOptions5"].ToString() != "")
                    {
                        if (Dr["PropertyOptions5"].ToString() == "AREA")
                            EngWBDS.Tables["ECOMtl"].Rows[row]["QtyPer"] = area.Value;

                        if (Dr["PropertyOptions5"].ToString() == "WEIGHT")
                        {
                            try
                            {
                                Part Part = new Part(DataList.EpicConn);

                                PartDataSet Pdata = new PartDataSet();

                                Pdata = Part.GetByID(parent_txt.Text);

                                string Length = RailLength(Pdata.Tables["Part"].Rows[0]["PartDescription"].ToString());

                                string partnumber = Dr["PropertyValue"].ToString();

                                double density = double.Parse(sheetCoil_UsageTableAdapter.GetDensity(partnumber,"%","%","%","%").ToString());

                                double width = double.Parse(sheetCoil_UsageTableAdapter.GetWidth(partnumber, "%", "%", "%","%","%").ToString());

                                double thickness = double.Parse(sheetCoil_UsageTableAdapter.GetLength(partnumber, "%", "%", "%", "%", "%").ToString());

                                weight.Value = (decimal)(double.Parse(Length) * density * width * thickness) / 2;
                            }
                            finally
                            {
                                EngWBDS.Tables["ECOMtl"].Rows[row]["QtyPer"] = weight.Value;
                            }
                        }
                    }

                    EngWB.Update(EngWBDS);
                }

                EnableItemDetails();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string RailLength(string description)
        {
            description = description.Replace("RAIL", "").Replace("FRONT","").Replace("REAR","").Replace("RS,","").Replace("CS,","").Substring(description.IndexOf("X")).Trim();

            return description;
        }

        private void findpart_btn_Click(object sender, EventArgs e)
        {
            SearchPart Searchfrm = new SearchPart("");

            Searchfrm.ShowDialog();

            if (Searchfrm.DialogResult == DialogResult.OK)
            {
                partnum_txt.Text = Searchfrm._PartNumber;

                desc_txt.Text = Searchfrm._Description;
            }

            Searchfrm.Close();

            Searchfrm = null;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Bill_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (DataRow dr in EngWBDS.Tables["ECOMtl"].Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        DialogResult DR = MessageBox.Show("Throw away unsaved changes?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (DR == DialogResult.No)
                        {
                            SaveChanges();

                            break;
                        }
                        else
                            return;
                    }
                }

                return;
            }
            catch { return; }
        }

        private void EnableNew_Tick(object sender, EventArgs e)
        {
            bool retval = true;

            for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
            {
                if (EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString() == "")
                    retval = false;

                if (EngWBDS.Tables["ECOMtl"].Rows[i]["UOMCode"].ToString() == "")
                {
                    try
                    {
                        if (uom_cbo.SelectedIndex != -1)
                            EngWBDS.Tables["ECOMtl"].Rows[i]["UOMCode"] = uom_cbo.Text;
                    }
                    catch { }
                    retval = false;
                }

                if (EngWBDS.Tables["ECOMtl"].Rows[i]["RelatedOperation"].ToString() == "")
                    retval = false;

                if (EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNumPartDescription"].ToString() == "")
                    retval = false;

                if (EngWBDS.Tables["ECOMtl"].Rows[i]["QtyPer"].ToString() == "0")
                    retval = false;
            }

            newbtn.Enabled = retval;

            savebtn.Enabled = retval;

            saveandclose_btn.Enabled = retval;
        }

        #endregion

        private void addraw_Click(object sender, EventArgs e)
        {
            RawMenu.Show(addraw, new Point(0, addraw.Height));
        }

        private void reseq_btn_Click(object sender, EventArgs e)
        {
            try
            {
                EngWB.Update(EngWBDS);

                EngWB.ResequenceMaterials(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, Properties.Settings.Default.mtlreqtype, false, false, false);

                EngWB.Update(EngWBDS);

                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
            }
            catch { }
        }
       
        void BillDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (BillDataGrid.CurrentCell == null)
                removebtn.Enabled = false;
            else
                removebtn.Enabled = true;

            UpdateFormFields();
        }

        private void factor_btn_Click(object sender, EventArgs e)
        {
            Item_SheetFactor Sheet = new Item_SheetFactor(qty_num.Value,partnum_txt.Text);

            Sheet.ShowDialog();

            qty_num.Value = Sheet.FactoredWeight;

            EngWBDS.Tables["ECOMtl"].Rows[BillDataGrid.CurrentCellAddress.Y]["QtyPer"] = qty_num.Value;

            Sheet.Dispose();
        }

        private void BillDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedindex = e.RowIndex;

            if (BillDataGrid.CurrentCellAddress.X == 6)
            {
                if (BillDataGrid.CurrentCellAddress.Y == selectedindex)
                {
                    ViewAsAsm_chk.Checked = !ViewAsAsm_chk.Checked;

                    ViewAsAsm_chk_Click(null, null);
                }
            }

            if (BillDataGrid.CurrentCellAddress.X == 7)
            {
                if (BillDataGrid.CurrentCellAddress.Y == selectedindex)
                {
                    PullAsAsm_chk.Checked = !PullAsAsm_chk.Checked;

                    PullAsAsm_chk_Click(null, null);
                }
            }
        }

        private void BW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            #region Fill Operations

            EngWBDS.Tables["ECOOpr"].Columns.Add("FullCode", typeof(string), "OprSeq + ' - ' + OpDesc");

            ops_cbo.DataSource = EngWBDS.Tables["ECOOpr"];

            ops_cbo.DisplayMember = "FullCode";

            ops_cbo.ValueMember = "OprSeq";

            ops_cbo.SelectedValue = "10";

            #endregion

            UpdateParentDesc();

            EnableItemDetails();

            Setbill((List<BillItem>)e.Argument);
        }

        private void BW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            BWForm.Close();
        }
    }
}
