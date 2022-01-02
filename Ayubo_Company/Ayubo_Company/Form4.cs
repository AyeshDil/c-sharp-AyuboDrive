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
    
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        //fill combo
        private void fillCombo()
        {
            SqlDataAdapter data = new SqlDataAdapter("select PackID from Package", con);
            DataTable dt = new DataTable();
            data.Fill(dt);
            cmdPack.DataSource = dt;
            cmdPack.DisplayMember = "PackID";
            cmdPack.ValueMember = "PackID";
        }

        //clear
        private void clear()
        {

            txtpName.Clear();
            txtVehicleType.Clear();
            txtCharge.Clear();
            txtMaxKm.Clear();
            txtMaxHr.Clear();
            txtExtraHrRate.Clear();
            txtExtraKmRate.Clear();
            txtDriverNightRate.Clear();
            txtVehicleNightRate.Clear();
            cmdPack.Focus();

        }

        //Grid viwe
        private void viwe()
        {
            con.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("select * from Package", con);
            DataTable dtTable = new DataTable();
            sqlData.Fill(dtTable);
            dataGridView1.DataSource = dtTable;
            con.Close();
        }

        public Form4()
        {
            InitializeComponent();
        }

        //search
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSt;

                sqlSt = "select * from Package where PackID='" + cmdPack.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtpName.Text = dr["PackName"].ToString();
                    txtCharge.Text = dr["PackRate"].ToString();
                    txtMaxKm.Text = dr["MaxKm"].ToString();
                    txtMaxHr.Text = dr["MaxHrs"].ToString();
                    txtExtraKmRate.Text = dr["ExtraKmRate"].ToString();
                    txtExtraHrRate.Text = dr["ExtraHrRate"].ToString();
                    txtDriverNightRate.Text = dr["DriverNightRate"].ToString();
                    txtVehicleNightRate.Text = dr["VehicleNightRate"].ToString();
                    txtVehicleType.Text = dr["VehicleType"].ToString();

                }
                else
                    MessageBox.Show("Package not found", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                con.Close();
            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            fillCombo();
            viwe();
        }
        
        //ADD
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdPack.Text == "" || txtpName.Text == "" || txtVehicleType.Text == "" || txtCharge.Text == "" || txtMaxKm.Text == "" || txtMaxHr.Text == "" || txtExtraKmRate.Text == "" || txtExtraHrRate.Text == "" || txtDriverNightRate.Text == "" || txtVehicleNightRate.Text == "")
                {
                    MessageBox.Show("Fields cannot be empty!!!!","Warning",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                }
                else
                {
                    string sqlAdd;

                    sqlAdd = "insert into Package(PackID, PackName, VehicleType, Packrate, MaxKm, MaxHrs, ExtraKmRate, ExtraHrRate, DriverNightRate, VehicleNightRate) " +
                        "values ('" + cmdPack.Text + "', '" + txtpName.Text + "', '" + txtVehicleType.Text + "', '" + txtCharge.Text + "', '" + txtMaxKm.Text + "', '" + txtMaxHr.Text + "', '" + txtExtraKmRate.Text + "', '" + txtExtraHrRate.Text + "', '" + txtDriverNightRate.Text + "', '" + txtVehicleNightRate.Text + "');";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sqlAdd, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                sqlUpdate = "update Package set PackName = '" + txtpName.Text + "', VehicleType ='" + txtVehicleType.Text + "'," +
                    " PackRate ='" + txtCharge.Text + "', MaxKm = '" + txtMaxKm.Text + "', MaxHrs = '" + txtMaxHr.Text + "', ExtraKmRate= '" + txtExtraKmRate.Text + "'," +
                    " ExtraHrRate = '" + txtExtraHrRate.Text + "', DriverNightRate = '" + txtDriverNightRate.Text + "', VehicleNightRate = '" + txtVehicleNightRate.Text + "' " +
                    "where PackID = '" + cmdPack.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, con);
                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to update '" + cmdPack.Text + "' this record ?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                sqlDelete = "delete from Package where PackID='" + cmdPack.Text + "'";
                con.Open();

                //Confirmation msg box
                DialogResult dialogResult = MessageBox.Show("Are you want to delete '" + cmdPack.Text + "' this record ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(sqlDelete, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }

                else
                {
                    MessageBox.Show("Record not deleted!!!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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
                MessageBox.Show("Record not cleared!!!", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
           // Form4 f4 = new Form4();
            //f4.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
