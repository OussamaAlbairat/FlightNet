using System.Linq;
using FlightNet.Core.Contracts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace FlightNet.Data;
public static class DataServicesRegistration
{
    static SqliteConnection _connection;
    public static SqliteConnection Connection() {
        if (_connection == null) {
            _connection = new SqliteConnection("FileName=:memory:");
            _connection.Open();
        }
        return _connection;
    }
    public static IServiceCollection AddFlightDataServices(this IServiceCollection services) {
        services.AddDbContext<FlightDbContext>(options => options.UseSqlite(Connection()));
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IPlaneRepository, PlaneRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        SeedData();
        return services;
    }

    public static void SeedData() {

        var options = new DbContextOptionsBuilder().UseSqlite(Connection()).Options;
        
        using var context = new FlightDbContext(options);

        /*create database*/
        context.Database.EnsureCreated();
        
        /* seeding cities */
        context.Cities.Add(new Core.Entities.City() {
            CityId = 1
            , Name = "Casablanca"
            , Latitude = 33.5333
            , Longitude = -7.5833
        });
        context.Cities.Add(new Core.Entities.City() {
            CityId = 2
            , Name = "New York"
            , Latitude = 40.7306
            , Longitude = -73.9352
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 3
            , Name = "Paris"
            , Latitude = 48.8567
            , Longitude = 2.3522
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 4
            , Name = "London"
            , Latitude = 51.5072
            , Longitude = -0.1275
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 5
            , Name = "Tokyo"
            , Latitude = 35.6897
            , Longitude = 139.6922
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 6
            , Name = "Cairo"
            , Latitude = 30.0444
            , Longitude = 31.2358
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 7
            , Name = "Johannesburg"
            , Latitude = -26.2044
            , Longitude = 28.0456
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 8
            , Name = "Rio"
            , Latitude = -22.9111
            , Longitude = -43.2056
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 9
            , Name = "Moscow"
            , Latitude = 55.7558
            , Longitude = 37.6172
        });        
        context.Cities.Add(new Core.Entities.City() {
            CityId = 10
            , Name = "Sydney"
            , Latitude = -33.8678
            , Longitude = 151.2100
        });

        context.SaveChanges();

        /* seeding planes */
        context.Planes.Add(new Core.Entities.Plane() {
            PlaneId = 1
            , Name = "Boeing 787-9 Dreamliner"
            , Number = "CN-RGX"
            , AvgCrusingSpeed = 900
            , AvgConsumptionAtCrusingAltitude = 3.8
            , AvgConsumptionOnTakeOff = (5.0 / 60) * 5
        });

        context.Planes.Add(new Core.Entities.Plane() {
            PlaneId = 2
            , Name = "Boeing 737-800"
            , Number = "CN-RGM"
            , AvgCrusingSpeed = 800
            , AvgConsumptionAtCrusingAltitude = 1.4
            , AvgConsumptionOnTakeOff = (2.3 / 60) * 5
        });

        context.Planes.Add(new Core.Entities.Plane() {
            PlaneId = 3
            , Name = "Embraer ERJ-190"
            , Number = "CN-RGO"
            , AvgCrusingSpeed = 600
            , AvgConsumptionAtCrusingAltitude = 1.1
            , AvgConsumptionOnTakeOff = (2.0 / 60) * 5
        });

        context.SaveChanges();

        /*seeding the flights*/
        context.Flights.Add(new Core.Entities.Flight() {
            FlightId = 1
            , Origin = context.Cities.First(c => c.CityId == 1)
            , Destination = context.Cities.First(c => c.CityId == 2)
            , Plane = context.Planes.First(c => c.PlaneId == 1)
        });
        
        context.SaveChanges();
    }
}
