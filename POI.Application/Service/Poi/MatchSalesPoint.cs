using POI.Domain.Entities.ExaEntities.Models;
using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.Ef.Context;

namespace POI.Application.Service.Poi;

public class MatchSalesPoint : IBusinessService<MatchSalesPoint.Request, MatchSalesPoint.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public MatchSalesPoint(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int POIId { get; set; }
        public int SalesPointId { get; set; }
        public string? MatchCase { get; set; }
    }

    public class Response
    {
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        Check.IsNull(request.POIId, "POI Id is required");
        Check.IsNull(request.SalesPointId, "SalesPoint Id is required");

        var poiCatalog = await _unitOfWork.Repository<PoiCatalog>()
            .GetByIdAsync(request.POIId);
        Check.EntityExists(poiCatalog);

        using var context = new AgentDbContext();
        var salesPoint = await context.SalesPoint.FirstOrDefaultAsync(x => x.Id == request.SalesPointId);
        //null kontrolü
        salesPoint = UpdateSalesPoint(request, salesPoint);

        context.SalesPoint.Update(salesPoint);

        var rows = await context.SaveChangesAsync();
        if (rows == 0)
            return Result<Response>.Fail("Update failed");

        return Result<Response>.Success(new());
    }


    
    
    private SalesPoint UpdateSalesPoint( Request request, SalesPoint salesPoint)
    {
        salesPoint.POIId = request.POIId;
        //write match case somewhere

        return salesPoint;
    }
}