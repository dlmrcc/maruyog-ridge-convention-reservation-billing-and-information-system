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
    public partial class ucAccommodation : UserControl
    {
        public ucAccommodation()
        {
            InitializeComponent();
        }
        void vGuest()
        {
            frmLogIn.q = "SELECT guestid,"
                + " concat(lname, ', ',fname, '-',mname) `CUSTOMER`,"
                + " company `COMPANY`"
                + " FROM guest where guestid != '2012-0001'"
                + " and (concat(lname, ', ',fname, '-',mname) like '%" + txtSearchCust.Text
                + "%' or company like '%" + txtSearchCust.Text
                + "%') order by lname, fname";
            frmLogIn.vTable();
            dgGuest.DataSource = frmLogIn.dtable;
            dgGuest.Columns[0].Visible = false;
            lbSearchCustomer.Text = dgGuest.Rows.Count + " customer result has found!";
        }
        void vAdditionalPax()
        {
            frmLogIn.q = "SELECT Price FROM service where kind = 'Additional' and category = 'Accommodation'";
            frmLogIn.vTable();
            dAddPrice = Convert.ToDouble(frmLogIn.dtable.Rows[0][0]);
        }
        private void ucAccommodation_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            vAdditionalPax();
            vGuest();
            nmStay.Value = 1;
            dtCheckIn.Text = DateTime.Now.ToString("MMM. dd, yyyy");
            vRooms();
            dtCheckOut_ValueChanged(sender, e);
            nmRegPax_ValueChanged(sender, e);
            txtCash.Text = "0";
        }
        Double ddPrice = 0;
        String sWhere;
        private void txtSearchRoom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ddPrice = Convert.ToDouble(txtSearchRoom.Text);
            }
            catch { ddPrice = 0; }
            sWhere = " and (name like '" + txtSearchRoom.Text
                + "%' or type like '" + txtSearchRoom.Text
                + "%' or category like '" + txtSearchRoom.Text
                + "%' or price between '" + (ddPrice - 500) + "' and '" + (ddPrice + 500) + "')";
            ucAccommodation_Load(sender, e);
        }
        string sRoomID, sName, sType, sLimit, sMax;
        private void dgRooms_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sRoomID = dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sName = dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[1].Value.ToString();
                sType = dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[2].Value.ToString();
                sLimit = dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[3].Value.ToString();
                sMax = dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[4].Value.ToString();
                dRoomPrice = Convert.ToDouble(dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[5].Value);
                nmChild.Value = 0;
                nmRegPax.Value = 1;
                nmSen.Value = 0;
                nmRegPax_ValueChanged(sender, e);
            }
            catch { }
        }
        void vRooms()
        {
            frmLogIn.q = "SELECT serviceid,"
                + " name `ROOM NUMBER`,"
                + " type `TYPE`,"
                + " `limit` `PAX LIMIT`,"
                + " max `MAX`,"
                + " price `RATE`"
                + " FROM service where class = 'Room' and category = 'Accommodation' and kind = 'Regular'"
                + sWhere + " and serviceid not in (SELECT a.serviceid FROM acco a left join sub s on s.subid = a.subid where a.checkin <= '"
                + dtCheckIn.Value.ToString("yyyy-MM-dd") + "' and a.checkout >= '"
                + dtCheckOut.Value.ToString("yyyy-MM-dd") + "' and s.void = 'N') order by price, name";
            frmLogIn.vTable();
            dgRooms.DataSource = frmLogIn.dtable;
            dgRooms.Columns[0].Visible = false;
            dgRooms.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgRooms.Columns[5].DefaultCellStyle.Format = "c";
            lbResultRoom.Text = dgRooms.RowCount + " room result has found.";
        }
        private void bnAddGuest_Click(object sender, EventArgs e)
        {
            ucGuest.Save = "Add";
            frmGuest g = new frmGuest();
            g.ShowDialog();
            vGuest();
        }
        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            vGuest();
        }

        ToolTip r = new ToolTip();
        private void dtCheckOut_ValueChanged(object sender, EventArgs e)
        {
            if (dtCheckOut.Value <= dtCheckIn.Value)
            {
                dtCheckOut.Value = dtCheckIn.Value.AddDays((double)nmStay.Value);
            }
            else
            {
                TimeSpan ts = dtCheckOut.Value - dtCheckIn.Value;
                try
                {
                    r.RemoveAll();
                    nmStay.Value = Convert.ToInt32(ts.TotalDays);
                }
                catch
                {
                    nmStay.Value = 30;
                    dtCheckOut.Value = DateTime.Now.AddDays(30);
                    r.Show("Guest stay mst not be more than thirty (30), therefore the system reset it to thirty (30)", nmStay);
                }
            }
            vRooms();
            try
            {
                nmRegPax_ValueChanged(sender, e);
            }
            catch { }
        }
        private void nmStay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
        private void nmStay_ValueChanged(object sender, EventArgs e)
        {
            dtCheckOut.Value = dtCheckIn.Value.AddDays(Convert.ToInt32(nmStay.Value));
        }
        private void nmSen_ValueChanged(object sender, EventArgs e)
        {
            nmRegPax_ValueChanged(sender, e);
        }
        private void nmRegPax_ValueChanged(object sender, EventArgs e)
        {
            dMaxPax = Convert.ToInt32(nmChild.Value) + Convert.ToInt32(nmRegPax.Value) + Convert.ToInt32(nmSen.Value);
            int iMax = Convert.ToInt32(sMax);
            if (dMaxPax == iMax)
            {
                nmRegPax.Maximum = dMaxPax - (Convert.ToInt32(nmChild.Value) + Convert.ToInt32(nmSen.Value));
                nmSen.Maximum = dMaxPax - (Convert.ToInt32(nmRegPax.Value) + Convert.ToInt32(nmChild.Value));
                nmChild.Maximum = dMaxPax - (Convert.ToInt32(nmRegPax.Value) + Convert.ToInt32(nmSen.Value));
            }
            else
            {
                if ((nmChild.Value >= 0) && nmRegPax.Value == 0 && nmSen.Value == 0)
                    try
                    {
                        nmChild.Value = 0;
                        nmRegPax.Value = 1;
                        nmSen.Value = 0;
                    }
                    catch
                    {
                        nmRegPax.Maximum = 1;
                        nmRegPax.Value = 1;
                    }
                nmRegPax.Maximum = iMax;
                nmSen.Maximum = iMax;
                nmChild.Maximum = iMax;
            }
            lbPax.Text = "Pax (" + dMaxPax + ")";
            vFormula();
        }
        private void nmChild_ValueChanged(object sender, EventArgs e)
        {
            nmRegPax_ValueChanged(sender, e);
        }
        int dMaxPax;//total ng pax
        double dTotalPrice;//para sa total price
        double dIndividualPrice;//para sa individual price
        double dAddPrice;//para sa additional pax price
        double dRoomPrice;//para sa price ng room
        double dTotalAddPaxPrice;//para sa total ng additional price
        double dDiscount;
        double dDiscountedPrice;
        double dDiscountTotal;
        double dDiscountedPriceTotal;
        double dTotalAmount;
        double dTotalIndividualPrice;
        double dTotalLessDiscount;
        double dVAT;
        double dVATable;
        int iAdditionalPaxCounter;
        int iMaxPaxLessChild;
        void vFormula()
        {
            iMaxPaxLessChild = dMaxPax - (int)nmChild.Value;
            iAdditionalPaxCounter = iMaxPaxLessChild - Convert.ToInt32(sLimit);
            if (iAdditionalPaxCounter < 0)
                iAdditionalPaxCounter = 0;
            dTotalAddPaxPrice = (iAdditionalPaxCounter * dAddPrice);
            dTotalPrice = (dRoomPrice + dTotalAddPaxPrice) * (double)nmStay.Value;
            dIndividualPrice = (dTotalPrice / iMaxPaxLessChild);
            if (nmSen.Value > 0)
            {
                dDiscount = (frmLogIn.dSeniorDiscount / 100) * dIndividualPrice;
                dDiscountTotal = dDiscount * (double)nmSen.Value;
                dDiscountedPrice = ((100 - frmLogIn.dSeniorDiscount) / 100) * dIndividualPrice;
                dDiscountedPriceTotal = dDiscountedPrice * (double)nmSen.Value;
            }
            else
            {
                dDiscount = 0;
                dDiscountTotal = 0;
                dDiscountedPrice = 0;
                dDiscountedPriceTotal = 0;
            }
            lbOriginalPrice.Text = dTotalPrice.ToString("c");
            lbIndividualPrice.Text = dIndividualPrice.ToString("c");
            lbDiscount.Text = dDiscount.ToString("c");
            lbDiscountedPrice.Text = dDiscountedPrice.ToString("c");
            dTotalAmount = (dIndividualPrice * (double)nmRegPax.Value) + (dDiscountedPrice * (double)nmSen.Value);
            lbTotalDiscount.Text = dDiscountTotal.ToString("c");
            lbTotalDiscountedPrice.Text = dDiscountedPriceTotal.ToString("c");
            dTotalIndividualPrice = dIndividualPrice * (double)nmRegPax.Value;
            lbTotalIndividualPrice.Text = dTotalIndividualPrice.ToString("c");
            dTotalLessDiscount = dTotalIndividualPrice + dDiscountedPriceTotal;
            lbTotalAmount.Text = dTotalLessDiscount.ToString("c");
            frmLogIn.vVat();
            dVAT = (frmLogIn.dVAT / 100) * dTotalAmount;
            dVATable = (((double)100 - frmLogIn.dVAT) / 100) * dTotalAmount;
            //lbpAmount.Text = dGrandTotal.ToString("c");
        }
        double dCash;
        double dpBalance;
        double dpChange;
        double dpVAT;
        double dpVATable;
        void vPayment()
        {
            dCash = Convert.ToDouble(txtCash.Text);            
                frmLogIn.vVat();
            if (dGrandTotal > dCash)
            {
                dpBalance = dGrandTotal - dCash;
                dpChange = 0;
                dpVAT = ( frmLogIn.dVAT/100) * dCash;
                dpVATable = ((100 - frmLogIn.dVAT) / 100) * dCash;
            }
            else
            {
                dpBalance = 0;
                dpChange = dCash - dGrandTotal;
                dpVAT = (frmLogIn.dVAT / 100) * dGrandTotal;
                dpVATable = ((100 - frmLogIn.dVAT) / 100) * dGrandTotal;
            }
            lbpBalance.Text = dpBalance.ToString("c");
            lbpChange.Text = dpChange.ToString("c");
            lbpVAT.Text = dpVAT.ToString("c");
            lbpVATable.Text = dpVATable.ToString("c");
        }
        private void bnAddRoom_Click(object sender, EventArgs e)
        {
            ucRoom.sSave = "Add";
            frmRoomSettings r = new frmRoomSettings();
            r.ShowDialog();
            vRooms();
        }
        string sGuestID;
        private void dgCustomer_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sGuestID = dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value.ToString();
                vAccommodation();
                vFormula();
                vGrandTotal();
                vPayment();
            }
            catch { }
        }
        private void bnAccommodate_Click(object sender, EventArgs e)
        {
            string sDateOccupied = "";
            TimeSpan ts = dtCheckOut.Value - dtCheckIn.Value;
            for (int a = 1; a < (int)ts.TotalDays; a++)
            {
                frmLogIn.q = "SELECT serviceid FROM acco where void = 'N' and checkin <= '" + dtCheckIn.Value.AddDays(a).ToString("yyyy-MM-dd")
                    + "' and checkout >= '" + dtCheckIn.Value.AddDays(a + 1).ToString("yyyy-MM-dd") + "' and serviceid = '"
                    + dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[0].Value + "'";
                frmLogIn.vTable();
                if (frmLogIn.dtable.Rows.Count > 0)
                    sDateOccupied += dtCheckIn.Value.AddDays(a).ToString("MMMM dd, yyyy") + "\n";
            }
            if (sDateOccupied != "")
            {
                MessageBox.Show("Room number " + dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[1].Value
                    + " has already occupied bt the following date:\n" + sDateOccupied
                    + "\nPlease select another date or room"
                    , "Room number " + dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[1].Value + " has been occupied"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            else
            {
                ucRestaurant.sGuestID = dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value.ToString();
                ucRestaurant.vBillingID();
                if (dgAccommodate.RowCount == 0)
                {
                    try
                    {
                        frmLogIn.q = "insert into billing (billingId, userid, guestid, flag, date) values ('" + ucRestaurant.sBillingID
                            + "','" + frmLogIn.suserid
                            + "','" + dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value
                            + "','" + "Unpaid"
                            + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        frmLogIn.vTable();
                    }
                    catch { }
                }
                ucRestaurant.vSubID();
                frmLogIn.q = "insert into sub (subid, billingid, serviceid, qty, price, amount, discount,"
                    + " discountedprice, vat, vatable, void, payflag) values ('" + ucRestaurant.sSubID
                    + "','" + ucRestaurant.sBillingID
                    + "','" + dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[0].Value
                    + "','" + dMaxPax
                    + "','" + (dTotalLessDiscount / (double)(nmRegPax.Value + nmSen.Value))
                    + "','" + dTotalLessDiscount
                    + "','" + dDiscountTotal
                    + "','" + dDiscountedPriceTotal
                    + "','" + dVAT
                    + "','" + dVATable
                    + "','N','N')";
                frmLogIn.vTable();
                vAccoID();
                frmLogIn.q = "insert into acco (accoid, subid, checkin, checkout, regpax, senior, child, serviceid,void) values ('" + sAccoID
                    + "','" + ucRestaurant.sSubID
                    + "','" + dtCheckIn.Value.ToString("yyyy-MM-dd")
                    + "','" + dtCheckOut.Value.ToString("yyyy-MM-dd")
                    + "','" + nmRegPax.Value
                    + "','" + nmSen.Value
                    + "','" + nmChild.Value
                    + "','" + dgRooms.Rows[dgRooms.CurrentCell.RowIndex].Cells[0].Value 
                    + "','N')";
                frmLogIn.vTable();
                vRooms();
                vAccommodation();
            }
        }
        public static String sAccoID;
        public static void vAccoID()
        {
            frmLogIn.q = "SELECT accoid FROM acco where checkin like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
            frmLogIn.vTable();
            sAccoID = DateTime.Now.ToString("yyyy-") + (frmLogIn.dtable.Rows.Count + 1).ToString("d5");
        }
        void vAccommodation()
        {
            frmLogIn.q = "SELECT s.subid,"
                + " sr.name `ROOM NUMBER`,"
                + " a.checkin `CHECK IN`,"
                + " a.checkout `CHECK OUT`,"
                + " a.regpax `REGULAR`,"
                + " a.senior `SENIOR`,"
                + " a.child `CHILD`,"
                + " s.amount `AMOUNT`,"
                + " a.accoid"
                + " FROM acco a left join (billing b, sub s, service sr, guest g)"
                + " on (b.billingid = s.billingid and g.guestid = b.guestid and sr.serviceid = s.serviceid"
                + " and a.serviceid = sr.serviceid and a.subid = s.subid)"
                + " where sr.class = 'Room' and b.flag = 'Unpaid' and s.void = 'N' and b.guestid = '" + sGuestID
                + "' and s.payflag = 'N'";
            frmLogIn.vTable();
            dgAccommodate.DataSource = frmLogIn.dtable;
            dgAccommodate.Columns[0].Visible = false;
            dgAccommodate.Columns[2].DefaultCellStyle.Format = "MMM. dd, yyyy";
            dgAccommodate.Columns[3].DefaultCellStyle.Format = "MMM. dd, yyyy";
            dgAccommodate.Columns[7].DefaultCellStyle.Format = "c";
            dgAccommodate.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgAccommodate.Columns[8].Visible = false;
            lbResulltAcco.Text = dgAccommodate.RowCount + " accommodation result has found.";
            vGrandTotal();
        }

        private void dtCheckIn_ValueChanged(object sender, EventArgs e)
        {
            if (dtCheckIn.Value < DateTime.Now)
                dtCheckIn.Value = DateTime.Now;
            else
            {
                dtCheckOut.Value = dtCheckIn.Value.AddDays((double)nmStay.Value);
            }
            vRooms();
        }
        double dGrandTotal;
        void vGrandTotal()
        {
            frmLogIn.q = "SELECT sum(s.qty),"
                + " sum(s.amount),"
                + " sum(s.discount),"
                + " sum(s.discountedprice),"
                + " sum(s.vat),"
                + " sum(s.vatable),"
                + " sum(a.regpax),"
                + " sum(a.senior),"
                + " sum(a.child)"
                + " FROM sub s left join (acco a, billing b,service se, guest g)"
                + " on (s.subid = a.subid and s.billingid = b.billingid and b.guestid"
                + " = g.guestid and s.serviceid = se.serviceid) where b.guestid = '"
                + dgGuest.Rows[dgGuest.CurrentCell.RowIndex].Cells[0].Value
                + "' and s.void = 'N' and s.payflag = 'N'";
            frmLogIn.vTable();
            if (dgAccommodate.Rows.Count > 0)
            {
                dGrandTotal = Convert.ToDouble(frmLogIn.dtable.Rows[0][1]);
                lbGrandAmount.Text = dGrandTotal.ToString("c");
                lbGrandPax.Text = frmLogIn.dtable.Rows[0][0].ToString()
                    + " (R: " + frmLogIn.dtable.Rows[0][6].ToString()
                    + " S: " + frmLogIn.dtable.Rows[0][7].ToString()
                    + " C: " + frmLogIn.dtable.Rows[0][8].ToString()
                    + ")";
                lbGrandDiscount.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][2]).ToString("c");
                lbGrandDiscountedPrice.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][3]).ToString("c");
                lbGrandVAT.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][4]).ToString("c");
                lbGrandVATable.Text = Convert.ToDouble(frmLogIn.dtable.Rows[0][5]).ToString("c");
                lbpAmount.Text = dGrandTotal.ToString();
            }
            else
            {
                dGrandTotal = 0;
                lbGrandAmount.Text = "0.00Php";
                lbGrandPax.Text = "0"
                    + " (R: " + "0"
                    + " S: " + "0"
                    + " C: " + "0"
                    + ")";
                lbGrandDiscount.Text = "0.00Php";
                lbGrandDiscountedPrice.Text = "0.00Php";
                lbGrandVAT.Text = "0.00Php";
                lbGrandVATable.Text = "0.00Php";
                lbpAmount.Text = "0.00Php";
            }
        }
        private void bnVoid_Click(object sender, EventArgs e)
        {
            frmLogIn.q = "update sub set void = 'Y' where subid = '"
                + dgAccommodate.Rows[dgAccommodate.CurrentCell.RowIndex].Cells[0].Value + "'";
            frmLogIn.vTable();
            frmLogIn.q = "update acco set void = 'Y' where subid = '"
                + dgAccommodate.Rows[dgAccommodate.CurrentCell.RowIndex].Cells[8].Value + "'";
            frmLogIn.vTable();
            vRooms();
            vAccommodation();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                vPayment();
            }
            catch { txtCash.Text = "0"; }
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            txtCash.Text = Convert.ToDouble(txtCash.Text).ToString("n");
        }
    }
}