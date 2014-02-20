using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Epicor.Mfg.BO;

namespace Epicor_Integration
{
    public partial class Operations_CopyMethods : Form
    {
        public string retPart { get; set; }

        public string retRev { get; set; }

        public Operations_CopyMethods()
        {
            InitializeComponent();

            pnum_txt.Leave += pnum_txt_Leave;
        }
        
        public Operations_CopyMethods(string PartNumber)
        {
            InitializeComponent();

            pnum_txt.Text = PartNumber;

            pnum_txt.Leave += pnum_txt_Leave;

            EventArgs e = new EventArgs();

            pnum_txt_Leave(pnum_txt,e);
        }

        void pnum_txt_Leave(object sender, EventArgs e)
        {
            try
            {
                PartDataSet Pdata = new PartDataSet();

                if (DataList.PartExists(pnum_txt.Text))
                    Pdata = DataList.GetPart(pnum_txt.Text);

                RevGrid.DataSource = Pdata.Tables["PartRev"];

                desc_txt.Text = Pdata.Tables["Part"].Rows[0]["PartDescription"].ToString();
            }
            catch
            {
                //nodata
            }

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void part_txt_Click(object sender, EventArgs e)
        {
            SearchPart Sp = new SearchPart();

            Sp.ShowDialog();

            pnum_txt.Text = Sp._PartNumber;

            desc_txt.Text = Sp._Description;

            pnum_txt_Leave(pnum_txt,e);

            Sp.Dispose();
        }

        private void Operations_CopyMethods_Load(object sender, EventArgs e)
        {
            RevGrid.DoubleClick += RevGrid_DoubleClick;
        }

        void RevGrid_DoubleClick(object sender, EventArgs e)
        {
            int row = RevGrid.CurrentCellAddress.Y;

            retRev = RevGrid["RevisionNum", row].Value.ToString();

            retPart = pnum_txt.Text;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            int row = RevGrid.CurrentCellAddress.Y;

            if (RevGrid["Approved", row].Value.ToString() == "True")
            {
                retRev = RevGrid["RevisionNum", row].Value.ToString();

                retPart = pnum_txt.Text;

                this.Close();
            }
            else
                MessageBox.Show("Selection must be approved and checked in to work.  Please try again.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
