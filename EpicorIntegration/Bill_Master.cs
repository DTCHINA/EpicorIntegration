using Epicor.Mfg.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace EpicorIntegration
{
    public partial class Bill_Master : Form
    {
        #region Variables

        private bool linechanged = false;
        
        private EngWorkBench EngWB = new EngWorkBench(DataList.EpicConn);

        private EngWorkBenchDataSet _EngWBDS = new EngWorkBenchDataSet();

        private EngWorkBenchDataSet EngWBDS
        {
            get { return _EngWBDS; }
            set { _EngWBDS = value; }
        }

        bool Form_Update_Enabled = false;

        bool DB_Update_Enabled = false;

        #endregion

        public Bill_Master(List<string> BillParts, List<string> BillQty, string ParentNumber)
        {
            InitializeComponent();

            BillDataGrid.AutoGenerateColumns = false;

            gid_txt.Text = Properties.Settings.Default.ecogroup;

            parentrev_txt.Text = DataList.GetCurrentRev(ParentNumber);

            partnum_txt.Leave += partnum_txt_Leave;

            mtlseq_txt.Text = GetNextSeq().ToString();

            parent_txt.Text = ParentNumber;

            try
            {
                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
            }
            catch (Exception ex) 
            {
                try
                {
                    MessageBox.Show("You must check part out before continuing.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    CheckOut_Master CO_M = new CheckOut_Master(parent_txt.Text);

                    DialogResult dr = CO_M.ShowDialog();

                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                    BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
                }
                catch (Exception ex1) {MessageBox .Show (ex1.Message + "\n\nThis process will now close","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error); }
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

            Setbill(BillParts, BillQty);
        }

        private void Bill_Master_Load(object sender, EventArgs e)
        {
            BillDataGrid.ClearSelection();

            BillDataGrid.CurrentCell = BillDataGrid.Rows[0].Cells[0];

            BillDataGrid.SelectionChanged += BillDataGrid_SelectionChanged;

            UpdateFormFields();

            FillRawMenu();
        }

        /// <summary>
        /// Finds current safe parts within the current bill of materials and keeps a separate list of them to add after the bom has been updated (Sheets/Coils/E-Coat)
        /// </summary>
        /// <param name="BillParts">Current Epicor Bill of Materials</param>
        /// <param name="BillQty">Current Epicor Bill of Materials Qty</param>
        /// <param name="Bill_Qty">Bill of Materials Qty for only Safe items</param>
        /// <returns>Bill of Materials for only safe items</returns>
        public List<string> SafeParts(List<string> BillParts, List<string> BillQty, out List<string> Bill_Qty)
        {
            List<string> RetVal = new List<string>();

            List<string> RetVal_Qty = new List<string>();

            List<RawMaterial> ListtoSave = new List<RawMaterial>();

            ListtoSave.AddRange(DataList.GetCoils());

            ListtoSave.AddRange(DataList.GetEcoat());

            ListtoSave.AddRange(DataList.GetSheets());

            for (int i = 0; i < BillParts.Count; i++)
            {
                for (int j = 0; j < ListtoSave.Count; j++)
                {
                    if (BillParts[i] == ListtoSave[j].part_number)
                    {
                        RetVal.Add(BillParts[i]);

                        RetVal_Qty .Add(BillQty[i]);
                    }
                }
            }

            Bill_Qty = RetVal_Qty;

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
                }
                catch (Exception Exception) { MessageBox.Show(Exception.Message, "Import Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void SaveChanges(bool fromNew)
        {
            string opMessage;

            string opMsgType;

            qty_num.ValueChanged -= qty_num_ValueChanged;

            partnum_txt.TextChanged -= partnum_txt_TextChanged;

            ViewAsAsm_chk.CheckedChanged -= ViewAsAsm_chk_CheckedChanged;

            uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;

            ops_cbo.SelectedIndexChanged -= ops_cbo_SelectedIndexChanged;

            foreach (DataRow DR in EngWBDS.Tables["ECOMtl"].Rows)
            {
                if (DR.RowState == DataRowState.Modified || DR.RowState == DataRowState.Added)
                {
                    #region Validate all data
                    
                    string partnumber = DR["MtlPartNum"].ToString();

                    string ops = DR["RelatedOperation"].ToString();

                    string mtlseq = DR["MtlSeq"].ToString();

                    EngWB.CheckECOMtlMtlPartNum(partnumber, out opMessage, out opMsgType, EngWBDS);

                    EngWB.CheckECOMtlMtlSeqRelatedOperation(int.Parse(mtlseq), int.Parse(ops), "", out opMessage, out opMsgType, EngWBDS);

                    EngWB.ChangeECOMtlQtyPer(EngWBDS);

                    EngWB.ChangeECOMtlRelatedOperation(int.Parse(ops), EngWBDS);

                    EngWB.ChangeECOMtlMtlPartNum(EngWBDS);

                    #endregion
                }
            }

            EngWB.Update(EngWBDS);
            
            if (!fromNew)
            {
                try
                {
                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                    EngWB.ResequenceMaterials(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, Properties.Settings.Default.mtlreqtype, false, false, false);

                    EngWB.Update(EngWBDS);

                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);                    
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            qty_num.ValueChanged += qty_num_ValueChanged;

            partnum_txt.TextChanged += partnum_txt_TextChanged;

            ViewAsAsm_chk.CheckedChanged += ViewAsAsm_chk_CheckedChanged;

            uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;

            ops_cbo.SelectedIndexChanged += ops_cbo_SelectedIndexChanged;

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
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
        private void Setbill(List<string> BillItems, List<string> BillQty)
        {
            qty_num.ValueChanged -= qty_num_ValueChanged;

            partnum_txt.TextChanged -= partnum_txt_TextChanged;

            uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;

            List<bool> FindItemEpicor = new List<bool>();

            List<bool> FindItemSldWrks = new List<bool>();

            List<string> AddBack = new List<string>();

            List<string> AddBack_Qty = new List<string>();

            #region Locate items

            for (int i = 0; i < BillItems.Count; i++)
            {FindItemSldWrks.Add(false);}

            for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
            {
                FindItemEpicor .Add(false);

                AddBack.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString());

                AddBack_Qty.Add(EngWBDS.Tables["ECOMtl"].Rows[i]["QtyPer"].ToString());
            }

            for (int i = 0; i < BillItems.Count; i++)
            {
                for (int j = 0; j < EngWBDS.Tables["ECOMtl"].Rows.Count; j++)
                {
                    string DS_val = EngWBDS.Tables["ECOMtl"].Rows[j]["MtlPartNum"].ToString();

                    if (DS_val == BillItems[i])
                        FindItemSldWrks[i] = true;
                }
            }

            for (int i = 0; i < EngWBDS.Tables["ECOMtl"].Rows.Count; i++)
            {
                for (int j = 0; j < BillItems.Count; j++)
                {
                    if (EngWBDS.Tables["ECOMtl"].Rows[i]["MtlPartNum"].ToString() == BillItems[j])
                        FindItemEpicor[i] = true;
                }
            }
            #endregion

            #region Determine what needs to be saved

            AddBack = SafeParts(AddBack, AddBack_Qty, out AddBack_Qty);

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
                for (int i = 0; i < BillItems.Count; i++)
                {
                    if (!FindItemSldWrks[i])
                    {
                        try
                        {
                            rowmod = EngWBDS.Tables["ECOMtl"].Rows.Count;
                            //Add item
                            EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["MtlPartNum"] = BillItems[i];

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = BillQty[i];

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["RelatedOperation"] = ops_cbo.SelectedValue;

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["ViewAsAsm"] = false;

                            partnum_txt.Text = BillItems[i];

                            DataTable ds = DataList.PartUOM(partnum_txt.Text);

                            uom_cbo.DataSource = ds;

                            uom_cbo.DisplayMember = "UOMCode";

                            uom_cbo.ValueMember = "UOMCode";

                            uom_cbo.SelectedIndex = 0;

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["UOMCode"] = uom_cbo.SelectedValue;

                            decimal qty_val = decimal.Parse(BillQty[i]);

                            EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = qty_val;

                            EngWB.Update(EngWBDS);

                            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch (Exception ex) { }
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
     
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = AddBack_Qty[i];
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["RelatedOperation"] = ops_cbo.SelectedValue;
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["ViewAsAsm"] = false;
      
                    partnum_txt.Text = AddBack[i];
            
                    DataTable ds = DataList.PartUOM(partnum_txt.Text);
           
                    uom_cbo.DataSource = ds;
        
                    uom_cbo.DisplayMember = "UOMCode";
        
                    uom_cbo.ValueMember = "UOMCode";
     
                    uom_cbo.SelectedIndex = 0;
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["UOMCode"] = uom_cbo.SelectedValue;
      
                    decimal qty_val = decimal.Parse(AddBack_Qty[i]);
       
                    EngWBDS.Tables["ECOMtl"].Rows[rowmod]["QtyPer"] = qty_val;
      
                    EngWB.Update(EngWBDS);
       
                    EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);
                }
            }
            catch(Exception ex){}

            #endregion

            #region Update per line
            for (int i = 0; i < BillItems.Count; i++)
            {
                for (int j = 0; j < EngWBDS.Tables["ECOMtl"].Rows.Count; j++)
                {
                    string EpicValue = EngWBDS.Tables["ECOMtl"].Rows[j]["MtlPartNum"].ToString();

                    string _BillItem = BillItems[i];

                    decimal EpicQty = decimal.Parse(EngWBDS.Tables["ECOMtl"].Rows[j]["QtyPer"].ToString());

                    decimal _BillQty = decimal.Parse(BillQty[i]);

                    if (EpicValue == _BillItem && EpicQty != _BillQty)
                    {
                        EngWBDS.Tables["ECOMtl"].Rows[j]["QtyPer"] = BillQty[i];
                    }
                }
            }
            #endregion

            try
            {
                EngWB.Update(EngWBDS);
            }
            catch (Exception ex) { }

            qty_num.ValueChanged += qty_num_ValueChanged;

            partnum_txt.TextChanged += partnum_txt_TextChanged;

            uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;

            EngWB.ResequenceMaterials(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, Properties.Settings.Default.mtlreqtype, false, false, false);

            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

            EnableItemDetails();
        }

        #region Update Info

        private void UpdateDataSet()
        {

            Form_Update_Enabled = false;

            DB_Update_Enabled = false;
            try
            {
                if (linechanged)
                {
                    int rowindex = BillDataGrid.CurrentCellAddress.Y;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"] = ViewAsAsm_chk.Checked;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"] = partnum_txt.Text;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"] = ops_cbo.SelectedValue;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["OpDesc"] = EngWBDS.Tables["ECOOpr"].Rows[ops_cbo.SelectedIndex]["OpDesc"];

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"] = uom_cbo.Text;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNumPartDescription"] = desc_txt.Text;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"] = qty_num.Value;

                    linechanged = false;
                }
            }
            catch { }

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        private void UpdateFormFields()
        {
            Form_Update_Enabled = false;

            DB_Update_Enabled = false;

            if (!linechanged)
            {
                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                if (rowindex != -1)
                {
                    mtlseq_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlSeq"].ToString();

                    qty_num.Value = int.Parse(EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"].ToString());

                    partnum_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"].ToString();

                    desc_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNumPartDescription"].ToString();

                    ops_cbo.SelectedValue = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"].ToString();

                    partnum_txt.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"].ToString();

                    DataTable ds = DataList.PartUOM(partnum_txt.Text);

                    uom_cbo.DataSource = ds;

                    uom_cbo.DisplayMember = "UOMCode";

                    uom_cbo.ValueMember = "UOMCode";

                    uom_cbo.Text = EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"].ToString();

                    ViewAsAsm_chk.Checked = Boolean.Parse(EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"].ToString());

                    linechanged = true;
                }
            }

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        private void UpdateUOM()
        {
            DataTable ds = DataList.PartUOM(partnum_txt.Text);

            uom_cbo.DataSource = ds;

            uom_cbo.DisplayMember = "UOMCode";

            uom_cbo.ValueMember = "UOMCode";
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

        private void partnum_txt_TextChanged(object sender, EventArgs e)
        {
            linechanged = true;

            PartTimer.Enabled = true;
        }

        private void ViewAsAsm_chk_CheckedChanged(object sender, EventArgs e)
        {
            linechanged = true;

            if (DB_Update_Enabled)
                UpdateDataSet();
        }

        private void uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            linechanged = true;

            if (DB_Update_Enabled)
                UpdateDataSet();
        }

        private void qty_num_ValueChanged(object sender, EventArgs e)
        {
            linechanged = true;

            if (DB_Update_Enabled)
                UpdateDataSet();
        }

        private void ops_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            linechanged = true;

            if (DB_Update_Enabled)
                UpdateDataSet();
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
            SaveChanges(false);

            this.Close();
        }

        private void newbtn_Click(object sender, EventArgs e)
        {

            Form_Update_Enabled = false;

            DB_Update_Enabled = false;

            try
            {
                BillDataGrid.ClearSelection();

                SaveChanges(true);

                EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

                BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

                qty_num.ValueChanged -= qty_num_ValueChanged;

                partnum_txt.TextChanged -= partnum_txt_TextChanged;

                ViewAsAsm_chk.CheckedChanged -= ViewAsAsm_chk_CheckedChanged;

                uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;

                ops_cbo.SelectedIndexChanged -= ops_cbo_SelectedIndexChanged;

                EngWB.GetNewECOMtl(EngWBDS, gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "");

                int rowindex = BillDataGrid.Rows.Count - 1;

                BillDataGrid.ClearSelection();

                BillDataGrid.CurrentCell = BillDataGrid.Rows[rowindex].Cells[0];

                qty_num.Value = 1;

                ops_cbo.SelectedIndex = 0;

                partnum_txt.Text = "";

                desc_txt.Text = "";

                qty_num.ValueChanged += qty_num_ValueChanged;

                partnum_txt.TextChanged += partnum_txt_TextChanged;

                ViewAsAsm_chk.CheckedChanged += ViewAsAsm_chk_CheckedChanged;

                uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;

                ops_cbo.SelectedIndexChanged += ops_cbo_SelectedIndexChanged;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["RelatedOperation"] = ops_cbo.SelectedValue;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["QtyPer"] = 1;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["ViewAsAsm"] = false;

                EngWBDS.Tables["ECOMtl"].Rows[rowindex]["UOMCode"] = uom_cbo.Text;

                EnableItemDetails();
            }
            catch { }

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            Form_Update_Enabled = false;

            DB_Update_Enabled = false;

            SaveChanges(false);

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            Form_Update_Enabled = false;

            DB_Update_Enabled = false;
            if (partnum_txt.Text != "")
                SaveChanges(false);

            int rowindex = BillDataGrid.CurrentCellAddress.Y;

            EngWBDS.Tables["ECOMtl"].Rows[rowindex].Delete();

            SaveChanges(false);

            //EngWB.Update(EngWBDS);

            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

            if (BillDataGrid.Rows.Count == 0)
                removebtn.Enabled = false;

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {

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

        void partnum_txt_Leave(object sender, EventArgs e)
        {
            if (linechanged && DB_Update_Enabled)
            {
                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                string cellval = BillDataGrid["MtlPartNum", rowindex].Value.ToString();

                if (cellval == "")
                {
                    string opMessage;

                    string opMsgType;

                    string partnumber = partnum_txt.Text;

                    EngWB.CheckECOMtlMtlPartNum(partnumber, out opMessage, out opMsgType, EngWBDS);

                    rowindex = BillDataGrid.CurrentCellAddress.Y;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"] = partnumber;

                    EngWB.ChangeECOMtlMtlPartNum(EngWBDS);

                    DataTable ds = DataList.PartUOM(partnum_txt.Text);

                    uom_cbo.DataSource = ds;

                    uom_cbo.DisplayMember = "UOMCode";

                    uom_cbo.ValueMember = "UOMCode";

                    linechanged = false;
                }
            }
        }

        void BillDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            linechanged = false;

            if (Form_Update_Enabled)
                UpdateFormFields();

            if (BillDataGrid.CurrentCell == null)
                removebtn.Enabled = false;
            else
                removebtn.Enabled = true;
        }

        private void PartTimer_Tick(object sender, EventArgs e)
        {
            Form_Update_Enabled = false;

            DB_Update_Enabled = false;

            try
            {
                if (partnum_txt.Text != "")
                {
                    PartTimer.Enabled = false;

                    UpdateDescField();

                    UpdateUOM();

                    //EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"] = partnum_txt.Text;

                    //EngWB.CheckECOMtlMtlPartNum(partnum_txt.Text, out opMessage, out opMsgType, EngWBDS);

                    //EngWB.ChangeECOMtlMtlPartNum(EngWBDS);

                    UpdateDataSet();
                }
            }
            catch { desc_txt.Text = ""; }

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
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
                            SaveChanges(false);

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
                    retval = false;

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

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Update_Enabled = false;

            DB_Update_Enabled = false;

            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

            Form_Update_Enabled = true;

            DB_Update_Enabled = true;
        }

        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            RawMenu.Show(button1, new Point(0, button1.Height));
        }
    }
}
