using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightDetails {

    public class DetailsItem {
        public int OriginCityId { get; set; }
        public string OriginCityName { get; set; } = string.Empty;
        public int DestinationCityId { get; set; }
        public string DestinationCityName { get; set; } = string.Empty;
        public int PlaneId { get; set; }
        public string PlaneNameAndNumber { get; set; } = string.Empty;
        public double Distance { get; set; }
        public double Consumption { get; set;}
    }

    private readonly IFlightRepository _FlightRepository;
    public FlightDetails(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
    }

    public IEnumerable<DetailsItem> GetDetails(int id) {
        var distance = 0.0;
        return _FlightRepository
            .GetFlight(id)
            .Select(f => new DetailsItem() {
                OriginCityId = f.Origin.CityId 
                , OriginCityName = f.Origin.Name
                , DestinationCityId = f.Destination.CityId
                , DestinationCityName = f.Destination.Name
                , PlaneId = f.Plane.PlaneId
                , PlaneNameAndNumber = $"{f.Plane.Name} - {f.Plane.Number}"
                , Distance = distance = CalculateDistance(f.Origin, f.Destination)
                , Consumption = CalculateConsumption(f.Plane, distance)
                })
            .ToList();
    }

    public double CalculateDistance(City origin, City destination)
    {
        var radius = 6370.0; // earth radius in km
        var deg = Math.PI / 180;
        var lat0 = origin.Latitude; 
        var lat1 = destination.Latitude;
        var lon0 = origin.Longitude; 
        var lon1 = destination.Longitude;
        var a = Math.Cos(lat0 * deg) * Math.Cos(lat1 * deg) * Math.Cos(lon0 * deg) * Math.Cos(lon1 * deg);
        var b = Math.Cos(lat0 * deg) * Math.Sin(lon0 * deg) * Math.Cos(lat1 * deg) * Math.Sin(lon1 * deg);
        var c = Math.Sin(lat0 * deg) * Math.Sin(lat1 * deg);
        var d = Math.Acos(a + b + c) * radius; 

        return d;
    }

    public double CalculateConsumption(Plane plane, double distance)
    {
        return plane.AvgConsumptionOnTakeOff 
            + plane.AvgConsumptionAtCrusingAltitude 
            * distance / plane.AvgCrusingSpeed;
    }
}