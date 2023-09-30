using Hangfire;
using POI.Application.Base.BackgroundJobs;
using Serilog;

namespace POI.Application.BackgroundJobs;

public class SampleJob3 : IRecurringJob
{
    [Queue("default")]
    [RecurringJobAttribute.RecurringJob("sample-job3", "*/1 * * * *")]
    public void Execute()
    {
        Log.Information("HANGFIRE3 JOB EXECUTED");
        Log.Information("HANGFIRE3 JOB EXECUTED");
        Log.Information("HANGFIRE3 JOB EXECUTED");
        Log.Information("HANGFIRE3 JOB EXECUTED");
        
    }
}
