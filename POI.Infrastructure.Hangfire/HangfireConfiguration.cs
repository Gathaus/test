using System.Linq.Expressions;
using System.Reflection;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using POI.Application.BackgroundJobs;
using POI.Application.Base.BackgroundJobs;
using POI.Application.RecurringJobAttribute;
using POI.Infrastructure.Hangfire.Filters;

namespace POI.Infrastructure.Hangfire;

public static class HangfireConfiguration
{
    public static void AddHangfireServices(this IServiceCollection services, string connectionString)
    {
        services.AddHangfire(config => config
            .UseSqlServerStorage(connectionString));

        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var uniqueServerName = $"{System.Environment.MachineName}-{environmentName}";
    
        services.AddHangfireServer(x =>
        {
            x.ServerName = uniqueServerName;
            x.WorkerCount = 1;
            x.Queues = new[] {"default", "critical"};
            x.ServerTimeout = TimeSpan.FromMinutes(0.5); 
            
        });
        
        ConfigureJobFilters();
        
    }

    public static void RegisterAllRecurringJobs(Assembly jobAssembly)
    {
        var jobTypes = jobAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IRecurringJob)));

        foreach (var jobType in jobTypes)
        {
            var executeMethod = jobType.GetMethod("Execute");
            var attribute = executeMethod.GetCustomAttribute<RecurringJobAttribute>();
        
            // if RecurringJobAttribute exists
            if (attribute != null)
            {
                var jobExpression = Expression.Lambda<Action>(
                    Expression.Call(Expression.New(jobType), executeMethod));

                RecurringJob.AddOrUpdate(attribute.JobId, jobExpression, attribute.CronExpression);
            }
        }
    }
    
    public static void ConfigureQueues(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthorizationFilter() }
        });
    }
    public static void ConfigureJobFilters()
    {
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });
        GlobalJobFilters.Filters.Add(new HangfireExceptionFilter());
    }
    
    public static IApplicationBuilder UseCustomHangfireDashboard(this IApplicationBuilder app)
    {
        
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthorizationFilter() },  
            StatsPollingInterval = 60000 // 1 minute
        });

        var jobAssembly = JobAssemblyProvider.GetJobAssembly();
        RegisterAllRecurringJobs(jobAssembly);

        return app;
    }
}
