using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TopLevelReport
{
    public partial class ReportViewer : Form
    {
        public ReportViewer(string SearchTerm, DataTable Dt)
        {
            InitializeComponent();

            ReportParameter RP = new ReportParameter("SearchNumber", SearchTerm);

            Dt.DefaultView.Sort = "TopLevel";

            DataSet1.Tables[0].Merge(Dt);

            foreach (DataRow dr in DataSet1.Tables[0].Rows)
            {
                dr["PartDescription"] = dr["Part.PartDescription"];

                dr["PartNum"] = dr["PartMtl.PartNum"];

                dr["RevisionNum"] = dr["PartMtl.RevisionNum"];

                dr["MtlPartNum"] = dr["PartMtl.MtlPartNum"];            
            }

            reportViewer1.LocalReport.SetParameters(RP);
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            

            this.reportViewer1.RefreshReport();
        }
    }
}
