using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmForgotPassword : Form
    {
        public frmForgotPassword()
        {
            InitializeComponent();
        }
        string sPassword, ssecans;
        private void button3_Click(object sender, EventArgs e)
        {
            if (pnSec.Visible == false)
            {
                frmLogIn.q = "SELECT password, secques, secans FROM user where username = '" + txtUsername.Text + "'";
                frmLogIn.vTable();
                if (frmLogIn.dtable.Rows.Count > 0)
                {
                    sPassword = frmLogIn.dtable.Rows[0][frmLogIn.dtable.Columns[0], DataRowVersion.Current].ToString();
                    lbSecQues.Text = frmLogIn.dtable.Rows[0][frmLogIn.dtable.Columns[1], DataRowVersion.Current].ToString();
                    ssecans = frmLogIn.dtable.Rows[0][frmLogIn.dtable.Columns[2], DataRowVersion.Current].ToString();
                    pnSec.Visible = true;
                    txtUsername.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Username doesn't exist!"
                        , "Bad Username"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Stop);
                    txtUsername.Focus();
                }
            }
            else
            {
                if (ssecans.ToUpper() == txtSecAns.Text.ToUpper())
                {
                    panel3.Visible = true;
                    panel1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Your security answer didn't match to our records"
                        , "Bad security answer"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Warning);
                    txtSecAns.Focus();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your current password is (" + sPassword + ")."
                , "Retrive Password"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information);
            Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirm.Text)
            {
                frmLogIn.q = "update user set password = '" + txtPassword.Text
                    + "' where username = '" + txtUsername.Text + "'";
                frmLogIn.vTable();
                MessageBox.Show("Your password has been successfully change."
                    , "Password changed"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Your password and confirm password didn't not match."
                    , "Bad password and confirm password"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Stop);
                txtPassword.Focus();
            }
        }
    }
}