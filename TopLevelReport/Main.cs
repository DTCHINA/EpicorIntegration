using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor_Integration;

namespace TopLevelReport
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }

        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            report_btn.Enabled = true;
        }

        DataSet Result { get; set; }

        private void search_btn_Click(object sender, EventArgs e)
        {
            if (searchterm_txt.Text != null && searchterm_txt.Text != "")
            {
                DataSet CleanSlate = new DataSet();

                Result = (DataSet)DataList.WhereUsed(searchterm_txt.Text);

                //Result.Tables["PartWhereUsed"].Columns.Add("TopLevel");

                foreach (DataRow dr in (Result.Tables["PartWhereUsed"].Rows))
                {
                    string searchnum = dr["PartNum"].ToString();

                    SearchDS(searchnum);
                }

                Result.Tables["PartWhereUsed"].DefaultView.Sort = "PartNum";

                dataGridView1.DataSource = Result.Tables["PartWhereUsed"];//.DefaultView.ToTable(true, "PartNum", "RevisionNum","MtlPartNum","PartNumPartDescription");
            }
        }

        private void SearchDS(string Pnum)
        {
            try            {
                foreach (DataRow dr in ((DataSet)DataList.WhereUsed(Pnum)).Tables["PartWhereUsed"].Rows)
                {
                    string newsearch = dr["PartNum"].ToString();

                    DataSet ds = (DataSet)DataList.WhereUsed(newsearch);


                    ds.Tables["PartWhereUsed"].Columns.Add("TopLevel");

                    if (ds.Tables["PartNum"] == null)
                    {
                        DataRow[] dr1 = new DataRow[1] { dr };

                        dr["TopLevel"] = 1;

                        Result.Merge(dr1);
                    }
                    else
                    {
                        Result.Merge(ds);
                    }

                    SearchDS(dr["PartNum"].ToString());
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

        private void report_btn_Click(object sender, EventArgs e)
        {
            ReportViewer RV = new ReportViewer(searchterm_txt.Text, (DataTable)Result.Tables["PartWhereUsed"]);

            RV.ShowDialog();
        }
    }
}
