using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class RevCompare : Form
    {
        private bool FromSW { get; set; }

        public RevCompare()
        {
            InitializeComponent();
        }

        public RevCompare(DataSet DSI, string PartNumber)
        {
            InitializeComponent();

            DS2 = DSI;

            rev2.Enabled = false;

            pnum_txt.Text = PartNumber;

            pnum_txt_Leave(this, null);

            pnum_txt.ReadOnly = true;

            FromSW = true;

            refresh_btn_Click(this, null);
        }

        DataSet DS0 = new DataSet();

        DataSet DS1 = new DataSet();

        DataSet DS2 = new DataSet();

        private void pnum_txt_Leave(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.WorkerSupportsCancellation = true;

                if (backgroundWorker1.IsBusy)
                    backgroundWorker1.CancelAsync();

                DS0 = new DataSet();

                DS0.Tables.Add();

                DS0.Tables[0].Columns.Add("Pnum");

                DS0.Tables[0].Columns.Add("Qty1");

                DS0.Tables[0].Columns.Add("Qty2");

                DS0.Tables[0].Columns.Add("Desc");

                DS0.Tables.Add();

                DS0.Tables[1].Columns.Add("Seq");

                DS0.Tables[1].Columns.Add("Code");

                DS0.Tables[1].Columns.Add("ProdHrs1");

                DS0.Tables[1].Columns.Add("ProdHrs2");

                desc_txt.Text = DataList.GetCurrentDesc(pnum_txt.Text);

                DataSet Rev = new DataSet();

                Rev = DataList.GetRevList(pnum_txt.Text);

                rev1.DisplayMember = "RevisionNum";

                rev1.ValueMember = "RevisionNum";

                rev1.DataSource = Rev.Tables[0];

                DataSet Rev2 = new DataSet();

                Rev2 = DataList.GetRevList(pnum_txt.Text);

                rev2.DisplayMember = "RevisionNum";

                rev2.ValueMember = "RevisionNum";

                rev2.DataSource = Rev2.Tables[0];

                refresh_btn.Enabled = true;

                if (!rev2.Enabled)
                    rev1.SelectedIndex = rev1.Items.Count - 1;

            }
            catch (Exception ex)
            {
            }
        }

        private void Colorize()
        {
            foreach (DataGridViewRow dr in Billgrid.Rows)
            {
                if (dr.Cells["Qty1"].Value.ToString() != dr.Cells["Qty2"].Value.ToString())
                {
                    dr.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            foreach (DataGridViewRow dr in Opsgrid.Rows)
            {
                if (dr.Cells["ProdHrs1"].Value.ToString() != dr.Cells["ProdHrs2"].Value.ToString())
                {
                    dr.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void DisplayGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Colorize();
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.AppStarting;

                status.Text = "Status: Working";

                Epicor.Mfg.BO.BomSearch BomSearch = new Epicor.Mfg.BO.BomSearch(DataList.EpicConn);

                DS0 = new DataSet();

                DS0.Tables.Add();

                DS0.Tables[0].Columns.Add("Pnum");

                DS0.Tables[0].Columns.Add("Qty1");

                DS0.Tables[0].Columns.Add("Qty2");

                DS0.Tables[0].Columns.Add("Desc");

                DS0.Tables.Add();

                DS0.Tables[1].Columns.Add("Seq");

                DS0.Tables[1].Columns.Add("Code");

                DS0.Tables[1].Columns.Add("ProdHrs1");

                DS0.Tables[1].Columns.Add("ProdHrs2");

                DS1 = (DataSet)BomSearch.GetDatasetForTree(pnum_txt.Text, rev1.SelectedValue.ToString(), "", true, DateTime.Now, false);

                if (rev2.Enabled)
                    DS2 = (DataSet)BomSearch.GetDatasetForTree(pnum_txt.Text, rev2.SelectedValue.ToString(), "", true, DateTime.Now, false);

                backgroundWorker1.RunWorkerAsync();

                refresh_btn.Enabled = !backgroundWorker1.IsBusy;

                pnum_txt.Enabled = !backgroundWorker1.IsBusy;

                rev1.Enabled = !backgroundWorker1.IsBusy;

                if (!FromSW)
                    rev2.Enabled = (!backgroundWorker1.IsBusy);

                toClipboard_btn.Enabled = !backgroundWorker1.IsBusy;
            }
            catch (Exception ex)
            {
                MessageBox.Show("This item may not have a revision already approved.", "Error!");
            }

        }

        private void FormResize(object sender, EventArgs e)
        {
            tabControl.Width = this.Width - 40;

            tabControl.Height = this.Height - 184;
        }

        private void toExcel_btn_Click(object sender, EventArgs e)
        {
            string s = "";

            foreach (DataGridViewRow dr in Billgrid.Rows)
            {
                decimal Q1 = Convert.ToDecimal(dr.Cells["Qty1"].Value);

                decimal Q2 = Convert.ToDecimal(dr.Cells["Qty2"].Value);

                string s2 = "";

                if (Q1 > Q2)
                {
                    s2 = "DEL (" + Math.Abs(Q2 - Q1) + ") " + dr.Cells["Pnum"].Value.ToString() + " (" + Q2 + ")";
                }
    
                if (Q1 < Q2)
                {
                    s2 = "ADD (" + Math.Abs(Q2 - Q1) + ") " + dr.Cells["Pnum"].Value.ToString() + " (" + Q2 + ")";    
                }

                if (s2 != "")
                    s += s2 + "\n";
            }

            if(!FromSW)
            foreach (DataGridViewRow dr in Opsgrid.Rows)
            {
                string s3 = "";

                Nullable<decimal> P1 = null;

                Nullable<decimal> P2 = null;

                try
                {
                    P1 = decimal.Parse(dr.Cells["ProdHrs1"].Value.ToString());
                }
                catch { P1 = null; }

                try
                {
                    P2 = decimal.Parse(dr.Cells["ProdHrs2"].Value.ToString());
                }
                catch { P2 = null; }

                if (P1 == null && P2 != null)
                {
                    s3 = "ADD (" + P2 + ") OP" + dr.Cells["Seq"].Value.ToString() + " - " + dr.Cells["Code"].Value.ToString() + " (" + P2 + ")";
                }

                if (P2 == null && P1 != null)
                {
                    s3 = "DEL (" + P1 + ") OP" + dr.Cells["Seq"].Value.ToString() + " - " + dr.Cells["Code"].Value.ToString() + " (0)";
                }
                if (P1 != null && P2 != null)
                {
                    if (P1 > P2)
                    {
                        s3 = "DEL (" + Math.Abs((decimal)P2 - (decimal)P1) + ") OP" + dr.Cells["Seq"].Value.ToString() + " - " + dr.Cells["Code"].Value.ToString() + " (" + P2 + ")";
                    }

                    if (P1 < P2)
                    {
                        s3 = "ADD (" + Math.Abs((decimal)P2 - (decimal)P1) + ") OP" + dr.Cells["Seq"].Value.ToString() + " - " + dr.Cells["Code"].Value.ToString() + " (" + P2 + ")";
                    }
                }

                s += s3 + "\n";
            }

            Clipboard.SetText(s);
        }

        private void _TabIndexChanged(object sender, EventArgs e)
        {
            Colorize();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            #region Set Bill Grid

            foreach (DataRow dr in DS1.Tables["PartMtl"].Rows)
            {
                DataRow Dr = DS0.Tables[0].NewRow();

                Dr["Pnum"] = dr["MtlPartNum"].ToString();

                Dr["Qty1"] = dr["QtyPer"].ToString();

                Dr["Qty2"] = "0";

                Dr["Desc"] = DataList.GetCurrentDesc(Dr["Pnum"].ToString());

                DS0.Tables[0].Rows.Add(Dr);

                backgroundWorker1.ReportProgress(1);
            }

            bool found = false;

            foreach (DataRow dr in DS2.Tables["PartMtl"].Rows)
            {
                found = false;

                foreach (DataRow Dr in DS0.Tables[0].Rows)
                {

                    if (dr["MtlPartNum"].ToString() == Dr["Pnum"].ToString())
                    {
                        Dr["Qty2"] = dr["QtyPer"].ToString();

                        found = true;

                        backgroundWorker1.ReportProgress(1);

                        break;
                    }
                }

                if (!found)
                {
                    DataRow DR = DS0.Tables[0].NewRow();

                    DR["Pnum"] = dr["MtlPartNum"].ToString();

                    DR["Qty2"] = dr["QtyPer"].ToString();

                    DR["Qty1"] = "0";

                    DR["Desc"] = DataList.GetCurrentDesc(DR["Pnum"].ToString());

                    DS0.Tables[0].Rows.Add(DR);

                    backgroundWorker1.ReportProgress(1);
                }
            }
            #endregion

            #region Set Ops Grid
            try
            {
                foreach (DataRow dr in DS1.Tables["PartOpr"].Rows)
                {
                    DataRow Dr = DS0.Tables[1].NewRow();

                    Dr["Seq"] = dr["OprSeq"].ToString();

                    Dr["ProdHrs1"] = dr["ProdStandard"].ToString();

                    //Dr["ProdHrs2"] = "0";

                    Dr["Code"] = dr["OpCode"].ToString();

                    DS0.Tables[1].Rows.Add(Dr);

                    backgroundWorker1.ReportProgress(1);
                }

                found = false;

                foreach (DataRow dr in DS2.Tables["PartOpr"].Rows)
                {
                    found = false;

                    foreach (DataRow Dr in DS0.Tables[1].Rows)
                    {

                        if (dr["OpCode"].ToString() == Dr["Code"].ToString() && dr["OprSeq"].ToString() == Dr["Seq"].ToString())
                        {
                            Dr["ProdHrs2"] = dr["ProdStandard"].ToString();

                            found = true;

                            backgroundWorker1.ReportProgress(1);

                            break;
                        }
                    }

                    if (!found)
                    {
                        DataRow Dr = DS0.Tables[1].NewRow();

                        Dr["Seq"] = dr["OprSeq"].ToString();

                        Dr["ProdHrs2"] = dr["ProdStandard"].ToString();

                        //Dr["ProdHrs1"] = "0";

                        Dr["Code"] = dr["OpCode"].ToString();

                        DS0.Tables[1].Rows.Add(Dr);

                        backgroundWorker1.ReportProgress(1);
                    }
                }
            }
            catch { }
            #endregion
        }

        private void WorkDone(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;

            refresh_btn.Enabled = !backgroundWorker1.IsBusy;

            pnum_txt.Enabled = !backgroundWorker1.IsBusy;

            rev1.Enabled = !backgroundWorker1.IsBusy;

            if (!FromSW)
                rev2.Enabled = !backgroundWorker1.IsBusy;

            toClipboard_btn.Enabled = !backgroundWorker1.IsBusy;

            status.Text = "Status: Idle";

            progress.Value = 0;

            Billgrid.DataSource = DS0.Tables[0];

            Opsgrid.DataSource = DS0.Tables[1];

            Colorize();
        }

        private void RevCompare_Load(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void ReportWork(object sender, ProgressChangedEventArgs e)
        {
            if (progress.Value != progress.Maximum)
                progress.Value++;
            else
                progress.Value = 0;
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config Config = new Config();

            Config.ShowDialog();
        }
    }
}
