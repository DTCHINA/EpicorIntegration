using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EpicorIntegration
{
    public partial class RawListing : Form
    {
        public RawListing()
        {
            InitializeComponent();
        }

        private void RawListing_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eNGDataDataSet.rawlisting' table. You can move, or remove it, as needed.
            this.rawlistingTableAdapter.Fill(this.eNGDataDataSet.rawlisting);

        }
    }
}
