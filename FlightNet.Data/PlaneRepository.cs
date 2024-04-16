using FlightNet.Core.Entities;
using FlightNet.Data;

namespace FlightNet.Core.Contracts;

public class PlaneRepository : IPlaneRepository {

    private readonly FlightDbContext _context;
    public PlaneRepository(FlightDbContext context) {
        _context = context;
    }
    public IEnumerable<Plane> GetPlanes() {
        return _context.Planes;
    }
}