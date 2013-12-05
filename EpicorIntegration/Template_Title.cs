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
    public partial class Template_Title : Form
    {
        public string RetVal { get; set; }

        public Template_Title()
        {
            InitializeComponent();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            RetVal = template_name_txt.Text;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void Template_Title_Load(object sender, EventArgs e)
        {

        }
    }
}
