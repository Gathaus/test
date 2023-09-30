using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POI.Domain.Repositories;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.Ef.Base;
using POI.Infrastructure.Ef.Context;

namespace POI.Infrastructure.Ef;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureEf(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));
        services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        
        services.AddDbContext<PoiDbContext>(options =>
            options.UseSqlServer(connectionString,
                x=>x.UseNetTopologySuite()));


        return services;
    }
}