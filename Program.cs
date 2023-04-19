using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSEF20M034_H02
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

        static void searchMenu()
        {
            Console.WriteLine("1: Search by ID");
            Console.WriteLine("2: Search by name");
            Console.WriteLine("3: Search by age");
            Console.WriteLine("4: Search by gender");
            Console.WriteLine("5: Search by vehicle type");
            Console.WriteLine("6: Exit searching");
        }
        static void Main(string[] args)
        {
            welcome();

            string option = "0";

            while(option != "4")
            {
                mainMenu();
                Console.Write("Select Option: ");
                Console.ForegroundColor = ConsoleColor.Green;
                option = Console.ReadLine();
                Console.ResetColor();
                while (option != "1" && option != "2" && option != "3" && option != "4")
                {
                    Console.Write("Select Valid Option: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    option = Console.ReadLine();
                    Console.ResetColor();
                }

                if(option == "1")
                {
                    //Asking Passenger Details For Ride

                    Console.Write("Enter name: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string passengerName = Console.ReadLine();
                    Console.ResetColor();

                    bool flag = true; // This is for entering correct phone number

                    Console.Write("Enter Phone Number: ");
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
                            // Check if any other character entered
                            int i = 0;
                            for (; i < phoneNumberLength; i++)
                            {
                                if (passengerPhoneNumber[i] != '0' && passengerPhoneNumber[i] != '1' && passengerPhoneNumber[i] != '2' && passengerPhoneNumber[i] != '3' && passengerPhoneNumber[i] != '4' && passengerPhoneNumber[i] != '5' && passengerPhoneNumber[i] != '6' && passengerPhoneNumber[i] != '7' && passengerPhoneNumber[i] != '8' && passengerPhoneNumber[i] != '9')
                                {
                                    flag = true;
                                    i = phoneNumberLength;
                                }
                            }
                        }

                        // If got correct number or not
                        if (flag)
                        {
                            Console.Write("Enter valid Number: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            passengerPhoneNumber = Console.ReadLine();
                            Console.ResetColor();
                        }
                    }

                    Passenger passenger = new Passenger { name = passengerName, phoneNumber = passengerPhoneNumber };

                    // Asking Starting Location
                    Console.Write("Enter Start Location: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string startLoc = Console.ReadLine();
                    Console.ResetColor();

                    string[] dimensions = startLoc.Split(',');

                    Location startLocation = new Location { longitude = float.Parse(dimensions[1]), latitude = float.Parse(dimensions[0]) };

                    // Asking Ending Location
                    Console.Write("Enter End Location: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string endLoc = Console.ReadLine();
                    Console.ResetColor();

                    dimensions = endLoc.Split(',');

                    Location endLocation = new Location { longitude = float.Parse(dimensions[1]), latitude = float.Parse(dimensions[0]) };

                    // Asking For Ride Type
                    Console.Write("Enter Ride Type: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string type = Console.ReadLine();
                    Console.ResetColor();

                    Ride myRide = new Ride();
                    bool flag2 = myRide.bookRide(passenger, startLocation, endLocation, type);

                    // No driver available
                    if(flag2 == false)
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine("No driver available");
                        Console.WriteLine("-------------------");
                    }

                    else
                    {
                        Console.WriteLine("\n--------------- THANK YOU ----------------\n");

                        Console.WriteLine("Total cost of ride is " + myRide.calculatePrice(type, startLocation, endLocation));

                        // Asking if rider wants that drive
                        Console.WriteLine("Enter ‘Y’ if you want to Book the ride, enter ‘N’ if you want to cancel operation: ");

                        Console.ForegroundColor = ConsoleColor.Green;
                        string option2 = Console.ReadLine();
                        Console.ResetColor();

                        // Check if rider proceeded or not
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
                        }
                        else
                        {
                            Console.WriteLine("Try another driver");
                        }
                    }
                }

                else if(option == "2")
                {
                    // First of all check if driver exists or not
                    Console.Write("Enter ID: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string id = Console.ReadLine();
                    Console.ResetColor();

                    if (Admin.searchByID(id) == false)
                    {
                        Console.WriteLine("-----------------");
                        Console.WriteLine("Driver not exists");
                        Console.WriteLine("-----------------");
                    }
                    else
                    {
                        string option2 = "0";

                        while (option2 != "3")
                        {
                            driverMenu();
                            Console.Write("Select Option: ");
                            option2 = Console.ReadLine();
                            while (option2 != "1" && option2 != "2" && option2 != "3")
                            {
                                Console.Write("Select Valid Option: ");
                                option2 = Console.ReadLine();
                            }

                            // Update availability
                            if (option2 == "1")
                            {
                                Admin.updateAvailability(id);
                                Console.WriteLine("***** Availability Updated *****");
                            }

                            // Update Location
                            else if (option2 == "2")
                            {
                                Console.Write("Enter Current Location : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string loc = Console.ReadLine();
                                Console.ResetColor();
                                string[] dimensions = loc.Split(',');

                                Location currentLocation = new Location { latitude = float.Parse(dimensions[0]), longitude = float.Parse(dimensions[1]) };

                                Admin.updateLocation(id, currentLocation);
                                Console.WriteLine("***** Location Updated *****");
                            }
                        }
                    }
                    
                }

                else if (option == "3")
                {
                    string option2 = "0";

                    while (option2 != "5")
                    {
                        adminMenu();
                        Console.Write("Select Option: ");
                        option2 = Console.ReadLine();
                        while (option2 != "1" && option2 != "2" && option2 != "3" && option2 != "4" && option2 != "5")
                        {
                            Console.Write("Select Valid Option: ");
                            option2 = Console.ReadLine();
                        }

                        if (option2 == "1")
                        {
                            // Asking Driver's Info To Register

                            Console.Write("Enter Name: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string name = Console.ReadLine();
                            Console.ResetColor();

                            Console.Write("Enter Age: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string val = Console.ReadLine();
                            Console.ResetColor();
                            int age = Convert.ToInt32(val);

                            Console.Write("Enter Gender: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string gender = Console.ReadLine();
                            Console.ResetColor();

                            Console.Write("Enter Address: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string address = Console.ReadLine();
                            Console.ResetColor();

                            bool flag = true; // This is for entering correct phone number
                            Console.Write("Enter Phone Number: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string phoneNumber = Console.ReadLine();
                            Console.ResetColor();
                            while (flag)
                            {
                                flag = false;

                                // Verifying Correct Phone Number Format
                                int phoneNumberLength = phoneNumber.Length;
                                int i = 0;
                                if (phoneNumberLength != 11)
                                {
                                    flag = true;
                                }

                                else
                                {
                                    for (; i < phoneNumberLength; i++)
                                    {
                                        if (phoneNumber[i] != '0' && phoneNumber[i] != '1' && phoneNumber[i] != '2' && phoneNumber[i] != '3' && phoneNumber[i] != '4' && phoneNumber[i] != '5' && phoneNumber[i] != '6' && phoneNumber[i] != '7' && phoneNumber[i] != '8' && phoneNumber[i] != '9')
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                }

                                if (flag)
                                {
                                    Console.Write("Enter valid Number: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    phoneNumber = Console.ReadLine();
                                    Console.ResetColor();
                                }
                            }

                            // Asking drivers location
                            Console.Write("Enter Location : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string loc = Console.ReadLine();
                            Console.ResetColor();
                            string[] dimensions = loc.Split(',');

                            Location currentLocation = new Location { latitude = float.Parse(dimensions[0]), longitude = float.Parse(dimensions[1]) };

                            // Asking for vehicle information
                            Console.Write("Enter Vehicle Type: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string type = Console.ReadLine();
                            Console.ResetColor();
                            Console.Write("Enter Model: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string model = Console.ReadLine();
                            Console.ResetColor();
                            Console.Write("Enter Licence Plate: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string plate = Console.ReadLine();
                            Console.ResetColor();

                            Vehicle vehicle = new Vehicle { type = type, model = model, licencePlate = plate };

                            Driver driver = new Driver { name = name, age = age, gender = gender, address = address, phoneNumber = phoneNumber, currentLocation = currentLocation, vehicle = vehicle, availability = true };

                            // Driver added
                            Admin.addDriver(driver);

                            Console.WriteLine("------------------------");
                            Console.WriteLine("----- Driver added -----");
                            Console.WriteLine("------------------------");
                        }

                        else if (option2 == "2")
                        {
                            // Removing driver
                            Console.Write("Enter ID: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string id = Console.ReadLine();
                            Console.ResetColor();
                            bool flag = Admin.removeDriver(id);

                            if (flag == true)
                            {
                                Console.WriteLine("Driver removed successfully");
                            }
                            else
                            {
                                Console.WriteLine("Driver not exists");
                            }
                        }

                        else if (option2 == "3")
                        {

                            // Updating driver
                            Console.Write("Enter ID to update: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            string id = Console.ReadLine();
                            Console.ResetColor();

                            bool flag = Admin.searchByID(id);

                            if (flag == true)
                            {
                                Console.Write("Enter Name: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string name = Console.ReadLine();
                                Console.ResetColor();

                                Console.Write("Enter Age: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string val = Console.ReadLine();
                                Console.ResetColor();
                                int age = Convert.ToInt32(val);

                                Console.Write("Enter Gender: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string gender = Console.ReadLine();
                                Console.ResetColor();

                                Console.Write("Enter Address: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string address = Console.ReadLine();
                                Console.ResetColor();

                                bool flag2 = true; // This is for entering correct phone number
                                Console.Write("Enter Phone Number: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string phoneNumber = Console.ReadLine();
                                Console.ResetColor();
                                while (flag2)
                                {
                                    flag2 = false;

                                    // Verifying Correct Phone Number Format
                                    int phoneNumberLength = phoneNumber.Length;
                                    int i = 0;
                                    if (phoneNumberLength != 11 && phoneNumberLength != 0)
                                    {
                                        flag2 = true;
                                    }

                                    else
                                    {
                                        for (; i < phoneNumberLength; i++)
                                        {
                                            if (phoneNumber[i] != '0' && phoneNumber[i] != '1' && phoneNumber[i] != '2' && phoneNumber[i] != '3' && phoneNumber[i] != '4' && phoneNumber[i] != '5' && phoneNumber[i] != '6' && phoneNumber[i] != '7' && phoneNumber[i] != '8' && phoneNumber[i] != '9')
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (flag2)
                                    {
                                        Console.Write("Enter valid Number: ");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        phoneNumber = Console.ReadLine();
                                        Console.ResetColor();
                                    }
                                }

                                Console.Write("Enter Location : ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string loc = Console.ReadLine();
                                Console.ResetColor();
                                string[] dimensions = loc.Split(',');

                                Location currentLocation = new Location { latitude = float.Parse(dimensions[0]), longitude = float.Parse(dimensions[1]) };

                                // Asking for vehicle information
                                Console.Write("Enter Vehicle Type: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string type = Console.ReadLine();
                                Console.ResetColor();
                                Console.Write("Enter Model: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string model = Console.ReadLine();
                                Console.ResetColor();
                                Console.Write("Enter Licence Plate: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                string plate = Console.ReadLine();
                                Console.ResetColor();

                                Vehicle vehicle = new Vehicle { type = type, model = model, licencePlate = plate };

                                Driver driver = new Driver { name = name, age = age, gender = gender, address = address, phoneNumber = phoneNumber, currentLocation = currentLocation, vehicle = vehicle, availability = true };

                                Admin.updateDriver(id, driver);
                                Console.WriteLine("Driver updated successfully");
                            }
                            else
                            {
                                Console.WriteLine("Driver not exists");
                            }
                        }

                        else if (option2 == "4")
                        {
                            string option3 = "0";

                            while (option3 != "6")
                            {
                                searchMenu();
                                Console.Write("Select Option: ");
                                option3 = Console.ReadLine();
                                while (option3 != "1" && option3 != "2" && option3 != "3" && option3 != "4" && option3 != "5" && option3 != "6")
                                {
                                    Console.Write("Select Valid Optionnn: ");
                                    option3 = Console.ReadLine();
                                }

                                if (option3 == "1")
                                {
                                    Console.Write("Enter ID: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    string id = Console.ReadLine();
                                    Console.ResetColor();

                                    bool flag = Admin.searchByID(id);

                                    if (flag == false)
                                    {
                                        Console.WriteLine("Driver not exists");
                                    }

                                    else
                                    {
                                        Driver driver = Admin.searchDriverByID(id);

                                        // Showing searched data
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", driver.name, driver.age, driver.gender, driver.vehicle.type, driver.vehicle.model, driver.vehicle.licencePlate));
                                        Console.WriteLine("---------------------------------------------------------------------------");

                                    }
                                }

                                else if(option3 == "2")
                                {
                                    // Asking for name to search
                                    Console.Write("Enter name: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    string name = Console.ReadLine();
                                    Console.ResetColor();

                                    List<Driver> drivers = Admin.searchByName(name);

                                    // If drivers with that name not exist
                                    if(drivers.Count == 0)
                                    {
                                        Console.WriteLine("--------------------------------");
                                        Console.WriteLine("Drivers not exist with that name");
                                        Console.WriteLine("--------------------------------");
                                    }

                                    else
                                    {
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                        Console.WriteLine("---------------------------------------------------------------------------");

                                        for (int i = 0; i < drivers.Count; i++)
                                        {
                                            Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", drivers[i].name, drivers[i].age, drivers[i].gender, drivers[i].vehicle.type, drivers[i].vehicle.model, drivers[i].vehicle.licencePlate));
                                        }
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                    }
                                }

                                else if (option3 == "3")
                                {
                                    // Search by name
                                    Console.Write("Enter age: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    string age = Console.ReadLine();
                                    Console.ResetColor();

                                    List<Driver> drivers = Admin.searchByAge(age);

                                    if (drivers.Count == 0)
                                    {
                                        Console.WriteLine("--------------------------------");
                                        Console.WriteLine("Drivers not exist with that age");
                                        Console.WriteLine("--------------------------------");
                                    }

                                    else
                                    {
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                        Console.WriteLine("---------------------------------------------------------------------------");

                                        for (int i = 0; i < drivers.Count; i++)
                                        {
                                            Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", drivers[i].name, drivers[i].age, drivers[i].gender, drivers[i].vehicle.type, drivers[i].vehicle.model, drivers[i].vehicle.licencePlate));
                                        }
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                    }
                                }

                                else if (option3 == "4")
                                {
                                    // Search by gender
                                    Console.Write("Enter gender: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    string gender = Console.ReadLine();
                                    Console.ResetColor();

                                    List<Driver> drivers = Admin.searchByGender(gender);

                                    if (drivers.Count == 0)
                                    {
                                        Console.WriteLine("--------------------------------");
                                        Console.WriteLine("Drivers not exist with that gender");
                                        Console.WriteLine("--------------------------------");
                                    }

                                    // Showing all drivers with matched gender
                                    else
                                    {
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                        Console.WriteLine("---------------------------------------------------------------------------");

                                        for (int i = 0; i < drivers.Count; i++)
                                        {
                                            Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", drivers[i].name, drivers[i].age, drivers[i].gender, drivers[i].vehicle.type, drivers[i].vehicle.model, drivers[i].vehicle.licencePlate));
                                        }
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                    }
                                }

                                else if (option3 == "5")
                                {
                                    // Search by vehicle type
                                    Console.Write("Enter vehicle type: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    string type = Console.ReadLine();
                                    Console.ResetColor();

                                    List<Driver> drivers = Admin.searchByVehicle(type);

                                    if (drivers.Count == 0)
                                    {
                                        Console.WriteLine("--------------------------------");
                                        Console.WriteLine("Drivers not exist with that vehicle");
                                        Console.WriteLine("--------------------------------");
                                    }

                                    // Showing all drivers with matched vehicle
                                    else
                                    {
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                        Console.WriteLine("Name        Age        Gender        V.Type        V.Model        V.Licence");
                                        Console.WriteLine("---------------------------------------------------------------------------");

                                        for (int i = 0; i < drivers.Count; i++)
                                        {
                                            Console.WriteLine(String.Format("{0,-12}{1,-11}{2,-14}{3,-14}{4,-15}{5,-17}", drivers[i].name, drivers[i].age, drivers[i].gender, drivers[i].vehicle.type, drivers[i].vehicle.model, drivers[i].vehicle.licencePlate));
                                        }
                                        Console.WriteLine("---------------------------------------------------------------------------");
                                    }
                                }
                            }          
                        }
                    }
                }
            }
        }
    }
}
