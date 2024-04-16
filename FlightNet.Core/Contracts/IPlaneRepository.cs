using FlightNet.Core.Entities;

namespace FlightNet.Core.Contracts;

public interface IPlaneRepository {
    IEnumerable<Plane> GetPlanes();
}