using System;
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

        public PartData pdata = new PartData();

        ENGDataDataSetTableAdapters.TemplatesTableAdapter TemplateAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

        public Template_Master()
        {
            InitializeComponent();

            AddItemHandles();

            #region Fill DataLists

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

            /*
            whse_cbo.DataSource = dt;

            whse_cbo.DisplayMember = DataList.WarehseDataSet().Tables[0].Columns["Description"].ToString();

            whse_cbo.ValueMember = "WarehouseCode";
            */

            DataSet DS = DataList.UOMSearchDataSet();

            DS.Tables[0].Columns.Add("FullCode", typeof(string), "UOMCode + ' - ' + UOMDesc");

            dr = DS.Tables[0].NewRow();

            dr["FullCode"] = "";

            DS.Tables[0].Rows.InsertAt(dr, 0);

            uom_cbo.DataSource = DS.Tables[0];

            uom_cbo.DisplayMember = DS.Tables[0].Columns["FullCode"].ToString();

            uom_cbo.ValueMember = "UOMCode";

            uomclass_cbo.SelectedIndex = 2;

            //type_cbo.SelectedIndex = 0;

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

            bool morePages;

            OpMaster OpMaster = new Epicor.Mfg.BO.OpMaster(DataList.EpicConn);

            DataSet ds = (DataSet)OpMaster.GetRows("", "", "", "", "", "", 100, 0, out morePages);

            opmast_cbo.DataSource = ds.Tables["OPMaster"];

            opmast_cbo.ValueMember = "OPCode";

            opmast_cbo.DisplayMember = "OPDesc";

            FillProdStd();

            #endregion

            FillResourceList();

            BillDataGrid.CellClick += BillDataGrid_CellClick;

            OPDataGrid.CellClick += OPDataGrid_CellClick;

            ResDataGrid.CellClick += ResDataGrid_CellClick;

            this.FormClosing += Template_Master_FormClosing;
        }

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

        void Template_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Dialog to save changes
            if (Transaction == null)
                this.Dispose();
            else
                ;//e.Cancel = true;
        }

        private void SaveProcess()
        {
            if (Transaction != null)
                try
                {
                    Transaction.Commit();

                    Transaction = null;
                }
                catch { Transaction.Rollback(); }
        }

        private void RefreshTransaction()
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);
        }

        public DataTable GetFullTemplate(string Name, string Type)
        {
            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TemplateAdapter.FillByNameType(RetVal, Type, Name);

            return (DataTable)RetVal;
        }

        #region Close Buttons

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_bill_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_res_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_op_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

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
            FillItemLists();

            this.Size = new Size(538, 530);

            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

            //this.itemTemplateTableAdapter.Fill(this.engDataDataSet.ItemTemplate);

            ItemTemplateList.ClearSelection();

            ItemTemplateList.CellClick += ItemTemplateList_CellClick;

            ItemTemplateList.DataSource = Templates.GetItemTemplates();

            OpsTemplateList.DataSource = Templates.GetOomTemplates();

            BillTemplateList.DataSource = Templates.GetBomTemplates();

            ResTemplateList.DataSource = Templates.GetResTemplates();

            //BillTemplateList.SelectionChanged += BillTemplateList_SelectionChanged;

            BillTemplateList.CellClick += BillTemplateList_CellClick;

            OpsTemplateList.CellClick += OpsTemplateList_CellClick;

            ResTemplateList.CellClick += ResTemplateList_CellClick;

            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            ItemTemplateList_CellClick(ItemTemplateList, new DataGridViewCellEventArgs(0, 1));
        }

        void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["ItemTab"])
            {
                this.Size = new Size(511, 530);

                this.CancelButton = close_item_btn;

                ItemTemplateList_CellClick(ItemTemplateList, new DataGridViewCellEventArgs(0, 1));
            }
            else
                this.Size = new Size(731, 542);

            if (tabControl.SelectedTab == tabControl.TabPages["BillTab"])
            {
                BillTemplateList_CellClick(BillTemplateList, new DataGridViewCellEventArgs(0, 1));

                CancelButton = close_bill_btn;
            }

            if (tabControl.SelectedTab == tabControl.TabPages["OprTab"])
            {
                OpsTemplateList_CellClick(OpsTemplateList, new DataGridViewCellEventArgs(0, 1));

                CancelButton = close_op_btn;

                opmast_cbo.Click += opmast_cbo_Click;

                prodstd_cbo.Click += prodstd_cbo_Click;
            }

            if (tabControl.SelectedTab == tabControl.TabPages["ResTab"])
            {
                ResTemplateList_CellClick(ResTemplateList, new DataGridViewCellEventArgs(0, 1));

                CancelButton = close_res_btn;

                resource_cbo.Click += resource_cbo_Click;

                resourcegrp_cbo.Click += resourcegrp_cbo_Click;
            }
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

        private void UpdateLine(DataGridView DGV, string TemplateName, string Prefix)
        {
            int rowindex = DGV.CurrentCellAddress.Y;

            int row_id = int.Parse(DGV[0, rowindex].Value.ToString());

            if (DGV[Prefix + "PropertyOptions",rowindex].Value == null)
                TemplateAdapter.UpdateQueryNoOptions(TemplateName, DGV[Prefix + "PropertyType", rowindex].Value.ToString(), DGV[Prefix + "PropertyValue", rowindex].Value.ToString(), DGV[Prefix + "PropertyQty", rowindex].Value.ToString(), DGV[Prefix + "PropertyUOM", rowindex].Value.ToString(), row_id);
            else
                TemplateAdapter.UpdateQuery(TemplateName, DGV[Prefix + "PropertyType", rowindex].Value.ToString(), DGV[Prefix + "PropertyValue", rowindex].Value.ToString(), DGV[Prefix + "PropertyQty", rowindex].Value.ToString(), DGV[Prefix + "PropertyUOM", rowindex].Value.ToString(), DGV[Prefix + "PropertyOptions", rowindex].Value.ToString(), row_id);
        }

        #region Bill Functions

        void BillDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
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

        void BillTemplateList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                billtemplatename_txt.Text = BillTemplateList.CurrentCell.Value.ToString();

                //BillDataGrid.DataSource = Templates.GetFullTemplate(billtemplatename_txt.Text, "BOM");

                TemplateAdapter.FillByNameType(engDataDataSet.Templates, "BOM", billtemplatename_txt.Text);

                BillDataGrid.DataSource = engDataDataSet.Templates;

                BillDataGrid_CellClick(BillDataGrid, new DataGridViewCellEventArgs(0, 1));
            }
            catch { }
        }

        private void findpart_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

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

            RefreshTransaction();
        }

        private void AddBill_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

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
            RefreshTransaction();

            int rowindex = BillDataGrid.CurrentCellAddress.Y;

            TemplateAdapter.DeleteLine(int.Parse(BillDataGrid.CurrentRow.Cells["row_id"].Value.ToString()));

            BillDataGrid.Rows.RemoveAt(rowindex);
        }

        private void partnum_txt_TextChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

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
            SaveProcess();
        }

        private void qty_num_ValueChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            BillDataGrid.CurrentRow.Cells["PropertyQty"].Value = qty_num.Value.ToString();

            UpdateLine(BillDataGrid, billtemplatename_txt.Text, "");
        }

        private void bill_uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            BillDataGrid.CurrentRow.Cells["PropertyUOM"].Value = bill_uom_cbo.SelectedValue;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text,"");
        }

        private void desc_txt_TextChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            BillDataGrid.CurrentRow.Cells["PartDescription"].Value = desc_txt.Text;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text, "");
        }

        private void ViewAsAsm_chk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            BillDataGrid.CurrentRow.Cells["PropertyOptions"].Value = ViewAsAsm_chk.Checked.ToString();

            UpdateLine(BillDataGrid, billtemplatename_txt.Text, "");
        }

        private void operation_txt_TextChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            BillDataGrid.CurrentRow.Cells["PropertyType"].Value = operation_txt.Text;

            UpdateLine(BillDataGrid, billtemplatename_txt.Text, "");
        }

        private void add_bill_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            Template_Title TT = new Template_Title();

            DialogResult Dr = TT.ShowDialog();

            if (TT.DialogResult == DialogResult.OK)
            {
                string TemplateName = TT.RetVal;

                TT.Dispose();

                TemplateAdapter.InsertNewLine(TemplateName, "BOM");

                TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "BOM");
            }
        }

        private void del_bill_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            TemplateAdapter.DeleteTemplate(billtemplatename_txt.Text, "BOM");

            TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "BOM");
        }

        #endregion

        #region Item Functions

        void ItemTemplateList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearItem();

                itemtemplatename_txt.Text = ItemTemplateList.CurrentCell.Value.ToString();

                pdata = ParseItemTemplate(itemtemplatename_txt.Text);

                type_cbo.DisplayMember = "Description";

                type_cbo.ValueMember = "Code";

                type_cbo.Text = pdata.PMT;

                //uomclass_cbo.Text = pdata.UOM_Class;

                //NetWeight.Value = pdata.Net_Weight;

                uomweight_cbo.Text = pdata.Net_Weight_UM;

                /*
                NetVolume.Value = pdata.Net_Vol;

                uomvol_cbo.Text = pdata.Net_Vol_UM;
                */

                uom_cbo.Text = pdata.Primary_UOM;

                group_cbo.Text = pdata.PartGroup;

                class_cbo.Text = pdata.PartClass;

                plant_cbo.Text = pdata.PartPlant;

                planner_cbo.Text = pdata.Planner;

                whse_cbo.Items.Clear();

                for (int i = 0; i < pdata.PlantWhse.Count; i++)
                {
                    whse_cbo.Items.Add(new PartTypeCode(pdata.PlantWhse_Code[i], pdata.PlantWhse[i]));
                }

                whse_cbo.SelectedIndex = 0;
            }
            catch { }
        }

        private void ClearItem()
        {
            type_cbo.SelectedIndex = 0;

            uomweight_cbo.SelectedIndex = 0;

            uom_cbo.SelectedIndex = 0;

            group_cbo.SelectedIndex = 0;

            class_cbo.SelectedIndex = 0;

            planner_cbo.SelectedIndex = 0;

            whse_cbo.Items.Clear();
        }

        private void del_item_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.DeleteTemplate(billtemplatename_txt.Text, "ITEM");

            TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "ITEM");
        }

        private void trackserial_CheckedChanged(object sender, EventArgs e)
        {
            if (trackserial.Checked)
            {
                SerialMask_Master SM = new SerialMask_Master("");

                SM.ShowDialog();

                if (SM.DialogResult == DialogResult.Cancel)
                {
                    trackserial.Checked = false;

                    int rowid = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "TRACKSERIAL", "%").ToString());

                    TemplateAdapter.UpdatebyRowID(itemtemplatename_txt.Text, "ITEM", "TRACKSERIAL", "FALSE", "", "", "", rowid);
                }
                else
                {
                    int rowid = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "TRACKSERIAL", "%").ToString());

                    TemplateAdapter.UpdatebyRowID(itemtemplatename_txt.Text, "ITEM", "TRACKSERIAL", "TRUE", "", "", SM.Prefix, rowid);
                }
            }
        }

        public PartData ParseItemTemplate(string TemplateName)
        {
            PartData Pdata = new PartData();

            //ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable DT = new ENGDataDataSet.TemplatesDataTable();

            TemplateAdapter.FillByNameType(DT, "ITEM", TemplateName);

            foreach (DataRow Dr in DT.Rows)
            {
                if (Dr["PropertyType"].ToString() == "TYPE")
                    Pdata.PMT = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "UOM")
                    Pdata.Primary_UOM = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "GROUP")
                    Pdata.PartGroup = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "QTYBEARING")
                    Pdata.QtyBearing = bool.Parse(Dr["PropertyValue"].ToString());

                if (Dr["PropertyType"].ToString() == "CLASS")
                    Pdata.PartClass = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "USEREV")
                    Pdata.UseRevision = bool.Parse(Dr["PropertyValue"].ToString());

                if (Dr["PropertyType"].ToString() == "TRACKSERIAL")
                {
                    Pdata.TrackSerial = bool.Parse(Dr["PropertyValue"].ToString());

                    Pdata.TrackSerial_Mask = Dr["PropertyOptions"].ToString();
                }

                if (Dr["PropertyType"].ToString() == "PLANT")
                    Pdata.PartPlant = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "WAREHOUSE")
                {
                    Pdata.PlantWhse.Add(Dr["PropertyValue"].ToString());

                    Pdata.PlantWhse_Code.Add(Dr["PropertyQty"].ToString());
                }

                if (Dr["PropertyType"].ToString() == "PLANNER")
                    Pdata.Planner = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "WEIGHT_UOM")
                    Pdata.Net_Weight_UM = Dr["PropertyValue"].ToString();
            }

            return Pdata;
        }

        private void add_item_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            Template_Title TT = new Template_Title();

            DialogResult Dr = TT.ShowDialog();

            if (TT.DialogResult == DialogResult.OK)
            {
                string TemplateName = TT.RetVal;

                TT.Dispose();

                InsertNewItem(TemplateName);

                TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)ItemTemplateList.DataSource, "ITEM");
            }

        }

        private void save_item_btn_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void type_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "TYPE", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "TYPE", type_cbo.Text, null, null, null, i);

            type_cbo.SelectedIndexChanged -= type_cbo_SelectedIndexChanged;
        }

        void type_cbo_Click(object sender, EventArgs e)
        {
            type_cbo.SelectedIndexChanged += type_cbo_SelectedIndexChanged;
        }

        void uomweight_cbo_Click(object sender, EventArgs e)
        {
            uomweight_cbo.SelectedIndexChanged += uomweight_cbo_SelectedIndexChanged;
        }

        private void uomweight_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "WEIGHT_UOM", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "WEIGHT_UOM", uomweight_cbo.Text, null, null, null, i);

            uomweight_cbo.SelectedIndexChanged -= uomweight_cbo_SelectedIndexChanged;
        }

        private void uom_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "UOM", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "UOM", uom_cbo.Text, null, null, null, i);

            uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;
        }

        private void group_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "GROUP", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "GROUP", group_cbo.Text, null, null, null, i);

            group_cbo.SelectedIndexChanged -= group_cbo_SelectedIndexChanged;
        }

        private void class_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "CLASS", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "CLASS", class_cbo.Text, null, null, null, i);

            class_cbo.SelectedIndexChanged -= class_cbo_SelectedIndexChanged;
        }

        private void plant_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "PLANT", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "PLANT", plant_cbo.Text, null, null, null, i);

            plant_cbo.SelectedIndexChanged -= plant_cbo_SelectedIndexChanged;
        }

        private void planner_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            int i = int.Parse(TemplateAdapter.GetRowID(itemtemplatename_txt.Text, "ITEM", "PLANNER", "%").ToString());

            TemplateAdapter.UpdateQuery(itemtemplatename_txt.Text, "PLANNER", planner_cbo.Text, null, null, null, i);

            planner_cbo.SelectedIndexChanged -= planner_cbo_SelectedIndexChanged;
        }

        void planner_cbo_Click(object sender, EventArgs e)
        {
            planner_cbo.SelectedIndexChanged += planner_cbo_SelectedIndexChanged;
        }

        void plant_cbo_Click(object sender, EventArgs e)
        {
            plant_cbo.SelectedIndexChanged += plant_cbo_SelectedIndexChanged;
        }

        void class_cbo_Click(object sender, EventArgs e)
        {
            class_cbo.SelectedIndexChanged += class_cbo_SelectedIndexChanged;
        }

        void group_cbo_Click(object sender, EventArgs e)
        {
            group_cbo.SelectedIndexChanged += group_cbo_SelectedIndexChanged;
        }

        void uom_cbo_Click(object sender, EventArgs e)
        {
            uom_cbo.SelectedIndexChanged += uom_cbo_SelectedIndexChanged;
        }

        private void AddItemHandles()
        {
            type_cbo.Click += type_cbo_Click;

            type_cbo.SelectedIndexChanged -= type_cbo_SelectedIndexChanged;

            uomweight_cbo.Click += uomweight_cbo_Click;

            uomweight_cbo.SelectedIndexChanged -= uomweight_cbo_SelectedIndexChanged;

            uom_cbo.Click += uom_cbo_Click;

            uom_cbo.SelectedIndexChanged -= uom_cbo_SelectedIndexChanged;

            group_cbo.Click += group_cbo_Click;

            group_cbo.SelectedIndexChanged -= group_cbo_SelectedIndexChanged;

            class_cbo.Click += class_cbo_Click;

            class_cbo.SelectedIndexChanged -= class_cbo_SelectedIndexChanged;

            plant_cbo.Click += plant_cbo_Click;

            plant_cbo.SelectedIndexChanged -= plant_cbo_SelectedIndexChanged;

            planner_cbo.Click += planner_cbo_Click;

            planner_cbo.SelectedIndexChanged -= planner_cbo_SelectedIndexChanged;
        }

        private void InsertNewItem(string TemplateName)
        {
            RefreshTransaction();

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "TYPE", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "UOM", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "GROUP", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "QTYBEARING", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "CLASS", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "USEREV", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "TRACKSERIAL", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "PLANT", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "PLANNER", "", "", "", "");

            TemplateAdapter.InsertPropertyLine(TemplateName, "ITEM", "WEIGHT_UOM", "", "", "", "");
        }

        private void addwhse_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            Warehouse_Master wm = new Warehouse_Master(itemtemplatename_txt.Text, plant_cbo.Text);

            if (whse_cbo.Items.Count > 0)
            {
                wm.Whse_DS.Tables.Add();

                wm.Whse_DS.Tables[0].Columns.Add(new DataColumn("WarehouseCode", typeof(System.String)));

                wm.Whse_DS.Tables[0].Columns.Add(new DataColumn("WarehouseName", typeof(System.String)));

                for (int i = 0; i < whse_cbo.Items.Count; i++)
                {
                    DataRow dr = wm.Whse_DS.Tables[0].NewRow();

                    whse_cbo.SelectedIndex = i;

                    dr["WarehouseCode"] = whse_cbo.SelectedValue;

                    dr["WarehouseName"] = whse_cbo.Text;

                    wm.Whse_DS.Tables[0].Rows.Add(dr);
                }
            }
            wm.ShowDialog();

            //get added warehouse list from form
            whse_cbo.DataSource = wm.Whse_DS.Tables[0];

            whse_cbo.DisplayMember = "WarehouseName";

            whse_cbo.ValueMember = "WarehouseCode";

            wm.Dispose();

            DataList.EpicClose();

            if (whse_cbo.Items.Count > 0)
                addwhse_btn.Text = "&Edit";
            else
                addwhse_btn.Text = "&Add";

            #region Add to SQL

            TemplateAdapter.DeleteWarehousePerItem(itemtemplatename_txt.Text);

            for (int i = 0; i < whse_cbo.Items.Count; i++)
            {
                TemplateAdapter.InsertPropertyLine(itemtemplatename_txt.Text, "ITEM", "WAREHOUSE", whse_cbo.Items[i].ToString(), "", "", "");
            }

            #endregion
        }

        #endregion

        #region Operation Functions

        void OPDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            prodhrs_num.ValueChanged -= prodhrs_num_ValueChanged;

            seq_txt.TextChanged -= seq_txt_TextChanged;

            prodhrs_num.Value = decimal.Parse(OPDataGrid["OprPropertyQty", OPDataGrid.CurrentRow.Index].Value.ToString());

            seq_txt.Text = OPDataGrid["OprPropertyType",OPDataGrid.CurrentRow.Index].Value.ToString();

            opmast_cbo.SelectedValue = OPDataGrid["OprPropertyValue", OPDataGrid.CurrentRow.Index].Value.ToString();

            prodstd_cbo.SelectedValue = OPDataGrid["OprPropertyUOM", OPDataGrid.CurrentRow.Index].Value.ToString();

            prodhrs_num.ValueChanged += prodhrs_num_ValueChanged;

            seq_txt.TextChanged += seq_txt_TextChanged;
        }

        void seq_txt_TextChanged(object sender, EventArgs e)
        {
            bool valid = true;
            
            for (int i = 0; i < OPDataGrid.Rows.Count; i++)
            {
                if (i != OPDataGrid.CurrentRow.Index)
                {
                    if (OPDataGrid["OprPropertyType", i].Value.ToString() == seq_txt.Text)
                    {
                        valid = false;

                        break;
                    }
                }
            }

            if (valid)
                OPDataGrid["OprPropertyType", OPDataGrid.CurrentRow.Index].Value = seq_txt.Text;
            else
                MessageBox.Show("Sequences must be unique within a single operation template","Warning!",MessageBoxButtons.OK,MessageBoxIcon.Hand);
        }

        private void add_op_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            Template_Title TT = new Template_Title();

            DialogResult Dr = TT.ShowDialog();

            if (TT.DialogResult == DialogResult.OK)
            {
                string TemplateName = TT.RetVal;

                TT.Dispose();

                TemplateAdapter.InsertNewLine(TemplateName, "OOM");

                TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "OOM");
            }
        }

        private void del_op_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.DeleteTemplate(billtemplatename_txt.Text, "ITEM");

            TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "OOM");
        }

        private void saveop_btn_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void addop_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.InsertNewLine(oomtemplatename_txt.Text, "OOM");

            TemplateAdapter.FillByNameType(engDataDataSet.Templates, "OOM", oomtemplatename_txt.Text);

            OPDataGrid.DataSource = engDataDataSet.Templates;

            OPDataGrid.ClearSelection();

            OPDataGrid.CurrentCell = OPDataGrid.Rows[OPDataGrid.Rows.Count - 1].Cells[1];

            //Set default values to basics
            MessageBox.Show("HEY");
        }

        private void remop_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            int rowindex = OPDataGrid.CurrentCellAddress.Y;

            TemplateAdapter.DeleteLine(int.Parse(OPDataGrid.CurrentRow.Cells["row_id"].Value.ToString()));

            OPDataGrid.Rows.RemoveAt(rowindex);
        }

        void OpsTemplateList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OPDataGrid.DataSource = GetFullTemplate(OpsTemplateList.CurrentCell.Value.ToString(), "OOM");

            oomtemplatename_txt.Text = OpsTemplateList.CurrentCell.Value.ToString();

            OPDataGrid_CellClick(OPDataGrid, new DataGridViewCellEventArgs(0, 0));
        }

        private void opmast_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OPDataGrid["OprPropertyValue", OPDataGrid.CurrentRow.Index].Value = opmast_cbo.SelectedValue;

                OPDataGrid["OpDesc", OPDataGrid.CurrentRow.Index].Value = opmast_cbo.Text;
            }
            catch { }
        }

        private void prodstd_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OPDataGrid["OprPropertyUOM", OPDataGrid.CurrentRow.Index].Value = prodstd_cbo.SelectedValue;
            }
            catch { }
        }

        void prodstd_cbo_Click(object sender, EventArgs e)
        {
            UpdateLine(OPDataGrid, oomtemplatename_txt.Text, "Opr");
        }

        void opmast_cbo_Click(object sender, EventArgs e)
        {
            UpdateLine(OPDataGrid, oomtemplatename_txt.Text, "Opr");
        }


        private void prodhrs_num_ValueChanged(object sender, EventArgs e)
        {
            OPDataGrid["OprPropertyQty", OPDataGrid.CurrentRow.Index].Value = prodhrs_num.Value;
        }

        #endregion

        #region Resource Functions

        private void add_res_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            Template_Title TT = new Template_Title();

            DialogResult Dr = TT.ShowDialog();

            if (TT.DialogResult == DialogResult.OK)
            {
                string TemplateName = TT.RetVal;

                TT.Dispose();

                TemplateAdapter.InsertNewLine(TemplateName, "RES");

                TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "RES");
            }
        }

        private void del_res_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.DeleteTemplate(billtemplatename_txt.Text, "RES");

            TemplateAdapter.FillByType((ENGDataDataSet.TemplatesDataTable)BillTemplateList.DataSource, "RES");
        }

        private void save_res_btn_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void delres_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            int rowindex = ResDataGrid.CurrentCellAddress.Y;

            TemplateAdapter.DeleteLine(int.Parse(ResDataGrid.CurrentRow.Cells["row_id"].Value.ToString()));

            ResDataGrid.Rows.RemoveAt(rowindex);
        }

        private void addres_btn_Click(object sender, EventArgs e)
        {
            if (Transaction == null)
                Transaction = TableHelper.BeginTransaction(TemplateAdapter);

            TemplateAdapter.InsertNewLine(restemplatename_txt.Text, "RES");

            TemplateAdapter.FillByNameType(engDataDataSet.Templates, "RES", restemplatename_txt.Text);

            ResDataGrid.DataSource = engDataDataSet.Templates;

            ResDataGrid.ClearSelection();

            ResDataGrid.CurrentCell = ResDataGrid.Rows[ResDataGrid.Rows.Count - 1].Cells[1];

            //Set default values to basics
            MessageBox.Show("HEY");
        }

        private void FillResourceList()
        {
            DataSet ds = DataList.ResourceGroup();

            resourcegrp_cbo.DataSource = ds.Tables[0];

            resourcegrp_cbo.ValueMember = ds.Tables[0].Columns["ResourceGrpID"].ToString();

            resourcegrp_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            ds = DataList.Resource(resourcegrp_cbo.SelectedValue.ToString());

            DataRow dr = ds.Tables[0].NewRow();

            dr["Description"] = "";

            dr["ResourceID"] = "";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            resource_cbo.DataSource = ds.Tables[0];

            resource_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            resource_cbo.ValueMember = ds.Tables[0].Columns["ResourceID"].ToString();
        }

        void ResTemplateList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ResDataGrid.DataSource = GetFullTemplate(ResTemplateList.CurrentCell.Value.ToString(), "RES");

            restemplatename_txt.Text = ResTemplateList.CurrentCell.Value.ToString();

            ResDataGrid_CellClick(ResDataGrid,new DataGridViewCellEventArgs(0, 0));
        }

        void ResDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            opr_txt.Text = ResDataGrid["ResPropertyType", e.RowIndex].Value.ToString();

            resourcegrp_cbo.SelectedValue = ResDataGrid["ResPropertyValue", e.RowIndex].Value.ToString();

            resource_cbo.SelectedValue = ResDataGrid["ResPropertyUOM", e.RowIndex].Value.ToString();
        }

        private void resourcegrp_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            resource_cbo.SelectedIndexChanged -= resource_cbo_SelectedIndexChanged;

            #region Fill Datalist

            DataSet ds = DataList.Resource(resourcegrp_cbo.SelectedValue.ToString());

            DataRow dr = ds.Tables[0].NewRow();

            dr["Description"] = "";

            dr["ResourceID"] = "";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            resource_cbo.DataSource = ds.Tables[0];

            resource_cbo.DisplayMember = ds.Tables[0].Columns["Description"].ToString();

            resource_cbo.ValueMember = ds.Tables[0].Columns["ResourceID"].ToString();

            #endregion

            RefreshTransaction();

            try
            {
                ResDataGrid.CurrentRow.Cells["ResPropertyValue"].Value = resourcegrp_cbo.SelectedValue;
            }
            catch { }

            resource_cbo.SelectedIndexChanged += resource_cbo_SelectedIndexChanged;
        }

        private void resource_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTransaction();

            ResDataGrid.CurrentRow.Cells["ResPropertyUOM"].Value = resource_cbo.SelectedValue;
        }

        void resourcegrp_cbo_Click(object sender, EventArgs e)
        {
            UpdateLine(ResDataGrid, restemplatename_txt.Text,"Res");
        }

        void resource_cbo_Click(object sender, EventArgs e)
        {
            UpdateLine(ResDataGrid, restemplatename_txt.Text, "Res");
        }

        #endregion


    }
}
