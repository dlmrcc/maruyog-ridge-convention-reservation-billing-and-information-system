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
    public partial class ucResto : UserControl
    {
        public ucResto()
        {
            InitializeComponent();
        }

        private void ucResto_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            frmLogIn.q = "SELECT serviceid, name `NAME`, type `TYPE`, price `PRICE` FROM service where class = 'resto' order by type";//5
            frmLogIn.vTable();
            dgResto.DataSource = frmLogIn.dtable;
            dgResto.Columns[0].Visible = false;
            dgResto.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgResto.Columns[3].DefaultCellStyle.Format = "c";
            lbResult.Text = dgResto.Rows.Count + " menu result has found!";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ucResto_Load(sender, e);
        }
        public static String sCategory;
        public static String sName;
        public static Double dPrice;
        public static String sId;
        private void dgResto_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sId = dgResto.Rows[dgResto.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sName = dgResto.Rows[dgResto.CurrentCell.RowIndex].Cells[1].Value.ToString();
                sCategory = dgResto.Rows[dgResto.CurrentCell.RowIndex].Cells[2].Value.ToString();
                dPrice = Convert.ToDouble(dgResto.Rows[dgResto.CurrentCell.RowIndex].Cells[3].Value);
            }
            catch { }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            sSave = "Add";
            vCallResto();
            ucResto_Load(sender, e);
        }
        public static String sSave;
        void vCallResto()
        {
            frmRestaurantMenu r = new frmRestaurantMenu();
            r.ShowDialog();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            sSave = "Edit";
            vCallResto();
            ucResto_Load(sender, e);
        }
    }
}