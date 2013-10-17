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
    public partial class OperationCreate : Form
    {
        private string Item { get; set; }

        private int OpNum { get; set; }

        public List<OperationItem> OpItems = new List<OperationItem>();

        public OperationCreate(string item)
        {
            InitializeComponent();

            OpGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(OpGrid_RowsAdded);

            OpGrid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(OpGrid_RowsRemoved);

            Item = item;

            this.Text += item;

            OpNum = 10;
        }

        void OpGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            OpNum = 10;

            for (int i = 0; i < OpGrid.Rows.Count; i++)
            {
                if (OpGrid[1, i].Value != null)
                {
                    OpGrid[0, i].Value = OpNum;

                    OpNum += 10;
                }
            }
        }

        void OpGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int Row = OpGrid.CurrentRow.Index;

            OpGrid[0, Row].Value = OpNum;

            OpNum += 10;
        }

        private void OperationCreate_Load(object sender, EventArgs e)
        {
            this.wcTableAdapter.Fill(this.dEKKER_AppDataSet.wc);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {        
            this.Close();
        }

        private void continuebtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < OpGrid.Rows.Count; i++)
            {
                if (OpGrid[0, i].Value != null)
                {
                    OperationItem Temp = new OperationItem();

                    Temp.Operation = Convert.ToInt32 (OpGrid[0, i].Value);

                    Temp.WC = OpGrid[1, i].Value.ToString ();

                    Temp.LaborHours = Convert.ToDouble(OpGrid[2, i].Value);

                    Temp.SetupHours = Convert.ToDouble (OpGrid[3, i].Value);

                    OpItems.Add(Temp);
                }
            }

            this.Close();
        }

    }

    public class OperationItem
    {
        public string WC { get; set; }

        public int Operation { get; set; }

        public double LaborHours { get; set; }

        public double SetupHours { get; set; }
    }
}
