using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.Ef.Context;

namespace POI.Application.Service.Poi;

public class
    CreatePoiBySalesPointId : IBusinessService<CreatePoiBySalesPointId.Request, CreatePoiBySalesPointId.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public CreatePoiBySalesPointId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int SalespointId { get; set; }
    }

    public class Response
    {
        public PoiCatalogDto PoiCatalog { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        using var context = new AgentDbContext();
        var salespoint = context.SalesPoint.FirstOrDefault(x => x.Id == request.SalespointId);
        Check.EntityExists(salespoint);
        
        var country = await _unitOfWork.Repository<Country>()
            .GetByIdAsync(salespoint.City.CountryId);
        Check.EntityExists(country);
          
        var city = await _unitOfWork.Repository<City>()
            .GetByIdAsync(salespoint.CityId);
        Check.EntityExists(city);
            
        var county = await _unitOfWork.Repository<County>()
            .GetByIdAsync(salespoint.CountyId);
        Check.EntityExists(county);
            

        var poiCatalog = new PoiCatalog()
        {
            Name = salespoint.Name,
            NameOnBoard = salespoint.NameOnBoard,
            Address = salespoint.Address,
            Latitude = (decimal)salespoint.Geography.Y,
            Longitude = (decimal)salespoint.Geography.X,
            CountryId = salespoint.City.CountryId,
            CountryName = country.Name,
            CityId = salespoint.CityId,
            CityName = city.Name,
            CountyId = salespoint.CountyId,
            CountyName = county.Name,
            PoiType = PoiTypeEnum.Apartment
        };
            
        await _unitOfWork.Repository<PoiCatalog>()
            .InsertAsync(poiCatalog);
        
        var rows = await _unitOfWork.SaveChangesAsync();
            
            
        if (rows == 0)
            return Result<Response>.Fail("Failed to create poi catalog");

        salespoint.POIId = poiCatalog.Id;
        salespoint.LastUpdatedDateTime = DateTime.UtcNow;
        salespoint.UserId_LastUpdatedBy = 55972; //mert
        context.SalesPoint.Update(salespoint);
        await context.SaveChangesAsync();
        
        var response = ToResponse(poiCatalog);

        return Result<Response>.Success(response);
    }
    
    private static Response ToResponse(PoiCatalog poiCatalog)
    {
        return new Response
        {
            PoiCatalog = poiCatalog.ToDto()
        };
    }
}