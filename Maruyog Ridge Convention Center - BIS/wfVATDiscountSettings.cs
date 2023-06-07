using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class wfVATDiscountSettings : Form
    {
        public wfVATDiscountSettings()
        {
            InitializeComponent();
        }
        private void wfVATDiscountSettings_Load(object sender, EventArgs e)
        {            
            frmLogIn.q = "select id, NAME, VALUE `VALUE (PERCENTAGE)` from vatdiscount";
            frmLogIn.vTable();
            dgDis.DataSource = frmLogIn.dtable;
            dgDis.Columns[0].Visible = false;
            dgDis.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lbResult.Text = dgDis.RowCount + " discount VAT or Discount has found!";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmLogIn.q = "update vatdiscount set name = '" + txtName.Text
                + "', value = '" + txtValue.Text
                + "' where id = '" + dgDis.Rows[dgDis.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";
            frmLogIn.vTable();
            if (dgDis.CurrentCell.RowIndex == 0)
                frmLogIn.dSeniorDiscount = Convert.ToDouble(txtValue.Text);
            if (dgDis.CurrentCell.RowIndex == 1)
                frmLogIn.dVAT = Convert.ToDouble(txtValue.Text);
            MessageBox.Show("Transaction has been successfully change.", "Changes Saved");
            wfVATDiscountSettings_Load(sender, e);
        }
        private void dgDis_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = dgDis.Rows[dgDis.CurrentCell.RowIndex].Cells[1].Value.ToString();
                txtValue.Text = dgDis.Rows[dgDis.CurrentCell.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
        }
    }
}