using Epicor.Mfg.BO;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace Epicor_Integration
{
    public partial class Item_Master : Form
    {
        public string SerialPrefix;

        public string SerialMask;

        public string _PartNumber;

        public string _Description;

        public decimal _Weight;

        public string _Group;

        public string _Class;

        public string _Type;

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

                phantom_chk.Checked = Part.Phantom;

                trackserial.CheckedChanged -= trackserial_CheckedChanged;

                trackserial.Checked = Part.TrackSerial;

                trackserial.CheckedChanged += trackserial_CheckedChanged;

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

                _PartNumber = PartNumber;

                _Description = Description;

                _Weight = Weight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data fields.  Default values will show.\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public Item_Master(string PartNumber, string Description, decimal Weight, string Group, string Class, string Type)
        {
            InitializeComponent();

            try
            {
                //Fill fields on form with inputs

                Partnumber_txt.Text = PartNumber;

                Description_txt.Text = Description;

                NetWeight.Value = Weight;

                _PartNumber = PartNumber;

                _Description = Description;

                _Weight = Weight;

                _Group = Group;

                _Class = Class;

                _Type = Type;

                //NetVolume.Value = Volume;
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

                type_cbo.SelectedIndex = 0;

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

            if (DataList.PartExists(Partnumber_txt.Text))
            {
                UpdateFormSet(Partnumber_txt.Text);

                uom_cbo.Enabled = false;

                if (Description_txt.Text != _Description || NetWeight.Value != _Weight || group_cbo.Text != _Group || class_cbo.Text != _Class || type_cbo.Text != _Type)
                {
                    DialogResult DR = MessageBox.Show("Override Epicor values with values from the model?\n\n" + _Description + " → " + Description_txt.Text + "\n" + _Weight.ToString() + " → " + NetWeight.Value.ToString() + "\n" + _Group + " → " + group_cbo.Text + "\n" + _Class + " → " + class_cbo.Text + "\n" + _Type + " → " + type_cbo.Text, "Property Override", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (DR == DialogResult.Yes)
                    {
                        Description_txt.Text = _Description;

                        NetWeight.Value = _Weight;

                        group_cbo.Text = _Group;

                        class_cbo.Text = _Class;
                    }
                }
            }

            try
            {
                //group_cbo.SelectedIndex = group_cbo.Items.IndexOf(Group);

                group_cbo.Text = _Group;
            }
            catch { }
            try
            {
                class_cbo.Text = _Class;
            }
            catch { }
            try
            {
                type_cbo.Text = _Type;
            }
            catch { }
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
            trackserial.CheckedChanged -= trackserial_CheckedChanged;

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

            trackserial.Checked = bool.Parse(Pdata.Tables[0].Rows[0]["TrackSerialNum"].ToString());

            qtybearing.Checked = bool.Parse(Pdata.Tables[0].Rows[0]["QtyBearing"].ToString());

            phantom_chk.Checked = bool.Parse(Pdata.Tables[0].Rows[0]["PhantomBOM"].ToString());

            userevision.Checked = bool.Parse(Pdata.Tables[0].Rows[0]["UsePartRev"].ToString());

            whse_cbo.DataSource = Pdata.Tables["PartWhse"];

            whse_cbo.DisplayMember = "WarehouseDescription";

            whse_cbo.ValueMember = "WarehouseCode";

            for (int i = 0; i < Pdata.Tables["PartPlant"].Rows.Count; i++)
            {
                if (Pdata.Tables["PartPlant"].Rows[i]["Plant"].ToString() == "MfgSys")
                    planner_cbo.Text = Pdata.Tables["PartPlant"].Rows[i]["PersonID"].ToString();
            }

            SerialPrefix = Pdata.Tables[0].Rows[0]["SNMaskPrefix"].ToString();

            trackserial.CheckedChanged += trackserial_CheckedChanged;
        }

        private PartDataSet UpdateDataSet(PartDataSet Pdata, DataViewRowState RowState)
        {
            Part Part = new Part(DataList.EpicConn);

            DataList.AddDatum(Pdata, "Part", 0, "PartDescription", Description_txt.Text,RowState);

            if (RowState == DataViewRowState.Unchanged)
                RowState = DataViewRowState.ModifiedCurrent;

            //SearchWord has 8 character limit
            if (Description_txt.Text.Length > 8)
                DataList.AddDatum(Pdata, "Part", 0, "SearchWord", Description_txt.Text.Substring(0, 8), RowState);
            else
                DataList.AddDatum(Pdata, "Part", 0, "SearchWord", Description_txt.Text, RowState);

            DataList.AddDatum(Pdata, "Part", 0, "NetWeight", NetWeight.Text, RowState);

            DataList.AddDatum(Pdata, "Part", 0, "NetWeightUOM", uomweight_cbo.SelectedValue.ToString(), RowState);

            DataList.AddDatum(Pdata, "Part", 0, "IUM", uom_cbo.SelectedValue.ToString(), RowState);

            DataList.AddDatum(Pdata, "Part", 0, "ClassID", class_cbo.SelectedValue.ToString(), RowState);

            DataList.AddDatum(Pdata, "Part", 0, "QtyBearing", qtybearing.Checked.ToString(), RowState);

            DataList.AddDatum(Pdata, "Part", 0, "UsePartRev", userevision.Checked.ToString(), RowState);

            DataList.AddDatum(Pdata, "Part", 0, "PhantomBOM", phantom_chk.Checked.ToString(), RowState);

            string Type_Code = type_cbo.SelectedItem.ToString();

            Part.ChangePartTypeCode(Type_Code, Pdata);

            Part.ChangePartProdCode(group_cbo.SelectedValue.ToString(), Pdata);

            //add trackserial number if necessary
            if (trackserial.Checked && (SerialPrefix != "" || SerialPrefix != null))
            {
                if (SerialPrefix == "" || SerialPrefix == null)
                    MessageBox.Show("Cannot use null serial prefix.  Set the prefix or uncheck 'Track Serial Number'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataList.AddDatum(Pdata, "Part", 0, "EnableSerialNum", trackserial.Checked.ToString(), RowState);

                    DataList.AddDatum(Pdata, "Part", 0, "TrackSerialNum", trackserial.Checked.ToString(), RowState);

                    Part.ChangePartSNBaseDataType("MASK", Pdata);

                    Part.ChangeSNMask(SerialMask, Pdata);

                    Part.ChangePartSNMaskPrefixSuffix(SerialPrefix, "", Pdata);
                }
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

                    DataViewRowState DRState;

                    if (!DataList.PartExists(PartNumber))
                    {
                        Part.GetPartXRefInfo(ref PartNumber, "", "", out serialWarning, out questionString, out multipleMatch);

                        Part.GetNewPart(Pdata);

                        Part.ChangePartNum(PartNumber, Pdata);

                        DRState = DataViewRowState.Added;
                    }
                    else
                    {
                        Pdata = Part.GetByID(PartNumber);

                        DRState = DataViewRowState.Unchanged;
                    }

                    Pdata = UpdateDataSet(Pdata, DRState);

                    if (trackserial.Checked)
                        Part.ChangePartTrackSerialNum(trackserial.Checked, Pdata);

                    //Add data to allow BO to create plant tables
                    Part.Update(Pdata);

                    //retrieve the new copy of the data
                    Pdata = Part.GetByID(PartNumber);

                    if (whse_cbo.Items.Count > 0)
                    {
                        Part.Update(Pdata);

                        for (int i = 0; i < whse_cbo.Items.Count; i++)
                        {
                            whse_cbo.SelectedIndex = i;                        

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "PrimWhse", whse_cbo.SelectedValue.ToString());

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "PrimWhseDescription", whse_cbo.Text);

                            DataList.UpdateDatum(Pdata, "PartPlant", 0, "DBRowIdent", null);

                            if (trackserial.Checked)
                                DataList.UpdateDatum(Pdata, "PartPlant", 0, "PartTrackSerialNum", trackserial.Checked.ToString());
                        }

                        if (trackserial.Checked)
                        {
                            Part.ChangePartSNBaseDataType("MASK", Pdata);

                            Part.ChangeSNMask(SerialMask, Pdata);
                        }

                        Part.Update(Pdata);

                        DataList.UpdateDatum(Pdata, "PartPlant", 0, "PersonID", planner_cbo.Text);

                        DataList.UpdateDatum(Pdata, "PartPlant", 0, "PersonName", planner_cbo.Text);

                        //Update with warehouse information
                        Part.Update(Pdata);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                finally
                {
                    DataList.EpicClose();

                    this.Close();
                }
            }
        }

        private void LoadData(PartDataSet pdata)
        {
            if (pdata.Tables[0].Rows[0]["Description"].ToString() != "")
                Description_txt.Text = pdata.Tables[0].Rows[0]["Description"].ToString();

            if (pdata.Tables[0].Rows[0]["PMT"].ToString() != "")
                type_cbo.SelectedText = pdata.Tables[0].Rows[0]["PMT"].ToString();

            if (pdata.Tables[0].Rows[0]["IUM"].ToString() != "")
                uomclass_cbo.SelectedText = pdata.Tables[0].Rows[0]["IUM"].ToString();

            if (decimal.Parse(pdata.Tables[0].Rows[0]["NetWeight"].ToString()) != 0)
                NetWeight.Value = decimal.Parse(pdata.Tables[0].Rows[0]["NetWeight"].ToString());

            if (pdata.Tables[0].Rows[0]["NetWeightUOM"].ToString() != "")
                uomweight_cbo.SelectedText = pdata.Tables[0].Rows[0]["NetWeightUOM"].ToString();

            trackserial.Checked = bool.Parse(pdata.Tables[0].Rows[0]["TrackSerialNum"].ToString());

            qtybearing.Checked = bool.Parse(pdata.Tables[0].Rows[0]["QtyBearing"].ToString());

            phantom_chk.Checked = bool.Parse(pdata.Tables[0].Rows[0]["PhantomBOM"].ToString());

            userevision.Checked = bool.Parse(pdata.Tables[0].Rows[0]["UsePartRev"].ToString());

            //Cannot change UOM after initial save, will cause all sorts of problems
            uom_cbo.Enabled = false;
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {
            TemplatesMenu.Items.Clear();

            DataTable DT = Templates.GetItemTemplates();

            foreach (DataRow Dr in DT.Rows)
            {
                ToolStripMenuItem TS = new ToolStripMenuItem();

                TS.Name = Dr["Name"].ToString();

                TS.Text = Dr["Name"].ToString();

                TS.Click += TS_Click;

                TemplatesMenu.Items.Add(TS);
            }

            TemplatesMenu.Show(copy_btn, new Point(0, copy_btn.Height));
        }

        void TS_Click(object sender, EventArgs e)
        {
            trackserial.CheckedChanged -= trackserial_CheckedChanged;

            ToolStripMenuItem TS = (ToolStripMenuItem)sender;

            PartData Pdata = Templates.ParseItemTemplate(TS.Name);

            type_cbo.SelectedValue = Pdata.PMT;

            //uom_cbo.SelectedValue = Pdata.UOM_Class;

            qtybearing.Checked = Pdata.QtyBearing;

            phantom_chk.Checked = Pdata.Phantom;

            userevision.Checked = Pdata.UseRevision;

            group_cbo.Text = Pdata.PartGroup;

            class_cbo.Text = Pdata.PartClass;

            plant_cbo.Text = Pdata.PartPlant;

            planner_cbo.Text = Pdata.Planner;

            DataTable DT = new DataTable();

            if (whse_cbo.DataSource != null)
            {
                DT = (DataTable)whse_cbo.DataSource;
            }
            else
            {
                DT.Columns.Add(new DataColumn("WarehouseCode", typeof(System.String)));

                DT.Columns.Add(new DataColumn("WarehouseDescription", typeof(System.String)));

                DT.Columns.Add(new DataColumn("Company", typeof(System.String)));

                DT.Columns.Add(new DataColumn("Plant", typeof(System.String)));

                DT.Columns.Add(new DataColumn("PartNum", typeof(System.String)));
            }

            for (int i = 0; i < Pdata.PlantWhse.Count ; i++)
            {
                bool toAdd = true;

                string Wh = Pdata.PlantWhse[i];

                string WhC = Pdata.PlantWhse_Code[i];

                foreach (DataRow Dr in DT.Rows)
                {
                    if (Dr["WarehouseDescription"].ToString() == Wh)
                    {
                        toAdd = false;

                        break;
                    }
                }

                if (toAdd)
                {
                    DataRow DR = DT.NewRow();

                    DR["WarehouseCode"] = WhC;

                    DR["WarehouseDescription"] = Wh;

                    DR["Company"] = "NORCO";

                    DR["PartNum"] = Partnumber_txt.Text;

                    DR["Plant"] = Pdata.PartPlant;

                    DT.Rows.Add(DR);
                }

                whse_cbo.DataSource = DT;

                whse_cbo.DisplayMember = "WarehouseDescription";

                whse_cbo.ValueMember = "WarehouseCode";
            }

            if (Pdata.TrackSerial)
            {
                trackserial.Checked = Pdata.TrackSerial;

                SerialPrefix = Pdata.TrackSerial_Mask;

                //SerialMask_Master SM = new SerialMask_Master(Pdata.TrackSerial_Mask);

                //SM.ShowDialog();
            }

            trackserial.CheckedChanged += trackserial_CheckedChanged;
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
            Warehouse_Master wm = new Warehouse_Master(Partnumber_txt.Text, plant_cbo.Text);

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
                {
                    SerialPrefix = SM.Prefix;

                    SerialMask = SM.Mask;
                }
            }
        }

        private void factor_btn_Click(object sender, EventArgs e)
        {
            Item_SheetFactor Sheet = new Item_SheetFactor(NetWeight.Value);

            Sheet.ShowDialog();

            NetWeight.Value = Sheet.FactoredWeight;
        }
    }
}
