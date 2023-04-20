using BSEF20M034_H03;

internal class Driver
{
    public string name { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string address { get; set; }
    public string phoneNumber { get; set; }
    public Location currentLocation { get; set; }
    public Vehicle vehicle { get; set; }
    public bool availability { get; set; }
    public void updateAvailability()
    {
        if (availability)
        {
            availability = false;
        }
        else
        {
            availability = true;
        }
    }
    public void updateLocation(Location l)
    {
        currentLocation.longitude = l.longitude;
        currentLocation.latitude = l.latitude;
    }
}