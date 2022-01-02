using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ayubo_Company
{
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        public Form6()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
           
            string sqluser;

            sqluser = "select * from Login where UserID='" + txtUID.Text + "' and password= '"+txtPassword.Text+"' ";
            SqlCommand cmd = new SqlCommand(sqluser, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
 

            if (dr.Read())
            {
                User.userName = dr["UserName"].ToString();
                this.Hide();
                MDIParent2 home = new MDIParent2();
                home.Show();
            }
            else
                MessageBox.Show("Login Details Not Found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

           
            con.Close();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please log in using the admin username and password.!!! ", "Registration of a new user",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnHide.BringToFront();
                txtPassword.PasswordChar = '\0';

            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if(txtPassword.PasswordChar == '\0')
            {
                btnShow.BringToFront();
                txtPassword.PasswordChar = '*';
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
