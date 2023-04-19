using System.Net.Mail;

namespace BSEF20M034_H01
{
    internal class Passenger
    {
        private string name;
        private string phoneNumber;

        // Parameterized Constructor
        public Passenger(string nam, string phone)
        {
            name = nam;
            phoneNumber = phone;
        }
        public void setPassenger(string nam, string phone)
        {
            this.name = nam;
            this.phoneNumber = phone;
        }

        // Getters
        public string getName()
        {
            return name;
        }

        public string getPhoneNumber()
        {
            return phoneNumber;
        }
    }
}
