using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace BSEF20M034_H03
{
    internal class Admin
    {
        static public bool searchByID(string id)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT COUNT(*) FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            int count = (int)cmd.ExecuteScalar();

            conn.Close();

            return count > 0;
        }
        static public int addDriver(Driver d)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string query = $"insert into Location (latitude, longitude) values ('{d.currentLocation.latitude}','{d.currentLocation.longitude}')";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.ExecuteNonQuery();

            query = "SELECT TOP 1 * FROM Location ORDER BY id DESC";
            cmd = new SqlCommand(query, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int locationID = reader.GetInt32(0);
            reader.Close();

            query = $"insert into Vehicle (type, model, licencePlate) values ('{d.vehicle.type}','{d.vehicle.model}', '{d.vehicle.licencePlate}')";

            cmd = new SqlCommand(query, conn);

            cmd.ExecuteNonQuery();

            query = "SELECT TOP 1 * FROM Vehicle ORDER BY id DESC";
            cmd = new SqlCommand(query, conn);

            reader = cmd.ExecuteReader();

            reader.Read();
            int vehicleID = reader.GetInt32(0);
            reader.Close();

            int avail = 0;

            if (d.availability == true)
                avail = 1;
            query = $"insert into Driver (name, age, gender, address, phoneNumber, vehicle_id, location_id, availability) values ('{d.name}','{d.age}', '{d.gender}','{d.address}','{d.phoneNumber}','{vehicleID}','{locationID}','{avail}')";

            cmd = new SqlCommand(query, conn);

            cmd.ExecuteNonQuery();

            query = "SELECT TOP 1 * FROM Driver ORDER BY id DESC";
            cmd = new SqlCommand(query, conn);

            reader = cmd.ExecuteReader();

            reader.Read();
            int driverid = reader.GetInt32(0);
            reader.Close();

            conn.Close();

            return driverid;
        }

        static public bool removeDriver(string id)
        {
            if (!searchByID(id))
            {
                return false;
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT location_id, vehicle_id FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int location_id = -1, vehicle_id = -1;

            if (reader.Read())
            {
                location_id = (int)reader["location_id"];
                vehicle_id = (int)reader["vehicle_id"];
            }
            reader.Close();

            //Delete the driver record
            cmd.CommandText = $"DELETE FROM Driver WHERE id = {id}";
            cmd.ExecuteNonQuery();

            // Delete the location record
            cmd.CommandText = $"DELETE FROM Location WHERE id = {location_id}";
            cmd.ExecuteNonQuery();

            // Delete the vehicle record
            cmd.CommandText = $"DELETE FROM Vehicle WHERE id = {vehicle_id}";
            cmd.ExecuteNonQuery();

            conn.Close();

            return true;
        }

        static public void updateDriver(string id, Driver driver)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT location_id, vehicle_id FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int location_id = -1, vehicle_id = -1;

            if (reader.Read())
            {
                location_id = (int)reader["location_id"];
                vehicle_id = (int)reader["vehicle_id"];
            }

            reader.Close();

            if (driver.currentLocation.latitude != 0 || driver.currentLocation.longitude != 0)
            {
                query = $"UPDATE Location SET latitude = {driver.currentLocation.latitude}, longitude = {driver.currentLocation.longitude} WHERE id = {location_id}";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            if (driver.vehicle.type != "" || driver.vehicle.model != "" || driver.vehicle.licencePlate != "")
            {
                if (driver.vehicle.type != "")
                {
                    query = $"UPDATE Vehicle SET type = '{driver.vehicle.type}' WHERE id =  {vehicle_id}";

                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }

                if (driver.vehicle.model != "")
                {
                    query = $"UPDATE Vehicle SET model = '{driver.vehicle.model}' WHERE id = {vehicle_id}";

                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }

                if (driver.vehicle.licencePlate != "")
                {
                    query = $"UPDATE Vehicle SET licencePlate = '{driver.vehicle.licencePlate}' WHERE id = {vehicle_id}";

                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                
            }

            if (driver.name != "")
            {
                query = $"UPDATE Driver SET name = '{driver.name}' WHERE id = {id}";

                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            if (driver.age != 0)
            {
                query = $"UPDATE Driver SET age = {driver.age} WHERE id = {id}";

                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            if (driver.gender != "")
            {
                query = $"UPDATE Driver SET gender = '{driver.gender}' WHERE id = {id}";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            if (driver.address != "")
            {
                query = $"UPDATE Driver SET address = '{driver.address}' WHERE id = {id}";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            if (driver.phoneNumber != "")
            {
                query = $"UPDATE Driver SET phoneNumber = '{driver.phoneNumber}' WHERE id = {id}";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        static public void updateAvailability(string id)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT availability FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int avail = (int)reader["availability"];

                if(avail != 0)
                {
                    conn.Close();
                    query = $"UPDATE Driver SET availability = 0 WHERE id = {id}";
                    cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Close();
                    query = $"UPDATE Driver SET availability = 1 WHERE id = {id}";
                    cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            reader.Close();
            conn.Close();
        }

        static public void updateLocation(string id, Location loc)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT location_id FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int location_id = (int)reader["location_id"];

                conn.Close();
                query = $"UPDATE Location SET latitude = '{loc.latitude}', longitude = '{loc.longitude}'  WHERE id = {location_id}";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            reader.Close();
            conn.Close();
        }

        static public Driver searchDriverByID(string id)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(id);
            string query = $"SELECT * FROM Driver WHERE id = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int location_id = -1, vehicle_id = -1;

            string name = "";
            int age = 0;
            string gender = "";
            string address = "";
            string phoneNumber = "";
            bool availability = true;

            if (reader.Read())
            {
                location_id = (int)reader["location_id"];
                vehicle_id = (int)reader["vehicle_id"];

                if ((int)reader["availability"] == 0)
                {
                    availability = false;
                }

                name = (string)reader["name"];
                age = (int)reader["age"];
                gender = (string)reader["gender"];
                address = (string)reader["address"];
                phoneNumber = (string)reader["phoneNumber"];
            }
            reader.Close();

            query = $"SELECT * FROM Location WHERE id = {location_id}";
            cmd = new SqlCommand(query, conn);
            SqlDataReader reader1 = cmd.ExecuteReader();

            float latitude = 0;
            float longitude = 0;

            if (reader1.Read())
            {
                latitude = (float)(double)reader1["latitude"];
                longitude = (float)(double)reader1["longitude"];
            }

            reader1.Close();

            query = $"SELECT * FROM Vehicle WHERE id = {vehicle_id}";
            cmd = new SqlCommand(query, conn);
            SqlDataReader reader2 = cmd.ExecuteReader();

            string type = "";
            string model = "";
            string licencePlate = "";

            if (reader2.Read())
            {
                type = (string)reader2["type"];
                model = (string)reader2["model"];
                licencePlate = (string)reader2["licencePlate"];
            }

            reader2.Close();

            Location location = new Location { latitude = latitude, longitude = longitude };
            Vehicle vehicle = new Vehicle { type = type, model = model, licencePlate = licencePlate };
            conn.Close();

            Driver driver = new Driver { name = name, age = age, gender = gender, address = address, phoneNumber = phoneNumber, currentLocation = location, vehicle = vehicle, availability = availability };

            return driver;
        }

        static public List<Driver> searchByName(string name)
        {
            List<Driver> drivers = new List<Driver>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string query = $"SELECT id FROM Driver WHERE name = '{name}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                drivers.Add(Utils.getDriverById(id));
            }
            reader.Close();

            conn.Close();

            return drivers;
        }

        static public List<Driver> searchByAge(string age)
        {
            List<Driver> drivers = new List<Driver>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            int val = int.Parse(age);
            string query = $"SELECT id FROM Driver WHERE age = {val}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                drivers.Add(Utils.getDriverById(id));
            }
            reader.Close();

            conn.Close();

            return drivers;
        }

        static public List<Driver> searchByGender(string gender)
        {
            List<Driver> drivers = new List<Driver>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            string query = $"SELECT id FROM Driver WHERE gender = '{gender}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                drivers.Add(Utils.getDriverById(id));
            }
            reader.Close();

            conn.Close();
            return drivers;
        }

        static public List<Driver> searchByVehicle(string type)
        {
            List<Driver> drivers = new List<Driver>();
            List<int> vehicleIds = new List<int>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string query = $"SELECT id FROM Vehicle WHERE type = '{type}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                vehicleIds.Add(id);
            }
            reader.Close();

            for(int i = 0; i < vehicleIds.Count; ++i)
            {
                query = $"SELECT id FROM Driver WHERE vehicle_id = {vehicleIds[i]}";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    int id = (int)reader["id"];
                    drivers.Add(Utils.getDriverById(id));
                }
                reader.Close();
            }

            conn.Close();
            return drivers;
        }
        
        static public List<Driver> availDrivers(string vehType)
        {
            List<Driver> drivers = new List<Driver>();
            List<int> vehicleIds = new List<int>();


            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            string query = $"SELECT id FROM Vehicle WHERE type = '{vehType}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["Id"];
                vehicleIds.Add(id);
            }

            reader.Close();

            for(int i = 0; i < vehicleIds.Count; ++i)
            {
                query = $"SELECT id FROM Driver WHERE vehicle_id = {vehicleIds[i]} and availability = 1";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    int driverId = (int)reader["Id"];
                    drivers.Add(Utils.getDriverById(driverId));
                }

                reader.Close();
            }

            conn.Close();

            return drivers;
        }
    }
}
