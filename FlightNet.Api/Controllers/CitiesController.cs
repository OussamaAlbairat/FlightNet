using Microsoft.AspNetCore.Mvc;
using FlightNet.Core.Features;

namespace FlightNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{
    private readonly CityListing _CityListing;

    public CitiesController(CityListing cityListing)
    {
        _CityListing = cityListing;
    }

    [HttpGet(Name = "GetCities")]
    public IEnumerable<CityListing.CityListingItem> Get()
    {
        return _CityListing.GetCities();
    }
}
