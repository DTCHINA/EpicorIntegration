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

        public Operations_SupplierSearch()
        {
            InitializeComponent();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            VendorPPSearch VendPP = new VendorPPSearch(DataList.EpicConn);

            string whereClause = Construct_whereClause();

            int pageSize = 100;

            int absolutePage = 0;

            bool morePages;

            VendorPPListDataSet VPPDS = VendPP.GetList(whereClause, pageSize, absolutePage, out morePages);

            SupplyGrid.DataSource = VPPDS.Tables[0];
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            SupplyGrid.ClearSelection();

            SupplyGrid.DataSource = null;
        }

        private void select_btn_Click(object sender, EventArgs e)
        {
            ReturnID = SupplyGrid["VendorNum", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnAddress = SupplyGrid["Address1", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

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
        }

        void SupplyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnID = SupplyGrid["VendorNum", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            ReturnAddress = SupplyGrid["Address1", SupplyGrid.CurrentCellAddress.Y].Value.ToString();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private string Construct_whereClause()
        {
            string retval = null;

            if (idnum_txt.Text !=null && idnum_txt.Text != "")
                retval += "VendorID = '" + idnum_txt.Text + "'";

            if (name_txt.Text != null && name_txt.Text != "")
                retval += " Name >= '" + name_txt.Text + "'";

            retval += " BY NAME";

            return retval;
        }
    }
}
