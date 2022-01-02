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
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");

        public Form5()
        {
            InitializeComponent();
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
            txtDate.Clear();
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

        //calculate no of days
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

        }

        //calculater Km
        private void btnCalKm_Click(object sender, EventArgs e)
        {
            int startKm, endKm, defKm, extraKm;

            startKm = int.Parse(txtStartKm.Text);
            endKm = int.Parse(txtEndKm.Text);

            defKm = endKm - startKm;

            txtNoofKm.Text = defKm.ToString();

            int maxKm = int.Parse(txtMaxKm.Text);

            int noOfDays = int.Parse(txtDate.Text);
            int ableTotKm = noOfDays * maxKm;
            int noOfKm = int.Parse(txtNoofKm.Text);

            extraKm = noOfKm - ableTotKm;
            txtExtraKm.Text = extraKm.ToString();
            if (noOfKm > ableTotKm)
            {
                extraKm = noOfKm - ableTotKm;
                txtExtraKm.Text = extraKm.ToString();
            }
            else 
            {
                txtExtraKm.Text = "0";
            }
        }

        private void Form5_Load(object sender, EventArgs e)
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

            //Meter reading txtbox enable
            txtNoofKm.Enabled = false;
            txtExtraKm.Enabled = false;
            txtExtraKmCharge.Enabled = false;

            //Calculate number of days txtbox enable
            txtDate.Enabled = false;

            // final section txtbox enable
            txtOverNightCharge.Enabled = false;
            txtChargeDup.Enabled = false;
            txtTotCost.Enabled = false;
        }

        //calculate total cost
        private void btnCalTotCost_Click(object sender, EventArgs e)
        {
            double charge = double.Parse(txtCharge.Text);
            double totCharge = charge * double.Parse(txtDate.Text);
            txtChargeDup.Text = totCharge.ToString();

            //calculate over night charge
            double dayCost = double.Parse(txtDriverNightRate.Text) + double.Parse(txtVehicleNightRate.Text);
            double overNightCharge = dayCost * double.Parse(txtDate.Text);
            txtOverNightCharge.Text = overNightCharge.ToString();

            //calculate extra km charge
            double extraKmCharge = double.Parse(txtExtraKm.Text) * double.Parse(txtExtraKmRate.Text);
            txtExtraKmCharge.Text = extraKmCharge.ToString();

            //calculate Total Cost
            double totCost = overNightCharge + extraKmCharge + totCharge;
            txtTotCost.Text = "Rs. " + totCost.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to clear all fields record ?", "CLEAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearPackageDetails();
                clearTime();
                clearKm();
                txtChargeDup.Clear();
                txtTotCost.Clear();
                txtOverNightCharge.Clear();
                MessageBox.Show("All records cleared successfully!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                MessageBox.Show("Record not cleared!!!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
