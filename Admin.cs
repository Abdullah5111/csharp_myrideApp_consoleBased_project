using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BSEF20M034_H02
{
    internal class Admin
    {
        static public bool searchByID(string id)
        {
            FileStream f = new FileStream("IDs.txt", FileMode.Open);
            StreamReader sr = new StreamReader(f);

            // Checking if that ID exists in our record or not
            bool flag = true;
            string myID = sr.ReadLine();
            while (flag == true && myID != null)
            {
                if (myID == id)
                {
                    flag = false;
                }
                myID = sr.ReadLine();
            }

            f.Close();
            sr.Close();

            if (flag == true)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
        static public int addDriver(Driver d)
        {
            FileStream f = new FileStream("Drivers.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(f);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader sr = new StreamReader(f2);

            // We will get the last ID from our IDs record
            string n = sr.ReadLine();
            int id = int.Parse(n); ;
            while(n != null)
            {
                id = int.Parse(n);
                n = sr.ReadLine();
            }

            // Increment 1 in the last recorded ID and will use it for the next driver
            ++id;

            f2.Close();
            sr.Close();

            f2 = new FileStream("IDs.txt", FileMode.Append);
            StreamWriter swID = new StreamWriter(f2);
            swID.WriteLine(id);
            swID.Close();
            f2.Close();

            // Writing new driver on file
            sw.WriteLine(id);
            sw.WriteLine(d.name);
            sw.WriteLine(d.age);
            sw.WriteLine(d.gender);
            sw.WriteLine(d.address);
            sw.WriteLine(d.phoneNumber);
            sw.WriteLine(d.currentLocation.latitude + "," + d.currentLocation.longitude);
            sw.WriteLine(d.vehicle.type + "," + d.vehicle.model + "," + d.vehicle.licencePlate);
            sw.WriteLine(d.availability);

            sw.Close();
            f.Close();

            return id;
        }

        static public bool removeDriver(string id)
        {
            // Either driver exists or not
            if(searchByID(id) == false)
            {
                return false;
            }

            else
            {
                List<Driver> drivers = new List<Driver>();
                // First id must me zero (for my logic)
                List<string> IDs = new List<string> { "0" };

                FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
                StreamReader srDriver = new StreamReader(f1);

                FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
                StreamReader srID = new StreamReader(f2);

                string myID = srID.ReadLine(); // Because first is 0 id
                myID = srID.ReadLine();
                while(myID != null)
                {
                    // All other records should be kept as it is
                    if (myID != id)
                    {
                        IDs.Add(myID);
                    }

                    // Creating driver objects and storing in list temporarily
                    string driverId = srDriver.ReadLine();
                    string name = srDriver.ReadLine();
                    string age = srDriver.ReadLine();
                    string gender = srDriver.ReadLine();
                    string address = srDriver.ReadLine();
                    string phone = srDriver.ReadLine();
                    string[] loc = srDriver.ReadLine().Split(',');
                    string[] veh = srDriver.ReadLine().Split(',');
                    string availability = srDriver.ReadLine();

                    Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                    Vehicle vehicle = new Vehicle {type = veh[0], model = veh[1], licencePlate = veh[2] };

                    bool avail;
                    if (availability == "True")
                        avail = true;
                    else
                        avail = false;

                    if(driverId != id)
                    {
                        Driver driver = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                        drivers.Add(driver);
                    }

                    myID = srID.ReadLine();
                }

                srDriver.Close();
                srID.Close();
                f1.Close();
                f2.Close();

                FileStream fDrivers = new FileStream("Drivers.txt", FileMode.Create);
                StreamWriter swDrivers = new StreamWriter(fDrivers);

                FileStream fIDs = new FileStream("IDs.txt", FileMode.Create);
                StreamWriter swIDs = new StreamWriter(fIDs);

                // Writing all the data back to the files except removed one
                for(int i = 0; i < drivers.Count; ++i)
                {
                    swIDs.WriteLine(IDs[i]);

                    swDrivers.WriteLine(IDs[i + 1]);
                    swDrivers.WriteLine(drivers[i].name);
                    swDrivers.WriteLine(drivers[i].age);
                    swDrivers.WriteLine(drivers[i].gender);
                    swDrivers.WriteLine(drivers[i].address);
                    swDrivers.WriteLine(drivers[i].phoneNumber);
                    swDrivers.WriteLine(drivers[i].currentLocation.latitude + "," + drivers[i].currentLocation.longitude);
                    swDrivers.WriteLine(drivers[i].vehicle.type + "," + drivers[i].vehicle.model + "," + drivers[i].vehicle.licencePlate);
                    swDrivers.WriteLine(drivers[i].availability);
                }

                swIDs.WriteLine(IDs[drivers.Count]);


                swDrivers.Close();
                fDrivers.Close();

                swIDs.Close();
                fIDs.Close();

                return true;
            }
        }

        static public void updateDriver(string id, Driver driver)
        {
            List<Driver> drivers = new List<Driver>();
            List<string> IDs = new List<string> { "0" };

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                IDs.Add(myID);

                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (driverId == id)
                {
                    if (driver.name != "")
                    {
                        name = driver.name;
                    }
                    if (driver.gender != "")
                    {
                        gender = driver.gender;
                    }
                    if (driver.address != "")
                    {
                        address = driver.address;
                    }
                    if (driver.phoneNumber != "")
                    {
                        phone = driver.phoneNumber;
                    }
                    if (driver.vehicle.type != "")
                    {
                        vehicle.type = driver.vehicle.type;
                    }
                    if (driver.vehicle.model != "")
                    {
                        vehicle.model = driver.vehicle.model;
                    }
                    if (driver.vehicle.licencePlate != "")
                    {
                        vehicle.licencePlate = driver.vehicle.licencePlate;
                    }

                    Driver d = new Driver { name = name, age = driver.age, gender = gender, address = address, phoneNumber = phone, currentLocation = driver.currentLocation, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                else
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                myID = srID.ReadLine();
            }

            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            FileStream fDrivers = new FileStream("Drivers.txt", FileMode.Create);
            StreamWriter swDrivers = new StreamWriter(fDrivers);

            FileStream fIDs = new FileStream("IDs.txt", FileMode.Create);
            StreamWriter swIDs = new StreamWriter(fIDs);

            for (int i = 0; i < drivers.Count; ++i)
            {
                swIDs.WriteLine(IDs[i]);

                swDrivers.WriteLine(IDs[i + 1]);
                swDrivers.WriteLine(drivers[i].name);
                swDrivers.WriteLine(drivers[i].age);
                swDrivers.WriteLine(drivers[i].gender);
                swDrivers.WriteLine(drivers[i].address);
                swDrivers.WriteLine(drivers[i].phoneNumber);
                swDrivers.WriteLine(drivers[i].currentLocation.latitude + "," + drivers[i].currentLocation.longitude);
                swDrivers.WriteLine(drivers[i].vehicle.type + "," + drivers[i].vehicle.model + "," + drivers[i].vehicle.licencePlate);
                swDrivers.WriteLine(drivers[i].availability);
            }

            swIDs.WriteLine(IDs[drivers.Count]);

            swDrivers.Close();
            fDrivers.Close();

            swIDs.Close();
            fIDs.Close();
        }

        static public Driver searchDriverByID(string id)
        {
            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (driverId == id)
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    srDriver.Close();
                    srID.Close();
                    f1.Close();
                    f2.Close(); 
                    
                    return d;
                }
                myID = srID.ReadLine();
            }

            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            return null;
        }

        static public List<Driver> searchByName(string nam)
        {
            List<Driver> drivers = new List<Driver>();

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (name == nam)
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                myID = srID.ReadLine();
            }
            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            return drivers;
        }

        static public List<Driver> searchByAge(string a)
        {
            List<Driver> drivers = new List<Driver>();

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (age == a)
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                myID = srID.ReadLine();
            }
            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            return drivers;
        }

        static public List<Driver> searchByGender(string g)
        {
            List<Driver> drivers = new List<Driver>();

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (gender == g)
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                myID = srID.ReadLine();
            }
            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            return drivers;
        }

        static public List<Driver> searchByVehicle(string typ)
        {
            List<Driver> drivers = new List<Driver>();

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (vehicle.type == typ)
                {
                    Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };

                    drivers.Add(d);
                }

                myID = srID.ReadLine();
            }
            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            return drivers;
        }

        static public void updateAvailability(string id)
        {
            List<Driver> drivers = new List<Driver>();
            List<string> IDs = new List<string> { "0" };

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                IDs.Add(myID);

                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (driverId == id)
                {
                    if(availability == "True")
                    {
                        avail = false;
                    }
                    else
                    {
                        avail = true;
                    }
                }

                Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };
                drivers.Add(d);

                myID = srID.ReadLine();
            }

            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            FileStream fDrivers = new FileStream("Drivers.txt", FileMode.Create);
            StreamWriter swDrivers = new StreamWriter(fDrivers);

            for (int i = 0; i < drivers.Count; ++i)
            {
                swDrivers.WriteLine(IDs[i + 1]);
                swDrivers.WriteLine(drivers[i].name);
                swDrivers.WriteLine(drivers[i].age);
                swDrivers.WriteLine(drivers[i].gender);
                swDrivers.WriteLine(drivers[i].address);
                swDrivers.WriteLine(drivers[i].phoneNumber);
                swDrivers.WriteLine(drivers[i].currentLocation.latitude + "," + drivers[i].currentLocation.longitude);
                swDrivers.WriteLine(drivers[i].vehicle.type + "," + drivers[i].vehicle.model + "," + drivers[i].vehicle.licencePlate);
                swDrivers.WriteLine(drivers[i].availability);
            }

            swDrivers.Close();
            fDrivers.Close();
        }

        static public void updateLocation(string id, Location l)
        {
            List<Driver> drivers = new List<Driver>();
            List<string> IDs = new List<string> { "0" };

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();
            while (myID != null)
            {
                IDs.Add(myID);

                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                if (driverId == id)
                {
                    location = l;
                }

                Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };
                drivers.Add(d);

                myID = srID.ReadLine();
            }

            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            FileStream fDrivers = new FileStream("Drivers.txt", FileMode.Create);
            StreamWriter swDrivers = new StreamWriter(fDrivers);

            for (int i = 0; i < drivers.Count; ++i)
            {
                swDrivers.WriteLine(IDs[i + 1]);
                swDrivers.WriteLine(drivers[i].name);
                swDrivers.WriteLine(drivers[i].age);
                swDrivers.WriteLine(drivers[i].gender);
                swDrivers.WriteLine(drivers[i].address);
                swDrivers.WriteLine(drivers[i].phoneNumber);
                swDrivers.WriteLine(drivers[i].currentLocation.latitude + "," + drivers[i].currentLocation.longitude);
                swDrivers.WriteLine(drivers[i].vehicle.type + "," + drivers[i].vehicle.model + "," + drivers[i].vehicle.licencePlate);
                swDrivers.WriteLine(drivers[i].availability);
            }

            swDrivers.Close();
            fDrivers.Close();
        }

        static public List<Driver> availDrivers(string vehType)
        {
            List<Driver> drivers = new List<Driver>();
            List<Driver> availableDrivers = new List<Driver>();
            List<string> IDs = new List<string> { "0" };

            FileStream f1 = new FileStream("Drivers.txt", FileMode.Open);
            StreamReader srDriver = new StreamReader(f1);

            FileStream f2 = new FileStream("IDs.txt", FileMode.Open);
            StreamReader srID = new StreamReader(f2);

            string myID = srID.ReadLine(); // Because first is 0 id
            myID = srID.ReadLine();

            while (myID != null)
            {
                IDs.Add(myID);

                string driverId = srDriver.ReadLine();
                string name = srDriver.ReadLine();
                string age = srDriver.ReadLine();
                string gender = srDriver.ReadLine();
                string address = srDriver.ReadLine();
                string phone = srDriver.ReadLine();
                string[] loc = srDriver.ReadLine().Split(',');
                string[] veh = srDriver.ReadLine().Split(',');
                string availability = srDriver.ReadLine();

                Location location = new Location { latitude = float.Parse(loc[0]), longitude = float.Parse(loc[1]) };

                Vehicle vehicle = new Vehicle { type = veh[0], model = veh[1], licencePlate = veh[2] };

                bool avail;
                if (availability == "True")
                    avail = true;
                else
                    avail = false;

                Driver d = new Driver { name = name, age = int.Parse(age), gender = gender, address = address, phoneNumber = phone, currentLocation = location, vehicle = vehicle, availability = avail };
                drivers.Add(d);

                if(avail == true && vehicle.type == vehType)
                {
                    availableDrivers.Add(d);
                }

                myID = srID.ReadLine();
            }

            srDriver.Close();
            srID.Close();
            f1.Close();
            f2.Close();

            FileStream fDrivers = new FileStream("Drivers.txt", FileMode.Create);
            StreamWriter swDrivers = new StreamWriter(fDrivers);

            for (int i = 0; i < drivers.Count; ++i)
            {
                swDrivers.WriteLine(IDs[i + 1]);
                swDrivers.WriteLine(drivers[i].name);
                swDrivers.WriteLine(drivers[i].age);
                swDrivers.WriteLine(drivers[i].gender);
                swDrivers.WriteLine(drivers[i].address);
                swDrivers.WriteLine(drivers[i].phoneNumber);
                swDrivers.WriteLine(drivers[i].currentLocation.latitude + "," + drivers[i].currentLocation.longitude);
                swDrivers.WriteLine(drivers[i].vehicle.type + "," + drivers[i].vehicle.model + "," + drivers[i].vehicle.licencePlate);
                swDrivers.WriteLine(drivers[i].availability);
            }

            swDrivers.Close();
            fDrivers.Close();

            return availableDrivers;
        }
    }
}
