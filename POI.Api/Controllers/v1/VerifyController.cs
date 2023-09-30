using Microsoft.AspNetCore.Mvc;
using POI.Application.Base.Service;
using POI.Application.Service.Poi;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class VerifyController : BaseController
{

    #region constructor

    private readonly BaseBusinessService _baseService;

    public VerifyController(BaseBusinessService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    [HttpPost("CreatePoiBySalesPointId")]
    public async Task<IActionResult> CreatePoiBySalesPointId([FromBody]CreatePoiBySalesPointId.Request request)
    {
        var response = await _baseService.InvokeAsync<CreatePoiBySalesPointId,
            CreatePoiBySalesPointId.Request, CreatePoiBySalesPointId.Response>(request);
        return Ok(response);
    }
}
