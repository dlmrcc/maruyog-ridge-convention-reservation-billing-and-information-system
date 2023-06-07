using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
namespace Maruyog_Ridge_Convention_Center___BIS
{
    public partial class wfConvention : Form
    {
        public wfConvention()
        {
            InitializeComponent();
        }
        void vName()
        {
            frmLogIn.q = "SELECT name FROM service where category = 'Convention' and class = 'Room' and kind = 'Regular' order by name";
            frmLogIn.vTable();
            for (int a = 0; a < frmLogIn.dtable.Rows.Count; a++)
                cbName.Items.Add(frmLogIn.dtable.Rows[a][0]);
        }
        void vType()
        {
            frmLogIn.q = "SELECT type FROM service where category = 'Convention' and class = 'Room' and kind = 'Regular' order by type";
            frmLogIn.vTable();
            for (int a = 0; a < frmLogIn.dtable.Rows.Count; a++)
                cbType.Items.Add(frmLogIn.dtable.Rows[a][0]);
        }
        private void wfConvention_Load(object sender, EventArgs e)
        {
            lbTitle.Text += " (" + ucConvention.sSave + ")";
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            vName();
            vType();
            if (ucConvention.sSave == "Edit")
            {
                cbName.Text = ucConvention.sname;
                txtMax.Text = ucConvention.smax;
                txtPrice.Text = ucConvention.sprice;
                cbType.Text = ucConvention.stype;
            }
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmLogIn.q = "SELECT max, price FROM service where type = '" + cbType.Text + "'";
            frmLogIn.vTable();
            txtMax.Text = frmLogIn.dtable.Rows[0][0].ToString();
            txtPrice.Text = frmLogIn.dtable.Rows[0][1].ToString();
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (ucConvention.sSave == "Add")
            {
                frmLogIn.q = "insert into service (serviceid, name, type, class, price, max, category, `limit`, kind)"
                    + " values (null,'" + cbName.Text
                    + "','" + cbType.Text
                    + "','" + "Room"
                    + "','" + Convert.ToDouble(txtPrice.Text)
                    + "','" + txtMax.Text
                    + "','" + "Convention"
                    + "','" + 0
                    + "','" + "Regular" + "')";
                frmLogIn.vTable();
            }
            else
            {
                frmLogIn.q = "";
            }
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
