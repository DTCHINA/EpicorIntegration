﻿using Epicor.Mfg.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class Operations_Master : Form
    {
        private EngWorkBench EngWB = new EngWorkBench(DataList.EpicConn);

        private EngWorkBenchDataSet _EngWBDS = new EngWorkBenchDataSet();

        private EngWorkBenchDataSet EngWBDS
        {
            get { return _EngWBDS; }
            set { _EngWBDS = value; }
        }

        private string _PartNumber { get; set; }

        private string _Rev { get; set; }

        private DataSet ResourceTable
        {
            get;
            set;
        }

        private List<string> OperationDefaults
        { get; set; }

        public void FillProdStd()
        {
            List<ProdStdType> ProdStdDS = new List<ProdStdType>();

            ProdStdDS.Add(new ProdStdType("Minutes/Piece", "MP"));

            ProdStdDS.Add(new ProdStdType("Hours/Piece", "HP"));

            ProdStdDS.Add(new ProdStdType("Pieces/Minute", "PM"));

            ProdStdDS.Add(new ProdStdType("Pieces/Hour", "HM"));

            ProdStdDS.Add(new ProdStdType("Operations/Minute", "OM"));

            ProdStdDS.Add(new ProdStdType("Operations/Hour", "OH"));

            ProdStdDS.Add(new ProdStdType("Fixed Hours", "HR"));

            BindingSource bind = new BindingSource();

            bind.DataSource = ProdStdDS;

            prodstd_cbo.DataSource = bind;

            prodstd_cbo.DisplayMember = "Description";

            prodstd_cbo.ValueMember = "Code";

            prodstd_cbo.SelectedIndex = 0;
        }

        private string UpdateParentDesc()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "PartNum = '" + partnumber_txt.Text + "'";

            int pagesize = 1;

            bool morePages;

            PartList = Part.GetList(WhereClause, pagesize, 0, out morePages);

            DataList.EpicClose();

            string desc = PartList.Tables[0].Rows[0]["PartDescription"].ToString();

            Part = null;

            PartList.Dispose();

            PartList = null;

            return desc;
        }

        public Operations_Master(string PartNumber, string Rev)
        {
            InitializeComponent();

            _PartNumber = PartNumber;

            _Rev = Rev;

            supplierid_txt.Leave += supplierid_txt_Leave;

            OPDataGrid.AutoGenerateColumns = false;
            
            partnumber_txt.Text = PartNumber;

            rev_txt.Text = Rev;

            desc_txt.Text = UpdateParentDesc();

            try
            {
                bool morePages;

                OpMaster OpMaster = new Epicor.Mfg.BO.OpMaster(DataList.EpicConn);

                DataSet ds = (DataSet)OpMaster.GetRows("", "", "", "", "", "", 100, 0, out morePages);

                opmast_cbo.DataSource = ds.Tables["OPMaster"];

                subcon_opsmast_cbo.DataSource = ds.Tables["OPMaster"];

                opmast_cbo.ValueMember = "OPCode";

                opmast_cbo.DisplayMember = "OPDesc";

                subcon_opsmast_cbo.ValueMember = "OPCode";

                subcon_opsmast_cbo.DisplayMember = "OPDesc";

                uom_cbo.DataSource = DataList.PartUOM(_PartNumber);

                uom_cbo.DisplayMember = "UOMCode";

                uom_cbo.ValueMember = "UOMCode";

                BW.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Inconsistency!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }
        }

        public Operations_Master()
        {
            InitializeComponent();

            OPDataGrid.AutoGenerateColumns = false;

            bool morePages;

            OpMaster OpMaster = new Epicor.Mfg.BO.OpMaster(DataList.EpicConn);

            DataSet ds = (DataSet)OpMaster.GetRows("", "", "", "", "", "", 100, 0, out morePages);

            opmast_cbo.DataSource = ds.Tables["OPMaster"];

            opmast_cbo.ValueMember = ds.Tables["OPMaster"].Columns["OPCode"].ToString();

            opmast_cbo.DisplayMember = ds.Tables["OPMaster"].Columns["OPDesc"].ToString();

            FillLaborEntry();

            DataList.EpicClose();
        }

        private void Operations_Master_Load(object sender, EventArgs e)
        {
            OPDataGrid.SelectionChanged += OPDataGrid_SelectionChanged;

            this.FormClosing += Operations_Master_FormClosing;

            ResourceTable = DataList.ResourceGroup();

            FillLaborEntryGrid();

            FillProdStd();

            EnableSNChk();

            string Message;

            bool status = !DataList.PartCheckOutStatus(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, out Message);

            if (status)
            {
                string cur = DataList.GetCurrentRev(partnumber_txt.Text);

                if (rev_txt.Text == cur)
                {
                    MessageBox.Show("Revisions of Epicor and SolidWorks do not match.  Please correct this and try again.", "Stop!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Part must be checked out by selected Group ID to continue.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    CheckOut_Master CO_M = new CheckOut_Master(partnumber_txt.Text);

                    DialogResult dr = CO_M.ShowDialog();

                    if (dr == DialogResult.Cancel)
                        this.Close();
                }
            }
            else
            {
                if (Message != "Checked Out by GroupID")
                {
                    MessageBox.Show("Part must be checked out by selected Group ID to continue.\n\n" + Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                }
            }

            OPDataGrid.ClearSelection();

            SNRequiredOpr_chk.Click += SNRequiredOpr_chk_Click;

            try
            {
                if (OPDataGrid.Rows.Count > 0)
                    OPDataGrid.CurrentCell = OPDataGrid[0, 0];
            }
            catch{ }
        }

        void SNRequiredOpr_chk_Click(object sender, EventArgs e)
        {
            int RowIndex = OPDataGrid.CurrentCell.RowIndex;

            for (int i = 0; i < RowIndex; i++)
            {
                if (EngWBDS.Tables["ECOOpr"].Rows[i]["SNRequiredOpr"].ToString() == "True")
                {
                    MessageBox.Show("Cannot set value, prior operations require serialization", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SNRequiredOpr_chk.Checked = true;

                    break;
                }
            }
        }

        void Operations_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool Changed = false;

            for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                if (EngWBDS.Tables["ECOOpr"].Rows[i].RowState == DataRowState.Added)
                {
                    Changed = true;

                    break;
                }
            }

            if (Changed)
            {
                DialogResult DR = MessageBox.Show("You have unsaved changes.  Do you want to save before closing?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DR == DialogResult.Yes)
                {
                    EngWB.Update(EngWBDS);

                    EngWB.ResequenceOperations(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

                    resource_show.Enabled = true;

                    EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                    OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
                }
                    //e.Cancel = true;
            }
        }

        void OPDataGrid_SelectionChanged(object sender, EventArgs e)
        {

            int rowindex = OPDataGrid.CurrentCellAddress.Y;

            object val = OPDataGrid["SubContract", rowindex].Value;

            if (!Convert.ToBoolean(val))
                try
                {
                    #region Not Sub Contract

                    ops_grp.Visible = true;

                    subcon_grp.Visible = false;

                    string LaborVal = EngWBDS.Tables["ECOOpr"].Rows[rowindex]["LaborEntryMethod"].ToString();

                    LaborEntryMethod_cbo.SelectedValue = LaborVal;

                    string CurrentOp = OPDataGrid["OpDesc", rowindex].Value.ToString();

                    string CurrentStd = (OPDataGrid["Stdformat", rowindex].Value.ToString());

                    decimal CurrentProd = (decimal)double.Parse(OPDataGrid["ProdStandard", rowindex].Value.ToString());

                    decimal CurrentSeq = (decimal)double.Parse(OPDataGrid["OprSeq", rowindex].Value.ToString());

                    if (CurrentOp != null)
                        opmast_cbo.SelectedIndex = opmast_cbo.FindStringExact(CurrentOp);
                    else
                        opmast_cbo.SelectedIndex = 1;

                    if (CurrentStd != null)
                    {
                        prodstd_cbo.SelectedValue = CurrentStd;
                    }
                    else
                        prodstd_cbo.SelectedIndex = 1;

                    prodhrs_num.Value = CurrentProd;

                    #endregion
                }
                catch { }
            else
            {
                #region Sub Contract

                subcon_grp.Visible = true;

                ops_grp.Visible = false;

                subcon_grp.Location = new Point(12, 61);

                string CurrentOp = OPDataGrid["OpDesc", rowindex].Value.ToString();

                string IUM_val = OPDataGrid["IUM", rowindex].Value.ToString();

                if (CurrentOp != null)
                    subcon_opsmast_cbo.SelectedIndex = subcon_opsmast_cbo.FindStringExact(CurrentOp);
                else
                    subcon_opsmast_cbo.SelectedIndex = 1;

                refneeded_chk.Checked = Convert.ToBoolean(OPDataGrid["RFQNeeded", rowindex].Value);

                quotesreq_num.Value = decimal.Parse(OPDataGrid["RFQVendQuotes", rowindex].Value.ToString());

                if (IUM_val != null)
                    uom_cbo.SelectedIndex = uom_cbo.FindStringExact(IUM_val);
                else
                    uom_cbo.SelectedIndex = 1;

                qtyper_num.Value = decimal.Parse(OPDataGrid["QtyPer", rowindex].Value.ToString());

                unitcost_num.Value = decimal.Parse(OPDataGrid["EstUnitCost", rowindex].Value.ToString());

                daysout_num.Value = decimal.Parse(OPDataGrid["DaysOut", rowindex].Value.ToString());

                supplierid_txt.Text = OPDataGrid["VendorNumVendorID", rowindex].Value.ToString();

                supplieradd_txt.Text = OPDataGrid["DspBillAddr", rowindex].Value.ToString();

                #endregion
            }

            SNRequiredOpr_chk.Checked = bool.Parse(EngWBDS.Tables["ECOOpr"].Rows[rowindex]["SNRequiredOpr"].ToString());

            AutoRecieve_chk.Checked = bool.Parse(EngWBDS.Tables["ECOOpr"].Rows[rowindex]["AutoReceive"].ToString());
        }

        private void SNRequiredOpr_chk_CheckedChanged(object sender, EventArgs e)
        {
            int RowIndex = OPDataGrid.CurrentCell.RowIndex;

            EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["SNRequiredOpr"] = SNRequiredOpr_chk.Checked;

            OPDataGrid["SNRequiredOpr", RowIndex].Value = SNRequiredOpr_chk.Checked;
            try
            {
                if (SNRequiredOpr_chk.Checked)
                {
                    for (int i = RowIndex; i < EngWBDS.Tables["ECOOpr"].Rows.Count; i++)
                    {
                        EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["SNRequiredOpr"] = SNRequiredOpr_chk.Checked;

                        OPDataGrid["SNRequiredOpr", i].Value = SNRequiredOpr_chk.Checked;
                    }
                }
            }
            catch { }

            AutoRecieve_chk.Checked = (SNRequiredOpr_chk.Checked ? false : AutoRecieve_chk.Checked);
        }

        private void AutoRecieve_chk_CheckedChanged(object sender, EventArgs e)
        {
            int RowIndex = OPDataGrid.CurrentCell.RowIndex;

            EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["AutoReceive"] = AutoRecieve_chk.Checked;

            if (AutoRecieve_chk.Checked)
            {
                foreach (DataGridViewRow dr in OPDataGrid.Rows)
                {
                    dr.Cells["AutoReceive"].Value = (dr.Index == RowIndex);
                    //EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["AutoReceive"] = (
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DataList.EpicClose();

            this.Close();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                EngWB.Update(EngWBDS);

                resource_show.Enabled = true;

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!"); }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            EngWB.GetNewECOOpr(EngWBDS, Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "");

            int RowIndex = OPDataGrid.Rows.Count - 1;

            OPDataGrid.ClearSelection();

            OPDataGrid.CurrentCell = OPDataGrid.Rows[RowIndex].Cells[0];

            opmast_cbo.SelectedIndex = 0;

            prodhrs_num.Value = 0;

            OpMaster OpMaster = new Epicor.Mfg.BO.OpMaster(DataList.EpicConn);

            bool morePages;

            DataSet ds = (DataSet)OpMaster.GetRows("", "", "", "", "", "", 100, 0, out morePages);

            DataList.EpicClose();

            EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OpCode"] = ds.Tables["OpMaster"].Rows[opmast_cbo.SelectedIndex]["OpCode"].ToString();

            EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OpDesc"] = opmast_cbo.Text;

            EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["ProdStandard"] = prodhrs_num.Value;

            LaborEntryMethod_cbo.SelectedValue = "Q";

            LaborEntryMethod_cbo_SelectedIndexChanged(LaborEntryMethod_cbo, null);

            resource_show.Enabled = false;

            EngWB.Update(EngWBDS);

            EngWB.ResequenceOperations(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

            resource_show.Enabled = true;

            EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

            OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

            removebtn.Enabled = true;

            bool serialized = false;

            for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                if (OPDataGrid["SNRequiredOpr", i].Value.ToString() == "true")
                {
                    serialized = true;

                    break;
                }
            }

            if (!serialized)
            for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["AutoReceive"] = (i == OPDataGrid.Rows.Count - 1);
            }

            try
            {
                OPDataGrid.CurrentCell = OPDataGrid.Rows[OPDataGrid.Rows.Count - 1].Cells[0];
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            int RowIndex = OPDataGrid.CurrentCell.RowIndex;

            savebtn_Click(savebtn, null);
            
            DataRow del_row = EngWBDS.Tables["ECOOpr"].Rows[RowIndex];

            OPDataGrid.SelectionChanged -= OPDataGrid_SelectionChanged;

            try
            {
                EngWBDS.Tables["ECOOpr"].Rows[RowIndex].Delete();

                EngWB.Update(EngWBDS);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

                if (EngWBDS.Tables["ECOOpr"].Rows.Count == 0)
                    removebtn.Enabled = false;

                //EngWB.ResequenceOperations(gid_txt.Text, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

                resource_show.Enabled = false;

                //Delete rows for RESOURCE GROUP
            }
            catch
            {
                MessageBox.Show("Error Deleting Row!\n\nCheck to see that there are not materials attached to this operation and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

                if (EngWBDS.Tables["ECOOpr"].Rows.Count == 0)
                    removebtn.Enabled = false;
            }
            finally {
                OPDataGrid.SelectionChanged += OPDataGrid_SelectionChanged;
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

            OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
        }

        private void opmast_cbo_Enter(object sender, EventArgs e)
        {
            opmast_cbo.SelectedIndexChanged += opmast_cbo_SelectedIndexChanged;
        }

        private void opmast_cbo_Leave(object sender, EventArgs e)
        {
            opmast_cbo.SelectedIndexChanged -= opmast_cbo_SelectedIndexChanged;
        }

        private void opmast_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = OPDataGrid.CurrentCell.RowIndex;
                if (ops_grp.Visible)
                {
                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OpCode"] = opmast_cbo.SelectedValue.ToString();

                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OpDesc"] = opmast_cbo.Text;

                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["ProdStandard"] = prodhrs_num.Value;

                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["AutoReceive"] = AutoRecieve_chk.Checked;

                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["SNRequiredOpr"] = SNRequiredOpr_chk.Checked;

                    if (opmast_cbo.Text == "TURRET")// || opmast_cbo.Text == "SHEAR")
                        LaborEntryMethod_cbo.Text = "Backflush";

                    try
                    {
                        string PrimaryProdOpDtlDesc = "";

                        string ResourceGroupCode = "";

                        string ResourceDesc = "";

                        #region Find the Code

                        foreach (DataRow row in EngWBDS.Tables["ECOOpDtl"].Rows)
                        {
                            if (row["OprSeq"].ToString() == EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OprSeq"].ToString())
                            {
                                PrimaryProdOpDtlDesc = ((DataTable)opmast_cbo.DataSource).Rows[opmast_cbo.SelectedIndex]["PrimaryProdOpDtlDesc"].ToString();//[OperationDefaults[opmast_cbo.SelectedIndex];

                                foreach (DataRow res_row in ResourceTable.Tables[0].Rows)
                                {
                                    if (res_row["Description"].ToString() == PrimaryProdOpDtlDesc || res_row["ResourceGrpID"].ToString() == PrimaryProdOpDtlDesc)
                                    {
                                        ResourceGroupCode = res_row["ResourceGrpID"].ToString();

                                        ResourceDesc = res_row["Description"].ToString();

                                        break;
                                    }
                                }

                                break;
                            }
                        }

                        #endregion

                        if (ResourceGroupCode == "")
                        {
                            foreach (DataRow row in EngWBDS.Tables["ECOOpDtl"].Rows)
                            {
                                if (row["OprSeq"].ToString() == EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["OprSeq"].ToString())
                                {
                                    row["ResourceGrpID"] = ResourceGroupCode;

                                    row["ResourceGrpDesc"] = ResourceDesc;

                                    row["OpDtlDesc"] = ResourceDesc;

                                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["PrimaryResourceGrpDesc"] = ResourceDesc;

                                    EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["PrimaryResourceGrpID"] = ResourceGroupCode;

                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); MessageBox.Show("Setting resource default didn't work, tell him to fix it"); }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
        }

        private void prodhrs_num_Enter(object sender, EventArgs e)
        {
            prodhrs_num.ValueChanged += prodhrs_num_ValueChanged;
        }

        private void prodhrs_num_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = OPDataGrid.CurrentCell.RowIndex;

                EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["ProdStandard"] = prodhrs_num.Value;
            }
            catch { }
        }

        void prodhrs_num_Leave(object sender, EventArgs e)
        {
            prodhrs_num.ValueChanged -= prodhrs_num_ValueChanged;
        }

        private void resource_show_Click(object sender, EventArgs e)
        {
            try
            {
                EngWB.Update(EngWBDS);
            }
            catch { }

            //EngWB.ResequenceOperations(gid_txt.Text, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

            //EngWBDS = EngWB.GetDatasetForTree(gid_txt.Text, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

            OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

            string operation = OPDataGrid["OprSeq", OPDataGrid.CurrentCellAddress.Y].Value.ToString();

            Resource_Master Resource_Mast = new Resource_Master(partnumber_txt.Text, rev_txt.Text, Properties.Settings.Default.ecogroup, operation, EngWBDS);

            Resource_Mast.ShowDialog();
        }

        private void prodstd_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = OPDataGrid.CurrentCell.RowIndex;

                EngWBDS.Tables["ECOOpr"].Rows[RowIndex]["Stdformat"] = prodstd_cbo.SelectedItem.ToString();
            }
            catch { }
        }

        private void saveclose_btn_Click(object sender, EventArgs e)
        {
            try
            {
                EngWB.Update(EngWBDS);

                resource_show.Enabled = true;

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!"); }

            this.Close();
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {
            TemplateMenu.Items.Clear();

            DataTable DT = Templates.GetOomTemplates();

            foreach (DataRow dr in DT.Rows)
            {
                ToolStripMenuItem TS = new ToolStripMenuItem();

                TS.Name = dr["Name"].ToString();

                TS.Text = dr["Name"].ToString();

                TS.Click += TS_Click;

                TemplateMenu.Items.Add(TS);
            }

            ToolStripMenuItem CopyMethod = new ToolStripMenuItem();

            CopyMethod.Name = "CopyMethod";

            CopyMethod.Text = "Copy Method From...";

            CopyMethod.Click += CopyMethod_Click;

            TemplateMenu.Items.Add(CopyMethod);

            TemplateMenu.Show(copy_btn, new Point(0, copy_btn.Height));
        }

        void CopyMethod_Click(object sender, EventArgs e)
       {
            Operations_CopyMethods OpCopy = new Operations_CopyMethods(partnumber_txt.Text);

            OpCopy.ShowDialog();

            DataList.GetDetailsFromMethods(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, OpCopy.retPart, OpCopy.retRev);

            EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

            OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
        }

        void TS_Click(object sender, EventArgs e)
        {
                EngWB.Update(EngWBDS);

                prodhrs_num.ValueChanged -= prodhrs_num_ValueChanged;

                for (int i = EngWBDS.Tables["ECOOpr"].Rows.Count - 1; i > -1; i--)
                {
                    EngWBDS.Tables["ECOOpr"].Rows[i].Delete();
                }

                ToolStripMenuItem TS = (ToolStripMenuItem)sender;

                //retrieve and update form per template
                DataTable DT = Templates.GetFullTemplate(TS.Name, "OOM");

                //Add all required operations
                //foreach (DataRow Dr in DT.Rows)
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow Dr = DT.Rows[i];

                    EngWB.GetNewECOOpr(EngWBDS, Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "");

                    opmast_cbo.SelectedValue = Dr["PropertyValue"].ToString();

                    decimal dec = decimal.Parse(Dr["PropertyQty"].ToString());

                    LaborEntryMethod_cbo.SelectedValue = Dr["PropertyOptions5"].ToString();

                    bool SNReq = false;

                    bool.TryParse(Dr["PropertyOptions6"].ToString(), out SNReq);

                    bool AutoRec = false;

                    bool.TryParse(Dr["PropertyOptions7"].ToString(), out AutoRec);          

                    prodhrs_num.Value = dec;

                    EngWBDS.Tables["ECOOpr"].Rows[EngWBDS.Tables["ECOOpr"].Rows.Count - 1]["OpCode"] = opmast_cbo.SelectedValue.ToString();

                    EngWBDS.Tables["ECOOpr"].Rows[EngWBDS.Tables["ECOOpr"].Rows.Count - 1]["OpDesc"] = opmast_cbo.Text;

                    EngWBDS.Tables["ECOOpr"].Rows[EngWBDS.Tables["ECOOpr"].Rows.Count - 1]["ProdStandard"] = prodhrs_num.Value;

                    EngWBDS.Tables["ECOOpr"].Rows[EngWBDS.Tables["ECOOpr"].Rows.Count - 1]["LaborEntryMethod"] = LaborEntryMethod_cbo.SelectedValue;

                    EngWB.Update(EngWBDS);

                    if (AutoRec)
                    {
                        EngWBDS.Tables["ECOOpr"].Rows[i]["AutoReceive"] = AutoRec;

                        AutoRecieve_chk.Checked = AutoRec;

                        EngWB.Update(EngWBDS);
                    }

                    if (SNReq)
                    {
                        EngWBDS.Tables["ECOOpr"].Rows[i]["SNRequiredOpr"] = SNReq;

                        SNRequiredOpr_chk.Checked = SNReq;

                        EngWB.Update(EngWBDS);
                    }
                }

                EngWB.ResequenceOperations(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Now, false, false, false, false);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

                //save

                EngWB.Update(EngWBDS);

                DT = Templates.GetFullTemplate(TS.Name, "RES");

                //Add all required resources
                foreach (DataRow Dr in DT.Rows)
                {
                    //EngWB.GetNewECOOpDtl(EngWBDS, gid_txt.Text, partnumber_txt.Text, rev_txt.Text, "", int.Parse(Dr["PropertyType"].ToString()));

                    int row = 0;

                    for (int i = 0; i < EngWBDS.Tables["ECOOpDtl"].Rows.Count; i++)
                    {
                        int PropQty = (int.Parse(Dr["PropertyQty"].ToString()) + 1) * 10;

                        if ((Dr["PropertyType"].ToString() == EngWBDS.Tables["ECOOpDtl"].Rows[i]["OprSeq"].ToString()) && (PropQty.ToString() == EngWBDS.Tables["ECOOpDtl"].Rows[i]["OpDtlSeq"].ToString()))
                        {
                            row = i;
                            break;
                        }
                    }

                    EngWBDS.Tables["ECOOpDtl"].Rows[row]["ResourceID"] = Dr["PropertyUOM"].ToString();

                    EngWBDS.Tables["ECOOpDtl"].Rows[row]["ResourceGrpID"] = Dr["PropertyValue"].ToString();

                    try
                    {
                        EngWB.Update(EngWBDS);
                    }
                    catch { }
                }

                //save
                EngWB.Update(EngWBDS);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

                if (OPDataGrid.Rows.Count > 0)
                {
                    removebtn.Enabled = true;

                    resource_show.Enabled = true;
                }

                prodhrs_num.ValueChanged += prodhrs_num_ValueChanged;
        }

        private void EnableSNChk()
        {
            bool SerialCapable = DataList.GetSerialized(partnumber_txt.Text);

            SNRequiredOpr_chk.Enabled = SerialCapable;

            //if (AutoRecieve_chk.Checked)
            //    SNRequiredOpr_chk.Enabled = false;
        }

        private void FillLaborEntry()
        {
            DataTable Dt = new DataTable();

            Dt.Columns.Add("Description");

            Dt.Columns.Add("Code");

            DataRow Dr = Dt.NewRow();

            Dr["Description"] = "Backflush";

            Dr["Code"] = "B";

            Dt.Rows.Add(Dr);

            Dr = Dt.NewRow();

            Dr["Description"] = "Quantity Only";

            Dr["Code"] = "Q";

            Dt.Rows.Add(Dr);

            Dr = Dt.NewRow();

            Dr["Description"] = "Time and Quantity";

            Dr["Code"] = "T";

            Dt.Rows.Add(Dr);

            LaborEntryMethod_cbo.DataSource = Dt;

            //LaborEntryMethod_cbo.Items.Add(new PartTypeCode("Backflush", "B"));

            //LaborEntryMethod_cbo.Items.Add(new PartTypeCode("Quantity Only", "Q"));

            //LaborEntryMethod_cbo.Items.Add(new PartTypeCode("Time and Quantity", "T"));

            LaborEntryMethod_cbo.DisplayMember = "Description";

            LaborEntryMethod_cbo.ValueMember = "Code";
            try
            {
                LaborEntryMethod_cbo.SelectedIndex = 1;
            }
            catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
        }

        private void FillLaborEntryGrid()
        {
            for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                switch (OPDataGrid["LaborEntry",i].Value.ToString())
                {
                    case "T":
                        OPDataGrid["LaborEntryDesc", i].Value = "Time and Quantity";
                        break;
                    case "Q":
                        OPDataGrid["LaborEntryDesc", i].Value = "Quantity Only";
                        break;
                    case "B":
                        OPDataGrid["LaborEntryDesc", i].Value = "Backflush";
                        break;
                }
            }
        }

        private void LaborEntryMethod_cbo_Leave(object sender, EventArgs e)
        {
            LaborEntryMethod_cbo.SelectedIndexChanged -= LaborEntryMethod_cbo_SelectedIndexChanged;
        }

        private void LaborEntryMethod_cbo_Enter(object sender, EventArgs e)
        {
            LaborEntryMethod_cbo.SelectedIndexChanged += LaborEntryMethod_cbo_SelectedIndexChanged;
        }

        private void LaborEntryMethod_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["LaborEntryMethod"] = LaborEntryMethod_cbo.SelectedValue;

                switch (OPDataGrid["LaborEntry", OPDataGrid.CurrentCellAddress.Y].Value.ToString())
                {
                    case "T":
                        OPDataGrid["LaborEntryDesc", OPDataGrid.CurrentCellAddress.Y].Value = "Time and Quantity";
                        break;
                    case "Q":
                        OPDataGrid["LaborEntryDesc", OPDataGrid.CurrentCellAddress.Y].Value = "Quantity Only";
                        break;
                    case "B":
                        OPDataGrid["LaborEntryDesc", OPDataGrid.CurrentCellAddress.Y].Value = "Backflush";
                        break;
                }
            }
            catch { }
        }

        #region SubCon Stuff

        private void Subcontract_btn_Click(object sender, EventArgs e)
        {
            EngWB.GetNewECOOpr(EngWBDS, Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "");

            EngWB.EcoOprInitSNReqSubConShip(EngWBDS);

            OPDataGrid["SubContract", OPDataGrid.Rows.Count - 1].Value = true;
        }

        private void refneeded_chk_CheckedChanged(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                quotesreq_num.Enabled = refneeded_chk.Checked;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["RFQNeeded"] = refneeded_chk.Checked;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["RFQVendQuotes"] = quotesreq_num.Value.ToString();
            }
        }

        private void supplierid_btn_Click(object sender, EventArgs e)
        {
            Operations_SupplierSearch OpsSupSearch = new Operations_SupplierSearch();

            OpsSupSearch.ShowDialog();

            if (OpsSupSearch.DialogResult == DialogResult.OK)
            {
                supplierid_txt.Text = OpsSupSearch.ReturnID;

                supplieradd_txt.Text = OpsSupSearch.ReturnDdsBillAddr;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumCity"] = OpsSupSearch.ReturnCity;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumZIP"] = OpsSupSearch.ReturnZip;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumState"] = OpsSupSearch.ReturnState;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumName"] = OpsSupSearch.ReturnName;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["DspBillAddr"] = OpsSupSearch.ReturnDdsBillAddr;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNum"] = OpsSupSearch.ReturnVendID;
            }
        }

        private void subcon_opsmast_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

                if (Convert.ToBoolean(val))
                {
                    EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["OpCode"] = subcon_opsmast_cbo.SelectedValue.ToString();

                    EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["OpDesc"] = subcon_opsmast_cbo.Text.ToString();
                }
            }
            catch { }
        }

        private void quotesreq_num_ValueChanged(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["RFQVendQuotes"] = quotesreq_num.Value.ToString();
            }
        }

        private void uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

                if (Convert.ToBoolean(val))
                {
                    EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["IUM"] = uom_cbo.Text;
                }
            }
            catch { }
        }

        private void qtyper_num_ValueChanged(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["QtyPer"] = qtyper_num.Value.ToString();
            }
        }

        private void unitcost_num_ValueChanged(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["EstUnitCost"] = unitcost_num.Value.ToString();
            }
        }

        private void daysout_num_ValueChanged(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["DaysOut"] = daysout_num.Value.ToString();
            }
        }

        private void supplierid_txt_Leave(object sender, EventArgs e)
        {
            object val = OPDataGrid["SubContract", OPDataGrid.CurrentCellAddress.Y].Value;

            if (Convert.ToBoolean(val))
            {
                Operations_SupplierSearch OpsSupSearch = new Operations_SupplierSearch(supplierid_txt.Text);

                supplierid_txt.Text = OpsSupSearch.ReturnID;

                supplieradd_txt.Text = OpsSupSearch.ReturnDdsBillAddr;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumCity"] = OpsSupSearch.ReturnCity;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumZIP"] = OpsSupSearch.ReturnZip;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumState"] = OpsSupSearch.ReturnState;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumName"] = OpsSupSearch.ReturnName;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["DspBillAddr"] = OpsSupSearch.ReturnDdsBillAddr;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNum"] = OpsSupSearch.ReturnVendID;

                EngWBDS.Tables["ECOOpr"].Rows[OPDataGrid.CurrentCellAddress.Y]["VendorNumVendorID"] = supplierid_txt.Text;
            }
        }

        #endregion

        private void moveup_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int rowid = OPDataGrid.CurrentCellAddress.Y;

                int rowseq = int.Parse (EngWBDS .Tables["ECOOpr"].Rows[rowid]["OprSeq"].ToString());

                int new_rowseq = int.Parse(EngWBDS.Tables["ECOOpr"].Rows[rowid - 1]["OprSeq"].ToString());

                string OpMessage, MsgType;


                EngWBDS.Tables["ECOOpr"].Rows[rowid]["OprSeq"] = (new_rowseq - 1).ToString();

                EngWB.CheckECOOprOprSeq(new_rowseq - 1, out OpMessage, out MsgType, EngWBDS);


                EngWBDS.Tables["ECOOpr"].Rows[rowid]["OprSeq"] = (new_rowseq - 1).ToString();
                                
                EngWB.ChangeECOOprOprSeq(new_rowseq - 1, EngWBDS);

                for (int i = 0; i < EngWBDS.Tables["ECOOpr"].Rows.Count; i++)
                {
                    if (EngWBDS.Tables["ECOOpr"].Rows[i]["OprSeq"].ToString() == rowseq.ToString())
                    {
                        EngWBDS.Tables["ECOOpr"].Rows.RemoveAt(i);

                        EngWBDS.Tables["ECOOpDtl"].Rows.RemoveAt(i);
                    }
                }

                //EngWB.Update(EngWBDS);

                EngWBDS = EngWB.ResequenceOperations(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
            }
            catch { }
        }

        private void movedown_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int rowid = OPDataGrid.CurrentCellAddress.Y;

                int rowseq = int.Parse(EngWBDS.Tables["ECOOpr"].Rows[rowid]["OprSeq"].ToString());

                EngWBDS.Tables["ECOOpr"].Rows[rowid]["OprSeq"] = (rowseq - 9).ToString();

                EngWBDS.Tables["ECOOpr"].Rows[rowid - 1]["OprSeq"] = (rowseq + 5).ToString();

                if (EngWBDS.Tables["ECOOpr"].Rows[0]["OprSeq"].ToString() != "10".ToString())
                    EngWBDS.Tables["ECOOpr"].Rows[0]["OprSeq"] = 10;

                EngWB.ResequenceOperations(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, true, true, false);

                EngWB.Update(EngWBDS);

                EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, partnumber_txt.Text, rev_txt.Text, "", DateTime.Today, false, false);

                OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];
            }
            catch { }
        }

        private void prodstd_btn_Click(object sender, EventArgs e)
        {
            Operations_Minutes OpMin = new Operations_Minutes();

            OpMin.ShowDialog();
            
            prodhrs_num.Value = OpMin.RetVal;

            OpMin.Dispose();

            opmast_cbo_SelectedIndexChanged(this, null);
        }

        private void resource_timer_Tick(object sender, EventArgs e)
        {
            resource_show.Enabled = (EngWBDS.Tables["ECOOpr"].Rows.Count != 0);
        }

        private void prodstd_cbo_Leave(object sender, EventArgs e)
        {
            prodstd_cbo.SelectedIndexChanged -= prodstd_cbo_SelectedIndexChanged;
        }

        private void prodstd_cbo_Enter(object sender, EventArgs e)
        {
            prodstd_cbo.SelectedIndexChanged += prodstd_cbo_SelectedIndexChanged;
        }

        private void AutoRecieve_chk_Enter(object sender, EventArgs e)
        {
            AutoRecieve_chk.CheckedChanged += AutoRecieve_chk_CheckedChanged;
        }

        private void AutoRecieve_chk_Leave(object sender, EventArgs e)
        {
            AutoRecieve_chk.CheckedChanged -= AutoRecieve_chk_CheckedChanged;
        }

        private void SNRequiredOpr_chk_Enter(object sender, EventArgs e)
        {
            SNRequiredOpr_chk.CheckedChanged += SNRequiredOpr_chk_CheckedChanged;
        }

        private void SNRequiredOpr_chk_Leave(object sender, EventArgs e)
        {
            SNRequiredOpr_chk.CheckedChanged -= SNRequiredOpr_chk_CheckedChanged;
        }

        private void DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            EngWBDS = EngWB.GetDatasetForTree(Properties.Settings.Default.ecogroup, _PartNumber, _Rev, "", DateTime.Today, false, false);
        }

        private void WorkFinished(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            OPDataGrid.DataSource = EngWBDS.Tables["ECOOpr"];

            DataList.EpicClose();

            FillLaborEntry();
        }

        private void ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }
    }
}