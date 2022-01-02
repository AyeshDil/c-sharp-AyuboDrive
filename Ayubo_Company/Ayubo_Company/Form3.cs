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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        public Form3()
        {
            InitializeComponent();
        }

        //fill combo
        private void fillCombo()
        {
            
            SqlDataAdapter data = new SqlDataAdapter("select RegNo from Vehicle", con);
            DataTable dt = new DataTable();
            data.Fill(dt);
            cmbRegNo.DataSource = dt;
            cmbRegNo.DisplayMember = "RegNo";
            cmbRegNo.ValueMember = "RegNo";
        }

        //clear
        private void clear()
        {

            txtType.Clear();
            txtMake.Clear();
            txtDayRate.Clear();
            txtWeekRate.Clear();
            txtMonthRate.Clear();
            txtDriveRate.Clear();
            cmbRegNo.Focus();

        }

       

        //Grid viwe
        private void viwe()
        {
            
            SqlDataAdapter sqlData = new SqlDataAdapter("select * from Vehicle", con);
            DataTable dtTable = new DataTable();
            sqlData.Fill(dtTable);
            dataGridView1.DataSource = dtTable;
            
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            fillCombo();
            viwe();
        }
        
        //search 
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSt;

                sqlSt = "select * from Vehicle where RegNo='" + cmbRegNo.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtType.Text = dr["VehicleType"].ToString();
                    txtMake.Text = dr["Make"].ToString();
                    txtDayRate.Text = dr["DailyRate"].ToString();
                    txtWeekRate.Text = dr["WeeklyRate"].ToString();
                    txtMonthRate.Text = dr["MonthlyRate"].ToString();
                    txtDriveRate.Text = dr["DriverRate"].ToString();



                }
                else
                    MessageBox.Show("Vehicle not found");

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
                if (cmbRegNo.Text == "" || txtType.Text == "" || txtMake.Text == "" || txtDayRate.Text == "" || txtWeekRate.Text == "" || txtMonthRate.Text == "" || txtDriveRate.Text == "")
                {
                    MessageBox.Show("Fields cannot be empty!!!!", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else 
                { 
                    string sqlAdd;

                    sqlAdd = "insert into Vehicle(RegNo, VehicleType, Make, DailyRate, WeeklyRate, MonthlyRate, DriverRate) " +
                        "values ('" + cmbRegNo.Text + "', '" + txtType.Text + "', '" + txtMake.Text + "', '" + txtDayRate.Text + "', '"+ txtWeekRate.Text +"', '"+ txtMonthRate.Text +"', '"+ txtDriveRate.Text +"');";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sqlAdd, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added!");
                    clear();
                    fillCombo();
                    viwe();
                    con.Close();
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

                sqlUpdate = "update Vehicle set VehicleType = '" + txtType.Text + "', Make ='" + txtMake.Text + "'," +
                    " DailyRate ='"+txtDayRate.Text+"', WeeklyRate = '"+txtWeekRate.Text+"', MonthlyRate = '" +txtMonthRate.Text+"', DriverRate = '"+txtDriveRate.Text+"' " +
                    "where RegNo = '" + cmbRegNo.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, con);
                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to update '" + cmbRegNo.Text + "' this record ?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                { 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!!!");
                    clear();
                }

                else
                {
                    MessageBox.Show("Record not updated!!!");
                }
                
                con.Close();
                fillCombo();
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
                sqlDelete = "delete from Vehicle where RegNo='" + cmbRegNo.Text + "'";
                con.Open();

                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to delete '" + cmbRegNo.Text + "' this record ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(sqlDelete, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!!!");
                    clear();
                }

                else
                {
                    MessageBox.Show("Record not deleted!!!");
                }

                con.Close();
                fillCombo();
                viwe();
            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        //clear field
        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to clear all fields record ?", "CLEAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clear();
                MessageBox.Show("Records cleared successfully!!!");
                
            }

            else
            {
                MessageBox.Show("Record not cleared!!!");
            }
            
        }

        //back
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            //Form3 f3 = new Form3();
            //f3.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
