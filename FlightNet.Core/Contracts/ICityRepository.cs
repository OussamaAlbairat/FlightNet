using FlightNet.Core.Entities;

namespace FlightNet.Core.Contracts;

public interface ICityRepository {
    IEnumerable<City> GetCities();
}