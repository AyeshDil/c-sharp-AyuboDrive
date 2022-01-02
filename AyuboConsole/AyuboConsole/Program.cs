using System;

namespace AyuboConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you need a which form? Rent(R), Day Tour(D), or Long Tour(L) ");
            Console.WriteLine("R-Rent, D-Day Tour, or L-Long Tour....Input [R/D/L] : ");
            string whichForm = Console.ReadLine();

            //This code for the Rent
            if (whichForm == "R")
            {
                String regNo;
                bool with_driver;

                Console.WriteLine("Enter rent date [mm/dd/yyyy]: ");
                DateTime rent_date = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter return date [mm/dd/yyyy]: ");
                DateTime return_date = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter vehicle ID : ");
                regNo = Console.ReadLine();

                Console.WriteLine("Do you need a driver [Y/N] : ");
                string driver = Console.ReadLine();

                if (driver == "Y")
                {
                    with_driver = true;
                }
                else
                {
                    with_driver = false;
                }

                Ayubo rentCal = new Ayubo();
                rentCal.rent_cal(rent_date, return_date, regNo, with_driver);
            }

            //This code for the Day Tour
            else if (whichForm == "D")
            {
                String packageID;

                Console.WriteLine("Package ID : ");
                packageID = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter start time [hh:mm:ss]: ");
                DateTime startTime = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter end time [hh:mm:ss]: ");
                DateTime endTime = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter start Km : ");
                int startKm = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter end Km : ");
                int endKm = Convert.ToInt32(Console.ReadLine());

                Ayubo dayTour = new Ayubo();
                dayTour.day_tour(packageID, startTime, endTime, startKm, endKm);

            }
            
            //This code for the Long Tour
            else if (whichForm == "L")
            {
                String packageID;

                Console.WriteLine("Package ID : ");
                packageID = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter rent date [mm/dd/yyyy]: ");
                DateTime rent_date = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter return date [mm/dd/yyyy]: ");
                DateTime return_date = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter start Km : ");
                int startKm = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter end Km : ");
                int endKm = Convert.ToInt32(Console.ReadLine());

                Ayubo longTour = new Ayubo();
                longTour.long_tour(packageID, rent_date, return_date, startKm, endKm);
            }
            else
            {
                Console.WriteLine("Please enter correct capital letter.");
            }

            
        }
    }
}
