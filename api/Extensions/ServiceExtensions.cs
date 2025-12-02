using Microsoft.Extensions.DependencyInjection;
using GdzieBus.Api.Services.Implementations;
using GdzieBus.Api.Services.Interfaces;

namespace GdzieBus.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IGpsPositionService, GpsPositionService>();
        services.AddScoped<IStop, StopService>();
    }
}
