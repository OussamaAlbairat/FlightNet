using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightDelete {

    private readonly IFlightRepository _FlightRepository;
    public FlightDelete(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
    }
    public bool Delete(int flightId) {
        var flight = _FlightRepository.GetFlight(flightId).First();
        return _FlightRepository.RemoveFlight(flight);
    }

}