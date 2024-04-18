using System.Net.Security;
using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;
using FlightNet.Core.Features;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

namespace FlightNet.Test;

public class MockCityRepository : ICityRepository
{
    private City[] _Cities;
    public MockCityRepository() {
        _Cities = [ new City() {
                CityId = 1,
                Name = "Casa",
                Latitude = 10.0,
                Longitude = 20.0
            }, new City() {
                CityId=2,
                Name = "New york",
                Latitude = 20.0,
                Longitude = 30.0
            }, new City() {
                CityId=3,
                Name = "Paris",
                Latitude = 30.0,
                Longitude = 40.0
            }];
    }
    public IEnumerable<City> GetCities()
    {
        return _Cities;
    }
}

public class MockPlaneRepository : IPlaneRepository
{
    private Plane[] _Planes;
    public MockPlaneRepository() {
        _Planes = [ new Plane() {
                PlaneId = 1,
                Name = "Boeing",
                Number = "123",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            }, new Plane() {
                PlaneId = 2,
                Name = "Boeing",
                Number = "456",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            }];
    }
    public IEnumerable<Plane> GetPlanes()
    {
        return _Planes;
    }
}
public class MockFlightRepository : IFlightRepository {

    private City[] _Cities;
    private Plane[] _Planes;
    private Flight[] _Flights;
    public MockFlightRepository() { 
        _Cities = [ new City() {
                CityId = 1,
                Name = "Casa",
                Latitude = 10.0,
                Longitude = 20.0
            }, new City() {
                CityId=2,
                Name = "New york",
                Latitude = 20.0,
                Longitude = 30.0
            }, new City() {
                CityId=3,
                Name = "Paris",
                Latitude = 30.0,
                Longitude = 40.0
            }];
        _Planes = [ new Plane() {
                PlaneId = 1,
                Name = "Boeing",
                Number = "123",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            }, new Plane() {
                PlaneId = 2,
                Name = "Boeing",
                Number = "456",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            }];
        _Flights = [ new Flight() {
            FlightId = 1,
            Origin = new City() {
                CityId = 1,
                Name = "Casa",
                Latitude = 10.0,
                Longitude = 20.0
            },
            Destination = new City() {
                CityId=2,
                Name = "New york",
                Latitude = 30.0,
                Longitude = 40.0
            },
            Plane = new Plane() {
                PlaneId = 1,
                Name = "Boeing",
                Number = "123",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            }
        }];
    }
    public IEnumerable<City> GetCities() {
        return _Cities;
    }
    public IEnumerable<Plane> GetPlanes() {
        return _Planes;
    }
    public IEnumerable<Flight> GetFlights() {
        return _Flights;
    }
    public IEnumerable<Flight> GetFlight(int id) {
        return GetFlights().Where(f=>f.FlightId == id);
    }
    public Flight _Addedflight;
    public bool AddFlight(Flight flight) {
        _Addedflight = flight;
        return true;
    }
    public Flight _RemovedFlight;
    public bool RemoveFlight(Flight flight) {
        _RemovedFlight = flight;
        return true;
    }
    public Flight _UpdatedFlight;
    public bool UpdateFlight(Flight flight) {
        _UpdatedFlight = flight;
        return true;
    }
    public bool FlightAlreadyExists(int flightId
        , int planeId, int originCityId, int destinationCityId) {
        return _Flights.Where(f=>f.FlightId != flightId
                && f.Plane.PlaneId == planeId
                && f.Origin.CityId == originCityId
                && f.Destination.CityId == destinationCityId)
                .Any();
    }
}

[TestClass]
public class UnitTestFlights
{
    [TestMethod]
    public void TestCityListing()
    {
        var listing = new CityListing(new MockCityRepository());
        var flights = listing.GetCities();
        Assert.AreEqual(3, flights.Count());
    }
    [TestMethod]
    public void TestPlaneListing()
    {
        var listing = new PlaneListing(new MockPlaneRepository());
        var flights = listing.GetPlanes();
        Assert.AreEqual(2, flights.Count());
    }
    [TestMethod]
    public void TestFlightListing()
    {
        var listing = new FlightListing(new MockFlightRepository());
        var flights = listing.GetFlights();
        Assert.AreEqual(1, flights.Count());
        Assert.AreEqual(1, flights.First().FlightId);
        Assert.AreEqual("Casa", flights.First().OriginCityName);
        Assert.AreEqual("New york", flights.First().DestinationCityName);
        Assert.AreEqual("Boeing - 123", flights.First().PlaneNameAndNumber);
    }

    [TestMethod]
    public void TestFlightDetails()
    {
        var listing = new FlightDetails(new MockFlightRepository());
        var flights = listing.GetDetails(1);
        Assert.AreEqual(1, flights.Count());
        Assert.AreEqual(1, flights.First().FlightId);
        Assert.AreEqual("Casa", flights.First().OriginCityName);
        Assert.AreEqual("New york", flights.First().DestinationCityName);
        Assert.AreEqual("Boeing - 123", flights.First().PlaneNameAndNumber);
        Assert.AreNotEqual(0, flights.First().Consumption);
        Assert.AreNotEqual(0, flights.First().Distance);
    }

    [TestMethod]
    public void TestFlightCreate() 
    {
        var repo = new MockFlightRepository();
        var item = new FlightCreate.CreateItem() {
            OriginCityId = 1,
            DestinationCityId = 3,
            PlaneId = 1,
        };
        var create = new FlightCreate(repo);
        create.Create(item);
        Assert.IsNotNull(repo._Addedflight);
        Assert.AreEqual(1, repo._Addedflight.Plane.PlaneId);
        Assert.AreEqual(1, repo._Addedflight.Origin.CityId);
        Assert.AreEqual(3, repo._Addedflight.Destination.CityId);
    }

    [TestMethod]
    public void TestFlightUpdate() 
    {
        var repo = new MockFlightRepository();
        var item = new FlightUpdate.UpdateItem() {
            FlightId = 1,
            OriginCityId = 2,
            DestinationCityId = 3,
            PlaneId = 2,
        };
        var update = new FlightUpdate(repo);
        update.Update(item);
        Assert.IsNotNull(repo._UpdatedFlight);
        Assert.AreEqual(2, repo._UpdatedFlight.Plane.PlaneId);
        Assert.AreEqual(2, repo._UpdatedFlight.Origin.CityId);
        Assert.AreEqual(3, repo._UpdatedFlight.Destination.CityId);
    }

    [TestMethod]
    public void TestFlightDelete() 
    {
        var repo = new MockFlightRepository();
        var delete = new FlightDelete(repo);
        delete.Delete(1);
        Assert.IsNotNull(repo._RemovedFlight);
        Assert.AreEqual(1, repo._RemovedFlight.FlightId);
    }

    [TestMethod]
    public void TestFlightValidate() 
    {
        var flight = new Flight() {
            FlightId = 1,
            Origin = null,
            Destination = null,
            Plane = null
        };
        var repo = new MockFlightRepository();
        var validate = new FlightValidate(repo);
        //check the plane
        Assert.ThrowsException<ValidationException>(
            ()=>validate.Assert(flight)
            , "Flight Plane is not valid");
        flight.Plane = new Plane() {
                PlaneId = 1,
                Name = "Boeing",
                Number = "123",
                AvgCrusingSpeed = 1,
                AvgConsumptionOnTakeOff = 1,
                AvgConsumptionAtCrusingAltitude = 1
            };
        //check the origin
        Assert.ThrowsException<ValidationException>(
            ()=>validate.Assert(flight)
            , "Flight Origin is not valid");
        flight.Origin = new City() {
                CityId = 1,
                Name = "Casa",
                Latitude = 0.0,
                Longitude = 0.0
            };
        //check the destination
        Assert.ThrowsException<ValidationException>(
            ()=>validate.Assert(flight)
            , "Flight Destination is not valid");
        flight.Destination = new City() {
                CityId=1,
                Name = "Casa",
                Latitude = 0.0,
                Longitude = 0.0
            };
        //check cities are equal
        Assert.ThrowsException<ValidationException>(
            ()=>validate.Assert(flight)
            , "Flight Origin and Destination cannot be the same");
        flight.Destination = new City() {
                CityId=2,
                Name = "New york",
                Latitude = 0.0,
                Longitude = 0.0
            };
        //check flight already exists
        flight.FlightId = 2;
        Assert.ThrowsException<ValidationException>(
            ()=>validate.Assert(flight)
            , "Flight Already exists");

    }
}