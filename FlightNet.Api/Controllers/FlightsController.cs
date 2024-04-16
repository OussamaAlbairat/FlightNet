using Microsoft.AspNetCore.Mvc;
using FlightNet.Core.Features;
using System.Net;

namespace FlightNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{

    private readonly FlightCreate _FlightCreate;
    private readonly FlightUpdate _FlightUpdate;
    private readonly FlightDelete _FlightDelete;
    private readonly FlightListing _FlightListing;
    private readonly FlightDetails _FlightDetails;

    public FlightsController(FlightListing flightListing
        , FlightDetails flightDetails
        , FlightCreate flightCreate
        , FlightUpdate flightUpdate
        , FlightDelete flightDelete)
    {
        _FlightListing = flightListing;
        _FlightDetails = flightDetails;
        _FlightCreate = flightCreate;
        _FlightUpdate = flightUpdate;
        _FlightDelete = flightDelete;
    }

    [HttpGet(Name = "GetFlights")]
    public IEnumerable<FlightListing.ListingItem> Get()
    {
        return _FlightListing.GetFlights();
    }

    [HttpGet("{id}")]
    public IEnumerable<FlightDetails.DetailsItem> Get(int id)
    {
        return _FlightDetails.GetDetails(id);
    }

    [HttpPost()]
    public ActionResult Post([FromBody] FlightCreate.CreateItem item) {
        _FlightCreate.Create(item);
        return Ok(HttpStatusCode.Created);
    }

    [HttpPut()]
    public ActionResult Put([FromBody] FlightUpdate.UpdateItem item) {
        _FlightUpdate.Update(item);
        return Ok(HttpStatusCode.OK);
    }

    [HttpDelete()]
    public ActionResult Delete(int id) {
        _FlightDelete.Delete(id);
        return Ok(HttpStatusCode.OK);
    }
}
