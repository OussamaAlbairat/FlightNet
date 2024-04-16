using FlightNet.Core.Contracts;

namespace FlightNet.Core.Features;

public class CityListing {

    public class CityListingItem {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }

    private readonly ICityRepository _CityRepository;
    public CityListing(ICityRepository cityRepository)
    {
        _CityRepository = cityRepository;
    }

    public IEnumerable<CityListingItem> GetCities() {
        return _CityRepository
            .GetCities()
            .Select(c => new CityListingItem() { 
                CityId = c.CityId
                , CityName = c.Name
                })
            .ToList();
    } 
}