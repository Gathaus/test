using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthCheckController : BaseController
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> CheckHealth()
    {
        var report = await _healthCheckService.CheckHealthAsync();

        return report.Status == HealthStatus.Healthy 
            ? Ok("Service running") 
            : StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service not running");
    }

}