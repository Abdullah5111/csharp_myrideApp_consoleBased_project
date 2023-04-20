using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSEF20M034_H03
{
    internal class Utils
    {
        static public string getPhoneNumber()
        {
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

            return passengerPhoneNumber;
        }

        static public Location inputLocation()
        {
            // Asking Starting Location
            Console.Write("Enter Location: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string startLoc = Console.ReadLine();
            Console.ResetColor();

            string[] dimensions = startLoc.Split(',');

            Location location = new Location { longitude = float.Parse(dimensions[1]), latitude = float.Parse(dimensions[0]) };

            return location;
        }

        static public Driver getDriverById(int id)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyRideApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string query = $"SELECT * FROM Driver WHERE id = {id}";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int location_id = -1, vehicle_id = -1;

            string name = "";
            int age = 0;
            string gender = "";
            string address = "";
            string phoneNumber = "";
            int availability = 0;

            if (reader.Read())
            {
                location_id = (int)reader["location_id"];
                vehicle_id = (int)reader["vehicle_id"];

                name = (string)reader["name"];
                age = (int)reader["age"];
                gender = (string)reader["gender"];
                address = (string)reader["address"];
                phoneNumber = (string)reader["phoneNumber"];
                availability = (int)reader["availability"];
            }
            reader.Close();

            query = $"SELECT * FROM Location WHERE id = {location_id}";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            Location loc = new Location();
            if(reader.Read())
            {
                loc = new Location { latitude = (float)(double)reader["latitude"], longitude = (float)(double)reader["longitude"] };
            }

            reader.Close();

            query = $"SELECT * FROM vehicle WHERE id = {vehicle_id}";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            Vehicle veh = new Vehicle();
            if (reader.Read())
            {
                veh = new Vehicle { type = (string)reader["type"], model = (string)reader["model"], licencePlate = (string)reader["licencePlate"] };
            }

            reader.Close();

            conn.Close();

            bool avail = availability == 0 ? false : true;

            return new Driver { name = name, age = age, gender = gender, address = address, phoneNumber = phoneNumber, currentLocation = loc, vehicle = veh, availability = avail };
        }
    }
}
