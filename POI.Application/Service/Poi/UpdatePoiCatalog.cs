using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;
using POI.Domain.Extensions;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.MessageBus.Messages;

namespace POI.Application.Service.Poi;

public class UpdatePoiCatalog : IBusinessService<UpdatePoiCatalog.Request, UpdatePoiCatalog.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBusControl _busControl;

    #region constructor

    public UpdatePoiCatalog(IUnitOfWork unitOfWork, IBusControl busControl)
    {
        _unitOfWork = unitOfWork;
        _busControl = busControl;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameOnBoard { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtitude { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? CountyId { get; set; }
        public bool? IsPassive { get; set; }
        public PoiTypeEnum? PoiType { get; set; }
    }

    public class Response
    {
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        Check.IsNull(request.Id, "Id is required");
        
        var poiCatalog = await _unitOfWork.Repository<PoiCatalog>()
            .GetByIdAsync(request.Id);
        Check.EntityExists(poiCatalog);
        
        var country = await GetEntityIfIdExistsAsync<Country>(request.CountryId);
        var city = await GetEntityIfIdExistsAsync<City>(request.CityId);
        var county = await GetEntityIfIdExistsAsync<County>(request.CountyId);
        bool calculatePoiDistance = (request.Latitude.HasValue && poiCatalog.Latitude != request.Latitude.Value) ||
                                    (request.Longtitude.HasValue && poiCatalog.Longitude != request.Longtitude.Value);

        poiCatalog = UpdateFromRequest(request,poiCatalog,country,city,county);

        _unitOfWork.Repository<PoiCatalog>().Update(poiCatalog);
        
        var rows = await _unitOfWork.SaveChangesAsync();
        if(rows == 0)
            return Result<Response>.Fail("Update failed");

        
        
        if(poiCatalog.IsPassive){}
        // await SendMessageToCreateCalculatePoiDistances(poiCatalog);
        else if(calculatePoiDistance)
            await SendMessageToUpdateCalculatePoiDistances(poiCatalog);

        
        return Result<Response>.Success(new());
    }


    public async Task<TEntity> GetEntityIfIdExistsAsync<TEntity>(int? Id) where TEntity : class
    {
        if (Id.IsNullOrBelowZero()) 
                return null;
        
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(Id.Value);
        Check.EntityExists(entity,$"{typeof(TEntity).Name} not found");
        
        return entity;
    }
    
    
    private PoiCatalog UpdateFromRequest( Request request,PoiCatalog poiCatalog,Country? country,City? city,County? county)
    {
        poiCatalog.Name = request.Name ?? poiCatalog.Name;
        poiCatalog.NameOnBoard = request.NameOnBoard ?? poiCatalog.NameOnBoard;
        poiCatalog.Address = request.Address ?? poiCatalog.Address;
        poiCatalog.Latitude = request.Latitude ?? poiCatalog.Latitude;
        poiCatalog.Longitude = request.Longtitude ?? poiCatalog.Longitude;
        poiCatalog.CountryId = country?.Id ?? poiCatalog.CountryId;
        poiCatalog.CountryName = country?.Name ?? poiCatalog.CountryName;
        poiCatalog.CityId = city?.Id ?? poiCatalog.CityId;
        poiCatalog.CityName = city?.Name ?? poiCatalog.CityName;
        poiCatalog.CountyId = county?.Id ?? poiCatalog.CountyId;
        poiCatalog.CountyName = county?.Name ?? poiCatalog.CountyName;
        poiCatalog.PoiType = request.PoiType ?? poiCatalog.PoiType;
        poiCatalog.IsPassive = request.IsPassive ?? poiCatalog.IsPassive;

        return poiCatalog;
    }
    
    private async Task SendMessageToUpdateCalculatePoiDistances(PoiCatalog poiCatalog)
    {
        var poiCreatedMessage = new PoiUpdatedMessage()
        {
            PoiId = poiCatalog.Id,
            CreatedAt = DateTime.Now
        };
        await _busControl.Publish(poiCreatedMessage);
    }
    


}