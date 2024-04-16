namespace FlightNet.Core.Entities;
public class City
{
    public int CityId { get; set;}
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

}