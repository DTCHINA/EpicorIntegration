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
    public partial class BillImport : Form
    {
        private List<BillItem> ListToImport { get; set; }

        private List<OperationItem> OperationToImport { get; set; }

        private string Parent_Item { get; set; }

        public BillImport(List<BillItem> ToImport)
        {
            InitializeComponent();

            WorkingGrid.CellLeave += new DataGridViewCellEventHandler(WorkingGrid_CellLeave);

            this.SizeChanged += new EventHandler(BillImport_SizeChanged);

            ListToImport = new List<BillItem>();

            OperationToImport = new List<OperationItem>();

            ListToImport = ToImport;

            this.Text += " " + ListToImport[0].Parent_item;

            WorkingGrid.CellEndEdit += new DataGridViewCellEventHandler(WorkingGrid_CellEndEdit);
        }

        void WorkingGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SetGrid();
        }

        void BillImport_SizeChanged(object sender, EventArgs e)
        {
            Point confirmloc = new Point();

            Point cancelloc = new Point();

            confirmloc.X = this.Size.Width - 238;

            confirmloc.Y = this.Size.Height - 73;

            confirmbtn.Location = confirmloc;

            cancelloc.X = this.Size.Width - 130;

            cancelloc.Y = this.Size.Height - 73;

            cancelbtn.Location = cancelloc;

            WorkingGrid.Width = this.Size.Width - 40;

            WorkingGrid.Height = this.Size.Height - 91;
        }

        string Fetch_u_m(string item)
        {
            int exists = 0;

            string ret_val = null;

            try
            {
                exists = Convert.ToInt32 (itemTableAdapter.ExistsCount(item));
            }
            catch { exists = 0; }

            if (exists > 0)
            {
                try
                {
                    ret_val = itemTableAdapter.u_mQuery(item);
                }
                catch { ret_val = null; }
            }

            return ret_val;
        }

        void WorkingGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            SetGrid();           
        }

        private void BillImport_Load(object sender, EventArgs e)
        {
            int jobcount = 0;

            Parent_Item = ListToImport[0].Parent_item;

            try
            {
                jobcount = (int)jobTableAdapter.CountJobs(ListToImport[0].Parent_item, "CJ%");
            }
            catch { }

            if (jobcount > 0)
            {
                FetchBill form = new FetchBill(ListToImport[0].Parent_item);

                form.ShowDialog();

                for (int i = 0; i < form.BillList.Count; i++)
                {
                    WorkingGrid.Rows.Add();

                    WorkingGrid[1, i].Value = form.BillList[i].Item;

                    WorkingGrid[0, i].Value = form.BillList[i].sequence;

                    WorkingGrid[3, i].Value = form.BillList[i].qty;

                    WorkingGrid[5, i].Value = form.BillList[i].u_m;
                }

                form.Close();

                form.Dispose();

                for (int i = 0; i < WorkingGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < ListToImport.Count; j++)
                    {
                        if (WorkingGrid[1, i].Value != null)
                        {
                            if (WorkingGrid[1, i].Value.ToString().Trim() == ListToImport[j].Item)
                            {
                                System.Diagnostics.Debug.Print(ListToImport[j].Item);

                                WorkingGrid[2, i].Value = ListToImport[j].Qty;

                                ListToImport.RemoveAt(j);
                            }
                        }
                    }
                }

                for (int i = 0; i < ListToImport.Count; i++)
                {
                    WorkingGrid.Rows.Insert(0);

                    WorkingGrid[1, 0].Value = ListToImport[i].Item;

                    WorkingGrid[2, 0].Value = ListToImport[i].Qty;

                    WorkingGrid[0, 0].Value = Convert.ToInt32 (WorkingGrid[0, WorkingGrid.Rows.Count - 1].Value) + 1;

                    WorkingGrid.Sort(WorkingGrid.Columns[1], ListSortDirection.Ascending);
                }

                SetGrid();
            }
            else
            {
                ItemCreate form = new ItemCreate(ListToImport[0].Parent_item ,"");

                form.ShowDialog();

                if (form.DialogResult == DialogResult.Cancel)
                {
                    form.Dispose();

                    this.Close();

                    this.Dispose();
                }
                else
                {

                    form.Dispose();

                    for (int i = 0; i < ListToImport.Count; i++)
                    {
                        WorkingGrid.Rows.Insert(0);

                        WorkingGrid[0, 0].Value = i + 1;

                        WorkingGrid[1, 0].Value = ListToImport[i].Item;

                        WorkingGrid[2, 0].Value = ListToImport[i].Qty;

                        WorkingGrid[4, 0].Value = ListToImport[i].Description;

                        WorkingGrid.Sort(WorkingGrid.Columns[1], ListSortDirection.Ascending);
                    }
                }
            }

            SetGrid();

            for (int i = 0; i < WorkingGrid.Rows.Count; i++)
            {
                if (WorkingGrid[4, i].Value == null)
                {
                    int ItemExists = 0;
                    try
                    {
                        ItemExists = Convert.ToInt32 (itemTableAdapter.ExistsCount(WorkingGrid[1, i].Value.ToString()));
                    }
                    catch { ItemExists = 0; }

                    if (ItemExists != 0)
                    {
                        WorkingGrid[4, i].Value = itemTableAdapter.DescQuery(WorkingGrid[1, i].Value.ToString());
                    }
                        
                }
            }
        }

        void SetGrid()
        {
            for (int i = 0; i < WorkingGrid.Rows.Count; i++)
            {
                if (WorkingGrid[1, i].Value != null)
                    WorkingGrid[0, i].Value = i + 1;

                try
                {
                    //Attempt to convert Qty to Integer - If it fails blank the cell and warn user
                    if (WorkingGrid [1,i].Value != null)
                        double.Parse(WorkingGrid[2, i].Value.ToString());
                }
                catch
                {
                    //MessageBox.Show("Quantity value must be a number", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    WorkingGrid[2, i].Value = 0;
                }
                //Yellow Qty != Sys Qty
                //Blue  New Item
                //Red Remove Item
                if (WorkingGrid[1, i].Value != null && WorkingGrid[2, i].Value != null && WorkingGrid[3, i].Value != null)
                {
                    if (WorkingGrid[2, i].Value.ToString() != WorkingGrid[3, i].Value.ToString())
                    {
                        WorkingGrid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
                if ((WorkingGrid[3, i].Value == null || WorkingGrid[3, i].Value.ToString() == "0") && WorkingGrid[1, i].Value != null)
                {
                    WorkingGrid.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if ((WorkingGrid[2, i].Value == null || WorkingGrid[2, i].Value.ToString() == "0") && WorkingGrid[1, i].Value != null)
                {
                    WorkingGrid.Rows[i].DefaultCellStyle.BackColor = Color.Pink;

                    WorkingGrid[2, i].Value = 0;
                }
                if (WorkingGrid[1, i].Value != null && Fetch_u_m(WorkingGrid[1, i].Value.ToString()) != null)
                {
                    WorkingGrid[4, i].Value = Fetch_u_m(WorkingGrid[1, i].Value.ToString());
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();

            this.Dispose();
        }

        private string RefJNo(string item)
        {
            string retval = "";

            try
            {
                retval = itemTableAdapter.RefItemJob(item).ToString();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                System.Diagnostics.Debug.Print(se.Message);

                retval = null;

                MessageBox.Show(ex.Message, "Error! - Could not find Job Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retval;

        }

        private string CreateJONo()
        {
            try
            {

                string retval = "";

                string currentjob = null;

                try
                {
                    //currentjob = (Convert.ToInt32 ((jobTableAdapter.HighestJob("CJ%").ToString().Substring(2))) + 1).ToString();
                }
                catch
                {

                }

                if (currentjob == null)
                {
                    currentjob = "1";
                }

                int curlen = currentjob.Length;

                string midpiece = "";

                for (int i = 0; i < 8 - curlen; i++)
                {
                    midpiece = midpiece + "0";
                }

                retval = "CJ" + midpiece + currentjob;

                return retval;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! - Create Job Number", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();

                this.Dispose();

                return "";
            }
        }

        private void confirmbtn_Click(object sender, EventArgs e)
        {
            #region Verify Data/Collect Routing
            for (int i = WorkingGrid.Rows.Count - 1; i > -1; i--)
            {
                if ((WorkingGrid[2, i].Value == null || WorkingGrid[2, i].Value.ToString() == "0") && WorkingGrid[1, i].Value != null)
                {
                    WorkingGrid.Rows[i].DefaultCellStyle.BackColor = Color.Pink;

                    WorkingGrid[2, i].Value = 0;

                    if (WorkingGrid[3, i].Value == null)
                        WorkingGrid.Rows.RemoveAt(i);
                }
            }

            for (int i = 0; i < WorkingGrid.Rows.Count; i++)
            {
                if (WorkingGrid[1, i].Value != null)
                {
                    int exists = 0;

                    try
                    {
                        exists = Convert.ToInt32(itemTableAdapter.ExistsCount(WorkingGrid[1, i].Value.ToString()));
                    }
                    catch { exists = 0; }

                    if (!(exists > 0))
                    {
                        string DescString = WorkingGrid[4,i].Value.ToString ();

                        if (DescString == null)
                            DescString = "";

                        ItemCreate form = new ItemCreate(WorkingGrid[1, i].Value.ToString(), DescString);

                        form.ShowDialog();

                        if (form.DialogResult != DialogResult.OK)
                        {
                            form.Dispose();

                            this.DialogResult = DialogResult.Cancel;

                            this.Close();

                            return;
                        }

                        form.Dispose();
                    }
                }
            }

            int route_exists = 0;

            string Parent_Job = null;

            try
            {
                Parent_Job = RefJNo(Parent_Item);

                route_exists = Convert.ToInt32 (jobrouteTableAdapter .jobrouteCount(Parent_Job));
            }
            catch { route_exists = 0; }

            if (!(route_exists > 0))
            {
                OperationCreate form = new OperationCreate(Parent_Item);

                DialogResult diag = form.ShowDialog();

                if (diag == DialogResult.Cancel)
                {
                    form.Dispose();

                    this.DialogResult = DialogResult.Cancel;

                    this.Close();

                    this.Dispose();

                    return;
                }
                else
                {
                    OperationToImport = form.OpItems;

                    form.Dispose();
                }
            }
            #endregion

            //Open transaction

            //System.Data.SqlClient.SqlTransaction transaction = null;

            try
            {
                //transaction = TableAdapterHelper.BeginTransaction(jobTableAdapter);

                //TableAdapterHelper .SetTransaction (itemTableAdapter,transaction);

                MessageBox.Show("Creating Job");

                #region Create Job for item

                //Parent_Job = RefJNo(Parent_Item);

                string p_item = Parent_Item;

                if (Parent_Job == null || Parent_Job == "")
                {
                    //string JobNo = CreateJONo();

                    //jobTableAdapter.InsertQuery("S", JobNo, 0, "I", p_item);

                    //itemTableAdapter.UpdateitemJob(JobNo, p_item);

                    //MessageBox .Show ("Job Number would be " + JobNo);
                }

                #endregion

                //TableAdapterHelper.SetTransaction(jobrouteTableAdapter, transaction);

                MessageBox.Show("Creating Operations");

                #region Create Routing

                int jobroute_count = 0;

                try
                {
                    //jobroute_count = Convert.ToInt32 (jobrouteTableAdapter.jobrouteCount(Parent_Job));
                }
                catch { jobroute_count = 0; }

                if (jobroute_count == 0)
                {
                    byte control = 0;

                    for (int i = 0; i < OperationToImport.Count; i++)
                    {
                        if (i == OperationToImport.Count - 1)
                            control = 1;

                        //decimal setup_rate = Convert.ToDecimal(wcTableAdapter.SetupRate(OperationToImport[i].WC));

                        //decimal run_rate_lbr = Convert.ToDecimal(wcTableAdapter .RunRate (OperationToImport [i].WC));

                        //jobrouteTableAdapter.InsertOp(ReferenceJob, 0, OperationToImport[i].Operation, OperationToImport[i].WC, Convert.ToDecimal(OperationToImport[i].SetupHours), control,setup_rate,run_rate_lbr);

                        MessageBox.Show("Operation added: " + OperationToImport[i].Operation + " - " + OperationToImport[i].WC + ": " + Convert.ToDecimal(OperationToImport[i].SetupHours) + "\nControl Point: " + control  + " \nJob No: " + Parent_Job);
                    }
                }
                else
                {
                    MessageBox.Show("Operations already exist for item/job: " + p_item + "/" + Parent_Job);
                }

                #endregion

                //TableAdapterHelper.SetTransaction(jrt_schTableAdapter, transaction);

                //TableAdapterHelper.SetTransaction(jrtresourcegroupTableAdapter, transaction);

                MessageBox.Show("Filling Operations");

                #region Create Routing Hours/Set Resources

                int jrtsch_count = 0;

                try
                {
                    //jrtsch_count = Convert.ToInt32 (jrt_schTableAdapter.jrt_sch_count(Parent_Job));
                }
                catch { jrtsch_count = 0; }

                if (jrtsch_count != 0)
                {
                    for (int i = 0; i < OperationToImport.Count; i++)
                    {
                        decimal hours = Convert.ToDecimal(OperationToImport[i].SetupHours + OperationToImport[i].LaborHours);

                        //jrt_schTableAdapter.UpdateHrs(hours, ReferenceJob, 0, OperationToImport[i].Operation);

                        //jrtresourcegroupTableAdapter.InsertQuery(ReferenceJob, 0, OperationToImport[i].Operation, "Labor");

                        MessageBox.Show("Operation " + OperationToImport[i].Operation + " filled for Job No. " + Parent_Job + "\nHours: " + hours);
                    }
                }

                #endregion

                //TableAdapterHelper.SetTransaction(jobmatlTableAdapter, transaction);

                MessageBox.Show("Changing Bill of Materials");

                #region Create Bill of materials

                for (int i = 0; i < WorkingGrid.Rows.Count; i++)
                {
                    if (WorkingGrid[1, i].Value != null)
                    {
                        //add/update/remove item on SL Bom

                        int ItemCount = 0;

                        string ins_item = WorkingGrid[1,i].Value.ToString();

                        try
                        {
                           //ItemCount = Convert.ToInt32 (jobmatlTableAdapter .ItemOnBomCount (Parent_Job, ins_item));
                        }
                        catch { ItemCount = 0; }

                        if (ItemCount > 0)
                        {
                            //Item exists - need to update or delete

                            decimal NewQty = Convert.ToDecimal(WorkingGrid[2,i].Value);

                            if (NewQty == 0)
                            {
                                //Delete item from bill

                                //jobmatlTableAdapter.DeleteItem(ins_item, ReferenceJob);

                                MessageBox.Show("Item removed from Job No: " + Parent_Job + "\nItem: " + ins_item);

                            }
                            else
                            {
                                //Update qty of item

                                decimal qty = Convert.ToDecimal(WorkingGrid[2,i].Value);

                                //jobmatlTableAdapter.UpdateItemQty(qty, ReferenceJob, ins_item);

                                MessageBox.Show("Item Qty Updated on Job No: " + Parent_Job + "\nItem: " + ins_item + " New Qty: " + qty);
                            }
                        }
                        else
                        {

                            //Item does not exist on Bill - add item

                            decimal qty = Convert.ToDecimal (WorkingGrid[2,i].Value);

                            //string item_u_m = itemTableAdapter.u_mQuery (ins_item);

                            //jobmatlTableAdapter.InsertItem(ReferenceJob, OpNum, (short)Convert.ToInt32(WorkingGrid[0, i].Value), ins_item, qty, item_u_m);

                            MessageBox.Show("New Item inserted to Job No: " + Parent_Job + "\nItem: " + ins_item + " Qty: " + qty);
                        }

                    }
                }
                #endregion

                //Commit Transactions

                //transaction.Commit();
            }
            catch 
            {
                //transaction.Rollback();

                throw;
            }
            finally 
            {
                //Close and Dispose

                //transaction.Connection.Close();

                //transaction.Dispose();

                this.Close();

                this.Dispose();
            } 
        }
    }
}
