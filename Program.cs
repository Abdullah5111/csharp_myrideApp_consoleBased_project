using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BSEF20M034_H01
{
    internal class Program
    {
        static void welcome()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("********************** WELCOME TO MYRIDE **********************");
            Console.WriteLine("***************************************************************\n");
        }

        static void mainMenu()
        {
            Console.WriteLine("1: Book a Ride");
            Console.WriteLine("2: Enter as Driver");
            Console.WriteLine("3: Enter as Admin");
            Console.WriteLine("4: Close App");
        }

        static void driverMenu()
        {
            Console.WriteLine("1: Change Availability");
            Console.WriteLine("2: Change Location");
            Console.WriteLine("3: Exit as Driver");
        }

        static void adminMenu()
        {
            Console.WriteLine("1: Add Driver");
            Console.WriteLine("2: Remove Driver");
            Console.WriteLine("3: Update Admin");
            Console.WriteLine("4: Search Driver");
            Console.WriteLine("5: Exit as Admin");
        }
        static void Main(string[] args)
        {
            welcome();

            int option = 0;

            Admin admin = new Admin();

            while (option != 4)
            {
                mainMenu();

                while (option < 1 || option > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string val = Console.ReadLine();
                    Console.ResetColor();
                    option = Convert.ToInt32(val);
                }

                if (option == 1)
                {
                    option = 0;

                    //Asking Passenger Details For Ride
                    Console.WriteLine("Enter name : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string passengerName = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;

                    bool flag = true; // This is for entering correct phone number

                    Console.WriteLine("Enter Phone Number : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string passengerPhoneNumber = Console.ReadLine();
                    Console.ResetColor();
                    while (flag)
                    {
                        flag = false;
                        // Verifying Correct Phone Number Format
                        int phoneNumberLength = passengerPhoneNumber.Length;
                        if (phoneNumberLength != 11)
                        {
                            flag = true;
                        }

                        else
                        {
                            int i = 0;
                            for (; i < phoneNumberLength; i++)
                            {
                                if (passengerPhoneNumber[i] != '0' && passengerPhoneNumber[i] != '1' && passengerPhoneNumber[i] != '2' && passengerPhoneNumber[i] != '3' && passengerPhoneNumber[i] != '4' && passengerPhoneNumber[i] != '5' && passengerPhoneNumber[i] != '6' && passengerPhoneNumber[i] != '7' && passengerPhoneNumber[i] != '8' && passengerPhoneNumber[i] != '9')
                                {
                                    Console.WriteLine(passengerPhoneNumber[i]);
                                    flag = true;
                                    break;
                                }
                            }
                        }

                        if (flag)
                        {
                            Console.WriteLine("Enter valid Number : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            passengerPhoneNumber = Console.ReadLine();
                            Console.ResetColor();
                        }
                    }

                    Passenger passenger = new Passenger(passengerName, passengerPhoneNumber);

                    // Asking Starting Location
                    Console.WriteLine("Enter Start Location : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string startLoc = Console.ReadLine();
                    Console.ResetColor();

                    string[] dimensions = startLoc.Split(',');

                    Location startLocation = new Location(float.Parse(dimensions[0]), float.Parse(dimensions[1]));

                    // Asking Ending Location
                    Console.WriteLine("Enter End Location : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string endLoc = Console.ReadLine();
                    Console.ResetColor();

                    dimensions = endLoc.Split(',');

                    Location endLocation = new Location(float.Parse(dimensions[0]), float.Parse(dimensions[1]));


                    // Asking For Ride Type
                    Console.WriteLine("Enter Ride Type : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string type = Console.ReadLine();
                    Console.ResetColor();

                    Ride ride = new Ride();
                    bool rideFlag = ride.bookRide(startLocation, endLocation, passenger, type);

                    if(rideFlag)
                    {
                        Console.WriteLine("\n--------------- THANK YOU ----------------\n");

                        Console.WriteLine("Total cost of ride is " + ride.calculatePrice(type));

                        // Asking if rider wants that drive
                        Console.WriteLine("Enter ‘Y’ if you want to Book the ride, enter ‘N’ if you want to cancel operation: ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        string option2 = Console.ReadLine();
                        Console.ResetColor();

                        if (option2 == "y" || option2 == "Y")
                        {
                            Console.WriteLine("\nHappy Travel…!\n");

                            Console.WriteLine("\nGive rating out of 5: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int rating = Convert.ToInt32(val);

                            while (rating < 1 || rating > 5)
                            {
                                Console.WriteLine("\nRating should be from 1 to 5: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                val = Console.ReadLine();
                                Console.ResetColor();
                                rating = Convert.ToInt32(val);
                            }

                            ride.addDriversRating(rating);
                        }
                        else
                        {
                            Console.WriteLine("Try another driver");
                        }
                    }

                    else
                    {
                        Console.WriteLine("No Driver Available");
                    }
                }

                else if (option == 2)
                {
                    option = 0;

                    // Asking Drivers detail to search
                    Console.WriteLine("Enter ID : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string val = Console.ReadLine();
                    Console.ResetColor();
                    int id = Convert.ToInt32(val);
                    Console.WriteLine("Enter name : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string name = Console.ReadLine();
                    Console.ResetColor();

                    Driver driver = admin.searchDriverById(id);

                    if (driver != null)
                    {
                        Console.WriteLine("Hello " + driver.getName());
                        Console.WriteLine("Where are you know?");

                        Console.ForegroundColor = ConsoleColor.Green;
                        string startLoc = Console.ReadLine();
                        Console.ResetColor();

                        string[] dimensions = startLoc.Split(',');

                        driver.updateLocation(float.Parse(dimensions[0]), float.Parse(dimensions[1]));

                        int option2 = 0;

                        while (option2 != 3)
                        {
                            driverMenu();
                            while (option2 < 1 || option2 > 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                string value = Console.ReadLine();
                                Console.ResetColor();
                                option2 = Convert.ToInt32(value);
                            }

                            if (option2 == 1)
                            {
                                option2 = 0;

                                driver.updateAvailability();
                            }

                            else if (option2 == 2)
                            {
                                option2 = 0;

                                Console.WriteLine("Enter Location : ");

                                Console.ForegroundColor = ConsoleColor.Green;
                                string loc = Console.ReadLine();
                                Console.ResetColor();

                                dimensions = startLoc.Split(',');

                                driver.updateLocation(float.Parse(dimensions[0]), float.Parse(dimensions[1]));
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("\n*** Driver not exists ***\n");
                    }
                }

                else if (option == 3)
                {
                    option = 0;

                    int option2 = 0;

                    while (option2 != 5)
                    {
                        adminMenu();
                        while (option2 < 1 || option2 > 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            option2 = Convert.ToInt32(val);
                        }

                        if (option2 == 1)
                        {
                            option2 = 0;


                            // Asking Driver's Info To Register

                            Console.WriteLine("Enter Name : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string name = Console.ReadLine();
                            Console.ResetColor();

                            Console.WriteLine("Enter Age : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int age = Convert.ToInt32(val);

                            Console.WriteLine("Enter Gender : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string gender = Console.ReadLine();
                            Console.ResetColor();

                            Console.WriteLine("Enter Address : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string address = Console.ReadLine();
                            Console.ResetColor();

                            bool flag = true; // This is for entering correct phone number
                            Console.WriteLine("Enter Phone Number : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string phoneNumber = Console.ReadLine();
                            Console.ResetColor();
                            while (flag)
                            {
                                flag = false;
                                // Verifying Correct Phone Number Format
                                int phoneNumberLength = phoneNumber.Length;
                                int i = 0;
                                for (; i < phoneNumberLength; i++)
                                {
                                    if (phoneNumber[i] != '0' && phoneNumber[i] != '1' && phoneNumber[i] != '2' && phoneNumber[i] != '3' && phoneNumber[i] != '4' && phoneNumber[i] != '5' && phoneNumber[i] != '6' && phoneNumber[i] != '7' && phoneNumber[i] != '8' && phoneNumber[i] != '9')
                                    {
                                        Console.WriteLine(phoneNumber[i]);
                                        flag = true;
                                        break;
                                    }
                                }

                                if (flag)
                                {
                                    Console.WriteLine("Enter valid Number : ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    phoneNumber = Console.ReadLine();
                                    Console.ResetColor();
                                }
                            }

                            Console.WriteLine("Enter Location : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string loc = Console.ReadLine();
                            Console.ResetColor();
                            string[] dimensions = loc.Split(',');
                            float latitude = float.Parse(dimensions[0]);
                            float longitude = float.Parse(dimensions[1]);

                            Location currentLocation = new Location(latitude, longitude);

                            // Asking for vehicle information
                            Console.WriteLine("Enter Vehicle Type : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string type = Console.ReadLine();
                            Console.ResetColor();
                            Console.WriteLine("Enter Model : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string model = Console.ReadLine();
                            Console.ResetColor();
                            Console.WriteLine("Enter Licence Plate : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string plate = Console.ReadLine();
                            Console.ResetColor();

                            Vehicle vehicle = new Vehicle(type, model, plate);

                            Driver driver = new Driver(name, age, gender, address, phoneNumber, currentLocation, vehicle, true);

                            Console.WriteLine("Driver Registered. ID : " + admin.addDriver(driver));
                        }

                        else if (option2 == 2)
                        {
                            option2 = 0;


                            Console.WriteLine("Enter ID : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int id = Convert.ToInt32(val);

                            admin.removeDriver(id);
                        }

                        else if (option2 == 3)
                        {
                            option2 = 0;

                            Console.WriteLine("Enter ID : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int id = Convert.ToInt32(val);

                            if(admin.searchDriverById(id) == null)
                            {
                                Console.WriteLine("------------Driver with ID " + id + " not exist-------------");
                            }

                            else
                            {
                                Console.WriteLine("------------Driver with ID " + id + " exists-------------");

                                Console.WriteLine("Enter Age : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                val = Console.ReadLine();
                                Console.ResetColor();
                                int age = Convert.ToInt32(val);

                                Console.WriteLine("Enter Vehicle Type : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string type = Console.ReadLine();
                                Console.ResetColor();

                                Console.WriteLine("Enter Model : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string model = Console.ReadLine();
                                Console.ResetColor();

                                Console.WriteLine("Enter Licence Plate : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string plate = Console.ReadLine();
                                Console.ResetColor();

                                admin.updateDriver(id, age, type, model, plate);

                                Console.WriteLine("------------ Driver Updated -------------");

                            }
                        }
                        else if (option2 == 4)
                        {
                            option2 = 0;

                            Console.WriteLine("Enter ID : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int id = Convert.ToInt32(val);

                            Driver driver = admin.searchDriver(id);

                            if(driver == null)
                            {
                                Console.WriteLine("------------Driver with ID " + id + " not exist-------------");
                            }
                            else
                            {
                                Console.WriteLine("---------------------------------------------------------------------------");
                                Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                Console.WriteLine("---------------------------------------------------------------------------");
                                Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", driver.getName(), driver.getAge(), driver.getGender(), driver.getVehicleType(), driver.getModel(), driver.getLicence()));
                                Console.WriteLine("---------------------------------------------------------------------------");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Thanks for visiting :)");
        }
    }
}
