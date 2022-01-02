using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ayubo_Company
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        int months, weeks, days;


        public Form1()
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

        //Date calculate
        private void btnDate_Click(object sender, EventArgs e)
        {
            DateTime rent_date, return_date;
            TimeSpan date_different;
            double nDays;

            rent_date = DateTime.Parse(dtRent.Text);
            return_date = DateTime.Parse(dtReturn.Text);

            date_different = return_date - rent_date;

            nDays = date_different.TotalDays;
            txtDate.Text = nDays.ToString();

            int totDays, remainder;
            totDays = int.Parse(txtDate.Text);

            //Cal M
            months = totDays / 30;
            txtMonths.Text = months.ToString();

            //cal W
            remainder = totDays % 30;
            weeks = remainder / 7;
            txtWeeks.Text = weeks.ToString();

            //cal D
            days = remainder % 7;
            txtDays.Text = days.ToString();

        }

        //form load
        private void Form1_Load(object sender, EventArgs e)
        {
            fillCombo();
            //Vehicle details txtbox enable
            txtType.Enabled = false;
            txtMake.Enabled = false;
            txtDayRate.Enabled = false;
            txtWeekRate.Enabled = false;
            txtMonthRate.Enabled = false;
            txtDriveRate.Enabled = false;

            //Calculate no of days - txtbox enable
            txtDate.Enabled = false;
            txtMonths.Enabled = false;
            txtWeeks.Enabled = false;
            txtDays.Enabled = false;

            //final section txtbox enable
            txtCost.Enabled = false;
        }

        //clear
        private void clearVehicleDetais()
        {
            
            txtType.Clear();
            txtMake.Clear();
            txtDayRate.Clear();
            txtWeekRate.Clear();
            txtMonthRate.Clear();
            txtDriveRate.Clear();
            checkDriver.Checked = false;
            cmbRegNo.Focus();

        }

        //clear day cal
        private void clearDayCal()
        {
            txtDate.Clear();
            txtMonths.Clear();
            txtWeeks.Clear();
            txtDays.Clear();
        }
        private void btnCalTotCost_Click(object sender, EventArgs e)
        {
            double dayRate, weekRate, monthRate, driverRate;
            double dayCost, weekCost, monthCost, driverCost, totCost;
            dayRate = double.Parse(txtDayRate.Text);
            weekRate = double.Parse(txtWeekRate.Text);
            monthRate = double.Parse(txtMonthRate.Text);
            driverRate = double.Parse(txtDriveRate.Text);

            dayCost = dayRate * days;
            weekCost = weekRate * weeks;
            monthCost = monthRate * months;
            driverCost = driverRate * days;

            if (checkDriver.Checked)
            {
                totCost = dayCost + weekCost + monthCost + driverCost;
            }
            else
            {
                totCost = dayCost + weekCost + monthCost;
            }

            txtCost.Text = "Rs. " + totCost.ToString();
        }

        //ADD
        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlAdd;

                sqlAdd = "insert into Vehicle(RegNo, VehicleType, Make, DailyRate, WeeklyRate, MonthlyRate) values ('" + cmbRegNo.Text + "', '" + txtType.Text + "', '" + txtMake.Text + "', '" + txtDayRate.Text + "', '" + txtWeekRate.Text + "', '" + txtMonthRate.Text + "', '" + txtDriveRate.Text + "');";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlAdd, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record added!","Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
                fillCombo();

                con.Close();
                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        
        private void picVehicle_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you want to clear all fields record ?", "CLEAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearVehicleDetais();
                clearDayCal();
                txtCost.Clear();
                MessageBox.Show("All records cleared successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                MessageBox.Show("Record not cleared!!!", " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        //search vehicle details
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
                    MessageBox.Show("Vehicle not found", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                con.Close();
            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
    
}
