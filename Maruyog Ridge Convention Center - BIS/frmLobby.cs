using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmLobby : Form
    {
        public frmLobby()
        {
            InitializeComponent();
        }

        string gender;
        private void timer1_Tick(object sender, EventArgs e)
        {
            gender = "Ma'am";
            if(frmLogIn.sGender =="Male")
                gender = "Sir";
            lbUser.Text = "Hi, " + gender + " " + frmLogIn.sFirstName + " " + frmLogIn.sMiddleName + " " + frmLogIn.sLastName;
            lbDate.Text = DateTime.Now.ToString("dddd MMMM dd, yyyy hh:mm:ss tt");
        }

        private void configureAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnGen.Controls.Clear();
            ucUser u = new ucUser();
            pnGen.Controls.Add(u);
            u.Dock = DockStyle.Fill;
        }

        private void configurePersonalAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucUser.sSave = "Edit";
            ucUser.vCallExpress();
        }

        private void frmLobby_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to Log - out?"
                , "Confirm log - out"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucGuest g = new ucGuest();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(g);
            g.Dock = DockStyle.Fill;
            
        }
        private void logOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void roomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucRoom r = new ucRoom();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(r);
            r.Dock = DockStyle.Fill;
        }

        private void frmLobby_Load(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }

        private void restaurantMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucResto r = new ucResto();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(r);
            r.Dock = DockStyle.Fill;
        }
        private void bnResto_Click(object sender, EventArgs e)
        {
            //wfRestaurant r = new wfRestaurant();
            //r.ShowDialog();
            ucRestaurant c = new ucRestaurant();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(c);
            c.Dock = DockStyle.Fill;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ucAccommodation a = new ucAccommodation();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(a);
            a.Dock = DockStyle.Fill;
        }

        private void bnConvention_Click(object sender, EventArgs e)
        {
            ucConvention c = new ucConvention();
            pnGen.Controls.Clear();
            pnGen.Controls.Add(c);
            c.Dock = DockStyle.Fill;
        }

        private void additionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLogIn.sutype == "Admin")
            {
                ucAddtional c = new ucAddtional();
                pnGen.Controls.Clear();
                pnGen.Controls.Add(c);
                c.Dock = DockStyle.Fill;
            }
            else
            {
                MessageBox.Show("Unable to access this form.", "Forbidden Access");
            }
        }

        private void vATAndDiscountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLogIn.sutype == "Admin")
            {
                wfVATDiscountSettings v = new wfVATDiscountSettings();
                v.ShowDialog();
            }
            else
            {
                MessageBox.Show("Unable to access this form.", "Forbidden Access");
            }
        }
    }
}