using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Poi;

public class GetPoiCatalogById : IBusinessService<GetPoiCatalogById.Request, GetPoiCatalogById.Response>
{
    private readonly IUnitOfWork _unitOfWork;
private readonly ILogger<GetPoiCatalogById> _logger;
    #region constructor

    public GetPoiCatalogById(IUnitOfWork unitOfWork, ILogger<GetPoiCatalogById> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int Id { get; set; }
    }

    public class Response
    {
        public PoiCatalogDto PoiCatalog { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        
        _logger.LogInformation("GetPoiCatalogById.ExecuteAsync");
        _logger.LogInformation("GetPoiCatalogById.ExecuteAsync");

        var result = await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x=>x.Id == request.Id)
            .Select(x=>x.ToDto())
            .FirstOrDefaultAsync();
        
        if (result == null)
            throw new Exception("Poi not found");

        var response = ToResponse(result);
        
        return Result<Response>.Success(response);
    }

    private  Response ToResponse(PoiCatalogDto result)
    {
        var response = new Response
        {
            PoiCatalog = result
        };
        return response;
    }
}