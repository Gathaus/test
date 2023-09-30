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
public class SalesPointController : BaseController
{

    private readonly BaseBusinessService _baseService;

    public SalesPointController(BaseBusinessService baseService, IUnitOfWork unitOfWork)
    {
        _baseService = baseService;
    }
    
    [HttpPost("MatchSalesPoint")]
    public async Task<IActionResult> MatchSalesPoint(MatchSalesPoint.Request request)
    {
        var response = await _baseService.InvokeAsync<MatchSalesPoint, MatchSalesPoint.Request, MatchSalesPoint.Response>(request);

        if (!response.IsSuccess)
            return BadRequest(response);
        
        return Ok(response);
    }

    [HttpPost("GetSalesPointForMatch")]
    public async Task<IActionResult> GetSalesPointForMatch(GetSalesPointForMatch.Request request, [FromQuery] PaginationFilter filter)
    {
        request.PaginationFilter = filter;
        var response = await _baseService.InvokeDynamicAsync<GetSalesPointForMatch, GetSalesPointForMatch.Request, PagedTableResponse<SalesPointDto>>(request);
        return Ok(response);
    }



}