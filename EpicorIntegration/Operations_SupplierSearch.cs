using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using Epicor.Mfg.IF;

namespace Epicor_Integration
{
    public partial class Operations_SupplierSearch : Form
    {
        public string ReturnID { get; set; }

        public string ReturnAddress { get; set; }

        public string ReturnCity { get; set; }

        public string ReturnZip { get; set; }

        public string ReturnState { get; set; }

        public string ReturnDdsBillAddr { get; set; }

        public string ReturnName { get; set; }

        public string ReturnCountry { get; set; }

        public string ReturnVendID { get; set; }

        public string VendorID { get; set; }

        public Operations_SupplierSearch()
        {
            InitializeComponent();
        }

        public Operations_SupplierSearch(string VendID)
        {
            InitializeComponent();

            bool morePages;

            int absolutePage = 0;

            idnum_txt.Text = VendID;

            int lines = Properties.Settings.Default.linelimit;

            string whereClause = Construct_whereClause();

            VendorPPSearch VendPP = new VendorPPSearch(DataList.EpicConn);
            try
            {
                VendorPPListDataSet VPPDS = VendPP.GetList(whereClause, lines, absolutePage, out morePages);

                SupplyGrid.DataSource = VPPDS.Tables[0];
            }
            catch { }

            ReturnID = SupplyGrid["VendorNumVendorID", 0].Value.ToString();

            ReturnAddress = SupplyGrid["Address1", 0].Value.ToString() + SupplyGrid["Address2", 0].Value.ToString() + SupplyGrid["Address3", 0].Value.ToString();

            ReturnCity = SupplyGrid["City", 0].Value.ToString();

            ReturnState = SupplyGrid["State", 0].Value.ToString();

            ReturnZip = SupplyGrid["ZIP", 0].Value.ToString();

            ReturnName = SupplyGrid["VName", 0].Value.ToString();

            ReturnCountry = SupplyGrid["Country", 0].Value.ToString();

            ReturnVendID = SupplyGrid["VendorNum", 0].Value.ToString();

            ReturnDdsBillAddr = ReturnName + " " + ReturnAddress + " " + ReturnCity + " " + ReturnState + " " + ReturnZip + " " + ReturnCountry;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            VendorPPSearch VendPP = new VendorPPSearch(DataList.EpicConn);

            string whereClause = Construct_whereClause();

            int absolutePage = 0;

            bool morePages;

            VendorPPListDataSet VPPDS = VendPP.GetList(whereClause, Properties.Settings.Default.linelimit, absolutePage, out morePages);

            SupplyGrid.DataSource = VPPDS.Tables[0];
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            SupplyGrid.ClearSelection();

            SupplyGrid.DataSource = null;
        }

        private void select_btn_Click(object sender, EventArgs e)
        {
            ReturnID = SupplyGrid["VendorNumVendorID", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnAddress = SupplyGrid["Address1", SupplyGrid.CurrentCellAddress.Y].Value.ToString() + SupplyGrid["Address2", SupplyGrid.CurrentCellAddress.Y].Value.ToString() + SupplyGrid["Address3", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnCity = SupplyGrid["City", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnState = SupplyGrid["State", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnZip = SupplyGrid["ZIP", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnName = SupplyGrid["VName", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnCountry = SupplyGrid["Country", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnVendID = SupplyGrid["VendorNum", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnDdsBillAddr = ReturnName + " " + ReturnAddress + " " + ReturnCity + " " + ReturnState + " " + ReturnZip + " " + ReturnCountry;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void Operations_SupplierSearch_Load(object sender, EventArgs e)
        {
            SupplyGrid.CellDoubleClick += SupplyGrid_CellDoubleClick;

            this.SizeChanged += Operations_SupplierSearch_SizeChanged;
        }

        void Operations_SupplierSearch_SizeChanged(object sender, EventArgs e)
        {
            select_btn.Location = new Point(this.Width - 419, select_btn.Location.Y);

            cancel_btn.Location = new Point(this.Width - 419, cancel_btn.Location.Y);
        }

        void SupplyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnID = SupplyGrid["VendorNumVendorID", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnAddress = SupplyGrid["Address1", SupplyGrid.CurrentCellAddress.Y].Value.ToString() + SupplyGrid["Address2", SupplyGrid.CurrentCellAddress.Y].Value.ToString() + SupplyGrid["Address3", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnCity = SupplyGrid["City", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnState = SupplyGrid["State", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnZip = SupplyGrid["ZIP", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnName = SupplyGrid["VName", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnCountry = SupplyGrid["Country", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnVendID = SupplyGrid ["VendorNum",SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnDdsBillAddr = ReturnName + " " + ReturnAddress + " " + ReturnCity + " " + ReturnState + " " + ReturnZip + " " + ReturnCountry;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private string Construct_whereClause()
        {
            string retval = null;

            try
            {
                Vendor Vend = new Vendor(DataList.EpicConn);

                bool morePages;

                VendorListDataSet VendList = Vend.GetList("VendorID = '" + idnum_txt.Text + "'", 100, 0, out morePages);

                VendorID = VendList.Tables[0].Rows[0]["VendorNum"].ToString();

                if (idnum_txt.Text != null && idnum_txt.Text != "")
                    retval += "VendorNum = '" + VendorID + "'";

                if (name_txt.Text != null && name_txt.Text != "")
                    retval += " Name >= '" + name_txt.Text + "'";

                retval += " BY NAME";
            }
            catch { retval = " BY NAME"; }
            return retval;
        }

        private void idnum_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
