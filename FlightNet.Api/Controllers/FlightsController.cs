using Microsoft.AspNetCore.Mvc;
using FlightNet.Core.Features;
using System.Net;
using FlightNet.Core.Contracts;

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
        try {
            var resp = _FlightCreate.Create(item);
            return Ok(resp);
        }
        catch (ValidationException ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut()]
    public ActionResult Put([FromBody] FlightUpdate.UpdateItem item) {
        try {
            _FlightUpdate.Update(item);
        }
        catch (ValidationException ex) {
            return BadRequest(ex.Message);
        }
        return Ok(HttpStatusCode.NoContent);
    }

    [HttpDelete()]
    public ActionResult Delete(int id) {
        _FlightDelete.Delete(id);
        return Ok(HttpStatusCode.OK);
    }
}
