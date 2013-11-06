using Epicor.Mfg.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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

            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

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

        private void HideFields(bool Toggle)
        {
            partnum_txt.Visible = Toggle;

            desc_txt.Visible = Toggle;

            mtlseq_txt.Visible = Toggle;

            ops_cbo.Visible = Toggle;

            ViewAsAsm_chk.Visible = Toggle;

            qty_num.Visible = Toggle;

            uom_cbo.Visible = Toggle;

            saveprogress.Visible = !Toggle;
        }

        private void SaveChanges(bool fromNew)
        {
            string opMessage;

            string opMsgType;

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

                    BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        void partnum_txt_Leave(object sender, EventArgs e)
        {
            if (linechanged)
            {
                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                string cellval = BillDataGrid["MtlPartNum", rowindex].Value.ToString();

                if (cellval == "")
                {
                    string opMessage;

                    string opMsgType;

                    string partnumber = partnum_txt.Text;

                    //EngWB.CheckECOMtlMtlPartNum(partnumber, out opMessage, out opMsgType, EngWBDS);

                    rowindex = BillDataGrid.CurrentCellAddress.Y;

                    EngWBDS.Tables["ECOMtl"].Rows[rowindex]["MtlPartNum"] = partnumber;

                    //EngWB.ChangeECOMtlMtlPartNum(EngWBDS);

                    DataTable ds = DataList.PartUOM(partnum_txt.Text);

                    uom_cbo.DataSource = ds;

                    uom_cbo.DisplayMember = "UOMCode";

                    uom_cbo.ValueMember = "UOMCode";

                    linechanged = false;

                    //ViewAsAsm_chk.Checked = IsAssembly();
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int GetNextSeq()
        {
            int rowcount = BillDataGrid.Rows.Count;

            return ++rowcount * 10;
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

        private void Bill_Master_Load(object sender, EventArgs e)
        {
            BillDataGrid.ClearSelection();

            BillDataGrid.CurrentCell = BillDataGrid.Rows[0].Cells[0];

            BillDataGrid.SelectionChanged += BillDataGrid_SelectionChanged;

            UpdateFormFields();
        }

        void BillDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            linechanged = false;

            UpdateFormFields();

            if (BillDataGrid.CurrentCell == null)
                removebtn.Enabled = false;
            else
                removebtn.Enabled = true;
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
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
            catch {}
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            SaveChanges(false);
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            SaveChanges(false);

            int rowindex = BillDataGrid.CurrentCellAddress.Y;

            EngWBDS.Tables["ECOMtl"].Rows[rowindex].Delete();

            SaveChanges(false);

            //EngWB.Update(EngWBDS);

            EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, parent_txt.Text, parentrev_txt.Text, "", null, false, false);

            BillDataGrid.DataSource = EngWBDS.Tables["ECOMtl"];

            if (BillDataGrid.Rows.Count == 0)
                removebtn.Enabled = false;
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {

        }

        private void UpdateDataSet()
        {
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
        }

        private void UpdateFormFields()
        {
            if (!linechanged)
            {
                int rowindex = BillDataGrid.CurrentCellAddress.Y;

                if (rowindex != -1)
                {
                    qty_num.ValueChanged -= qty_num_ValueChanged;

                    partnum_txt.TextChanged -= partnum_txt_TextChanged;

                    ViewAsAsm_chk.CheckedChanged -= ViewAsAsm_chk_CheckedChanged;

                    uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;

                    ops_cbo.SelectedIndexChanged -= ops_cbo_SelectedIndexChanged;

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

                    qty_num.ValueChanged += qty_num_ValueChanged;

                    partnum_txt.TextChanged += partnum_txt_TextChanged;

                    ViewAsAsm_chk.CheckedChanged += ViewAsAsm_chk_CheckedChanged;

                    uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;

                    ops_cbo.SelectedIndexChanged += ops_cbo_SelectedIndexChanged;
                }
            }
        }

        private void PartTimer_Tick(object sender, EventArgs e)
        {
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

        #region Changed Values

        private void partnum_txt_TextChanged(object sender, EventArgs e)
        {
            linechanged = true;

            PartTimer.Enabled = true;
        }

        private void ViewAsAsm_chk_CheckedChanged(object sender, EventArgs e)
        {
            linechanged = true;

            UpdateDataSet();
        }

        private void uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            linechanged = true;

            UpdateDataSet();
        }

        private void qty_num_ValueChanged(object sender, EventArgs e)
        {
            linechanged = true;

            UpdateDataSet();
        }

        private void ops_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            linechanged = true;

            UpdateDataSet();
        }

        #endregion

        private void saveandclose_btn_Click(object sender, EventArgs e)
        {
            SaveChanges(false);

            this.Close();
        }

    }
}
