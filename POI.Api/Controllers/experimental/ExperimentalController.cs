using Microsoft.AspNetCore.Mvc;
using POI.Api.Extensions;
using POI.Application.Base.Service;
using POI.Application.Service;
using POI.Application.Service.Poi;

namespace POI.Api.Controllers.@internal;

//these endpoints are here just to show new features of the application
[ApiExplorerSettings(GroupName = "experimental")]
[Route("api/experimental/[controller]")]
[ApiController]
public class ExperimentalController : BaseController
{
    
    private readonly BaseBusinessService _baseService;
    public ExperimentalController(BaseBusinessService baseService)
    {
        _baseService = baseService;
    }

    
    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
        var response = await _baseService.InvokeAsync<Test, Test.Request, Test.Response>(new());
        return Ok(response);
    }

    [HttpGet("GetPoiCatalogByIdRawSql/{Id}")]
    public async Task<IActionResult> GetPoiCatalogByIdWithRawSql([FromRoute]GetPoiCatalogByIdRawSql.Request request)
    {
        var response = await _baseService.InvokeAsync<GetPoiCatalogByIdRawSql, GetPoiCatalogByIdRawSql.Request, GetPoiCatalogByIdRawSql.Response>(request);
        return Ok(response);
    }
    
    [HttpGet("GetSalesPointFromExaLive")]
    public async Task<IActionResult> GetSalesPointFromExaLive()
    {
        var response = await _baseService.InvokeAsync<GetSalesPointFromExaLive, GetSalesPointFromExaLive.Request, GetSalesPointFromExaLive.Response>(new());
        return response.ToActionResult();
    }

}