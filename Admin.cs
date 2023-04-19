using System;
using System.Collections.Generic;

namespace BSEF20M034_H01
{
    internal class Admin
    {
        static private List<Driver> drivers;
        static private int driversCount = 0;
        static int ID = 0;

        public Admin()
        {
            drivers = new List<Driver>();
        }
        // This function gives nearest driver with respect to passengers current Location
        static public Driver giveNearestDriver(Location passengerLoc, string type)
        {
            List<Driver> availableDrivers = null;

            int count2 = 0;

            int n = driversCount;

            for (int i = 0; i < n; ++i)
            {
                if (drivers[i].getAvailability() && drivers[i].getVehicleType() == type)
                {
                    if (count2 == 0)
                    {
                        availableDrivers = new List<Driver> { drivers[i] };
                        count2 = 1;
                    }
                    else
                    {
                        availableDrivers.Add(drivers[i]);
                        count2++;
                    }
                }
            }

            n = count2;

            if (n == 0)
            {
                Console.WriteLine("No Driver Available");
                return null;
            }

            Driver driver = availableDrivers[0];

            int num1 = (int)availableDrivers[0].getCurrentLocation().getLocation()[0] + (int)availableDrivers[0].getCurrentLocation().getLocation()[1];
            int num2 = (int)passengerLoc.getLocation()[0] + (int)passengerLoc.getLocation()[1];

            int minimumDistance = Math.Abs(num1 - num2);

            for (int i = 1; i < n; i++)
            {
                int temp = 0;

                num1 = (int)availableDrivers[i].getCurrentLocation().getLocation()[0] + (int)availableDrivers[i].getCurrentLocation().getLocation()[1];
                temp = Math.Abs(num1 - num2);
                if (temp < minimumDistance)
                {
                    driver = availableDrivers[i];
                }
            }
            return driver;
        }

        public Driver searchDriverById(int id)
        {

            // Checking if such driver exists in our drivers list
            for (int i = 0; i < driversCount; i++)
            {
                if (drivers[i].getID() == id)
                {
                    return drivers[i];
                }
            }
            return null;
        }

        public int addDriver(Driver driver)
        {
            drivers.Add(driver);
            driversCount++;
            return ID;
        }

        public void removeDriver(int id)
        {
            for (int i = 0; i < driversCount; i++)
            {
                if (drivers[i].getID() == id)
                {
                    drivers.RemoveAt(i);
                    Console.WriteLine("\nDriver Removed !!!\n");
                    driversCount--;
                    return;
                }
            }

            Console.WriteLine("\nDriver With This ID Not Exists !!!\n");
        }

        public void updateDriver(int id, int age, string type, string model, string plate)
        {
            for(int i = 0; i < driversCount; ++i)
            {
                if (drivers[i].getID() == id)
                {
                    drivers[i].setAge(age);
                    drivers[i].setVehicle(type, model, plate);
                    return;
                }
            }
        }

        public Driver searchDriver(int id)
        {
            return searchDriverById(id);
        }

        static public int getUniqueID()
        {
            ID++;
            return ID;
        }
    }
}
