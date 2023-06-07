using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucRestaurant : UserControl
    { 
        public static String sBillingID;
        public static String sSubID;
        int iQuantity;
        double dTotalPrice;
        double dDiscount;
        double dDiscountedPrice;
        double dTotalDiscount;
        double dTotalDiscountedPrice;
        double dOriginalPrice;
        double dTotalIndividualPrice;
        double dVAT;
        double dVATable;
        double dMenuPrice;
        public ucRestaurant()
        {
            InitializeComponent();
        }
        void vMenu()
        {
            frmLogIn.q = "SELECT serviceid, name `Menu`, price `PRICE` FROM service where"
                + " class = 'resto' and type like '%" + cbCategory.Text + "%'";
            frmLogIn.vTable();
            dgMenu.DataSource = frmLogIn.dtable;
            dgMenu.Columns[0].Visible = false;
            dgMenu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgMenu.Columns[2].DefaultCellStyle.Format = "c";
            lbResultMenu.Text = dgMenu.RowCount + " menu result has been found.";
        }
        private void dgMenu_SelectionChanged(object sender, EventArgs e)  
        {
            try
            {
                dMenuPrice = Convert.ToDouble(dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[2].Value);
            }
            catch { }
        }
        private void ucRestaurant_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            vCategory();
            vGuest();
            nmRegPerson.Value = 1;
        }
        void vCategory()
        {
            cbCategory.Items.Clear();
            frmLogIn.q = "SELECT distinct type FROM service where class = 'Resto' order by type";
            frmLogIn.vTable();
            for (int a = 0; a < frmLogIn.dtable.Rows.Count; a++)
                cbCategory.Items.Add(frmLogIn.dtable.Rows[a][0]);
            try
            {
                cbCategory.SelectedIndex = 0;
            }
            catch { }
        }
        void vGuest()
        {
            frmLogIn.q = "SELECT guestid,"
                +" concat(fname,' ',mname,' ',lname) `GUEST`,"
                +" company `COMPANY NAME`,"
                +" `add` `ADDRESS`,"
                +" phonenum `PHONE #`,"
                +" `e-mailadd` `E-MAIL`,"
                +" officeadd `OFFICE ADDRESS`,"
                +" telnumber `TELEPHONE #` FROM"
                + " guest where (concat(fname,' ',mname,' ',lname) like '%" + txtSearchGuest.Text
                + "%' or company like '%" + txtSearchGuest.Text
                + "%' or guestid like '" + txtSearchGuest.Text
                + "%') order by lname";
            frmLogIn.vTable();
            dgGuest.DataSource = frmLogIn.dtable;
            dgGuest.Columns[0].Visible = false;
            lbResultGuest.Text = dgGuest.RowCount + " guest result has been found!";
        }
        private void txtSearchMenu_TextChanged(object sender, EventArgs e)
        {
            vMenu();
        }
        private void txtSearchGuest_TextChanged(object sender, EventArgs e)
        {
            vGuest();
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            vMenu();
        }
        private void dgGuest_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                vOrderList();
            }
            catch { }
        }
        private void bnAddGuest_Click(object sender, EventArgs e)
        {
            ucGuest.Save = "Add";
            frmGuest g = new frmGuest();
            g.ShowDialog();
            vGuest();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ucResto.sSave = "Add";
            frmRestaurantMenu r = new frmRestaurantMenu();
            r.ShowDialog();
            vCategory();
        }
        private void nmSenior_ValueChanged(object sender, EventArgs e)
        {
            nmRegPerson_ValueChanged(sender, e);
        }
        private void nmRegPerson_ValueChanged(object sender, EventArgs e)
        {
            if (nmSenior.Value == 0 && nmRegPerson.Value == 0)
                nmRegPerson.Value = 1;
            iQuantity = Convert.ToInt32(nmRegPerson.Value + nmSenior.Value);
            lbQty.Text = "Quantity (" + iQuantity + ")";
            vFormula();
        }
        void vFormula()
        {
            frmLogIn.vVat();
            dOriginalPrice = dMenuPrice * iQuantity;
            dTotalIndividualPrice = dMenuPrice * Convert.ToDouble(nmRegPerson.Value);
            if (nmSenior.Value > 0)
            {
                dDiscount = ((frmLogIn.dSeniorDiscount) / 100) * dMenuPrice;
                dDiscountedPrice = ((100 - frmLogIn.dSeniorDiscount) / 100) * dMenuPrice;
                dTotalDiscount = ((frmLogIn.dSeniorDiscount) / 100) * (dMenuPrice * Convert.ToDouble(nmSenior.Value));
                dTotalDiscountedPrice = ((100 - frmLogIn.dSeniorDiscount) / 100) * (dMenuPrice * Convert.ToDouble(nmSenior.Value));
            }
            else
            {
                dDiscount = 0;
                dDiscountedPrice = 0;
                dTotalDiscount = 0;
                dTotalDiscountedPrice = 0;
            }
            dTotalPrice = dTotalIndividualPrice + dTotalDiscountedPrice;
            dVAT = (Convert.ToDouble(frmLogIn.dVAT) / 100) * dTotalPrice;
            dVATable = ((100 - Convert.ToDouble(frmLogIn.dVAT)) / 100) * dTotalPrice;
            lbTotalPrice.Text = dTotalPrice.ToString("c");
            lbOriginalPrice.Text = dOriginalPrice.ToString("c");
            lbIndividualPrice.Text = dMenuPrice.ToString("c");
            lbDiscount.Text = dDiscount.ToString("c");
            lbTotalDiscount.Text = dTotalDiscount.ToString("c");
            lbDiscountPrice.Text = dDiscountedPrice.ToString("c");
            lbTotalDiscountedPrice.Text = dTotalDiscountedPrice.ToString("c");
            lbTotalIndividualPrice.Text = dTotalIndividualPrice.ToString("c");
        }
        public static void vBillingID()
        {
            frmLogIn.q = "select billingid from billing where guestid = '" + sGuestID
                + "' and flag = 'Unpaid'";
            frmLogIn.vTable();
            try
            {
                sBillingID = frmLogIn.dtable.Rows[0][0].ToString();
            }
            catch
            {
                frmLogIn.q = "SELECT billingId FROM billing where date like '" + DateTime.Now.ToString("yyyy") + "%'";
                frmLogIn.vTable();
                sBillingID = DateTime.Now.ToString("2012-") + (frmLogIn.dtable.Rows.Count + 1).ToString("d5");
            }
        }
        public static String sGuestID;
        void vOrderList()
        {
            frmLogIn.q = "SELECT s.subid,s.billingid, sr.name `MENU`, s.qty `QUANTITY`,s.price `PRICE`, s.amount `AMOUNT` FROM sub s left join (service sr, billing b, guest g, user u)"
                + " on (s.serviceid = sr.serviceid and s.billingid = b.billingid and b.guestid = g.guestid and u.userid"
                + " = b.userid) where b.flag = 'Unpaid' and b.guestid = '"
                + dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value 
                + "' and void = 'N' and sr.class = 'Resto' and s.payflag = 'N'";
            frmLogIn.vTable();
            dgOrderList.DataSource = frmLogIn.dtable;
            dgOrderList.Columns[0].Visible = false;
            dgOrderList.Columns[1].Visible = false;
            dgOrderList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgOrderList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgOrderList.Columns[4].DefaultCellStyle.Format = "c";
            dgOrderList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgOrderList.Columns[5].DefaultCellStyle.Format = "c";
            lbResultOrder.Text = dgOrderList.RowCount + " order list has found!";
            vGrandTotal();
        }
        public static void vSubID()
        {
            frmLogIn.q = "SELECT subid FROM sub s left join billing b on b.billingid = s.billingid where b.date like '" + DateTime.Now.ToString("yyyy") + "%'";
            frmLogIn.vTable();
            sSubID = DateTime.Now.ToString("yyyy-") + (frmLogIn.dtable.Rows.Count + 1).ToString("d5");
        }
        private void bnAddOrder_Click(object sender, EventArgs e)
        {
            int iCounter = 0;
            for (int a = 0; a < dgOrderList.RowCount; a++)
            {
                if (dgOrderList.Rows[a].Cells[2].Value.ToString() ==
                    dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[1].Value.ToString())
                {
                    iCounter++;
                    break;
                }
            }
            if (iCounter > 0)
            {
                MessageBox.Show("The guest has already bought " 
                    + dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[1].Value.ToString()
                    ,"Unable to buy same menu");
            }
            else
            {
                if (dgOrderList.Rows.Count == 0)
                {
                    sGuestID = dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    vBillingID(); 
                    try
                    {
                        frmLogIn.q = "insert into billing (billingId, userid, guestid, flag, date) values ('"
                            + sBillingID + "','" + frmLogIn.suserid
                            + "','" + dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value
                            + "','Unpaid','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        frmLogIn.vTable();
                    }
                    catch { vBillingID(); }
                }
                else
                    sBillingID = dgOrderList.Rows[dgOrderList.CurrentCell.RowIndex].Cells[1].Value.ToString();
                vSubID();
                frmLogIn.q = "insert into sub (subid, billingid, serviceid, qty, price,"
                    + " amount, discount, discountedprice, vat, vatable, void, payflag) values ('" + sSubID
                    + "','" + sBillingID
                    + "','" + dgMenu.Rows[dgMenu.CurrentCell.RowIndex].Cells[0].Value
                    + "','" + iQuantity
                    + "','" + dTotalPrice / iQuantity
                    + "','" + dTotalPrice
                    + "','" + dTotalDiscount
                    + "','" + (dTotalDiscountedPrice + dTotalIndividualPrice)
                    + "','" + dVAT
                    + "','" + dVATable
                    + "','N','N')";
                frmLogIn.vTable();
                vOrderList();
            }
        }
        void vGrandTotal()
        {
            frmLogIn.q = "SELECT sum(s.amount), sum(s.qty), sum(s.discount), sum(s.discountedprice),"
                + " sum(s.vat), sum(s.vatable) FROM sub s left join (service sr, billing b, guest g,"
                + " user u) on (s.serviceid = sr.serviceid and s.billingid = b.billingid and"
                + " b.guestid = g.guestid and u.userid = b.userid) where b.flag = 'Unpaid' and b.guestid = '"
                + dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value
                + "' and void = 'N' and sr.class = 'Resto' and s.payflag = 'N'";
            frmLogIn.vTable();
            try
            {
                lbGrandAmount.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][0]).ToString("c");
                lbGrandQuantity.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][1]).ToString();
                lbGrandDiscount.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][2]).ToString("c");
                lbGrandDiscountedPrice.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][3]).ToString("c");
                lbTotalVAT.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][4]).ToString("c");
                lbTotalVATable.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][5]).ToString("c");
            }
            catch
            {
                lbGrandAmount.Text = "0.00Php";
                lbGrandQuantity.Text = "0";
                lbGrandDiscount.Text = "0.00Php";
                lbGrandDiscountedPrice.Text = "0.00Php";
                lbTotalVAT.Text = "0.00Php";
                lbTotalVATable.Text = "0.00Php";
            }
        }

        private void bnVoid_Click(object sender, EventArgs e)
        {
            frmLogIn.q = "update sub set void = 'Y' where subid = '" 
                + dgOrderList.Rows[dgOrderList.CurrentCell.RowIndex].Cells[0].Value + "'";
            frmLogIn.vTable();
            vOrderList();
        }

        private void bnPayment_Click(object sender, EventArgs e)
        {

        }
    }
}