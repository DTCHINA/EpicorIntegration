using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAddin
{
    public partial class ItemVendorAdd : Form
    {
        private string item { get; set; }

        public ItemVendorAdd(string inc_item)
        {
            InitializeComponent();

            item = inc_item;

            this.Text += item;
        }

        private void ItemVendorAdd_Load(object sender, EventArgs e)
        {
            this.u_mTableAdapter.Fill(this.dEKKER_AppDataSet.u_m);

            this.vendaddrTableAdapter.Fill(this.dEKKER_AppDataSet.vendaddr);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("Are you sure you do not want to enter a vendor for this item?","Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

            if (diag == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private void continuebtn_Click(object sender, EventArgs e)
        {
            //Add vendor to item

            System.Data.SqlClient.SqlTransaction transaction = null;

            try
            {

                //transaction = TableAdapterHelper.BeginTransaction(itemvendTableAdapter);

                //TableAdapterHelper.SetTransaction(itemvendpriceTableAdapter, transaction);

                Nullable<DateTime> Qdate = null;

                if (quotedchk.Checked)
                    Qdate = Quotedate.Value;

                Nullable<short> lead = null;

                if (quotedchk.Checked)
                    lead = (short)Convert.ToInt32(leadtxt.Text);

                Nullable<decimal> cost = null;

                if (quotedchk.Checked)
                    cost = Convert.ToDecimal(costtxt.Text);

                //itemvendTableAdapter.InsertVendor(item, vendorcbo.SelectedValue.ToString(), vendoritemtxt.Text, lead, Qdate);

                //itemvendpriceTableAdapter.InsertQuery(item, vendorcbo.SelectedValue.ToString(),cost);
                                
                transaction.Commit();

            }
            catch
            { 
                //transaction.Rollback();

                this.DialogResult = DialogResult.Cancel;

                this.Close();
            }
            finally
            {
                //transaction.Connection.Close();

                //transaction.Dispose();

                this.Close();

                this.Dispose();
            }

            this.Close();
        }

        private void quotedchk_CheckedChanged(object sender, EventArgs e)
        {
            leadtxt.Enabled = quotedchk.Checked;

            Quotedate.Enabled = quotedchk.Checked;

            costtxt.Enabled = quotedchk.Checked;
        }
    }
}
