using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightNet.Data;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;
    public FlightRepository(FlightDbContext context) {
        _context = context;
    }

    public IEnumerable<City> GetCities() {
        return _context.Cities;
    }

    public IEnumerable<Plane> GetPlanes() {
        return _context.Planes;
    }

    public IEnumerable<Flight> GetFlights()
    {
        return _context.Flights
            .Include(f => f.Origin)
            .Include(f => f.Destination)
            .Include(f => f.Plane);
    }

    public IEnumerable<Flight> GetFlight(int id) {
        return GetFlights().Where(f => f.FlightId == id);
    }

    public bool AddFlight(Flight flight)
    {
        _context.Flights.Add(flight);
        return _context.SaveChanges() == 1;
    }

    public bool RemoveFlight(Flight flight)
    {
        _context.Entry(flight).State = EntityState.Deleted;
        return _context.SaveChanges() == 1;
    }

    public bool UpdateFlight(Flight flight)
    {
        _context.Entry(flight).State = EntityState.Modified;
        return _context.SaveChanges() == 1;
    }

    public bool FlightAlreadyExists(int flightId, int planeId, int originCityId, int destinationCityId) {
        return _context.Flights
            .Include(f=>f.Plane)
            .Include(f=>f.Origin)
            .Include(f=>f.Destination)
            .Where(f=>f.FlightId != flightId 
                    && f.Plane.PlaneId == planeId
                    && f.Origin.CityId == originCityId
                    && f.Destination.CityId == destinationCityId)
            .Any();
    }
}