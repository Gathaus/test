using Microsoft.AspNetCore.Mvc;
using POI.Api.Extensions;
using POI.Application.Base.Filters;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Responses;
using POI.Application.Service.Poi;
using POI.Domain.UnitOfWorks;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PoiCatalogController : BaseController
{

    private readonly BaseBusinessService _baseService;
    private readonly ILogger<PoiCatalogController> _logger;

    public PoiCatalogController(BaseBusinessService baseService, IUnitOfWork unitOfWork, ILogger<PoiCatalogController> logger)
    {
        _baseService = baseService;
        _logger = logger;
    }
    

    [HttpGet("GetPoiCatalogById/{Id}")]
    public async Task<IActionResult> GetPoiCatalogById([FromRoute]GetPoiCatalogById.Request request)
    {
        _logger.LogInformation("GetPoiCatalogById called", request,request.Id);
        
        
        var response = await _baseService.InvokeAsync<GetPoiCatalogById, GetPoiCatalogById.Request, GetPoiCatalogById.Response>(request);
        return Ok(response);
    }
    
    [HttpPost("GetPoiCatalogs")]
    public async Task<IActionResult> GetPoiCatalogs(GetPoiCatalogs.Request request, [FromQuery]PaginationFilter filter)
    {
        
        request.PaginationFilter = filter;
        var response = await _baseService.InvokeAsync<GetPoiCatalogs, GetPoiCatalogs.Request, GetPoiCatalogs.Response>(request);
        return Ok(response);
    }
    [HttpPost("GetPoiCatalogsAsTable")]
    public async Task<IActionResult> GetPoiCatalogsAsTable(GetPoiCatalogsAsTable.Request request, [FromQuery]PaginationFilter filter)
    {
        
        request.PaginationFilter = filter;
        var response = await _baseService.InvokeDynamicAsync<GetPoiCatalogsAsTable, GetPoiCatalogsAsTable.Request, PagedTableResponse<PoiCatalogDto>>(request);
        return Ok(response);
    }
  
    [HttpPost("GetPoiCatalogsWithoutPagination")]
    public async Task<IActionResult> GetPoiCatalogs(GetPoiCatalogsWithoutPagination.Request request)
    {
        
        var response = await _baseService.InvokeAsync<GetPoiCatalogsWithoutPagination, GetPoiCatalogsWithoutPagination.Request, GetPoiCatalogsWithoutPagination.Response>(request);
        return Ok(response);
    }
    
    [HttpPost("CreatePoiCatalog")]
    public async Task<IActionResult> CreatePoiCatalog(CreatePoiCatalog.Request request)
    {
        var response = await _baseService.InvokeAsync<CreatePoiCatalog, CreatePoiCatalog.Request>(request);
        return Ok(response);
    }

    
    [HttpPost("UpdatePoiCatalog")]
    public async Task<IActionResult> UpdatePoiCatalog(UpdatePoiCatalog.Request request)
    {
        var response = await _baseService.InvokeAsync<UpdatePoiCatalog, UpdatePoiCatalog.Request, UpdatePoiCatalog.Response>(request);

        if (!response.IsSuccess)
            return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpGet("CalculatePoiDistances")]
    public async Task<IActionResult> CalculatePoiDistances()
    {
        var response = await _baseService.InvokeAsync<CalculatePoiDistances, CalculatePoiDistances.Request, CalculatePoiDistances.Response>(new());
        return Ok(response);
    }
    
    [HttpPost("DeletePoiCatalog")]
    public async Task<IActionResult> DeletePoiCatalog(DeletePoiCatalog.Request request)
    {
        var response = await _baseService.InvokeAsync<DeletePoiCatalog, DeletePoiCatalog.Request, DeletePoiCatalog.Response>(request);
        return Ok(response);
    }
    
    [HttpGet("GetSalesPointFromExaLive")]
    public async Task<IActionResult> GetSalesPointFromExaLive()
    {
        var response = await _baseService.InvokeAsync<GetSalesPointFromExaLive, GetSalesPointFromExaLive.Request, GetSalesPointFromExaLive.Response>(new());
        return response.ToActionResult();
    }

    //[HttpGet("GetSalesPointForMatch")]
    //public async Task<IActionResult> GetSalesPointForMatch()
    //{
    //    var response = await _baseService.InvokeAsync<GetSalesPointForMatch, GetSalesPointForMatch.Request, GetSalesPointForMatch.Response>(new());
    //    return response.ToActionResult();
    //}



}