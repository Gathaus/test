using Hangfire.Dashboard;

namespace POI.Infrastructure.Hangfire.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        //TODO - Add authorization logic will be added here
        return true;  
    }
}