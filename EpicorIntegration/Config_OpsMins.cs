using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TableAdapterHelper;
using System.Data.SqlClient;

namespace Epicor_Integration
{
    public partial class Config_OpsMins : Form
    {
        private SqlTransaction Trans { get; set; }

        public Config_OpsMins()
        {
            InitializeComponent();

            opminsGrid.SelectionChanged += opminsGrid_SelectionChanged;

            Trans = TableHelper.BeginTransaction(epicorMinutesTableAdapter);
        }

        void opminsGrid_SelectionChanged(object sender, EventArgs e)
        {
            type_txt.Text = opminsGrid["Type", opminsGrid.CurrentCellAddress.Y].Value.ToString();

            operations_txt.Text = opminsGrid["ops_Name", opminsGrid.CurrentCellAddress.Y].Value.ToString();

            efficiency_txt.Text = opminsGrid["Efficiency", opminsGrid.CurrentCellAddress.Y].Value.ToString();

            per_txt.Text = opminsGrid["Per", opminsGrid.CurrentCellAddress.Y].Value.ToString();

            mp_txt.Text = opminsGrid["MP", opminsGrid.CurrentCellAddress.Y].Value.ToString();

            seconds_txt.Text = opminsGrid["Seconds", opminsGrid.CurrentCellAddress.Y].Value.ToString();
        }

        private void Config_OpsMins_Load(object sender, EventArgs e)
        {
            this.epicorMinutesTableAdapter.Fill(this.eNGDataDataSet.EpicorMinutes);

            type_txt.Leave += type_txt_Leave;
        }

        void type_txt_Leave(object sender, EventArgs e)
        {
            opminsGrid["Type", opminsGrid.CurrentCellAddress.Y].Value = type_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        void operations_txt_Leave(object sender, System.EventArgs e)
        {
            opminsGrid["Name", opminsGrid.CurrentCellAddress.Y].Value = operations_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            Trans.Commit();

            Trans.Dispose();

            Trans = null;
        }

        private void rem_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            epicorMinutesTableAdapter.DeletebyRowID(int.Parse(opminsGrid["row_id",opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            RefreshTransaction();

            epicorMinutesTableAdapter.InsertNewRow("", "", "", "", "", "");

            opminsGrid.CurrentCell = opminsGrid[0, opminsGrid.Rows.Count - 1];
        }

        private void efficiency_txt_Leave(object sender, EventArgs e)
        {
            opminsGrid["Efficiency", opminsGrid.CurrentCellAddress.Y].Value = efficiency_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void per_txt_Leave(object sender, EventArgs e)
        {
            opminsGrid["Per", opminsGrid.CurrentCellAddress.Y].Value = per_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void seconds_txt_Leave(object sender, EventArgs e)
        {
            opminsGrid["Seconds", opminsGrid.CurrentCellAddress.Y].Value = seconds_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void mp_txt_Leave(object sender, EventArgs e)
        {
            opminsGrid["MP", opminsGrid.CurrentCellAddress.Y].Value = mp_txt.Text;

            epicorMinutesTableAdapter.UpdatebyRowID(type_txt.Text, operations_txt.Text, efficiency_txt.Text, seconds_txt.Text, per_txt.Text, mp_txt.Text, int.Parse(opminsGrid["row_id", opminsGrid.CurrentCellAddress.Y].Value.ToString()));
        }

        private void RefreshTransaction()
        {
            if (Trans == null)
                Trans = TableHelper.BeginTransaction(epicorMinutesTableAdapter);
        }
    }
}
