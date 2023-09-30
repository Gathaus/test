using Hangfire;
using POI.Application.Base.BackgroundJobs;
using Serilog;

namespace POI.Application.BackgroundJobs;

public class SampleJob : IRecurringJob
{
    [Queue("critical")]
    [RecurringJobAttribute.RecurringJob("sample-job", "*/1 * * * *")]
    public void Execute()
    {
        Log.Information("HANGFIRE1 JOB EXECUTED");
        Log.Information("HANGFIRE1 JOB EXECUTED");
        Log.Information("HANGFIRE1 JOB EXECUTED");
        Log.Information("HANGFIRE1 JOB EXECUTED");
        
    }
}
