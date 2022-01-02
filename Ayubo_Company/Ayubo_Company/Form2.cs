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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        double totExtraKmCost, totExtraHrCost;
        

        public Form2()
        {
            InitializeComponent();
        }

        //clear
        private void clearPackageDetails()
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
        
        //clear time data
        private void clearTime()
        {
            txtNoofHr.Clear();
            txtExtraHr.Clear();
            txtExtraHrCharge.Clear();
        }

        //clear km data
        private void clearKm()
        {
            txtStartKm.Clear();
            txtEndKm.Clear();
            txtNoofKm.Clear();
            txtExtraKm.Clear();
            txtExtraKmCharge.Clear();
        }

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
        private void label16_Click(object sender, EventArgs e)
        {

        }

        //calculate Time
        private void btnCalTime_Click(object sender, EventArgs e)
        {
            //calculate time
            DateTime startTime;
            DateTime endTime;
            TimeSpan defTime;
            startTime = DateTime.Parse(dtStartTime.Text);
            endTime = DateTime.Parse(dtEndTime.Text);

            defTime = endTime - startTime;
            txtNoofHr.Text = Convert.ToString(defTime.Hours);

            //cal extra hr
            int noOfHrs = int.Parse(txtNoofHr.Text);
            int maxHrs = int.Parse(txtMaxHr.Text);
            int totExtraHr;

            if (noOfHrs > maxHrs)
            {
                totExtraHr = noOfHrs -maxHrs;
                txtExtraHr.Text = totExtraHr.ToString();
                totExtraHrCost = double.Parse(txtExtraHr.Text) * double.Parse(txtExtraHrRate.Text);
                txtExtraHrCharge.Text = totExtraHrCost.ToString();
            }
            else
            {
                txtExtraHr.Text = "0";
                txtExtraHrCharge.Text = "0";
            }

          if (txtExtraHr.Text == "0")
            {
                MessageBox.Show("No Extra Hr!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Search
        private void btnSearch_Click(object sender, EventArgs e)
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
                    txtExtraKmRate.Text= dr["ExtraKmRate"].ToString();
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            fillCombo();
            //package Details txtbox enable
            txtDriverNightRate.Enabled = false;
            txtMaxKm.Enabled = false;
            txtMaxHr.Enabled = false;
            txtExtraKmRate.Enabled = false;
            txtExtraHrRate.Enabled = false;
            txtVehicleType.Enabled = false;
            txtCharge.Enabled = false;
            txtDriverNightRate.Enabled = false;
            txtVehicleNightRate.Enabled = false;
            txtpName.Enabled = false;

            //Meter reading 
            txtNoofKm.Enabled = false;
            txtExtraKm.Enabled = false;
            txtExtraKmCharge.Enabled = false;

            //Time txtbox enable
            txtNoofHr.Enabled = false;
            txtExtraHr.Enabled = false;
            txtExtraHrCharge.Enabled = false;

            txtTotCost.Enabled = false;
        }

        //Edit form load
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        //Calculate Km
        private void btnCalKm_Click(object sender, EventArgs e)
        {
            int startKm, endKm, defKm;

            startKm = int.Parse(txtStartKm.Text);
            endKm = int.Parse(txtEndKm.Text);

            defKm = endKm - startKm;

            txtNoofKm.Text = defKm.ToString();

            //cal Km
            int extraKm;
            int noOfKm = int.Parse(txtNoofKm.Text);
            int maxKm = int.Parse(txtMaxKm.Text);

            if (defKm > maxKm)
            {
                extraKm = noOfKm - maxKm;
                txtExtraKm.Text = extraKm.ToString();
                totExtraKmCost = double.Parse(txtExtraKm.Text) * double.Parse(txtExtraKmRate.Text);
                txtExtraKmCharge.Text = totExtraKmCost.ToString();
            }
            else 
            {
                txtExtraKm.Text = "0";
                txtExtraKmCharge.Text = "0";
            }
        }

        //clear all data
        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to clear all fields record ?", "CLEAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearPackageDetails();
                clearTime();
                clearKm();
                MessageBox.Show("All records cleared successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                MessageBox.Show("Record not cleared!!!", " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //total day tour cost
        private void btnCalHire_Click(object sender, EventArgs e)
        {
            double baseCharge = double.Parse(txtCharge.Text);
            double totalCost;

            totalCost = baseCharge + double.Parse(txtExtraHrCharge.Text) + double.Parse(txtExtraKmCharge.Text);

            txtTotCost.Text = "Rs. " + totalCost.ToString();
        }
    }
}
