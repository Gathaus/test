using POI.Domain.Entities.ExaEntities.Models;
using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.Ef.Context;

namespace POI.Application.Service.Poi;

public class
    GetCompanies : IFilterService<GetCompanies.Request, List<Company>>
{

    #region constructor

    public GetCompanies()
    {
    }

    #endregion

    #region Request & Response

    public class Request
    {
    }


    #endregion

    public async Task<FilterResult<List<Company>>> ExecuteAsync(Request request)
    {
        using var context = new AgentDbContext();

        var data = await context.Company.Where(x => !x.IsPassive).ToListAsync();
        Check.IsNull(data);

        // var response = ToResponse(data);

        return FilterResult<List<Company>>.Success(data);
    }

}