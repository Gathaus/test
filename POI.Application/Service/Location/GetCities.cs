using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Location;

public class GetCities : IFilterService<GetCities.Request, List<GetPoiCityDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetCities(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int CountryId { get; set; }
    }



    #endregion

    public async Task<FilterResult<List<GetPoiCityDto>>> ExecuteAsync(Request request)
    {
        var data = await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x => x.CountryId == request.CountryId && !x.IsPassive)
            .Select(x=>new GetPoiCityDto()
            {
                Id = x.CityId,
                Name = x.CityName
            })
            .Distinct()
            .OrderBy(x=>x.Name)
            .ToListAsync();
        
        // var response = ToResponse(data);
        
        return FilterResult<List<GetPoiCityDto>>.Success(data);
    }
    
    // private  Response ToResponse(List<GetPoiCityDto> result)
    // {
    //     var response = new Response
    //     {
    //         DataGroup = result
    //     };
    //     return response;
    // }
    
}