﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor.Mfg.BO;
using TableAdapterHelper;
using System.Data.SqlClient;

namespace EpicorIntegration
{
    public partial class Template_Master : Form
    {
        SqlTransaction Transaction { get; set; }

        ENGDataDataSetTableAdapters.TemplatesTableAdapter TemplateAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

        public Template_Master()
        {
            InitializeComponent();

            #region Fill DataLists

            type_cbo.Items.Add(new PartTypeCode("", ""));

            type_cbo.Items.Add(new PartTypeCode("Manufactured", "M"));

            type_cbo.Items.Add(new PartTypeCode("Purchased", "P"));

            type_cbo.Items.Add(new PartTypeCode("Sales Kit", "K"));

            type_cbo.DisplayMember = "Description";

            DataTable dt = DataList.ProdGrupDataSet().Tables[0];

            DataRow dr = dt.NewRow();

            dr["Description"] = "";

            dt.Rows.InsertAt(dr, 0);

            group_cbo.DataSource = dt;

            group_cbo.DisplayMember = "Description";

            group_cbo.ValueMember = "ProdCode";

            dt = DataList.PartClassDataSet().Tables[0];

            dr = dt.NewRow();

            dr["Description"] = "";

            dt.Rows.InsertAt(dr, 0);

            class_cbo.DataSource = dt;

            class_cbo.DisplayMember = DataList.PartClassDataSet().Tables[0].Columns["Description"].ToString();

            class_cbo.ValueMember = DataList.PartClassDataSet().Tables[0].Columns["ClassID"].ToString();

            dt = DataList.UOMClassDataSet().Tables[0];

            dr = dt.NewRow();

            dr["Description"] = "";

            dt.Rows.InsertAt(dr, 0);

            uomclass_cbo.DataSource = dt;

            uomclass_cbo.DisplayMember = DataList.UOMClassDataSet().Tables[0].Columns["Description"].ToString();

            dt = DataList.PlantDataSet().Tables[0];

            dr = dt.NewRow();

            dr["NAME"] = "";

            dt.Rows.InsertAt(dr, 0);

            plant_cbo.DataSource = dt;

            plant_cbo.DisplayMember = DataList.PlantDataSet().Tables[0].Columns["NAME"].ToString();

            plant_cbo.ValueMember = "Plant";

            dt = DataList.WarehseDataSet().Tables[0];

            dr = dt.NewRow();

            dr["Description"] = "";

            dt.Rows.InsertAt(dr, 0);

            whse_cbo.DataSource = dt;

            whse_cbo.DisplayMember = DataList.WarehseDataSet().Tables[0].Columns["Description"].ToString();

            whse_cbo.ValueMember = "WarehouseCode";

            DataSet DS = DataList.UOMSearchDataSet();

            DS.Tables[0].Columns.Add("FullCode", typeof(string), "UOMCode + ' - ' + UOMDesc");

            dr = DS.Tables[0].NewRow();

            dr["FullCode"] = "";

            DS.Tables[0].Rows.InsertAt(dr, 0);

            uom_cbo.DataSource = DS.Tables[0];

            uom_cbo.DisplayMember = DS.Tables[0].Columns["FullCode"].ToString();

            uom_cbo.ValueMember = "UOMCode";

            uomclass_cbo.SelectedIndex = 2;

            type_cbo.SelectedIndex = 0;

            dt = DataList.UOMVolumeDataSet().Tables[0];

            dr = dt.NewRow();

            dr["UOMCode"] = "";

            dt.Rows.InsertAt(dr, 0);

            uomvol_cbo.DataSource = dt;

            uomvol_cbo.DisplayMember = DataList.UOMVolumeDataSet().Tables[0].Columns["UOMCode"].ToString();

            uomvol_cbo.ValueMember = "UOMCode";

            dt = DataList.UOMWeightDataSet().Tables[0];

            dr = dt.NewRow();

            dr["UOMCode"] = "";

            dt.Rows.InsertAt(dr, 0);

            uomweight_cbo.DataSource = dt;

            uomweight_cbo.DisplayMember = DataList.UOMWeightDataSet().Tables[0].Columns["UOMCode"].ToString();

            uomweight_cbo.ValueMember = "UOMCode";

            #endregion

            BillDataGrid.SelectionChanged += BillDataGrid_SelectionChanged;

            OPDataGrid.SelectionChanged += OPDataGrid_SelectionChanged;
        }

        void OPDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        void BillDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            qty_num.ValueChanged -= qty_num_ValueChanged;

            partnum_txt.TextChanged -= partnum_txt_TextChanged;

            operation_txt.TextChanged -= operation_txt_TextChanged;

            ViewAsAsm_chk.CheckedChanged -= ViewAsAsm_chk_CheckedChanged;

            bill_uom_cbo.SelectedIndexChanged -= bill_uom_cbo_SelectedIndexChanged;

            try
            {
                qty_num.Value = decimal.Parse(BillDataGrid.CurrentRow.Cells["PropertyQty"].Value.ToString());

                partnum_txt.Text = BillDataGrid.CurrentRow.Cells["PropertyValue"].Value.ToString();

                operation_txt.Text = BillDataGrid.CurrentRow.Cells["PropertyType"].Value.ToString();

                ViewAsAsm_chk.Checked = bool.Parse(BillDataGrid.CurrentRow.Cells["PropertyOptions"].Value.ToString());

                DataTable ds = DataList.PartUOM(partnum_txt.Text);

                bill_uom_cbo.DataSource = ds;

                bill_uom_cbo.DisplayMember = "UOMCode";

                bill_uom_cbo.ValueMember = "UOMCode";

                bill_uom_cbo.SelectedValue = BillDataGrid.CurrentRow.Cells["PropertyUOM"].Value.ToString();
            }
            catch { }

            qty_num.ValueChanged += qty_num_ValueChanged;

            partnum_txt.TextChanged += partnum_txt_TextChanged;

            operation_txt.TextChanged += operation_txt_TextChanged;

            ViewAsAsm_chk.CheckedChanged += ViewAsAsm_chk_CheckedChanged;

            bill_uom_cbo.SelectedIndexChanged += bill_uom_cbo_SelectedIndexChanged;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_bill_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillItemLists()
        {
            type_cbo.Items.Add(new PartTypeCode("Manufactured", "M"));

            type_cbo.Items.Add(new PartTypeCode("Purchased", "P"));

            type_cbo.Items.Add(new PartTypeCode("Sales Kit", "K"));

            type_cbo.DisplayMember = "Description";

            type_cbo.SelectedIndex = 0;

            group_cbo.DataSource = DataList.ProdGrupDataSet().Tables[0];

            group_cbo.DisplayMember = "Description";

            group_cbo.ValueMember = "ProdCode";

            class_cbo.DataSource = DataList.PartClassDataSet().Tables[0];

            class_cbo.DisplayMember = DataList.PartClassDataSet().Tables[0].Columns["Description"].ToString();

            class_cbo.ValueMember = DataList.PartClassDataSet().Tables[0].Columns["ClassID"].ToString();

            uomclass_cbo.DataSource = DataList.UOMClassDataSet().Tables[0];

            uomclass_cbo.DisplayMember = DataList.UOMClassDataSet().Tables[0].Columns["Description"].ToString();

            plant_cbo.DataSource = DataList.PlantDataSet().Tables[0];

            plant_cbo.DisplayMember = DataList.PlantDataSet().Tables[0].Columns["NAME"].ToString();

            plant_cbo.ValueMember = "Plant";

            DataSet DS = DataList.UOMSearchDataSet();

            DS.Tables[0].Columns.Add("FullCode", typeof(string), "UOMCode + ' - ' + UOMDesc");

            uom_cbo.DataSource = DS.Tables[0];

            uom_cbo.DisplayMember = DS.Tables[0].Columns["FullCode"].ToString();

            uom_cbo.ValueMember = "UOMCode";

            uomclass_cbo.SelectedIndex = 2;

            uomweight_cbo.DataSource = DataList.UOMWeightDataSet().Tables[0];

            uomweight_cbo.DisplayMember = DataList.UOMWeightDataSet().Tables[0].Columns["UOMCode"].ToString();

            uomweight_cbo.ValueMember = "UOMCode";

            planner_cbo.DataSource = DataList.PlannerList().Tables[0];

            planner_cbo.DisplayMember = "Name";

            planner_cbo.ValueMember = "PersonID";
        }

        private void Template_Master_Load(object sender, EventArgs e)
        {
            this.Size = new Size(538, 580);

            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

            //this.itemTemplateTableAdapter.Fill(this.engDataDataSet.ItemTemplate);

            ItemTemplateList.ClearSelection();

            ItemTemplateList.CurrentCellChanged += ItemTemplateList_CurrentCellChanged;

            ItemTemplateList.DataSource = Templates.GetItemTemplates();

            OpsTemplateList.DataSource = Templates.GetOomTemplates();

            BillTemplateList.DataSource = Templates.GetBomTemplates();

            ResTemplateList.DataSource = Templates.GetResTemplates();

            FillItemLists();

            ItemTemplateList.SelectionChanged += ItemTemplateList_SelectionChanged;

            BillTemplateList.SelectionChanged += BillTemplateList_SelectionChanged;

            OpsTemplateList.SelectionChanged += OpsTemplateList_SelectionChanged;

            ResTemplateList.SelectionChanged += ResTemplateList_SelectionChanged;

            Transaction = TableHelper.BeginTransaction(TemplateAdapter);
        }

        void ResTemplateList_SelectionChanged(object sender, EventArgs e)
        {
            ResDataGrid.DataSource = Templates.GetFullTemplate(ResTemplateList.CurrentCell.Value.ToString(), "RES");
        }

        void OpsTemplateList_SelectionChanged(object sender, EventArgs e)
        {
            OPDataGrid.DataSource = Templates.GetFullTemplate(OpsTemplateList.CurrentCell.Value.ToString(), "OOM");

            /*for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                try
                {
                    
                }
                catch { }
            }*/
        }

        void ItemTemplateList_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["ItemTab"])
            {
                this.Size = new Size(511, 580);

                this.CancelButton = close_item_btn;
            }
            else
            {
                this.Size = new Size(731, 542);

                CancelButton = close_op_btn;
            }
        }

        public PartData pdata = new PartData();

        void ItemTemplateList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {                    
                Description_txt.Text = pdata.Description;
                    
                type_cbo.SelectedText = pdata.PMT;
                    
                uomclass_cbo.SelectedText = pdata.UOM_Class;
                    
                NetWeight.Value = pdata.Net_Weight;
                
                uomweight_cbo.SelectedText = pdata.Net_Weight_UM;
                
                NetVolume.Value = pdata.Net_Vol;
                
                uomvol_cbo.SelectedText = pdata.Net_Vol_UM;
                
                uom_cbo.SelectedText = pdata.Primary_UOM;
                
                group_cbo.SelectedText = pdata.PartGroup;
                
                class_cbo.SelectedText = pdata.PartClass;
                
                plant_cbo.SelectedText = pdata.PartPlant;

                //whse_cbo.SelectedText = pdata.PlantWhse;
            }
            catch { }
        }

        private void type_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        #region Bill Functions

        void BillTemplateList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                billtemplatename_txt.Text = BillTemplateList.CurrentCell.Value.ToString();

                //BillDataGrid.DataSource = Templates.GetFullTemplate(billtemplatename_txt.Text, "BOM");

                TemplateAdapter.FillByNameType(engDataDataSet.Templates, "BOM", billtemplatename_txt.Text);

                BillDataGrid.DataSource = engDataDataSet.Templates;
            }
            catch { }
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

        private void RawMenu_Click(object sender, EventArgs e)
        {
            RawMenuStrip.Show(RawMenu, new Point(0, RawMenu.Height));
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

            RawMenuStrip.Items.Add(TS_item);

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

            RawMenuStrip.Items.Add(TS_item1);

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

            RawMenuStrip.Items.Add(TS_item2);

            #endregion
        }

        void TS_sub_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem TS = (ToolStripMenuItem)sender;

            partnum_txt.Text = TS.ToolTipText;

            PartTimer.Enabled = true;
        }

        private void AddBill_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.InsertNewLine(billtemplatename_txt.Text, "BOM");

            TemplateAdapter.FillByNameType(engDataDataSet.Templates, "BOM", billtemplatename_txt.Text);

            BillDataGrid.DataSource = engDataDataSet.Templates;

            BillDataGrid.ClearSelection();

            BillDataGrid.CurrentCell = BillDataGrid.Rows[BillDataGrid.Rows.Count - 1].Cells[1];

            BillDataGrid.CurrentRow.Cells["PropertyType"].Value = operation_txt.Text;

            partnum_txt.Text = "";

            qty_num.Value = 0;

            ViewAsAsm_chk.Checked = false;

            bill_uom_cbo.SelectedIndex = 0;
        }

        private void DelBill_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            int rowindex = BillDataGrid.CurrentCellAddress.Y;

            TemplateAdapter.DeleteLine(int.Parse(BillDataGrid.CurrentRow.Cells["row_id"].Value.ToString()));

            BillDataGrid.Rows.RemoveAt(rowindex);
        }

        private void partnum_txt_TextChanged(object sender, EventArgs e)
        {
            //desc_txt.Text = DataList.GetCurrentDesc(partnum_txt.Text);

            DataTable ds = DataList.PartUOM(partnum_txt.Text);

            uom_cbo.DataSource = ds;

            uom_cbo.DisplayMember = "UOMCode";

            uom_cbo.ValueMember = "UOMCode";

            BillDataGrid.CurrentRow.Cells["PropertyValue"].Value = partnum_txt.Text;
        }

        private void PartTimer_Tick(object sender, EventArgs e)
        {
            UpdateDescField();
        }

        private void save_bill_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Transaction.Commit();

                Transaction = null;
            }
            catch { Transaction.Rollback(); }
        }

        private void qty_num_ValueChanged(object sender, EventArgs e)
        {
            BillDataGrid.CurrentRow.Cells["PropertyQty"].Value = qty_num.Value.ToString();

            UpdateLine(BillDataGrid, billtemplatename_txt.Text);
        }

        private void bill_uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BillDataGrid.CurrentRow.Cells["PropertyUOM"].Value = bill_uom_cbo.SelectedValue;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text);
        }

        private void desc_txt_TextChanged(object sender, EventArgs e)
        {
            BillDataGrid.CurrentRow.Cells["PartDescription"].Value = desc_txt.Text;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text);
        }

        private void ViewAsAsm_chk_CheckedChanged(object sender, EventArgs e)
        {
            BillDataGrid.CurrentRow.Cells["PropertyOptions"].Value = ViewAsAsm_chk.Checked.ToString();

            UpdateLine(BillDataGrid, billtemplatename_txt.Text);
        }

        private void operation_txt_TextChanged(object sender, EventArgs e)
        {
            BillDataGrid.CurrentRow.Cells["PropertyType"].Value = operation_txt.Text;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text);
        }

        private void add_bill_btn_Click(object sender, EventArgs e)
        {
            Template_Title TT = new Template_Title();

            DialogResult Dr = TT.ShowDialog();

            if (TT.DialogResult == DialogResult.OK)
            {
                string TemplateName = TT.RetVal;

                TT.Dispose();
                
                TemplateAdapter.InsertNewLine(TemplateName, "BOM");

                TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "BOM");

                //BillTemplateList.CurrentCell = BillTemplateList.Rows[BillTemplateList.Rows.Count - 1].Cells[1];
            }
        }

        #endregion

        private void UpdateLine(DataGridView DGV,string TemplateName)
        {
            int rowindex = DGV.CurrentCellAddress.Y;

            int row_id = int.Parse(DGV["row_id", rowindex].Value.ToString());

            TemplateAdapter.UpdateQuery(TemplateName, DGV["PropertyType", rowindex].Value.ToString(), DGV["PropertyValue", rowindex].Value.ToString(), DGV["PropertyQty", rowindex].Value.ToString(), DGV["PropertyUOM", rowindex].Value.ToString(), DGV["PropertyOptions", rowindex].Value.ToString(), row_id);
        }

        private void del_item_btn_Click(object sender, EventArgs e)
        {
            TemplateAdapter.DeleteTemplate(billtemplatename_txt.Text, "BOM");

            TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "BOM");
        }
    }
}
