using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class frmRestaurantMenu : Form
    {
        public frmRestaurantMenu()
        {
            InitializeComponent();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        void vCategory()
        {
            cbCategory.Items.Clear();
            frmLogIn.q = "SELECT distinct `type` FROM service where class = 'resto' order by `type`";
            frmLogIn.vTable();
            for (int q = 0; q < frmLogIn.dtable.Rows.Count; q++)
                cbCategory.Items.Add(frmLogIn.dtable.Rows[q][0]);
        }
        private void wfRestaurantMenu_Load(object sender, EventArgs e)
        {
            vCategory();
            if (ucResto.sSave == "Edit")
            {
                txtName.Text = ucResto.sName;
                txtPrice.Text = ucResto.dPrice.ToString("n");
                cbCategory.Text = ucResto.sCategory;
            }
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (ucResto.sSave == "Add")
            {//serviceid, name, type, class, price, max, category, limit, kind
                frmLogIn.q = "insert into service"
                    + " (serviceid, name, type, class, price, max, category,`limit`,kind)"
                    + " values (null,'" + txtName.Text
                    + "','" + cbCategory.Text
                    + "','" + "Resto"
                    + "','" + txtPrice.Text
                    + "','" + 0
                    + "','','0','Regular')";
            }
            else
            {
                frmLogIn.q = "update service set name = '" + txtName.Text
                    + "', type = '" + cbCategory.Text
                    + "', price = '" + dPrice
                    + "' where serviceid = '" + ucResto.sId + "'";
            }
            frmLogIn.vTable();
            Close();
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