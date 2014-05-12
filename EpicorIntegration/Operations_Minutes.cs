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
    public partial class Operations_Minutes : Form
    {
        public OperationsType Band = new OperationsType();

        public OperationsType Huck = new OperationsType();

        public OperationsType Tec = new OperationsType();

        public OperationsType Bolt = new OperationsType();

        public OperationsType Springs = new OperationsType();

        public OperationsType Zhooks = new OperationsType();

        public OperationsType Heat = new OperationsType();

        public OperationsType Rivet = new OperationsType();

        public decimal RetVal { get; set; }

        public Operations_Minutes()
        {
            InitializeComponent();
        }

        private void Operations_Minutes_Load(object sender, EventArgs e)
        {
            this.epicorMinutesTableAdapter.Fill(this.engDataDataSet.EpicorMinutes);

            operation_cbo.DataSource = engDataDataSet.Tables[1];

            operation_cbo.DisplayMember = "Name";

            band_txt.TextChanged += band_txt_TextChanged;

            hucks_txt.TextChanged += hucks_txt_TextChanged;

            tec_txt.TextChanged += tec_txt_TextChanged;

            bolt_txt.TextChanged += bolt_txt_TextChanged;

            springs_txt.TextChanged += springs_txt_TextChanged;

            zhooks_txt.TextChanged += zhooks_txt_TextChanged;

            heat_txt.TextChanged += heat_txt_TextChanged;

            foreach (DataRow dr in engDataDataSet.Tables[1].Rows)
            {
                if (dr["Name"].ToString() == "BAND")
                {
                    Band.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Band.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Time Per Huck")
                {
                    Huck.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Huck.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Time per Tec Screw")
                {
                    Tec.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Tec.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Time per BOLT")
                {
                    Bolt.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Bolt.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Springs")
                {
                    Springs.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Springs.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Z Hooks")
                {
                    Zhooks.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Zhooks.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "Heat Seal-Bags")
                {
                    Heat.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Heat.Seconds = double.Parse(dr["Seconds"].ToString());
                }

                if (dr["Name"].ToString() == "RIVET")
                {
                    Rivet.Efficiency = double.Parse(dr["Efficiency"].ToString());

                    Rivet.Seconds = double.Parse(dr["Seconds"].ToString());
                }
            }
        }

        private void CalculateAssemble()
        {
            double band_time = 0;

            double huck_time = 0;

            double tec_time = 0;

            double bolt_time = 0;

            double spring_time = 0;

            double zhook_time = 0;

            double rivet_time = 0;

            try
            {
                band_time = ((Band.Seconds / Band.Efficiency) / 60) * int.Parse(band_txt.Text);
            }
            catch { }

            try
            {
                huck_time = ((Huck.Seconds / Huck.Efficiency) / 60) * int.Parse(hucks_txt.Text);
            }
            catch { }

            try
            {
                tec_time = ((Tec.Seconds / Tec.Efficiency) / 60) * int.Parse(tec_txt.Text);
            }
            catch { }

            try
            {
                bolt_time = ((Bolt.Seconds / Bolt.Efficiency) / 60) * int.Parse(bolt_txt.Text);
            }
            catch { }

            try
            {
                spring_time = ((Springs.Seconds / Springs.Efficiency) / 60) * int.Parse(springs_txt.Text);
            }
            catch { }

            try
            {
                zhook_time = ((Zhooks.Seconds / Zhooks.Efficiency) / 60) * int.Parse(zhooks_txt.Text);
            }
            catch { }

            try
            {
                rivet_time = ((Rivet.Seconds / Rivet.Efficiency) / 60) * int.Parse(rivet_txt.Text);
            }
            catch { }

            minpc_txt.Text = (band_time + huck_time + tec_time + bolt_time + spring_time + zhook_time + rivet_time).ToString();
        }

        void heat_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void zhooks_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void springs_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void bolt_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void tec_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void hucks_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        void band_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateAssemble();
        }

        private void operation_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowid = operation_cbo.SelectedIndex;

            type_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Type"].ToString();

            efficiency_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Efficiency"].ToString();

            per_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Per"].ToString();

            seconds_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["Seconds"].ToString();

            minpc_txt.Text = engDataDataSet.Tables[1].Rows[rowid]["MP"].ToString();

            if (type_txt.Text == "ASSEMBLE")
            {
                assembly_group.Enabled = true;

                assembly_group.Show();

                CalculateAssemble();
            }
            else
            {
                assembly_group.Enabled = false;

                assembly_group.Hide();
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void seconds_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            try
            {
                RetVal = decimal.Parse(minpc_txt.Text);
            }
            catch { MessageBox.Show("Value is not valid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
        }

        private void mult_txt_TextChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }

        void CalculateVals()
        {
            string MP = null;

            double railfactor = 1;

            if (rails_chk.Checked)
                railfactor = .5;

            try
            {
                MP = ((double.Parse(seconds_txt.Text) / double.Parse(efficiency_txt.Text) / 60) * (double.Parse(mult_txt.Text)) * railfactor).ToString();
            }
            catch { MP = "Error!"; }
            finally { minpc_txt.Text = MP; }
        }

        private void rails_chk_CheckedChanged(object sender, EventArgs e)
        {
            CalculateVals();
        }
    }

    public class OperationsType
    {
        public double Efficiency {get;set;}
        public double Seconds {get;set;}
    }
}
