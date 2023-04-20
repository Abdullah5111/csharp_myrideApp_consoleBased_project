using System;
using System.Collections.Generic;

namespace BSEF20M034_H03
{
    internal class Ride
    {
        public Location startLocation { get; set; }
        public Location endLocation { get; set; }
        public float price { get; set; }
        public Driver driver { get; set; }
        public Passenger passenger { get; set; }

        public Driver bookRide(Passenger p, Location startLoc, Location endLoc, string vehType)
        {
            // Getting list of available drivers
            List<Driver> availableDrivers = Admin.availDrivers(vehType);

            // If no driver available
            if (availableDrivers.Count == 0)
            {
                return null;
            }

            Driver d = availableDrivers[0];

            // Finding minimum distance driver .....
            // Applying eucular formula
            int num1 = (int)availableDrivers[0].currentLocation.latitude + (int)availableDrivers[0].currentLocation.longitude;
            int num2 = (int)startLoc.latitude + (int)startLoc.longitude;

            int minimumDistance = Math.Abs(num1 - num2);

            for (int i = 1; i < availableDrivers.Count; i++)
            {
                num1 = (int)availableDrivers[i].currentLocation.latitude + (int)availableDrivers[i].currentLocation.longitude;
                int temp = Math.Abs(num1 - num2);
                if (temp < minimumDistance)
                {
                    d = availableDrivers[i];
                }
            }

            // assignings
            assignDriver(d);
            assignPassenger(p);
            setLocations(startLoc, endLoc);

            return d;
        }

        public float calculatePrice(string vehicle, Location startLocation, Location endLocation)
        {
            // Applying eucular formula
            float num1 = ((float)endLocation.latitude - (float)startLocation.latitude) * ((float)endLocation.latitude - (float)startLocation.latitude);
            float num2 = ((float)endLocation.longitude - (float)startLocation.longitude) * ((float)endLocation.longitude - (float)startLocation.longitude);

            float distance = (float)Math.Sqrt(num2 + num1);

            if (vehicle == "bike")
            {
                price = (distance * 275) / 50;
                price = price + price / 20;
            }

            else if (vehicle == "Rickshaw")
            {
                price = ((distance * 275) / 35);
                price = price + price / 10;
            }

            else
            {
                price = ((distance * 275) / 15);
                price = price + price / 5;
            }

            return price;
        }
        public void assignDriver(Driver d)
        {
            driver = d;
        }
        public void assignPassenger(Passenger p)
        {
            passenger = new Passenger { name = p.name, phoneNumber = p.phoneNumber };
        }

        public void setLocations(Location startLoc, Location endLoc)
        {
            startLocation = new Location { latitude = startLoc.latitude, longitude = startLoc.longitude };

            endLocation = new Location { latitude = endLoc.latitude, longitude = endLoc.longitude };
        }
    }
}
