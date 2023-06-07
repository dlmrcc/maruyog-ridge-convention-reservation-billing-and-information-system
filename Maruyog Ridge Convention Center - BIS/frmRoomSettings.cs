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
    public partial class frmRoomSettings : Form
    {
        public frmRoomSettings()
        {
            InitializeComponent();
        }
        void vRoomNumber()
        {
            cbRoomNumber.Items.Clear();
            frmLogIn.q = "SELECT distinct name FROM service where class = 'Room' and category = 'Accommodation' and kind = 'Regular' order by name";
            frmLogIn.vTable();
            for (int q = 0; q < frmLogIn.dtable.Rows.Count; q++)
                cbRoomNumber.Items.Add(frmLogIn.dtable.Rows[q][0]);
        }
        void vRoomClass()
        {
            cbType.Items.Clear();
            frmLogIn.q = "SELECT distinct type FROM service where class = 'Room' and category = 'Accommodation' and kind = 'Regular' order by type";
            frmLogIn.vTable();
            for (int q = 0; q < frmLogIn.dtable.Rows.Count; q++)
                cbType.Items.Add(frmLogIn.dtable.Rows[q][0]);
        }
        private void wfRoomSettings_Load(object sender, EventArgs e)
        {
            lbTitle.Text += " (" + ucRoom.sSave + ")";
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ph");
            vRoomNumber();
            vRoomClass();
            if (ucRoom.sSave == "Edit")
            {
                txtPaxLimit.Text = ucRoom.slimit;
                cbRoomNumber.Text = ucRoom.sname;
                cbType.Text = ucRoom.stype;
                txtPrice.Text = Convert.ToDouble(ucRoom.sprice).ToString("n");
                txtMax.Text = ucRoom.smax;
            }
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbRoomNumber_Leave(object sender, EventArgs e)
        {
            cbRoomNumber.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cbRoomNumber.Text);
        }
        double dPrice;
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                dPrice = Convert.ToDouble(txtPrice.Text);
                txtPrice.Text = Convert.ToDouble(dPrice).ToString("n");
            }
            catch
            {
                txtPrice.Text = "0.00";
                dPrice = 0;
            }
        }

        private void cbClass_Leave(object sender, EventArgs e)
        {
            cbType.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cbType.Text);
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (ucRoom.sSave == "Add")
            {
                frmLogIn.q = "insert into service (serviceid, name, type, class, price, max, category, `limit`, kind) values (null,'" + cbRoomNumber.Text
                    + "','" + cbType.Text
                    + "','" + "Room"
                    + "','" + Convert.ToDouble(txtPrice.Text)
                    + "','" + txtMax.Text
                    + "','" + "Accommodation"
                    + "','" + txtPaxLimit.Text
                    + "','Regular')";
                frmLogIn.vTable();
            }
            else
            {
                frmLogIn.q = "update service set type = '" + cbType.Text
                    + "', max = '" + txtMax.Text
                    + "', `limit`   = '" + txtPaxLimit.Text
                    + "', price = '" + Convert.ToDouble(txtPrice.Text)
                    + "' where type = '" + ucRoom.stype + "' and category = 'Accommodation' and class = 'Room'";
                frmLogIn.vTable();
                if (cbRoomNumber.Text != ucRoom.sname)
                {
                    frmLogIn.q = "update service set name = '" + cbRoomNumber.Text
                        + "' where serviceid = '" + ucRoom.sserviceid + "'";
                    frmLogIn.vTable();
                }
            }
            Close();
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmLogIn.q = "select price, max, `limit` from service where type = '"+cbType.Text+"'";
            frmLogIn.vTable();
            txtPaxLimit.Text = frmLogIn.dtable.Rows[0][2].ToString();
            txtMax.Text = frmLogIn.dtable.Rows[0][1].ToString();
            txtPrice.Text = frmLogIn.dtable.Rows[0][0].ToString();
        }
    }
}
