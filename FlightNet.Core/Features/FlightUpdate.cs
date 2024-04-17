using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightUpdate {

    public class UpdateItem {
        public int FlightId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public int PlaneId { get; set; }
    }

    private readonly FlightValidate _FlightValidate;
    private readonly IFlightRepository _FlightRepository;
    public FlightUpdate(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
        _FlightValidate = new FlightValidate();
    }
    public bool Update(UpdateItem item) {
        var flight = _FlightRepository
            .GetFlight(item.FlightId)
            .Select(f => {
#pragma warning disable CS8601 // Possible null reference assignment.
                    f.Origin = _FlightRepository.GetCities()
                        .FirstOrDefault(c => c.CityId == item.OriginCityId);
                    f.Destination = _FlightRepository.GetCities()
                        .FirstOrDefault(c => c.CityId == item.DestinationCityId);
                    f.Plane = _FlightRepository.GetPlanes()
                        .FirstOrDefault(c => c.PlaneId == item.PlaneId);
                    return f; 
#pragma warning restore CS8601 // Possible null reference assignment.
                }
            )
            .First();
        _FlightValidate.Assert(flight);
        return _FlightRepository
            .UpdateFlight(flight);
    }

}