using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POI.Infrastructure.MessageBus.Consumers;
using POI.Infrastructure.MessageBus.Messages;

namespace POI.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<SampleMessageConsumer>();
        services.AddScoped<PoiCreatedConsumer>();
        services.AddScoped<PoiDeletedConsumer>();
        services.AddScoped<PoiUpdatedConsumer>();

    }


}