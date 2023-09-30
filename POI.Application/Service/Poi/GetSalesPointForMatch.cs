using POI.Domain.Entities.ExaEntities.Models;
using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Infrastructure.Ef.Context;
using POI.Application.Dto;
using POI.Application.Responses;
using POI.Domain.Entities;
using POI.Application.Base.Filters;

namespace POI.Application.Service.Poi;

public class
    GetSalesPointForMatch : IDynamicResultService<GetSalesPointForMatch.Request, PagedTableResponse<SalesPointDto>>
{

    #region constructor

    public GetSalesPointForMatch()
    {
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int CompanyId { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
        public PaginationFilter? PaginationFilter { get; set; }
    }


    #endregion

    public async Task<PagedTableResponse<SalesPointDto>> ExecuteAsync(Request request)
    {
        using var context = new AgentDbContext();

        var data = await context.SalesPoint.Where(x => x.CompanyId == request.CompanyId && x.CityId == request.CityId && x.CountyId == request.CountyId).Select(x => x.ToDto()).ToListAsync();
        Check.IsNull(data);


        var totalItemCount = data.Count();
        return new PagedTableResponse<SalesPointDto>
        {
            IsSuccess = true,
            Rows = data,
            PageNumber = request.PaginationFilter.PageNumber,
            PageSize = request.PaginationFilter.PageSize,
            TotalItemCount = totalItemCount,
            Headers = new List<string> { "Name", "CountryName", "CityName", "CountyName" },
            PropertiesWithHeaders = new List<Header>
                {
                    new Header { name = "Name", param = "Name", dataType = "string" },
                    new Header { name = "Country Name", param = "CountryName", dataType = "string" },
                    new Header { name = "City Name", param = "CityName", dataType = "string" },
                    new Header { name = "County Name", param = "CountyName", dataType = "string" }
                }
        };

    }

}