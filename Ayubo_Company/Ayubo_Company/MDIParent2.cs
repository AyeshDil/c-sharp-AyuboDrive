using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ayubo_Company
{
    public partial class MDIParent2 : Form
    {
        

        public MDIParent2()
        {
            InitializeComponent();
        }

        private void btnRentVehicle_Click(object sender, EventArgs e)
        {
            panelDisplay.Controls.Clear();
            Form1 f1 = new Form1();
            f1.TopLevel = false;
            panelDisplay.Controls.Add(f1);
            f1.Show();

            //Title change
            lblTitle.Text = "Rent Vehicle";
            f1.FormClosed += F1_FormClosed1;
        }

        private void F1_FormClosed1(object sender, FormClosedEventArgs e)
        {
            
            lblTitle.Text = "Ayubo Drive";
        }

        

        private void panelDisplay_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnDayTour_Click(object sender, EventArgs e)
        {
            panelDisplay.Controls.Clear();
            Form2 f2 = new Form2();
            f2.TopLevel = false;
            panelDisplay.Controls.Add(f2);
            f2.Show();

            //Title change
            lblTitle.Text = "Day Tour";
            f2.FormClosed += F2_FormClosed;
        }

        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "Ayubo Drive";
        }

        private void btnLongTour_Click(object sender, EventArgs e)
        {
            panelDisplay.Controls.Clear();
            Form5 f5 = new Form5();
            f5.TopLevel = false;
            panelDisplay.Controls.Add(f5);
            f5.Show();

            //Title change
            lblTitle.Text = "Long Tour";
            f5.FormClosed += F5_FormClosed;
        }

        private void F5_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "Ayubo Drive";
        }

        private void MDIParent2_Load(object sender, EventArgs e)
        {
            lblUser.Text = "User : " + User.userName;

            if (User.userName == "UserAdmin")
            {
                btnLog.Enabled = true;
            }
            else
                btnLog.Enabled = false;

            
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.Show();
            f8.Focus();
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Account : " + User.userName, "Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to log out ?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                Form6 f6 = new Form6();
                f6.Show();
            }

            else
            {
                
            }


            
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            panelDisplay.Controls.Clear();
            Form7 f7 = new Form7();
            f7.TopLevel = false;
            panelDisplay.Controls.Add(f7);
            f7.Show();

            //Title change
            lblTitle.Text = "Custermer Details";
            f7.FormClosed += F7_FormClosed;
        }

        private void F7_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "Ayubo Drive";
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            panelDisplay.Controls.Clear();
            Form9 f9 = new Form9();
            f9.TopLevel = false;
            panelDisplay.Controls.Add(f9);
            f9.Show();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            //User.detail = null;
            Form10 f10 = new Form10();
            f10.Show();
            f10.Focus();
            
            //waiting for form10 close
            f10.Closed += F10_Closed;
            
        }

        //after form10 closed
        private void F10_Closed(object sender, EventArgs e)
        {
            if (User.detail == "V")
            {
                
                Form3 f3 = new Form3();
                f3.TopLevel = false;
                panelDisplay.Controls.Add(f3);
                f3.Show();

                //Title change
                lblTitle.Text = "Vehicle DataBase";
                f3.FormClosed += F3_FormClosed;

            }
            else if (User.detail == "P")
            {
                
                Form4 f4 = new Form4();
                f4.TopLevel = false;
                panelDisplay.Controls.Add(f4);
                f4.Show();

                //Title change
                lblTitle.Text = "Package DataBase";
                f4.FormClosed += F4_FormClosed;
            }
        }

        private void F4_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "Ayubo Drive";
        }

        private void F3_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "Ayubo Drive";
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer - W.M.Ayesh Dilshan(Batch 67).   " +
                "Email - dilshanwma@gmail.com", "About Developer.....", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
