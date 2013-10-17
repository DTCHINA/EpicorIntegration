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
    public partial class FetchBill : Form
    {
        private string Search_Val;

        public List<ExistingBillItem> BillList = new List<ExistingBillItem>();

        public FetchBill(string ToFetch)
        {
            InitializeComponent();

            Search_Val = ToFetch;
        }

        private void FetchBill_Load(object sender, EventArgs e)
        {
            try
            {
                string job;

                job = jobTableAdapter.JobQuery(Search_Val, "CJ%");

                jobmatlTableAdapter.FillBy(this.dEKKER_AppDataSet.jobmatl, job);

                for (int i = 0; i < RefGrid.Rows.Count; i++)
                {
                    ExistingBillItem New = new ExistingBillItem();

                    New.Item = RefGrid[0, i].Value.ToString();

                    New.qty = double.Parse(RefGrid[1, i].Value.ToString());

                    New.u_m = RefGrid[2, i].Value.ToString();

                    New.sequence = (int)double.Parse(RefGrid[3, i].Value.ToString());

                    BillList.Add(New);
                }
            }
            catch { }
            finally { this.Close(); }
        }
    }

    public class ExistingBillItem: IComparable <ExistingBillItem>
    {
        public string Item { get; set; }

        public int sequence { get; set; }

        public double qty { get; set; }

        public string u_m { get; set; }

        public int CompareTo (ExistingBillItem other)
        {
            return Item.CompareTo (other.Item );
        }
    }
}
