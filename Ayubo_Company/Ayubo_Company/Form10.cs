using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ayubo_Company
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            if (rbVehicle.Checked)
            {
                User.detail = "V";
            }
            else
                User.detail = "P";

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            User.detail = null;
            this.Close();
            
        }
    }
}
