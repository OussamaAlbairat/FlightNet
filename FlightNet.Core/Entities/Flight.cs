namespace FlightNet.Core.Entities;
public class Flight
{
    public int FlightId { get; set; }   
    public City Origin { get; set; } = new City();
    public City Destination { get; set; } = new City();
    public Plane Plane {get; set; } = new Plane();

}
