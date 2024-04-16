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

    private readonly IFlightRepository _FlightRepository;
    public FlightUpdate(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
    }
    public bool Update(UpdateItem item) {
        var flight = _FlightRepository
            .GetFlight(item.FlightId)
            .Select(f => {
                    f.Origin = _FlightRepository.GetCities()
                        .First(c => c.CityId == item.OriginCityId);
                    f.Destination = _FlightRepository.GetCities()
                        .First(c => c.CityId == item.DestinationCityId);
                    f.Plane = _FlightRepository.GetPlanes()
                        .First(c => c.PlaneId == item.PlaneId);
                    return f; 
                }
            )
            .First();
        return _FlightRepository
            .UpdateFlight(flight);
    }

}