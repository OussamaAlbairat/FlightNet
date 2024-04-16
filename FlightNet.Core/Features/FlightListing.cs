using FlightNet.Core.Contracts;

namespace FlightNet.Core.Features;

public class FlightListing {

    public class ListingItem {
        public string OriginCityName { get; set; } = string.Empty;
        public string DestinationCityName { get; set; } = string.Empty;
        public string PlaneNameAndNumber { get; set; } = string.Empty;
    }

    private readonly IFlightRepository _FlightRepository;
    public FlightListing(IFlightRepository flightRepository)
    {
        _FlightRepository = flightRepository;
    }

    public IEnumerable<ListingItem> GetFlights() {
        return _FlightRepository
            .GetFlights()
            .Select(f => new ListingItem() { 
                OriginCityName = f.Origin.Name
                , DestinationCityName = f.Destination.Name
                , PlaneNameAndNumber = $"{f.Plane.Name} - {f.Plane.Number}"
                })
            .ToList();
    } 
}