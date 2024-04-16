using Microsoft.AspNetCore.Mvc;
using FlightNet.Core.Features;

namespace FlightNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanesController : ControllerBase
{
    private readonly PlaneListing _PlaneListing;

    public PlanesController(PlaneListing planeListing)
    {
        _PlaneListing = planeListing;
    }

    [HttpGet(Name = "GetPanes")]
    public IEnumerable<PlaneListing.PlaneListingItem> Get()
    {
        return _PlaneListing.GetPlanes();
    }
}
