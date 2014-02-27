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

            this.KeyPreview = true;

            this.KeyDown += main_KeyDown;
        }

        void main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                Config Conf = new Config();

                Conf.ShowDialog();
            }
        }

        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            report_btn.Enabled = true;
        }

        DataSet Result { get; set; }

        private void search_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            dataGridView1.Refresh();

            //DataList.GetCurrentDesc(searchterm_txt.Text);

            if (searchterm_txt.Text != null && searchterm_txt.Text != "")
            {
                Result = (DataSet)DataList.WhereUsed(searchterm_txt.Text);

                Result.Tables["Results"].Columns.Add("TopLevel",typeof(Boolean));

                for (int i = 0; i < Result.Tables["Results"].Rows.Count; i++)
                {
                    DataRow dr = Result.Tables["Results"].Rows[i];

                    string searchnum = dr["PartMtl.PartNum"].ToString();

                    Result.Tables["Results"].Rows[i]["TopLevel"] = (SearchDS(searchnum));
                }

                Result.Tables["Results"].DefaultView.Sort = "PartMtl.PartNum";

                dataGridView1.DataSource = Result.Tables["Results"];//.DefaultView.ToTable(true, "PartNum", "RevisionNum","MtlPartNum","PartNumPartDescription");
            }
        }

        private bool SearchDS(string Pnum)
        {
            try
            {
                DataSet data = (DataSet)DataList.WhereUsed(Pnum);

                foreach (DataRow dr in data.Tables["Results"].Rows)
                {
                    string newsearch = dr["PartMtl.MtlPartNum"].ToString();

                    DataSet ds = (DataSet)DataList.WhereUsed(newsearch);

                    ds.Tables["Results"].Columns.Add("TopLevel", typeof(Boolean));

                    if (ds.Tables["Results"] == null)
                    {
                        DataRow[] dr1 = new DataRow[1] { dr };

                        dr["TopLevel"] = true;

                        Result.Merge(dr1);
                    }
                    else
                    {
                        Result.Merge(ds);
                    }

                    SearchDS(dr["PartNum"].ToString());
                }

                return true;

            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message);
            return false;
            }
        }

        private void report_btn_Click(object sender, EventArgs e)
        {
            ReportViewer RV = new ReportViewer(searchterm_txt.Text, (DataTable)Result.Tables["Results"]);

            RV.ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }
}
