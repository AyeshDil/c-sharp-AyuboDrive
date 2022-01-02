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
    
    public partial class Form9 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        public Form9()
        {
            InitializeComponent();
        }

        //Grid viwe
        private void viwe()
        {
            con.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("select * from Login", con);
            DataTable dtTable = new DataTable();
            sqlData.Fill(dtTable);
            dataGridView1.DataSource = dtTable;
            con.Close();
        }

        //clear
        private void clear()
        {

            txtUserName.Clear();
            txtPassword.Clear();
            
        }

        //fill combo
        private void fillCombo()
        {
            SqlDataAdapter data = new SqlDataAdapter("select UserID from Login", con);
            DataTable dt = new DataTable();
            data.Fill(dt);
            cmbUserID.DataSource = dt;
            cmbUserID.DisplayMember = "UserID";
            cmbUserID.ValueMember = "UserID";
        }


        //search
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSt;

                sqlSt = "select * from login where UserID ='" + cmbUserID.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtUserName.Text = dr["UserName"].ToString();
                    txtPassword.Text = dr["Password"].ToString();
                    
                }
                else
                    MessageBox.Show("User not found", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        //ADD
        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlAdd;

                sqlAdd = "insert into Login(UserID, UserName, Password) " +
                    "values ('" + cmbUserID.Text + "', '" + txtUserName.Text + "', '" + txtPassword.Text + "');";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlAdd, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record added!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fillCombo();
                clear();

                con.Close();
                viwe();

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        //Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlUpdate;

                sqlUpdate = "update Login set UserName = '" + txtUserName.Text + "', Password ='" + txtPassword.Text + "'" +
                    "where UserID = '" + cmbUserID.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, con);
                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to update '" + cmbUserID.Text + "' this record ?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();


                }

                else
                {
                    MessageBox.Show("Record not updated!!!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
                fillCombo();
                con.Close();
                viwe();
            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        //Delete
        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlDelete;
                sqlDelete = "delete from Login where UserID='" + cmbUserID.Text + "'";
                con.Open();

                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to delete '" + cmbUserID.Text + "' this record ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(sqlDelete, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillCombo();
                    clear();


                }

                else
                {
                    MessageBox.Show("Record not deleted!!!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }

                con.Close();
                viwe();

            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        //Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to clear all fields record ?", "CLEAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clear();
                MessageBox.Show("Records cleared successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                MessageBox.Show("Record not cleared!!!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            fillCombo();
            viwe();
        }
    }
}
