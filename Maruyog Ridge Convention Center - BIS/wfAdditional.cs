using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class wfAdditional : Form
    {
        public wfAdditional()
        {
            InitializeComponent();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            frmLogIn.q = "update service set name = '" + txtName.Text
                + "', type = '" + cbType.Text
                + "', category = '" + cbCategory.Text
                + "', price = '" + dPrice
                + "' where serviceid = '" + ucAddtional.sServiceID + "'";
            frmLogIn.vTable();
            Close();
        }
        private void wfAdditional_Load(object sender, EventArgs e)
        {
            txtName.Text = ucAddtional.sName;
            dPrice = Convert.ToDouble(ucAddtional.sPrice);
            txtPrice.Text = dPrice.ToString("n");
            cbCategory.Text = ucAddtional.sCategory;
            cbType.Text = ucAddtional.sType;
        }
        double dPrice;
        private void txtPrice_Leave(object sender, EventArgs e)
        {            
            try
            {
                dPrice = Convert.ToDouble(txtPrice.Text);
            }
            catch { dPrice = 0; }
            txtPrice.Text = dPrice.ToString("n");
        }
    }
}
