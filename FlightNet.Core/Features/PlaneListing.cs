using FlightNet.Core.Contracts;

namespace FlightNet.Core.Features;

public class PlaneListing {

    public class PlaneListingItem {
        public int PlaneId { get; set; }
        public string PlaneNameAndNumber { get; set; } = string.Empty;
    }

    private readonly IPlaneRepository _PlaneRepository;
    public PlaneListing(IPlaneRepository planeRepository)
    {
        _PlaneRepository = planeRepository;
    }

    public IEnumerable<PlaneListingItem> GetPlanes() {
        return _PlaneRepository
            .GetPlanes()
            .Select(p => new PlaneListingItem() { 
                PlaneId = p.PlaneId
                , PlaneNameAndNumber = $"{p.Name} - {p.Number}"
                })
            .ToList();
    } 
}