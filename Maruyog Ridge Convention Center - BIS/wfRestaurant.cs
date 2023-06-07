using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class wfRestaurant : Form
    {
        public wfRestaurant()
        {
            InitializeComponent();
        }
        void vCustomer()
        {
            frmLogIn.q = "SELECT concat(fname,' ', mname,' ', lname) FROM guest";
            frmLogIn.vTable();
            for (int a = 0; a < frmLogIn.dtable.Rows.Count; a++)
                cbCustomer.Items.Add(frmLogIn.dtable.Rows[a][0]);           
        }
        void vCategory()
        {
            frmLogIn.q = "SELECT distinct type FROM service where class = 'resto' order by type";
            frmLogIn.vTable();
            for (int a = 0; a < frmLogIn.dtable.Rows.Count; a++)
                cbCategory.Items.Add(frmLogIn.dtable.Rows[a][0]);
        }

        private void wfRestaurant_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            vCustomer();
            vCategory(); try
            {
                cbCategory.SelectedIndex = 0;
                cbCustomer.SelectedIndex = 0;
            }
            catch { }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmLogIn.q = "SELECT serviceid, name `NAME`, price `PRICE` FROM service where class = 'resto' and type = '" + cbCategory.Text + "'";
            frmLogIn.vTable();
            dgMenu.DataSource = frmLogIn.dtable;
            dgMenu.Columns[0].Visible = false;
            dgMenu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgMenu.Columns[2].DefaultCellStyle.Format = "c";
        }

        private void bnAdd_Click(object sender, EventArgs e)
        {
            if (dgOrder.Rows.Count == 0)
            {
                frmLogIn.q = "insert into billing"
                    + " (billingId, userid, guestid, flag, date)"
                    + " values (null,'" + frmLogIn.suserid
                    + "','" + sGuestID
                    + "','" + "Unpaid"
                    + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                frmLogIn.vTable();
            }
            vBillingID();
            frmLogIn.q = "insert into sub"
                + " (subid, billingid, serviceid, quantity, price, amount)"
                + " values (null,'" + sBillingID
                + "','" + sServiceID
                + "','" + txtQuantity.Text
                + "','" + dPrice
                + "','" + (dPrice * Convert.ToDouble(txtQuantity.Text)) + "')";
            frmLogIn.vTable();
            vOrder();
        }
        void vOrder()
        {
            frmLogIn.q = "SELECT subid, ss.name `NAME`,s.quantity `QTY`,s.price `PRICE`,s.amount `AMOUNT` FROM sub s left join (billing b ,`user` u, service ss, guest g) on (s.billingid = b.billingid and ss.serviceid = s.serviceid and b.userid = u.userid and b.guestid = g.guestid) where b.guestid = '"+sGuestID
                +"' and b.flag = 'Unpaid'";
            frmLogIn.vTable();
            dgOrder.DataSource = frmLogIn.dtable;
            dgOrder.Columns[0].Visible = false;
            dgOrder.Columns[3].DefaultCellStyle.Format = "c";
            dgOrder.Columns[4].DefaultCellStyle.Format = "c";
            dgOrder.Columns[0].Visible = false;
            vTotalAmount();
            lbSubTotal.Text = dTotalAmount.ToString("c");
        }
        double dTotalAmount;
        void vTotalAmount()
        {
            frmLogIn.q = "SELECT sum(s.amount) FROM sub s left join (billing b ,`user` u, service ss, guest g) on (s.billingid = b.billingid and ss.serviceid = s.serviceid and b.userid = u.userid and b.guestid = g.guestid) where b.guestid = '" + sGuestID
                + "' and b.flag = 'Unpaid'";
            frmLogIn.vTable();
            try
            {
                dTotalAmount = Convert.ToDouble(frmLogIn.dtable.Rows[0][0]);
            }
            catch
            {
                dTotalAmount = 0;
            }
        }
        void vBillingID()
        {
            try
            {
                frmLogIn.q = "SELECT billingId FROM billing where guestid = '"
                    + sGuestID + "' and flag = 'Unpaid'";
                frmLogIn.vTable();
                sBillingID = frmLogIn.dtable.Rows[0][0].ToString();
            }
            catch { sBillingID = ""; }
        }
        string sBillingID;
        string sGuestID;
        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmLogIn.q = "select guestid from guest where concat(fname,' ', mname,' ', lname) = '" + cbCustomer.Text + "'";
            frmLogIn.vTable();
            sGuestID = frmLogIn.dtable.Rows[0][0].ToString();
            vBillingID();
            vOrder();
        }
        string sServiceID;
        double dPrice;
        private void dgMenu_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sServiceID = dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[0].Value.ToString();
                dPrice = Convert.ToDouble(dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[2].Value);
            }
            catch { }
        }
        double dChange;
        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dChange = Convert.ToDouble(txtCash.Text) - dTotalAmount;
                if (dChange < 0)
                {
                    dChange = 0;
                }
            }
            catch { dChange = 0; }
            lbChange.Text = dChange.ToString("c");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtCash.Text) >= dTotalAmount)
            {
                frmLogIn.q = "insert into payment (payid, billingid, totalamount, cash, balance, `change`, date) values (null,'" + sBillingID
                    + "','" + dTotalAmount
                    + "','" + txtCash.Text
                    + "','" + 0
                    + "','" + dChange
                    + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                frmLogIn.vTable();
                frmLogIn.q = "update billing set flag = 'Paid' where billingid = '" + sBillingID + "'";
                frmLogIn.vTable();
                vOrder();
                txtCash.Text = "";
            }
            else
            {
                MessageBox.Show("The guest money is not enough.", "Insufienct funds");
                txtCash.Focus();
            }
        }
    }
}