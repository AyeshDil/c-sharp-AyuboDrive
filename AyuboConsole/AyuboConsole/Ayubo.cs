using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AyuboConsole
{
    class Ayubo
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NM684M5;Initial Catalog=Ayubo;Integrated Security=True");
        
        //Rent Calculation
        public void rent_cal(DateTime rent_date, DateTime return_date, string regNo, bool with_driver)
        {
            try
            {
                string sqlSt = "select * from vehicle where RegNo = '" + regNo + "'";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    double dayRate, weekRate, monthRate, driverRate;
                    dayRate = Convert.ToDouble(dr["DailyRate"]);
                    weekRate = Convert.ToDouble(dr["weeklyRate"]);
                    monthRate = Convert.ToDouble(dr["MonthlyRate"]);
                    driverRate = Convert.ToDouble(dr["DriverRate"]);

                    //display database data
                    Console.WriteLine("Day Rate : " + dayRate);
                    Console.WriteLine("Week Rate : " + weekRate);
                    Console.WriteLine("Month Rate : " + monthRate);
                    Console.WriteLine("Driver Rate : " + driverRate);

                    //Calculate Date
                    TimeSpan defDate = return_date - rent_date;
                    double noOfDays = defDate.TotalDays;
                    int months, remainder, weeks, days;

                    months = Convert.ToInt32(noOfDays) / 30;
                    remainder = Convert.ToInt32(noOfDays) % 30;
                    weeks = remainder / 7;
                    days = remainder % 7;

                    //calculate Total Cost
                    double totCost;
                    double amount = (months * monthRate) + (weeks * weekRate) + (days * dayRate);
                    if (with_driver == true)
                    {
                        totCost = amount + (driverRate * noOfDays);
                    }
                    else
                    {
                        totCost = amount;
                    }

                    //Display
                    Console.WriteLine("_________________________ ");
                    Console.WriteLine("No of todal days :  " + Convert.ToInt32(noOfDays));
                    Console.WriteLine("No of days : " + days);
                    Console.WriteLine("No of Weeks : " + weeks);
                    Console.WriteLine("No of Months : " + months);
                    Console.WriteLine("_________________________ ");
                    //Display Total Cost
                    Console.WriteLine("            ----------- ");
                    Console.WriteLine("Total cost : " + totCost);
                    Console.WriteLine("            ----------- ");
                    Console.WriteLine("_________________________ ");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Vehicle not found");
                }
                con.Close();
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        //Day Tour
        public void day_tour(string packageID, DateTime startTime, DateTime endTime, int startKm, int endKm)
        {
            try
            {
                string sqlSt = "select * from Package where PackID = '" + packageID + "'";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    double packRate, extraKmRate, extraHrRate;
                    int maxKm, maxHrs;
                    string packName, vehicleType;

                    packRate = Convert.ToDouble(dr["PackRate"]);
                    maxHrs = Convert.ToInt32(dr["MaxHrs"]);
                    extraKmRate = Convert.ToDouble(dr["ExtraKmRate"]);
                    extraHrRate = Convert.ToDouble(dr["ExtraHrrate"]);
                    maxKm = Convert.ToInt32(dr["MaxKm"]);
                    packName = Convert.ToString(dr["packName"]);
                    vehicleType = Convert.ToString(dr["VehicleType"]);

                    //display database data
                    Console.WriteLine("________________________");
                    Console.WriteLine("Package Name : " + packName);
                    Console.WriteLine("Vehicle type : " + vehicleType);
                    Console.WriteLine("Package Rate : " + packRate);
                    Console.WriteLine("Maximum Km : " + maxKm);
                    Console.WriteLine("Maximum Hrs : " + maxHrs);
                    Console.WriteLine("Extra Km Rate : " + extraKmRate);
                    Console.WriteLine("Extra Hr Rate : " + extraHrRate);

                    //Calculate time
                    TimeSpan defTime = endTime - startTime;
                    int noOfHrs =Convert.ToInt32(defTime.TotalHours);
                    int extraHrs, extraHrsCharge;
                    if (noOfHrs > maxHrs)
                    {
                        extraHrs = noOfHrs - maxHrs;
                        extraHrsCharge = extraHrs * Convert.ToInt32(extraHrRate);
                    }
                    else
                    {
                        extraHrs = 0;
                        extraHrsCharge = 0;
                    }

                    //calculate Km
                    int defKm = endKm - startKm;
                    double totExtraKmCost;
                    int extraKm;
                    if (defKm > maxKm)
                    {
                        extraKm = defKm - maxKm; 
                        totExtraKmCost = extraKm * extraKmRate;
                    }
                    else
                    {
                        extraKm = 0;
                        totExtraKmCost = 0;
                    }
                    //Calculate total day tour cost
                    double totalCost = packRate + Convert.ToDouble(extraHrsCharge) + totExtraKmCost;

                    //Display
                    Console.WriteLine("________________________");
                    Console.WriteLine("extra km : " + extraKm);
                    Console.WriteLine("Extra Km Cost : " + totExtraKmCost);
                    Console.WriteLine("extra hrs : " + extraHrs);
                    Console.WriteLine("Extra Hrs Charge : " + extraHrsCharge);
                    Console.WriteLine("________________________");
                    //Display Total Cost
                    Console.WriteLine("           ------------");
                    Console.WriteLine("Total Cost : " + totalCost);
                    Console.WriteLine("           ------------");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Package Not Found");
                }
               
                con.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        //Long Tour
        public void long_tour(string packageID, DateTime rent_date, DateTime return_date, int startKm, int endKm)
        {

            try
            {
                string sqlSt = "select * from Package where PackID = '" + packageID + "'";
                SqlCommand cmd = new SqlCommand(sqlSt, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    double packRate, extraKmRate, driverNightRate, vehicleNightRate;
                    int maxKm;
                    string packName, vehicleType;

                    packRate = Convert.ToDouble(dr["PackRate"]);
                    extraKmRate = Convert.ToDouble(dr["ExtraKmRate"]);
                    maxKm = Convert.ToInt32(dr["MaxKm"]);
                    packName = Convert.ToString(dr["packName"]);
                    vehicleType = Convert.ToString(dr["VehicleType"]);
                    driverNightRate = Convert.ToDouble(dr["DriverNightRate"]);
                    vehicleNightRate = Convert.ToDouble(dr["VehicleNightRate"]);

                    //display database data
                    Console.WriteLine("________________________");
                    Console.WriteLine("Package Name : " + packName);
                    Console.WriteLine("Vehicle type : " + vehicleType);
                    Console.WriteLine("Package Rate : " + packRate);
                    Console.WriteLine("Maximum Km : " + maxKm);
                    Console.WriteLine("Extra Km Rate : " + extraKmRate);

                    //Calculate time
                    TimeSpan date_different = return_date - rent_date;
                    double noOfDays = date_different.TotalDays;

                    int defKm = endKm - startKm;
                    int ableTotKm = maxKm *Convert.ToInt32(noOfDays);

                    int extraKm;
                    double totExtraKmCharge;

                    if (defKm > ableTotKm)
                    {
                        extraKm = defKm - ableTotKm;
                        totExtraKmCharge = Convert.ToDouble(extraKm) * extraKmRate;
                    }
                    else
                    {
                        extraKm = 0;
                        totExtraKmCharge = 0;
                    }

                    //calculation
                    double overNightCharge = (driverNightRate + vehicleNightRate) * noOfDays;
                    double baseCharge = packRate * noOfDays;
                    double totCost = baseCharge + overNightCharge + totExtraKmCharge;

                    //Display
                    Console.WriteLine("________________________");
                    Console.WriteLine("No of Days : " + noOfDays);
                    Console.WriteLine("extra Km : " + extraKm);
                    Console.WriteLine("Extra Km Cost : " + totExtraKmCharge);
                    Console.WriteLine("Nights Charge : " + overNightCharge);
                    Console.WriteLine("Base charge : " + baseCharge);
                    Console.WriteLine("________________________");
                    //Display Total Cost
                    Console.WriteLine("           ------------");
                    Console.WriteLine("Total Cost : " + totCost);
                    Console.WriteLine("           ------------");
                    Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("Package Not Found");
                }

                con.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
