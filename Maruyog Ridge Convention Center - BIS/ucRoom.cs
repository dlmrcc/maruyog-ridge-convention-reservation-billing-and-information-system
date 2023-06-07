using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucRoom : UserControl
    {
        public ucRoom()
        {
            InitializeComponent();
        }

        public static String sserviceid, sname, stype, smax, sprice, sclass, slimit;
        private void dgRoom_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sserviceid = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sname = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[1].Value.ToString();
                stype = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[2].Value.ToString();
                slimit = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[3].Value.ToString();
                smax = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[4].Value.ToString();
                sprice = dgRoom.Rows[dgRoom.CurrentCell.RowIndex].Cells[5].Value.ToString();
            }
            catch { }
        }
        private void ucRoom_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            frmLogIn.q = "SELECT serviceid,"//0
                +" name `ROOM NUMBER`,"//1
                +" type `ROOM TYPE`,"//2
                +" `limit` `PAX LIMIT`,"//3
                +" max `MAX PAX`,"//4
                +" price `ROOM RATE`"//5
                +" FROM service where kind = 'regular' and category = 'Accommodation' order by price";
            frmLogIn.vTable();
            dgRoom.DataSource = frmLogIn.dtable;
            dgRoom.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[0].Visible = false;
            dgRoom.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgRoom.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgRoom.Columns[5].DefaultCellStyle.Format = "c";
            lbResult.Text = dgRoom.Rows.Count + " room result has found!";
        }
        public static String sSave;
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            sSave = "Add";
            vCallRoomSettings();
            ucRoom_Load(sender, e);
        }
        void vCallRoomSettings()
        {
            frmRoomSettings r = new frmRoomSettings();
            r.ShowDialog();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            sSave = "Edit";
            vCallRoomSettings();
            ucRoom_Load(sender, e);
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ucRoom_Load(sender, e);
        }
    }
}