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
    public partial class Waiting : Form
    {
        public Waiting(string Text)
        {
            InitializeComponent();

            label1.Text = Text;
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;

            if (progressBar1.Value == progressBar1.Maximum)
                progressBar1.Value = 0;
        }
    }
}
