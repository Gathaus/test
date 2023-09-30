using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.PoiDistanceService;

public class GetPoiDistancesByPoiIds : IBusinessService<GetPoiDistancesByPoiIds.Request, GetPoiDistancesByPoiIds.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetPoiDistancesByPoiIds(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public List<int> PoiIds { get; set; }
    }

    public class Response
    {
        public List<PoiCatalogDto> PoiDistances { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var poiCatalogs =await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x => request.PoiIds.Contains(x.Id))
            .Include(x=>x.FirstPoiDistances)
            .ThenInclude(x=>x.SecondPoiCatalog)
            .AsNoTracking()
            .ToListAsync();
        Check.EntityExists(poiCatalogs);

        var response = ToResponse(poiCatalogs.ToDtoWithDistances());

        return Result<Response>.Success(response);
    }
    
    
    private Response ToResponse(List<PoiCatalogDto> poiCatalogs)
    {
        return new Response
        {
            PoiDistances = poiCatalogs
        };
    }
}