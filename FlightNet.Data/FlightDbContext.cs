using FlightNet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightNet.Data;

public class FlightDbContext : DbContext {

    public FlightDbContext() { }
    public FlightDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Flight> Flights{ get; set; }
    public DbSet<City> Cities{ get; set; }
    public DbSet<Plane> Planes{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder) { 

        base.OnModelCreating(builder);

        //builder.Entity<Flight>().Property(x => x.Origin).HasColumnName("OriginId");
        //builder.Entity<Flight>().Property(x => x.Destination).HasColumnName("DestinationId");
        //builder.Entity<Flight>().HasOne<City>(f => f.Origin);
        //builder.Entity<Flight>().HasOne<City>(f => f.Destination);

    }

}