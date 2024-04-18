using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightCreate {

    public class CreateItem {
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public int PlaneId { get; set; }
    }

    public class CreateItemResponse {
        public int FlightId { get; set; }
    }

    private readonly FlightValidate _FlightValidate;
    private readonly IFlightRepository _FlightRepository;
    public FlightCreate(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
        _FlightValidate = new FlightValidate(flightRepository);
    }
    public CreateItemResponse Create(CreateItem item) {
#pragma warning disable CS8601 // Possible null reference assignment.
        var flight = new Flight() {
            Origin = _FlightRepository.GetCities()
                .FirstOrDefault(c => c.CityId == item.OriginCityId)
            , Destination = _FlightRepository.GetCities()
                .FirstOrDefault(c => c.CityId == item.DestinationCityId)
            , Plane = _FlightRepository.GetPlanes()
                .FirstOrDefault(c => c.PlaneId == item.PlaneId)
        };
#pragma warning restore CS8601 // Possible null reference assignment.
        _FlightValidate.Assert(flight);
        _FlightRepository.AddFlight(flight);
        return new CreateItemResponse() { FlightId = flight.FlightId };
    }

}