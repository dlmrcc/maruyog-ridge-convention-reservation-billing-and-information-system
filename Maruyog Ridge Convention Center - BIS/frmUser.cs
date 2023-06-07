using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }
        int iYears;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            iYears = DateTime.Now.Year - dtBirthDate.Value.Year;
            if (DateTime.Now.Month < dtBirthDate.Value.Month ||
                (DateTime.Now.Month == dtBirthDate.Value.Month
                && DateTime.Now.Day < dtBirthDate.Value.Day))
                iYears--;
            lbAge.Text = "Age: " + iYears.ToString();
        }
        string uStatus;
        void vUserStatus()
        {
            frmLogIn.q = "SELECT userid FROM `user` where utype = 'Admin'";
            frmLogIn.vTable();
            if (frmLogIn.dtable.Rows.Count > 0)
                uStatus = "Front Desk";
            else
                uStatus = "Admin";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtConfirm.Text == txtPassword.Text)
            {

                if (ucUser.sSave == "Add")
                {
                    vUserStatus();
                    frmLogIn.q = "insert into user values ('" + txtLName.Text
                        + "','" + txtFName.Text
                        + "','" + txtMName.Text
                        + "','" + txtConNum.Text
                        + "','" + cbCivilStatus.Text
                        + "','" + cbGender.Text
                        + "','" + dtBirthDate.Value.ToString("yyyy-MM-dd")
                        + "','" + rtAddress.Text
                        + "','" + txtUsername.Text
                        + "','" + txtPassword.Text
                        + "',null,'" + cbSecQues.Text
                        + "','" + txtSecAns.Text
                        + "','" + uStatus 
                        + "','Active')";
                }
                else
                {
                    frmLogIn.sDateofBirth = dtBirthDate.Value.ToString("yyyy-MM-dd");
                    frmLogIn.sFirstName = txtFName.Text;
                    frmLogIn.sGender = cbGender.Text;
                    frmLogIn.sLastName = txtLName.Text;
                    frmLogIn.sMiddleName = txtMName.Text;
                    frmLogIn.sPassword = txtPassword.Text;
                    frmLogIn.ssecans = txtSecAns.Text;
                    frmLogIn.ssecques = cbSecQues.Text;
                    frmLogIn.sUserName = txtUsername.Text;
                    frmLogIn.q = "update user set LastName = '" + txtLName.Text
                        + "', FirstName = '" + txtFName.Text
                        + "', MiddleName = '" + txtMName.Text
                        + "', ContactNo = '" + txtConNum.Text
                        + "', CivilStat = '" + cbCivilStatus.Text
                        + "', Gender = '" + cbGender.Text
                        + "', DateofBirth = '" + dtBirthDate.Value.ToString("yyyy-MM-dd")
                        + "', Address = '" + rtAddress.Text
                        + "', UserName = '" + txtUsername.Text
                        + "', Password = '" + txtPassword.Text
                        + "', secques = '" + cbSecQues.Text
                        + "', secans = '" + txtSecAns.Text
                        + "' where userid = '" + frmLogIn.suserid + "'";
                }
                try
                {
                    frmLogIn.vTable();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Please put a back slash (\\) in every a single quote ('), Place it before the single quote (')."
                        , "Error has found"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Password and Confirmed password didn't match.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        void vCallCivilStatus()
        {
            cbCivilStatus.Items.Clear();
            frmLogIn.q = "select distinct CivilStat from user order by CivilStat";
            frmLogIn.vTable();
            for (int q = 0; q < frmLogIn.dtable.Rows.Count; q++)
                cbCivilStatus.Items.Add(frmLogIn.dtable.Rows[q][frmLogIn.dtable.Columns[0], DataRowVersion.Current]);
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            vCallCivilStatus();
            lbTitle.Text = "User Configuration - Create User";
            Text = "Maruyog_Ridge_Convention_Center___BIS Create User Accounts Form";
            if (ucUser.sSave == "Edit")
            {
                lbTitle.Text = "User Configuration - Modify User";
                Text = "Maruyog_Ridge_Convention_Center___BIS Modify User Accounts Form";
                txtConfirm.Text = frmLogIn.sPassword;
                txtConNum.Text = frmLogIn.sContactNo;
                txtFName.Text = frmLogIn.sFirstName;
                txtLName.Text = frmLogIn.sLastName;
                txtMName.Text = frmLogIn.sMiddleName;
                txtPassword.Text = frmLogIn.sPassword;
                txtSecAns.Text = frmLogIn.ssecans;
                txtUsername.Text = frmLogIn.sUserName;
                cbCivilStatus.Text = frmLogIn.sCivilStat;
                cbGender.Text = frmLogIn.sGender;
                cbSecQues.Text = frmLogIn.ssecques;
                dtBirthDate.Text = frmLogIn.sDateofBirth;
                rtAddress.Text = frmLogIn.sAddress;
            }
        }
    }
}