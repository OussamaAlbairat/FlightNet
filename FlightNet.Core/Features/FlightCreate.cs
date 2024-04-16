using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightCreate {

    public class CreateItem {
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public int PlaneId { get; set; }
    }

    private readonly IFlightRepository _FlightRepository;
    public FlightCreate(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
    }
    public bool Create(CreateItem item) {
        var flight = new Flight() {
            Origin = _FlightRepository.GetCities()
                .First(c => c.CityId == item.OriginCityId)
            , Destination = _FlightRepository.GetCities()
                .First(c => c.CityId == item.DestinationCityId)
            , Plane = _FlightRepository.GetPlanes()
                .First(c => c.PlaneId == item.PlaneId)
        };
        return _FlightRepository
            .AddFlight(flight);
    }

}