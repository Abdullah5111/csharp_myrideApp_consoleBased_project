using System.Runtime;

namespace BSEF20M034_H01
{
    internal class Location
    {
        private float latitude;
        private float longitude;

        //Default Constructor
        public Location()
        {
            latitude = 0;
            longitude = 0;
        }

        // Parameterized Constructor
        public Location(float lat, float lon)
        {
            latitude = lat;
            longitude = lon;
        }

        // Getter
        public float[] getLocation()
        {
            return new float[] { this.latitude, this.longitude };
        }

        //Setter
        public void setLocation(float lat, float lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}
