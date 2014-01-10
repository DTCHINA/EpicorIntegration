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

            reportViewer1.LocalReport.SetParameters(RP);
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            

            this.reportViewer1.RefreshReport();
        }
    }
}
