using Hangfire;
using POI.Application.Base.BackgroundJobs;
using Serilog;

namespace POI.Application.BackgroundJobs;

public class SampleJob2 : IRecurringJob
{
    [Queue("default")]
    [RecurringJobAttribute.RecurringJob("sample-job2", "*/1 * * * *")]
    public void Execute()
    {
        Log.Information("HANGFIRE2 JOB EXECUTED");
        Log.Information("HANGFIRE2 JOB EXECUTED");
        Log.Information("HANGFIRE2 JOB EXECUTED");
        Log.Information("HANGFIRE2 JOB EXECUTED");
        
    }
}
