using Microsoft.AspNetCore.Mvc;
using POI.Api.Extensions;
using POI.Application.Base.Filters;
using POI.Application.Base.Service;
using POI.Application.Service.Poi;
using POI.Domain.Entities.ExaEntities.Models;
using POI.Domain.UnitOfWorks;

namespace POI.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CompanyController : BaseController
{

    private readonly BaseBusinessService _baseService;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(BaseBusinessService baseService, IUnitOfWork unitOfWork)
    {
        _baseService = baseService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetCompanies")]
    public async Task<IActionResult> GetCompanies()
    {
        var response = await _baseService.InvokeFilterAsync<GetCompanies, GetCompanies.Request, List<Company>>(new());
        return Ok(response);
    }



}