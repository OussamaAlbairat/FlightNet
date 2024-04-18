using System.Net.Security;
using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;
using FlightNet.Core.Features;

namespace FlightNet.Test;

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
                Latitude = 0.0,
                Longitude = 0.0
            },
            Destination = new City() {
                CityId=2,
                Name = "New york",
                Latitude = 0.0,
                Longitude = 0.0
            },
            Plane = new Plane() {
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
        return new Flight[] { new Flight()};
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
}