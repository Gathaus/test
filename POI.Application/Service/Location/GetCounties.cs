using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Location;

public class GetCounties : IFilterService<GetCounties.Request,List<GetPoiCountyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetCounties(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int CityId { get; set; }
    }


    #endregion

    public async Task<FilterResult<List<GetPoiCountyDto>>> ExecuteAsync(Request request)
    {
        var data = await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x=>x.CountyId == request.CityId && !x.IsPassive)
            .Select(x=> new GetPoiCountyDto()
            {
                Id = x.CountyId,
                Name = x.CountyName
            })
            .Distinct()
            .OrderBy(x=>x.Name)
            .ToListAsync();
        
        // var response = ToResponse(data);
        
        return FilterResult<List<GetPoiCountyDto>>.Success(data);
    }
    
    // private  Response ToResponse(List<GetPoiCountyDto> result)
    // {
    //     var response = new Response
    //     {
    //         DataGroup = result
    //     };
    //     return response;
    // }
}