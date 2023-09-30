using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.PoiDistanceService;

public class GetPoiDistancesByPoiId : IBusinessService<GetPoiDistancesByPoiId.Request, GetPoiDistancesByPoiId.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetPoiDistancesByPoiId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int PoiId { get; set; } 
    }

    public class Response
    {
        public PoiCatalogDto PoiCatalog { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var poiCatalog =await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x => x.Id == request.PoiId)
            .Include(x=>x.FirstPoiDistances)
            .ThenInclude(x=>x.SecondPoiCatalog)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Check.EntityExists(poiCatalog);

        var response = ToResponse(poiCatalog.ToDtoWithDistances());

        return Result<Response>.Success(response);
    }

    private Response ToResponse(PoiCatalogDto poiCatalog)
    {
        return new Response()
        {
            PoiCatalog = poiCatalog
        };
    }
}