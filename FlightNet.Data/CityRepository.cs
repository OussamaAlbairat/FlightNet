using FlightNet.Core.Entities;
using FlightNet.Data;

namespace FlightNet.Core.Contracts;

public class CityRepository : ICityRepository {

    private readonly FlightDbContext _context;
    public CityRepository(FlightDbContext context) {
        _context = context;
    }
    public IEnumerable<City> GetCities() {
        return _context.Cities;

    }
}