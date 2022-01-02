using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ayubo_Company
{
    public partial class Form8 : Form
    {
        Double result = 0;
        String oprt = "";
        bool isOprt = false;

        public Form8()
        {
            InitializeComponent();
        }

        private void btn_click(object sender, EventArgs e)
        {
            if ((txtDisplay.Text == "0") || (isOprt))
                txtDisplay.Clear();

            isOprt = false;
            Button btn = (Button)sender;
            if (btn.Text == ".")
            {
                if (!txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text = txtDisplay.Text + btn.Text;
                }
            }
            else
                txtDisplay.Text = txtDisplay.Text + btn.Text;

        }

        private void oprt_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (result != 0)
            {
                btnequal.PerformClick();
                oprt = btn.Text;
                lblshow.Text = result + " " + oprt;
                isOprt = true;
            }
            else
            {
                oprt = btn.Text;
                result = Double.Parse(txtDisplay.Text);
                lblshow.Text = result + " " + oprt;
                isOprt = true;
            }



        }

        private void btnback_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            result = 0;
        }

        //equal button
        private void btnequal_Click_1(object sender, EventArgs e)
        {
            switch (oprt)
            {
                case "+":
                    txtDisplay.Text = (result + Double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "-":
                    txtDisplay.Text = (result - Double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "*":
                    txtDisplay.Text = (result * Double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "/":
                    txtDisplay.Text = (result / Double.Parse(txtDisplay.Text)).ToString();
                    break;
                default:
                    break;
            }
            //update lblshow
            result = Double.Parse(txtDisplay.Text);
            lblshow.Text = "";
        }

        private void btnback_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void btncancle_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            result = 0;
        }
    }
}

