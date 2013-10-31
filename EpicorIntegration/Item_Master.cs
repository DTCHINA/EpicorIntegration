using Epicor.Mfg.BO;
using System;
using System.Data;
using System.Windows.Forms;

namespace EpicorIntegration
{
    public partial class Item_Master : Form
    {
        public PartData PartDatum
        {
            get
            {
                PartData PartDatum = new PartData();

                return PartDatum;
            }
        }

        public string SerialPrefix;

        public Item_Master()
        {
            InitializeComponent();
        }

        public Item_Master(PartData Part)
        {
            InitializeComponent();
            try
            {
                //Use Part to fill data fields

                Partnumber_txt.Text = Part.PartNumber;

                Description_txt.Text = Part.Description;

                type_cbo.SelectedIndex = type_cbo.Items.IndexOf(Part.PMT);

                NetWeight.Value = Part.Net_Weight;

                userevision.Checked = Part.UseRevision;

                qtybearing.Checked = Part.QtyBearing;

                trackserial.Checked = Part.TrackSerial;

                //NetVolume.Value = Part.Net_Vol;

                group_cbo.SelectedIndex = group_cbo.Items.IndexOf(Part.PartGroup);

                class_cbo.SelectedIndex = class_cbo.Items.IndexOf(Part.PartClass);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data fields.  Default values will show.\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Item_Master(string PartNumber, string Description, decimal Weight)
        {
            InitializeComponent();

            try
            {
                //Fill fields on form with inputs

                Partnumber_txt.Text = PartNumber;

                Description_txt.Text = Description;

                NetWeight.Value = Weight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data fields.  Default values will show.\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public Item_Master(string PartNumber,string Description,string Type, decimal Weight, decimal Volume,string Group, string Class)
        {
            InitializeComponent();

            try
            {
                //Fill fields on form with inputs

                Partnumber_txt.Text = PartNumber;

                Description_txt.Text = Description;

                type_cbo.SelectedIndex = type_cbo.Items.IndexOf(Type);

                NetWeight.Value = Weight;

                //NetVolume.Value = Volume;

                group_cbo.SelectedIndex = group_cbo.Items.IndexOf(Group);

                class_cbo.SelectedIndex = class_cbo.Items.IndexOf(Class);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data fields.  Default values will show.\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Item_Master_Load(object sender, EventArgs e)
        {
            Partnumber_txt.Leave += Partnumber_txt_Leave;

            Description_txt.Leave += Description_txt_Leave;

            try
            {
                #region Fill DataLists

                type_cbo.Items.Add(new PartTypeCode("Manufactured", "M"));

                type_cbo.Items.Add(new PartTypeCode("Purchased", "P"));

                type_cbo.Items.Add(new PartTypeCode("Sales Kit", "K"));

                type_cbo.DisplayMember = "Description";

                group_cbo.DataSource = DataList.ProdGrupDataSet ().Tables[0];
                
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

                //whse_cbo.DataSource = DataList.WarehseDataSet().Tables[0];

                //whse_cbo.DisplayMember = DataList.WarehseDataSet().Tables[0].Columns["Description"].ToString();

                //whse_cbo.ValueMember = "WarehouseCode";

                DataSet DS = DataList.UOMSearchDataSet();

                DS.Tables[0].Columns.Add("FullCode", typeof(string), "UOMCode + ' - ' + UOMDesc");

                uom_cbo.DataSource = DS.Tables[0];

                uom_cbo.DisplayMember = DS.Tables[0].Columns["FullCode"].ToString();

                uom_cbo.ValueMember = "UOMCode";

                uomclass_cbo.SelectedIndex = 2;

                //type_cbo.SelectedIndex = 0;
                /*
                uomvol_cbo.DataSource = DataList.UOMVolumeDataSet().Tables[0];

                uomvol_cbo.DisplayMember = DataList.UOMVolumeDataSet().Tables[0].Columns["UOMCode"].ToString();

                uomvol_cbo.ValueMember = "UOMCode";
                 * */

                uomweight_cbo.DataSource = DataList.UOMWeightDataSet().Tables[0];

                uomweight_cbo.DisplayMember = DataList.UOMWeightDataSet().Tables[0].Columns["UOMCode"].ToString();

                uomweight_cbo.ValueMember = "UOMCode";

                planner_cbo.DataSource = DataList.PlannerList().Tables[0];

                planner_cbo.DisplayMember = "Name";

                planner_cbo.ValueMember = "PersonID";

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nThis application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
        }

        void Description_txt_Leave(object sender, EventArgs e)
        {
            AllToUpper();
        }

        void Partnumber_txt_Leave(object sender, EventArgs e)
        {
            AllToUpper();

            //201XXXXX is a frame, 404XXXXX is a Flo-machine - both need to be serialized
            if (Partnumber_txt.Text.Substring(0, 3) == "201" || Partnumber_txt.Text.Substring(0,3) == "404")
                trackserial.Checked = true;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AllToUpper()
        {
            Partnumber_txt.Text = Partnumber_txt.Text.ToUpper();

            Description_txt.Text = Description_txt.Text.ToUpper();
        }

        private void UpdateFormSet(string Pnum)
        {
            Part Part = new Part(DataList.EpicConn);

            PartDataSet Pdata = new PartDataSet();

            Pdata = Part.GetByID(Pnum);

            Description_txt.Text = Pdata.Tables["Part"].Rows[0]["PartDescription"].ToString();

            NetWeight.Value = decimal.Parse(Pdata.Tables["Part"].Rows[0]["NetWeight"].ToString());

            uomweight_cbo.SelectedValue = Pdata.Tables["Part"].Rows[0]["NetWeightUOM"].ToString();

            uom_cbo.SelectedValue = Pdata.Tables["Part"].Rows[0]["IUM"].ToString();

            class_cbo.SelectedValue = Pdata.Tables["Part"].Rows[0]["ClassID"].ToString();

            type_cbo.SelectedValue = Pdata.Tables["Part"].Rows[0]["TypeCode"].ToString();

            group_cbo.SelectedValue = Pdata.Tables["Part"].Rows[0]["ProdCode"].ToString();
        }

        private PartDataSet UpdateDataSet(PartDataSet Pdata)
        {
            Part Part = new Part(DataList.EpicConn);

            DataList.AddDatum(Pdata, "Part", 0, "PartDescription", Description_txt.Text);

            //SearchWord has 8 character limit
            if (Description_txt.Text.Length > 8)
                DataList.AddDatum(Pdata, "Part", 0, "SearchWord", Description_txt.Text.Substring(0, 8));
            else
                DataList.AddDatum(Pdata, "Part", 0, "SearchWord", Description_txt.Text);

            DataList.AddDatum(Pdata, "Part", 0, "NetWeight", NetWeight.Text);

            DataList.AddDatum(Pdata, "Part", 0, "NetWeightUOM", uomweight_cbo.SelectedValue.ToString());

            //DataList.AddDatum(Pdata, "Part", 0, "NetVolume", NetVolume.Text);

            //DataList.AddDatum(Pdata, "Part", 0, "NetVolumeUOM", uomvol_cbo.SelectedValue.ToString());

            DataList.AddDatum(Pdata, "Part", 0, "IUM", uom_cbo.SelectedValue.ToString());

            DataList.AddDatum(Pdata, "Part", 0, "ClassID", class_cbo.SelectedValue.ToString());

            string Type_Code = type_cbo.SelectedItem.ToString();

            Part.ChangePartTypeCode(Type_Code, Pdata);

            Part.ChangePartProdCode(group_cbo.SelectedValue.ToString(), Pdata);

            //add trackserial number if necessary
            if (trackserial.Checked && (SerialPrefix != "" || SerialPrefix != null))
            {
                Part.ChangePartSNBaseDataType("MASK", Pdata);

                Part.ChangeSNMask("ELK", Pdata);

                Part.ChangePartSNMaskPrefixSuffix(SerialPrefix, "", Pdata);
            }
            else
            {
                if (SerialPrefix == "" || SerialPrefix == null)
                    MessageBox.Show("Cannot use null serial prefix.  Set the prefix or uncheck 'Track Serial Number'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Pdata;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (trackserial.Checked && (SerialPrefix == "" || SerialPrefix == null))
                MessageBox.Show("Cannot use null serial prefix.  Set the prefix or uncheck 'Track Serial Number'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    //Commit Part Changes

                    Part Part = new Part(DataList.EpicConn);

                    PartDataSet Pdata = new PartDataSet();

                    //Pdata = (PartDataSet)DL.PartSearchDataSet("");

                    string serialWarning;

                    string questionString;

                    bool multipleMatch;

                    string PartNumber = Partnumber_txt.Text;

                    Part.GetPartXRefInfo(ref PartNumber, "", "", out serialWarning, out questionString, out multipleMatch);

                    Part.GetNewPart(Pdata);

                    Part.ChangePartNum(PartNumber, Pdata);

                    Pdata = UpdateDataSet(Pdata);

                    //Add data to allow BO to create plant tables
                    Part.Update(Pdata);

                    //retrieve the new copy of the data
                    Pdata = Part.GetByID(PartNumber);

                    if (whse_cbo.Items.Count > 0)
                    {
                        for (int i = 0; i < whse_cbo.Items.Count; i++)
                        {

                            whse_cbo.SelectedIndex = i;

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "PrimWhse", whse_cbo.SelectedValue.ToString());

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "PrimWhseDescription", whse_cbo.Text);

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "DBRowIdent", null);
                        }

                        //Update with warehouse information
                        Part.Update(Pdata);
                    }

                    DataList.EpicClose();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void LoadData(PartData pdata)
        {
            if (pdata.Description != "")
                Description_txt.Text = pdata.Description;

            if (pdata.PMT != "")
                type_cbo.SelectedText = pdata.PMT;

            if (pdata.UOM_Class != "")
                uomclass_cbo.SelectedText = pdata.UOM_Class;

            if (pdata.Net_Weight != 0)
                NetWeight.Value = pdata.Net_Weight;

            if (pdata.Net_Weight_UM != "")
                uomweight_cbo.SelectedText = pdata.Net_Weight_UM;

                trackserial.Checked = pdata.TrackSerial;

                qtybearing.Checked = pdata.QtyBearing;

                userevision.Checked = pdata.UseRevision;
            
            /*if (pdata.Net_Vol != 0)
                NetVolume.Value = pdata.Net_Vol;

            if (pdata.Net_Vol_UM != "")
                uomvol_cbo.SelectedText = pdata.Net_Vol_UM;*/

            if (pdata.Primary_UOM != "")
                uom_cbo.SelectedText = pdata.Primary_UOM;

            if (pdata.PartGroup != "")
                group_cbo.SelectedText = pdata.PartGroup;

            if (pdata.PartClass != "")
                class_cbo.SelectedText = pdata.PartClass;

            if (pdata.PartPlant != "")
                plant_cbo.SelectedText = pdata.PartPlant;

            if (pdata.PlantWhse != "")
                whse_cbo.SelectedText = pdata.PlantWhse;
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {
            Item_CopyFrom frm = new Item_CopyFrom();

            PartData pdata = new PartData();

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Ignore)
            {
                MessageBox.Show("Item was duplicated successfully.", "Item Duplicated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                pdata = frm._Pdata;

                LoadData(pdata);
            }
        }

        private void type_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (type_cbo.Text == "Manufactured")
                {
                    qtybearing.Checked = true;
                    userevision.Checked = true;
                }
                else
                {
                    qtybearing.Checked = false;
                    userevision.Checked = false;
                }
            }
            catch { }
        }

        private void addwhse_btn_Click(object sender, EventArgs e)
        {
            //instance with partdataset
            Warehouse_Master wm = new Warehouse_Master();//Partnumber_txt.Text,plant_cbo.Text);

            wm.ShowDialog();

            //get added warehouse list from form
            whse_cbo.DataSource = wm.Whse_DS.Tables[0];

            whse_cbo.DisplayMember = "WarehouseName";

            whse_cbo.ValueMember = "WarehouseCode";

            wm.Dispose();

            DataList.EpicClose();

        }

        private void trackserial_CheckedChanged(object sender, EventArgs e)
        {
            if (trackserial.Checked)
            {
                string prefix = "";

                if (Partnumber_txt.Text.Substring(0, 3) == "201")
                    prefix = "BAL";
                if (Partnumber_txt.Text.Substring(0, 3) == "404")
                    prefix = "FLO";

                SerialMask_Master SM = new SerialMask_Master(prefix);

                SM.ShowDialog();

                if (SM.DialogResult == DialogResult.Cancel)
                    trackserial.Checked = false;
                else
                    SerialPrefix = SM.Prefix;
            }
        }

    }
}
