using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpicorIntegration
{
    public partial class Template_Master : Form
    {
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
        }

        void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemTemplateList.DataSource = Templates.GetItemTemplates();

            OpsTemplateList.DataSource = Templates.GetOomTemplates();

            BillTemplateList.DataSource = Templates.GetBomTemplates();

            if (tabControl.SelectedTab == tabControl.TabPages["ItemTab"])
            {
                this.Size = new Size(511, 580);

                this.CancelButton = close_item_btn;

                FillItemLists();
            }
            else
            {
                this.Size = new Size(731, 542);

                CancelButton = close_op_btn;
            }
        }

        public PartData pdata = new PartData();

        private void SaveVals()
        {
            DataRow dr = engDataDataSet.Tables[0].Rows[ItemTemplateList.CurrentCellAddress.Y];

                pdata.Description = dr["Desc"].ToString();

                pdata.PMT = dr["Type"].ToString();

                pdata.UOM_Class = dr["UOMClass"].ToString();

                decimal fail = 0;

                decimal.TryParse(dr["NetWeight"].ToString(), out fail);

                pdata.Net_Weight = fail;

                pdata.Net_Weight_UM = dr["UOMWeight"].ToString();

                decimal.TryParse(dr["NetVol"].ToString(),out fail);

                pdata.Net_Vol = fail;

                pdata.Net_Vol_UM = dr["UOMVol"].ToString();

                pdata.Primary_UOM = dr["UOM"].ToString();

                pdata.PartGroup = dr["Group"].ToString();

                pdata.PartClass = dr["Class"].ToString();

                pdata.PartPlant = dr["Plant"].ToString();

                //pdata.PlantWhse = dr["Whse"].ToString();
        }

        void ItemTemplateList_CurrentCellChanged(object sender, EventArgs e)
        {
            //SaveVals();

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

        private void ViewAsAsm_chk_CheckedChanged(object sender, EventArgs e)
        {

        }

        /*void TS_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem TS = (ToolStripMenuItem)sender;

            //retrieve template data
            DataTable DT = new DataTable();

            DT = Templates.GetFullTemplate(TS.Name, "BOM");

            try
            {
                //line for each row
                foreach (DataRow Dr in DT.Rows)
                {
                    int row = EngWBDS.Tables["ECOMtl"].Rows.Count - 1;

                    EngWBDS.Tables["ECOMtl"].Rows[row]["MtlPartNum"] = Dr["PropertyValue"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["RelatedOperation"] = Dr["PropertyType"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["MtlPartNumPartDescription"] = DataList.GetCurrentDesc(Dr["PropertyValue"].ToString());

                    EngWBDS.Tables["ECOMtl"].Rows[row]["UOMCode"] = Dr["PropertyUOM"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["QtyPer"] = Dr["PropertyQty"].ToString();

                    EngWBDS.Tables["ECOMtl"].Rows[row]["ViewAsAsm"] = Dr["PropertyOptions"].ToString();

                    EngWB.Update(EngWBDS);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }*/

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

        private void PartTimer_Tick(object sender, EventArgs e)
        {

        }

        private void type_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
