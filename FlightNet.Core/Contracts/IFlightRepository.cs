using FlightNet.Core.Entities;

namespace FlightNet.Core.Contracts;

public interface IFlightRepository {
    IEnumerable<City> GetCities();
    IEnumerable<Plane> GetPlanes();
    IEnumerable<Flight> GetFlights();
    IEnumerable<Flight> GetFlight(int id);
    bool AddFlight(Flight flight);
    bool RemoveFlight(Flight flight);
    bool UpdateFlight(Flight flight);
    bool FlightAlreadyExists(int flightId, int planeId, int originCityId, int destinationCityId);
}