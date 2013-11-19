using Epicor.Mfg.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EpicorIntegration
{
    public partial class Resource_Master : Form
    {
        private EngWorkBench EngWB = new EngWorkBench(DataList.EpicConn);

        private int MaxIndex = new int();

        public EngWorkBenchDataSet EngWB_DS;

        List<int> GridRow
        { get; set; }

        List<int> IndexforRes(EngWorkBenchDataSet EngWB_DS, string operation)
        {
            List<int> RetVal = new List<int>();

            for (int i = 0; i < EngWB_DS.Tables["ECOOpDtl"].Rows.Count; i++)
            {
                if (EngWB_DS.Tables["ECOOpDtl"].Rows[i]["OprSeq"].ToString() == operation)
                    RetVal.Add(i);
            }

            return RetVal;
        }

        public void FormStart()
        {
            DataSet ds = DataList.ResourceGroup();

            resourcegrp_cbo.DataSource = ds.Tables[0];

            resourcegrp_cbo.ValueMember = ds.Tables[0].Columns["ResourceGrpID"].ToString();

            resourcegrp_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            ds = DataList.Resource(resourcegrp_cbo.SelectedValue.ToString());

            DataRow dr = ds.Tables[0].NewRow();

            dr["Description"] = "";

            dr["ResourceID"] = "";

            ds.Tables[0].Rows.InsertAt(dr,0);

            resource_cbo.DataSource = ds.Tables[0];

            resource_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            resource_cbo.ValueMember = ds.Tables[0].Columns["ResourceID"].ToString();
        }

        public Resource_Master(string PartNumber, string Rev, string GroupID, string Operation,EngWorkBenchDataSet EngWBDS)
        {
            InitializeComponent();

            GridRow = IndexforRes(EngWBDS, Operation);

            resource_cbo.SelectedIndexChanged -= resource_cbo_SelectedIndexChanged;

            resourcegrp_cbo.SelectedIndexChanged -= resourcegrp_cbo_SelectedIndexChanged;

            ResourceGrid.SelectionChanged -= ResourceGrid_SelectionChanged;

            ResourceGrid.AutoGenerateColumns = false;

            FormStart();

            partnumber_txt.Text = PartNumber;

            rev_txt.Text = Rev;

            gid_txt.Text = GroupID;

            operation_txt.Text = Operation;

            EngWB_DS = EngWBDS;

            DataView DV = EngWB_DS.Tables["ECOOpDtl"].DefaultView;

            DV.RowFilter = string.Format("OprSeq = " + operation_txt.Text);

            ResourceGrid.DataSource = DV;

            MaxIndex = ResourceGrid.Rows.Count;

            resource_cbo.SelectedIndexChanged += resource_cbo_SelectedIndexChanged;

            resourcegrp_cbo.SelectedIndexChanged += resourcegrp_cbo_SelectedIndexChanged;

            ResourceGrid.SelectionChanged += ResourceGrid_SelectionChanged;

            /*
            for (int i = 0; i < ResourceGrid.Rows.Count; i++)
            {
                if (operation_txt.Text == ResourceGrid["OprSeq", i].Value.ToString())
                    indicies.Add(i);
            }*/           
        }

        public Resource_Master()
        {
            InitializeComponent();

            ResourceGrid.AutoGenerateColumns = false;

            FormStart();
        }

        private void Resource_Master_Load(object sender, EventArgs e)
        {
            ResourceGrid.AutoGenerateColumns = false;

            ResourceGrid.SelectionChanged += ResourceGrid_SelectionChanged;

            ResourceGrid.ClearSelection();

            ResourceGrid.CurrentCell = ResourceGrid.Rows[0].Cells[0];

            resourcegrp_cbo.SelectedIndexChanged += resourcegrp_cbo_SelectedIndexChanged;

            resource_cbo.SelectedIndexChanged += resource_cbo_SelectedIndexChanged;
        }

        void ResourceGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string CurrentGroup = ResourceGrid["ResourceGrpID", ResourceGrid.CurrentCellAddress.Y].Value.ToString();

                string CurrentRes = ResourceGrid["ResourceID", ResourceGrid.CurrentCellAddress.Y].Value.ToString();

                resourcegrp_cbo.SelectedValue = CurrentGroup;

                resource_cbo.SelectedValue = CurrentRes;
            }
            catch { }
        }

        private void resourcegrp_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Fill Resource list

            DataSet ds = DataList.Resource(resourcegrp_cbo.SelectedValue.ToString());

            DataRow dr = ds.Tables[0].NewRow();

            dr["Description"] = "";

            dr["ResourceID"] = "";

            ds.Tables[0].Rows.InsertAt(dr,0);

            resource_cbo.DataSource = ds.Tables[0];

            resource_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            resource_cbo.ValueMember = ds.Tables[0].Columns["ResourceID"].ToString();

            #endregion

            try
            {
                EngWB_DS.Tables["ECOOpDtl"].Rows[GridRow[ResourceGrid.CurrentCellAddress.Y]]["OpDtlDesc"] = resourcegrp_cbo.Text;

                EngWB_DS.Tables["ECOOpDtl"].Rows[GridRow[ResourceGrid.CurrentCellAddress.Y]]["ResourceGrpID"] = resourcegrp_cbo.SelectedValue.ToString();

                EngWB_DS.Tables["ECOOpDtl"].Rows[GridRow[ResourceGrid.CurrentCellAddress.Y]]["ResourceGrpDesc"] = resourcegrp_cbo.Text;
            }
            catch { }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();

            this.Dispose();
        }

        private void resource_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResourceGrid.CurrentCellAddress.Y != -1)
            try
            {
                EngWB_DS.Tables["ECOOpDtl"].Rows[GridRow[ResourceGrid.CurrentCellAddress.Y]]["ResourceID"] = resource_cbo.SelectedValue.ToString();

                EngWB_DS.Tables["ECOOpDtl"].Rows[GridRow[ResourceGrid.CurrentCellAddress.Y]]["ResourceDesc"] = resource_cbo.Text;
            }
            catch { }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                EngWB.Update(EngWB_DS);

                this.Close();

                this.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!"); }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (ResourceGrid.Rows.Count > 1)
                MessageBox.Show("No additional ECO Operation Detail is allowed. Advanced Scheduling license is required.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            else
                EngWB.GetNewECOOpDtl(EngWB_DS, gid_txt.Text, partnumber_txt.Text, rev_txt.Text, "", int.Parse(operation_txt.Text));

            GridRow.Add(MaxIndex);

            ResourceGrid.ClearSelection();

            ResourceGrid.CurrentCell = ResourceGrid.Rows[ResourceGrid.Rows.Count].Cells[0];
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            if (ResourceGrid.Rows.Count < 2)
                MessageBox.Show("Cannot delete last resource!","Warning!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                EngWB_DS.Tables["ECOOpDtl"].Rows.RemoveAt(GridRow[ResourceGrid.CurrentCellAddress.Y]);
        }
    }
}
