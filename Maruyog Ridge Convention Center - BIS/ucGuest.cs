using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucGuest : UserControl
    {
        public ucGuest()
        {
            InitializeComponent();
   
        }

        private void ucGuest_Load(object sender, EventArgs e)
        {
            frmLogIn.q = "SELECT guestid,"//0
                + " lname `LASTNAME`,"//1
                + " fname `FIRSTNAME`,"//2
                + " mname `MIDDLENAME`,"//3
                + " concat(fname,' ',mname,' ',lname) GUEST,"//4
                + " `add` `RESIDENT ADDRESS`,"//5
                + " phonenum `MOBILE PHONE NUMBER`,"//6
                + " `e-mailadd` `E-MAIL ADDRESS`,"//7
                + " company `NAME OF COMPANY/OFFICE`,"//8
                + " officeadd `OFFICE ADDRESS`,"//9
                + " telnumber `TELEPHONE NUMBER`"//10
                + " FRoM guest where (lname like '%" + txtSearch.Text
                + "%' or concat(lname,', ',fname,' ',mname) like '%" + txtSearch.Text
                + "%' or `add` like  '%" + txtSearch.Text
                + "%' or phonenum like '%" + txtSearch.Text
                + "%' or `e-mailadd` like  '%" + txtSearch.Text
                + "%' or company like '%" + txtSearch.Text
                + "%' or officeadd like '%" + txtSearch.Text
                + "%' or telnumber like '%" + txtSearch.Text
                + "') and guestid != '" + "2012-0001" + "'";

            frmLogIn.vTable();
            dgCustomer.DataSource = frmLogIn.dtable;
            //dgCustomer.Columns[0].Visible = false;
            //dgCustomer.Columns[1].Visible = false;
            //dgCustomer.Columns[2].Visible = false;
            //dgCustomer.Columns[3].Visible = false;
            for (int p = 0; p < 4; p++)
                dgCustomer.Columns[p].Visible = false;
            lbResult.Text = dgCustomer.Rows.Count.ToString() + " Customer result has Found!";
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Save = "Add";
            vAddEdit(sender, e);
       
        }
        public static String Save;
        void vAddEdit(object sender, EventArgs e)
        {
            frmGuest g = new frmGuest();
            g.ShowDialog();
            ucGuest_Load(sender, e);
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Save = "Edit";
            vAddEdit(sender, e);
        }
        public static String sguestid, slname, sfname, smname, sadd, sphonenum, semailadd, scompany, sofficeadd, stelnumber, sdreg;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ucGuest_Load(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonEdit_Click(sender, e);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonAdd_Click(sender, e);
        }

        private void dgCustomer_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                sguestid = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[0].Value.ToString();
                slname = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[1].Value.ToString();
                sfname = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[2].Value.ToString();
                smname = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[3].Value.ToString();
                sadd = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[5].Value.ToString();
                sphonenum = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[6].Value.ToString();
                semailadd = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[7].Value.ToString();
                scompany = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[8].Value.ToString();
                sofficeadd= dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[9].Value.ToString();
                stelnumber = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[10].Value.ToString();
                sdreg = dgCustomer.Rows[dgCustomer.CurrentCell.RowIndex].Cells[11].Value.ToString();
            
            }
            catch { }
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
