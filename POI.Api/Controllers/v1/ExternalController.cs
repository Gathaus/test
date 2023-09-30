using Microsoft.AspNetCore.Mvc;
using POI.Application.Base.Service;
using POI.Application.Service.External;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ExternalController : BaseController
{

    #region constructor

    private readonly BaseBusinessService _baseService;

    public ExternalController(BaseBusinessService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    [HttpPost("GetGoogleMapsInfo")]
    public async Task<IActionResult> GetGoogleMapsInfo([FromBody]GetGoogleMapsInfo.Request request)
    {
        var result = await _baseService.InvokeDynamicAsync<GetGoogleMapsInfo, GetGoogleMapsInfo.Request, GetGoogleMapsInfo.Response>(request);
        return Ok(result);
    }
    
    [HttpPost("GetGoogleMapsInfo2")]
    public async Task<IActionResult> GetGoogleMapsInfo2([FromBody]GetGoogleMapsInfo2.Request request)
    {
        var result = await _baseService.InvokeDynamicAsync<GetGoogleMapsInfo2, GetGoogleMapsInfo2.Request, GetGoogleMapsInfo2.Response>(request);
        return Ok(result);
    }
}