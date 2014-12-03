﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Epicor_Integration
{
    public partial class Item_SheetFactor : Form
    {
        public decimal FactoredWeight { get; set; }

        public decimal OriginalWeight { get; set; }

        public Item_SheetFactor(decimal Weight, string Material)
        {
            InitializeComponent();

            partnumber_cbo.SelectedIndexChanged -= partnumber_cbo_SelectedIndexChanged;

            gaugeRefTableAdapter.Fill(eNGDataDataSet.GaugeRef);

            partnumber_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumber();

            partnumber_cbo.DisplayMember = "partnumber";

            material_cbo.DataSource = sheetCoil_UsageTableAdapter.GetMaterial("%", "%", "%", "%", "%", "%");

            material_cbo.DisplayMember = "material";

            type_cbo.DataSource = sheetCoil_UsageTableAdapter.GetType("%", "%", "%", "%", "%", "%");

            type_cbo.DisplayMember = "type";

            density_cbo.DataSource = sheetCoil_UsageTableAdapter.GetDensity("%", "%", "%", "%", "%");

            density_cbo.DisplayMember = "density";

            length_cbo.DataSource = sheetCoil_UsageTableAdapter.GetLength("%", "%", "%", "%", "%", "%");

            length_cbo.DisplayMember = "length";

            width_cbo.DataSource = sheetCoil_UsageTableAdapter.GetWidth("%", "%", "%", "%", "%", "%");

            width_cbo.DisplayMember = "width";

            partnumber_cbo.SelectedIndexChanged += partnumber_cbo_SelectedIndexChanged;

            OriginalWeight = Weight;

            type_cbo.TextChanged += type_cbo_TextChanged;

            partnumber_cbo.Text = Material;

            partnumber_cbo_SelectedIndexChanged(this, null);
        }

        void type_cbo_TextChanged(object sender, EventArgs e)
        {
            rail_chk.Checked = (type_cbo.Text == "Coil");

            if (type_cbo.Text == "Coil")
            {
                sheetcoilinput_lbl.Text = "Length";

                rail_chk.Enabled = true;
            }
            else
            {
                sheetcoilinput_lbl.Text = "Qty Nested";

                rail_chk.Enabled = false;
            }
        }

        private void Operations_SheetFactor_Load(object sender, EventArgs e)
        {

        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            partnumber_cbo.SelectedIndexChanged -= partnumber_cbo_SelectedIndexChanged;

            gaugeRefTableAdapter.Fill(eNGDataDataSet.GaugeRef);

            partnumber_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumber();

            partnumber_cbo.DisplayMember = "partnumber";

            material_cbo.DataSource = sheetCoil_UsageTableAdapter.GetMaterial("%", "%", "%", "%", "%", "%");

            material_cbo.DisplayMember = "material";

            type_cbo.DataSource = sheetCoil_UsageTableAdapter.GetType("%", "%", "%", "%", "%", "%");

            type_cbo.DisplayMember = "type";

            density_cbo.DataSource = sheetCoil_UsageTableAdapter.GetDensity("%", "%", "%", "%", "%");

            density_cbo.DisplayMember = "density";

            length_cbo.DataSource = sheetCoil_UsageTableAdapter.GetLength("%", "%", "%", "%", "%", "%");

            length_cbo.DisplayMember = "length";

            width_cbo.DataSource = sheetCoil_UsageTableAdapter.GetWidth("%", "%", "%", "%", "%", "%");

            width_cbo.DisplayMember = "width";

            partnumber_cbo.SelectedIndexChanged += partnumber_cbo_SelectedIndexChanged;
        }

        private void partnumber_cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            partnumber_cbo.SelectedIndexChanged -= partnumber_cbo_SelectedIndexChanged;

            try
            {
                length_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                length_cbo.DisplayMember = "length";

                width_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                width_cbo.DisplayMember = "width";

                material_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                material_cbo.DisplayMember = "material";

                type_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                type_cbo.DisplayMember = "type";

                density_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                density_cbo.DisplayMember = "density";

                DataTable dt = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                gauge_cbo.Text = dt.Rows[0]["gauge"].ToString();
            }
            catch { }

            partnumber_cbo.SelectedIndexChanged += partnumber_cbo_SelectedIndexChanged;

            double width = 1;

            if (width_cbo.Text != "")
                double.TryParse(width_cbo.Text, out width);

            double length = 1;

            if (length_cbo.Text != "")
                double.TryParse(length_cbo.Text, out length);

            double thickness = 1;

            if (gauge_cbo.Text != "")
                double.TryParse(gauge_cbo.SelectedValue.ToString(), out thickness);

            double density = 1;

            if (density_cbo.Text != "")
                double.TryParse(density_cbo.Text, out density);

            factor_txt.Text = (width * length * thickness * density).ToString();

            if (type_cbo.Text == "Coil")
            {
                int div = 1;

                if (rail_chk.Checked)
                    div = 2;

                factoredweight_txt.Text = (((width * thickness * density) * (double)weight_num.Value) / div).ToString();
            }
            else
                factoredweight_txt.Text = ((width * length * thickness * density) / (double)weight_num.Value).ToString();
        }

        private void accept_btn_Click(object sender, EventArgs e)
        {
            FactoredWeight = decimal.Parse(factoredweight_txt.Text);

            this.Close();
        }

        private void factor_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void weight_num_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                length_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                length_cbo.DisplayMember = "length";

                width_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                width_cbo.DisplayMember = "width";

                material_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                material_cbo.DisplayMember = "material";

                type_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                type_cbo.DisplayMember = "type";

                density_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                density_cbo.DisplayMember = "density";

                DataTable dt = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                gauge_cbo.Text = dt.Rows[0]["gauge"].ToString();
            }
            catch { }

            double width = 1;

            if (width_cbo.Text != "")
                double.TryParse(width_cbo.Text, out width);

            double length = 1;

            if (length_cbo.Text != "")
                double.TryParse(length_cbo.Text, out length);

            double thickness = 1;

            if (gauge_cbo.Text != "")
                double.TryParse(gauge_cbo.SelectedValue.ToString(), out thickness);

            double density = 1;

            if (density_cbo.Text != "")
                double.TryParse(density_cbo.Text, out density);

            if (type_cbo.Text == "Coil")
            {
                int div = 1;

                if (rail_chk.Checked)
                    div = 2;

                factoredweight_txt.Text = (((width * thickness * density) * (double)weight_num.Value) / div).ToString();
            }
            else
                factoredweight_txt.Text = ((width * length * thickness * density) / (double)weight_num.Value).ToString();
        }

        private void rail_chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                length_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                length_cbo.DisplayMember = "length";

                width_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                width_cbo.DisplayMember = "width";

                material_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                material_cbo.DisplayMember = "material";

                type_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                type_cbo.DisplayMember = "type";

                density_cbo.DataSource = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                density_cbo.DisplayMember = "density";

                DataTable dt = sheetCoil_UsageTableAdapter.GetPartNumberDetails(partnumber_cbo.Text);

                gauge_cbo.Text = dt.Rows[0]["gauge"].ToString();
            }
            catch { }

            double width = 1;

            if (width_cbo.Text != "")
                double.TryParse(width_cbo.Text, out width);

            double length = 1;

            if (length_cbo.Text != "")
                double.TryParse(length_cbo.Text, out length);

            double thickness = 1;

            if (gauge_cbo.Text != "")
                double.TryParse(gauge_cbo.SelectedValue.ToString(), out thickness);

            double density = 1;

            if (density_cbo.Text != "")
                double.TryParse(density_cbo.Text, out density);

            if (type_cbo.Text == "Coil")
            {
                int div = 1;

                if (rail_chk.Checked)
                    div = 2;

                factoredweight_txt.Text = (((width * thickness * density) * (double)weight_num.Value) / div).ToString();
            }
            else
                factoredweight_txt.Text = ((width * length * thickness * density) / (double)weight_num.Value).ToString();
        }
    }
}
