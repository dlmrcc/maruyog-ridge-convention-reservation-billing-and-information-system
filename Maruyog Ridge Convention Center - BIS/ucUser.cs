using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class ucUser : UserControl
    {
        public ucUser()
        {
            InitializeComponent();
        }

        private void ucUserView_Load(object sender, EventArgs e)
        {
            frmLogIn.q = "SELECT userid,"
                + " UserName `USERNAME`,"
                + " LastName,"
                + " FirstName,"
                + " MiddleName,"
                + " concat(LastName,', ', FirstName,' ', MiddleName) FULLNAME,"
                + " ContactNo `CONTACT NUMBER`,"
                + " CivilStat `CIVIL STATUS`,"
                + " Gender `GENDER`,"
                + " DateofBirth `BIRTHDATE`,"
                + " Address `ADDRESS`,"
                + " utype `TYPE`,"
                + " ustatus `STATUS`"
                + " FROM user where (concat(LastName,', ', FirstName,' ', MiddleName) like '%"
                + txtSearch.Text + "%' or ContactNo like '"
                + txtSearch.Text + "%' or CivilStat like '"
                + txtSearch.Text + "%' or Gender like '"
                + txtSearch.Text + "%' or Address like '"
                + txtSearch.Text + "%')";
            frmLogIn.vTable();
            dgUser.DataSource = frmLogIn.dtable;
            dgUser.Columns[0].Visible = false;
            dgUser.Columns[2].Visible = false;
            dgUser.Columns[3].Visible = false;
            dgUser.Columns[4].Visible = false;
            dgUser.Columns[9].DefaultCellStyle.Format = "MMM. dd, yyyy";
            lbResult.Text = dgUser.Rows.Count.ToString() + " user(s) has found!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
        public static String sSave;
        public static void vCallExpress()
        {
            frmUser u = new frmUser();
            u.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sSave = "Add";
            vCallExpress();
            ucUserView_Load(sender, e);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = print.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
        Printing print;
        private bool SetupThePrinting()//3====================================
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;
            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;
            printDocument1.DocumentName = "Pulot National High School\nList of User";
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            printDocument1.DefaultPageSettings.Landscape = MyPrintDialog.PrinterSettings.DefaultPageSettings.Landscape;
            print = new Printing(dgUser/* 5 ====================================*/
                , printDocument1, true, true, "Pulot National High School\nList of User"
                , new Font("Tahoma", 18, FontStyle.Bold,
                GraphicsUnit.Point), Color.Black, true);
            return true;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = printDocument1;
                MyPrintPreviewDialog.ShowDialog();
                printDocument1.Print();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex == -1 || cbType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select in Status or type", "Invalid Action");
            }
            else
            {
                frmLogIn.q = "update user set utype = '" + cbType.Text
                    + "', ustatus = '" + cbStatus.Text
                    + "' where userid = '" + dgUser.Rows[dgUser.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";
                frmLogIn.vTable();
                MessageBox.Show("User type and user status has been successfully change.","Update Complete");
                cbType.Text = "Type";
                cbStatus.Text = "Status";
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    }

        private void dgUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}