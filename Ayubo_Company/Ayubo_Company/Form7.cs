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
    public partial class Form7 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        public Form7()
        {
            InitializeComponent();
        }
        //Grid viwe
        private void viwe()
        {
            con.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("select * from customer", con);
            DataTable dtTable = new DataTable();
            sqlData.Fill(dtTable);
            dataGridView1.DataSource = dtTable;
            con.Close();
        }

        //clear
        private void clear()
        {

            txtCusName.Clear();
            txtCusCon.Clear();
            txtCusAdd.Clear();
            cmbCusLicenceNo.Focus();

        }

        //fill combo
        private void fillCombo()
        {
            SqlDataAdapter data = new SqlDataAdapter("select CusLicenceNum from Customer", con);
            DataTable dt = new DataTable();
            data.Fill(dt);
            cmbCusLicenceNo.DataSource = dt;
            cmbCusLicenceNo.DisplayMember = "CusLicenceNum";
            cmbCusLicenceNo.ValueMember = "CusLicenceNum";
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            viwe();
            fillCombo();
        }

        //Add
        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCusLicenceNo.Text == "" || txtCusName.Text == "" || txtCusCon.Text == "" || txtCusAdd.Text == "")
                {
                    MessageBox.Show("Fields cannot be empty!!!!", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {
                    string sqlAdd;

                    sqlAdd = "insert into customer(CusLicenceNum, CusName, CusContact, CusAddress) " +
                        "values ('" + cmbCusLicenceNo.Text + "', '" + txtCusName.Text + "', '" + txtCusCon.Text + "', '" + txtCusAdd.Text + "');";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sqlAdd, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    fillCombo();
                    clear();

                    con.Close();
                    viwe();
                }

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

                sqlUpdate = "update Customer set CusLicenceNum = '" + cmbCusLicenceNo.Text + "', CusName ='" + txtCusName.Text + "'," +
                    " CusContact ='" + txtCusCon.Text + "', CusAddress = '" + txtCusAdd.Text + "' " +
                    "where CusLicenceNum = '" + cmbCusLicenceNo.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, con);
                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to update '" + cmbCusLicenceNo.Text + "' this record ?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                sqlDelete = "delete from Customer where CusLicenceNum='" + cmbCusLicenceNo.Text + "'";
                con.Open();

                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to delete '" + cmbCusLicenceNo.Text + "' this record ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Record not deleted!!!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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

        //Search
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSt;

                sqlSt = "select * from customer where CusLicenceNum ='" + cmbCusLicenceNo.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCusName.Text = dr["CusName"].ToString();
                    txtCusCon.Text = dr["CusContact"].ToString();
                    txtCusAdd.Text = dr["CusAddress"].ToString();
                   
                }
                else
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
