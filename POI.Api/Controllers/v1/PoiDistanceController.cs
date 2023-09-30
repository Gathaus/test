using Microsoft.AspNetCore.Mvc;
using POI.Api.Extensions;
using POI.Application.Base.Service;
using POI.Application.Service.PoiDistanceService;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PoiDistanceController : BaseController
{
    #region constructor

    private readonly BaseBusinessService _baseService;


    public PoiDistanceController(BaseBusinessService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    [HttpPost("GetPoiDistanceByPoiId")]
    public async Task<IActionResult> GetPoiDistanceByPoiId([FromBody]GetPoiDistancesByPoiId.Request request)
    {
        var response = await _baseService.InvokeAsync<GetPoiDistancesByPoiId,
            GetPoiDistancesByPoiId.Request, GetPoiDistancesByPoiId.Response>(request);

        return response.ToActionResult();
    }

    [HttpPost("GetPoiDistancesByPoiIds")]
    public async Task<IActionResult> GetPoiDistancesByPoiIds([FromBody]GetPoiDistancesByPoiIds.Request request)
    {
        var response = await _baseService.InvokeAsync<GetPoiDistancesByPoiIds,
            GetPoiDistancesByPoiIds.Request, GetPoiDistancesByPoiIds.Response>(request);

        return response.ToActionResult();
    }
}
