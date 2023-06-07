using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmLogIn : Form
    {
        public static MySqlConnection Mycon = new MySqlConnection("username = root; password =psu ; database = mridge;server = localhost;");
        public static MySqlDataAdapter MyDA = new MySqlDataAdapter();
        public static MySqlCommand Mycmd;
        public static DataTable dtable;
        public static String q;
        public static void vTable()
        {
            dtable = new DataTable();
            Mycmd = new MySqlCommand(q, Mycon);
            MyDA.SelectCommand = Mycmd;
            MyDA.Fill(dtable);
        }
        public frmLogIn()
        {
            InitializeComponent();
        }
        public static void vVat()
        {
            q = "SELECT value FROM vatdiscount";
            vTable();
            dVAT = Convert.ToDouble(dtable.Rows[0][0]);
            dSeniorDiscount = Convert.ToDouble(dtable.Rows[1][0]);
        }
        public static Double dVAT;
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public static String sLastName, sFirstName, sMiddleName, sContactNo, sCivilStat, sGender, sDateofBirth, sAddress, sUserName, sPassword, suserid, ssecques, ssecans, sutype, sustatus;
        private void button2_Click(object sender, EventArgs e)
        {
            frmLogIn.q = "select * from user where username = '" + txtUsername.Text
                + "' and password = '" + txtPassword.Text + "'";
            frmLogIn.vTable();
            if (dtable.Rows.Count > 0)
            {
                sLastName = dtable.Rows[0][0].ToString();
                sFirstName = dtable.Rows[0][1].ToString();
                sMiddleName = dtable.Rows[0][2].ToString();
                sContactNo = dtable.Rows[0][3].ToString();
                sCivilStat = dtable.Rows[0][4].ToString();
                sGender = dtable.Rows[0][5].ToString();
                sDateofBirth = dtable.Rows[0][6].ToString();
                sAddress = dtable.Rows[0][7].ToString();
                sUserName = dtable.Rows[0][8].ToString();
                sPassword = dtable.Rows[0][9].ToString();
                suserid = dtable.Rows[0][10].ToString();
                ssecques = dtable.Rows[0][11].ToString();
                ssecans = dtable.Rows[0][12].ToString();
                sutype = dtable.Rows[0][13].ToString();
                sustatus = dtable.Rows[0][14].ToString();
                if (sUserName + sPassword == txtUsername.Text + txtPassword.Text)
                {
                    if (sustatus == "Active")
                    {
                        Hide();
                        frmLobby l = new frmLobby();
                        l.WindowState = FormWindowState.Maximized;
                        l.ShowDialog();
                        lnForgotPassword.Visible = false;
                        txtPassword.Clear();
                        txtUsername.Clear();
                        txtUsername.Focus();
                        Show();
                    }
                    else
                    {
                        MessageBox.Show("Your account is currently blocked, Please contact your administrator.", "Invalid Log - In");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bad username or password."
                        , "Bad"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Warning);
                    lnForgotPassword.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Bad username or password."
                    , "Bad"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning);
                lnForgotPassword.Visible = true;
            }
        }
        public static Double dSeniorDiscount;
        private void frmLogIn_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "CanoiJoya";
            txtPassword.Text = "YonexLining";
        }

        private void lnForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            frmForgotPassword f = new frmForgotPassword();
            f.ShowDialog();
            Show();
            txtPassword.Clear();
            txtUsername.Clear();
            txtUsername.Focus();
        }
    }
}