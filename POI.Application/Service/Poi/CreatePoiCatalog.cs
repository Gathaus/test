using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.MessageBus.Messages;


namespace POI.Application.Service.Poi
{
    public class CreatePoiCatalog : IBusinessService<CreatePoiCatalog.Request>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PoiBufferService _poiBufferService;
        private readonly IBusControl _busControl;

        #region constructor
        public CreatePoiCatalog(IUnitOfWork unitOfWork, PoiBufferService poiBufferService,IBusControl busControl)
        {
            _unitOfWork = unitOfWork;
            _poiBufferService = poiBufferService;
            _busControl = busControl;
        }    
        #endregion

        #region Request

        public class Request
        {
            public string Name { get; set; }
            public string NameOnBoard { get; set; }
            public string Address { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longtitude { get; set; }
            public int CountryId { get; set; }
            public int CityId { get; set; }
            public int CountyId { get; set; }
            public PoiTypeEnum PoiType { get; set; }
        }

        #endregion
    
        public async Task<Result> ExecuteAsync(Request request)
        {
            var country = await _unitOfWork.Repository<Country>()
                .GetByIdAsync(request.CountryId);
            Check.EntityExists(country);
          
            var city = await _unitOfWork.Repository<City>()
                .GetByIdAsync(request.CityId);
            Check.EntityExists(city);
            
            var county = await _unitOfWork.Repository<County>()
                .GetByIdAsync(request.CountyId);
            Check.EntityExists(county);
            

            var poiCatalog = new PoiCatalog()
            {
                Name = request.Name,
                NameOnBoard = request.NameOnBoard,
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longtitude,
                CountryId = request.CountryId,
                CountryName = country.Name,
                CityId = request.CityId,
                CityName = city.Name,
                CountyId = request.CountyId,
                CountyName = county.Name,
                PoiType = request.PoiType
            };
            
            await _unitOfWork.Repository<PoiCatalog>()
                .InsertAsync(poiCatalog);
        
            var rows = await _unitOfWork.SaveChangesAsync();
            
            
            if (rows == 0)
                return Result.Fail("Failed to create poi catalog");

            // await CalculatePoiDistance(poiCatalog);
            
            await SendMessageToCreateCalculatePoiDistances(poiCatalog);

            return Result.Success();
        }
        
         private async Task SendMessageToCreateCalculatePoiDistances(PoiCatalog poiCatalog)
         {
             var poiCreatedMessage = new PoiCreatedMessage()
             {
                 PoiId = poiCatalog.Id,
                 CreatedAt = DateTime.Now
             };
             await _busControl.Publish(poiCreatedMessage);
         }
    }
}