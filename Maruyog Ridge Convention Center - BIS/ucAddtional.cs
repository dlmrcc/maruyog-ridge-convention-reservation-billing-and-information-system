using System.Globalization;
using System.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucAddtional : UserControl
    {
        public ucAddtional()
        {
            InitializeComponent();
        }
        private void ucAddtional_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            frmLogIn.q = "SELECT serviceid, name, type, category, price FROM service where kind = 'additional' order by category ";
            frmLogIn.q = frmLogIn.q.ToUpper();
            frmLogIn.vTable();
            dgRoom.DataSource = frmLogIn.dtable;
            dgRoom.Columns[0].Visible = false;
            dgRoom.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgRoom.Columns[4].DefaultCellStyle.Format = "c";
            lbResult.Text = dgRoom.Rows.Count + " additional price has found!";
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            wfAdditional a = new wfAdditional();
            a.ShowDialog();
            ucAddtional_Load(sender, e);
        }
        public static String sServiceID, sName, sType, sCategory, sPrice;
        private void dgRoom_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sServiceID = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sName = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[1].Value.ToString();
                sType = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[2].Value.ToString();
                sCategory = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[3].Value.ToString();
                sPrice = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[4].Value.ToString();
            }
            catch { }
        }
    }
}