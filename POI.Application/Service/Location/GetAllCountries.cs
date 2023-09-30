using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Location;

public class GetAllCountries : IFilterService<GetAllCountries.Request, List<GetCountryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetAllCountries(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
    }

    public class Response
    {
        public List<GetCountryDto> DataGroup { get; set; }
    }

    #endregion

 
    public async Task<FilterResult<List<GetCountryDto>>> ExecuteAsync(Request request)
    {
        var data = await _unitOfWork.Repository<Country>()
            .FindBy(x => !x.IsPassive)
            .Select(x => x.ToDto())
            .ToListAsync();

        return FilterResult<List<GetCountryDto>>.Success(data);
    }

    private  Response ToResponse(List<GetCountryDto> data)
    {
        var response = new Response
        {
            DataGroup = data
        };
        return response;
    }

}
