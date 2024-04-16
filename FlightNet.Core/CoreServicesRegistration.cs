using FlightNet.Core.Features;
using Microsoft.Extensions.DependencyInjection;

namespace FlightNet.Data;
public static class CoreServicesRegistration
{
    public static IServiceCollection AddFlightCoreServices(this IServiceCollection services)
    {
        services.AddScoped<CityListing>();
        services.AddScoped<PlaneListing>();
        services.AddScoped<FlightListing>();
        services.AddScoped<FlightDetails>();
        services.AddScoped<FlightCreate>();
        services.AddScoped<FlightUpdate>();
        services.AddScoped<FlightDelete>();
        return services;
    }
}
