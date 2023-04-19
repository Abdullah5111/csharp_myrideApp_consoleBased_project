namespace BSEF20M034_H01
{
    internal class Vehicle
    {
        private string type;
        private string model;
        private string licencePlate;

        // Parameterized Constructor
        public Vehicle(string typ, string mod, string plate)
        {
            type = typ;
            model = mod;
            licencePlate = plate;
        }

        // Getters
        public string getType()
        {
            return type;
        }
        public string getModel()
        {
            return model;
        }
        public string getLicence()
        {
            return licencePlate;
        }

        // Setter
        public void setType(string typ)
        {
            type = typ;
        }
        public void setModel(string mod)
        {
            model = mod;
        }
        public void setLicencePlate(string plate)
        {
            licencePlate = plate;
        }
    }
}
