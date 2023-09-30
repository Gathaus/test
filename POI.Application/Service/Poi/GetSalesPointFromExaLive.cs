using POI.Domain.Entities.ExaEntities.Models;
using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Infrastructure.Ef.Context;

namespace POI.Application.Service.Poi;

public class
    GetSalesPointFromExaLive : IBusinessService<GetSalesPointFromExaLive.Request, GetSalesPointFromExaLive.Response>
{

    #region constructor

    public GetSalesPointFromExaLive()
    {
    }

    #endregion

    #region Request & Response

    public class Request
    {
    }

    public class Response
    {
        public SalesPoint SalesPoint { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        using var context = new AgentDbContext();

        var data = await context.SalesPoint.FirstOrDefaultAsync();
        Check.IsNull(data);
        
        var response = ToResponse(data);
        
        return Result<Response>.Success(response);
    }
    
    private  Response ToResponse(SalesPoint result)
    {
        var response = new Response
        {
            SalesPoint = result
        };
        
        return response;
    }
}