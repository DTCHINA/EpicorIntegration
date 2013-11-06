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
    public partial class Warehouse_Master : Form
    {
        public DataSet Whse_DS = new DataSet();

        public Warehouse_Master()
        {
            try
            {
                InitializeComponent();
            }
            catch { }
        }

        public Warehouse_Master(string PartNumber, string Plant)
        {
            try
            {
                InitializeComponent();
            }
            catch { }

            partnum_txt.Text = PartNumber;

            plant_txt.Text = Plant;
        }

        private void Warehouse_Master_Load(object sender, EventArgs e)
        {
            whse_cbo.DataSource = DataList.WarehseDataSet().Tables[0];

            whse_cbo.DisplayMember = DataList.WarehseDataSet().Tables[0].Columns["Description"].ToString();

            whse_cbo.ValueMember = "WarehouseCode";

            if (Whse_DS.Tables.Count < 1)
            {
                Whse_DS.Tables.Add();

                Whse_DS.Tables[0].Columns.Add(new DataColumn("WarehouseCode", typeof(System.String)));

                Whse_DS.Tables[0].Columns.Add(new DataColumn("WarehouseName", typeof(System.String)));
            }
            else
            {
                rem_btn.Enabled = true;
            }

            WhseDataGrid.DataSource = Whse_DS.Tables[0];
        }

        private void done_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                bool toAdd = true;

                foreach (DataRow dr in Whse_DS.Tables[0].Rows)
                {
                    if (dr["WarehouseCode"].ToString() == whse_cbo.SelectedValue.ToString())
                        toAdd = false;
                }

                if (toAdd)
                {
                    DataRow dr = Whse_DS.Tables[0].NewRow();

                    dr["WarehouseCode"] = whse_cbo.SelectedValue.ToString();

                    dr["WarehouseName"] = whse_cbo.Text;

                    Whse_DS.Tables[0].Rows.Add(dr);

                    rem_btn.Enabled = true;
                }
                else
                    MessageBox.Show("Item already exists!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch { MessageBox.Show("An error occured adding this record to the database", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void rem_btn_Click(object sender, EventArgs e)
        {
            if (WhseDataGrid.CurrentCellAddress.Y != -1)
            {
                Whse_DS.Tables[0].Rows.RemoveAt(WhseDataGrid.CurrentCellAddress.Y);

                if (WhseDataGrid.Rows.Count < 1)
                    rem_btn.Enabled = false;
            }
            else
                MessageBox.Show("Selected a row first!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

    }
}
