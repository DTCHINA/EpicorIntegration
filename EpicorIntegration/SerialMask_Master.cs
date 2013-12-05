using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Epicor.Mfg.BO;

namespace Epicor_Integration
{
    public partial class SerialMask_Master : Form
    {
        public string Prefix;

        public SerialMask_Master()
        {
            InitializeComponent();
        }

        public SerialMask_Master(string _prefix)
        {
            InitializeComponent();

            serialprefix_txt.Text = _prefix;
        }

        private void SerialMask_Master_Load(object sender, EventArgs e)
        {

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();

            this.DialogResult = DialogResult.Cancel;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {   
            if (serialprefix_txt.Text == "" || serialprefix_txt.Text == null)
            {
                DialogResult dr = MessageBox.Show("Cannot use null prefix.  Do you want to enter a prefix before continuing?\n\nSelecting No will close this dialog and remove 'Track Serial Number'", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                if (dr == DialogResult.No)
                {
                    this.DialogResult = DialogResult.Cancel;

                    this.Close();
                }

            }
            else
            {
                Prefix = serialprefix_txt.Text;

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}
