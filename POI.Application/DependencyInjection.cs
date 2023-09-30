using Microsoft.Extensions.DependencyInjection;
using POI.Application.Base.Service;
using POI.Application.Service;

namespace POI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<BaseBusinessService>();
        services.AddSingleton<PoiBufferService>();
        // Otomatik servis kayıt işlemi
        // Tek argümanlı olanlar için
        var businessServiceTypeSingle = typeof(IBusinessService<>);
        var typesSingle = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p =>
                p.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == businessServiceTypeSingle) &&
                !p.IsAbstract);

        foreach (var type in typesSingle)
        {
            services.AddScoped(type);
        }

        // İki argümanlı olanlar için
        var businessServiceTypeDouble = typeof(IBusinessService<,>);
        var typesDouble = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p =>
                p.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == businessServiceTypeDouble) &&
                !p.IsAbstract);

        foreach (var type in typesDouble)
        {
            services.AddScoped(type);
        }
        // İki argümanlı olanlar için
        var filterServiceTypeDouble = typeof(IFilterService<,>);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p =>
                p.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == filterServiceTypeDouble) &&
                !p.IsAbstract);

        foreach (var type in types)
        {
            services.AddScoped(type);
        }
        
        // İki argümanlı olanlar için
        var dynamicServiceTypeDouble = typeof(IDynamicResultService<,>);
        var dynamicTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p =>
                p.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == dynamicServiceTypeDouble) &&
                !p.IsAbstract);

        foreach (var type in dynamicTypes)
        {
            services.AddScoped(type);
        }

        return services;
    }
}