namespace POI.Application.RecurringJobAttribute;

[AttributeUsage(AttributeTargets.Method)]
public class RecurringJobAttribute : Attribute
{
    public string CronExpression { get; }
    public string JobId { get; }

    public RecurringJobAttribute(string jobId, string cronExpression)
    {
        JobId = jobId;
        CronExpression = cronExpression;
    }
}
