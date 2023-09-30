using Microsoft.AspNetCore.Mvc;
using POI.Api.Extensions;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Service.Location;
using POI.Domain.UnitOfWorks;

namespace POI.Api.Controllers.v1;


[ApiController]
[Route("api/v1/[controller]")]
public class LocationController : BaseController
{
    private readonly BaseBusinessService _baseService;

    public LocationController(BaseBusinessService baseService, IUnitOfWork unitOfWork)
    {
        _baseService = baseService;
    }

    [HttpGet("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        var response = await _baseService.InvokeFilterAsync<GetAllCountries, GetAllCountries.Request, List<GetCountryDto>>(new());

        return Ok(response);
    }

    
    [HttpGet("GetCities")]
    public async Task<IActionResult> GetCities([FromQuery]GetCities.Request request)
    {
        var response = await _baseService.InvokeFilterAsync<GetCities,GetCities.Request, List<GetPoiCityDto>>(request);

        return Ok(response);
    }
    
    [HttpGet("GetCounties")]
    public async Task<IActionResult> GetCounties([FromQuery]GetCounties.Request request)
    {
        var response = await _baseService.InvokeFilterAsync<GetCounties,GetCounties.Request, List<GetPoiCountyDto>>(request);

        return Ok(response);
    }
    


}