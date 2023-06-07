using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmGuest : Form
    {
        public frmGuest()
        {
            InitializeComponent();
        }



        private void frmGuest_Load(object sender, EventArgs e)
        {
            lbTitle.Text += "(" + ucGuest.Save + ")";
            if (ucGuest.Save == "Edit")
            {
                txtLname.Text = ucGuest.slname;
                txtFname.Text = ucGuest.sfname;
                txtMname.Text = ucGuest.smname;
                rtAddress.Text = ucGuest.sadd;
                txtPnumber.Text = ucGuest.sphonenum;
                txtEmail.Text = ucGuest.semailadd;
                txtCompany.Text = ucGuest.scompany;
                rtofficeaddress.Text = ucGuest.sofficeadd;
                txtTelenumber.Text = ucGuest.stelnumber;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        void vGuestID()
        {
            frmLogIn.q = "SELECT guestid FROM guest where dreg like '" + DateTime.Now.ToString("yyyy") + "%'";
            frmLogIn.vTable();
            sGuestID = DateTime.Now.ToString("yyyy") + "-" + Convert.ToInt32(frmLogIn.dtable.Rows.Count + 1).ToString("d4");
        }
        string sGuestID;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (ucGuest.Save == "Add")
                {
                    vGuestID();
                    frmLogIn.q = "insert into guest values ('" + sGuestID
                        + "','" + txtLname.Text
                        + "','" + txtFname.Text
                        + "','" + txtMname.Text
                        + "','" + rtAddress.Text
                        + "','" + txtPnumber.Text
                        + "','" + txtEmail.Text
                        + "','" + txtCompany.Text
                        + "','" + rtofficeaddress.Text
                        + "','" + txtTelenumber.Text
                        + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    frmLogIn.vTable();
                }
                else
                {
                    frmLogIn.q = "update guest set lname = '" + txtLname.Text
                        + "', fname = '" + txtFname.Text
                        + "', mname = '" + txtMname.Text
                        + "', `add` = '" + rtAddress.Text
                        + "', phonenum = '" + txtPnumber.Text
                        + "', `e-mailadd` = '" + txtEmail.Text
                        + "', company = '" + txtCompany.Text
                        + "', officeadd = '" + rtofficeaddress.Text
                        + "', telnumber = '" + txtTelenumber.Text
                        + "' where guestid = '" + ucGuest.sguestid + "'";
                    frmLogIn.vTable();
                }
                Close();
            }
            catch
            {
                MessageBox.Show("Please put \\ before \'");
            }
        }
    }
}