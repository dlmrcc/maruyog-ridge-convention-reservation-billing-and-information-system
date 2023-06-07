using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucConvention : UserControl
    {
        public ucConvention()
        {
            InitializeComponent();
        }
        public static String sserviceid, sname, stype, smax, sprice;
        private void dgRoom_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sserviceid = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sname = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[1].Value.ToString();
                stype = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[2].Value.ToString();
                smax = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[3].Value.ToString();
                sprice = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[4].Value.ToString();
            }
            catch { }
        }
        private void ucConvention_Load(object sender, EventArgs e)
        {
            frmLogIn.q = "SELECT serviceid, name, type, max `MAX PAX`, price FROM service where category = 'Convention' and kind = 'Regular' order by price";
            frmLogIn.q = frmLogIn.q.ToUpper();
            frmLogIn.vTable();
            dgRoom.DataSource = frmLogIn.dtable;
            dgRoom.Columns[0].Visible = false;
            dgRoom.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[4].DefaultCellStyle.Format = "c";
            dgRoom.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            lbResult.Text = dgRoom.Rows.Count + " convention room result has found";
        }
        void vCallConvention()
        {
            wfConvention c = new wfConvention();
            c.ShowDialog();
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            sSave = "Add";
            vCallConvention();
            ucConvention_Load(sender, e);
        }
        public static String sSave;
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            sSave = "Edit";
            vCallConvention();
            ucConvention_Load(sender, e);
        }
    }
}