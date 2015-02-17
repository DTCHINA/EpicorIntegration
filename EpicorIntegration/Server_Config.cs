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
    public partial class Server_Config : Form
    {
        public string sa_pass
        {
            get;
            set;
        }

        public string server_name
        {
            get;
            set;
        }

        public Server_Config()
        {
            InitializeComponent();
        }

        private void Server_Config_Load(object sender, EventArgs e)
        {
            string conn_string = Properties.Settings.Default.ENGDataConnectionString ;

            int pass_loc = conn_string.IndexOf("Password=", 0);

            int server_loc = conn_string.IndexOf("Data Source=", 0);

            int db_loc = conn_string.IndexOf(";Initial Catalog=", 0);

            string temp_val = DateTime.Now.ToString();

            string server_name = conn_string.Substring(server_loc + "Data Source=".Length, db_loc - (server_loc + "Data Source=".Length));

            string password = conn_string.Substring(pass_loc + "Password=".Length);

            server_name_txt.Text = server_name;

            sa_pass_txt.Text = password;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string conn_string = Properties.Settings.Default.ENGDataConnectionString;

            int pass_loc = conn_string.IndexOf("Password=", 0);

            int server_loc = conn_string.IndexOf("Data Source=", 0);

            int db_loc = conn_string.IndexOf(";Initial Catalog=", 0);

            string temp_val = DateTime.Now.ToString();

            string server_name = conn_string.Substring(server_loc + "Data Source=".Length, db_loc - (server_loc + "Data Source=".Length));

            string password = conn_string.Substring(pass_loc + "Password=".Length);

            conn_string = conn_string.Replace(password, sa_pass_txt.Text);

            conn_string = conn_string.Replace(server_name, server_name_txt.Text);

            //Properties.Settings.Default.ENGDataConnectionString = conn_string;

            Properties.Settings.Default.Save();

            MessageBox.Show("Application must be restarted for settings to take effect.", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            this.Close();
        }
    }
}
