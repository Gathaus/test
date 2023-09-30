using System.Reflection;

namespace POI.Application.BackgroundJobs;

public static class JobAssemblyProvider
{
    public static Assembly GetJobAssembly() => typeof(JobAssemblyProvider).Assembly;
}