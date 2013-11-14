using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor.Mfg.BO;
using Epicor.Mfg.IF;

namespace JobStatusByPlanner
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            JobStatus JS = new JobStatus(DataList.EpicConn);

            bool morePages;

            JobStatusDataSet JSDS = JS.GetRows("Plant = 'MfgSys' AND Jobclosed = False AND JobComplete = false AND JobFirm=false AND JobEngineered=true AND JobReleased=false BY DueDate", "", 100, 0, out morePages);

            dataGridView1.DataSource = JSDS.Tables[0];
        }
    }
}
