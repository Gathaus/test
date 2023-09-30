using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Poi;

public class CalculatePoiDistances : IBusinessService<CalculatePoiDistances.Request, CalculatePoiDistances.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PoiBufferService _poiBufferService;

    private const double
        DistanceConversionFactor = 111320.0; // 1 degree is approximately 111320 meters on Earth's surface


    #region constructor

    public CalculatePoiDistances(IUnitOfWork unitOfWork, PoiBufferService poiBufferService)
    {
        _unitOfWork = unitOfWork;
        _poiBufferService = poiBufferService;
    }

    #endregion

    #region Request & Response

    public class Request
    {
    }

    public class Response
    {
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var poiIds = _poiBufferService.GetAndClear();


        var allPoiCatalogs = await _unitOfWork.Repository<PoiCatalog>()
            .FindBy(x => poiIds.Contains(x.Id)).ToListAsync();

        foreach (var poiCatalog in allPoiCatalogs)
        {
            var nearbyPoiCatalogs = await _unitOfWork.Repository<PoiCatalog>()
                .FindBy(p => p.Geography.Distance(poiCatalog.Geography) <= 1000 && p.Id != poiCatalog.Id)
                .ToListAsync();

            foreach (var nearbyPoi in nearbyPoiCatalogs)
            {
                var distanceInMeters = DistanceInMeters(poiCatalog.Geography, nearbyPoi.Geography);
                var poiDistance = new PoiDistance
                {
                    FirstPoiCatalogId = poiCatalog.Id,
                    SecondPoiCatalogId = nearbyPoi.Id,
                    Distance = distanceInMeters
                };
                await _unitOfWork.Repository<PoiDistance>().InsertAsync(poiDistance);
            }
        }


        await _unitOfWork.SaveChangesAsync();

        return Result<Response>.Success(new());
    }


    public short DistanceInMeters(Point point1, Point point2)
    {
        const double EarthRadiusInMeters = 6371000.0; // Earth's mean radius in meters

        double lat1Radians = DegreeToRadian(point1.Y);
        double lon1Radians = DegreeToRadian(point1.X);
        double lat2Radians = DegreeToRadian(point2.Y);
        double lon2Radians = DegreeToRadian(point2.X);

        double deltaLat = lat2Radians - lat1Radians;
        double deltaLon = lon2Radians - lon1Radians;

        double a = Math.Sin(deltaLat / 2.0) * Math.Sin(deltaLat / 2.0) +
                   Math.Cos(lat1Radians) * Math.Cos(lat2Radians) *
                   Math.Sin(deltaLon / 2.0) * Math.Sin(deltaLon / 2.0);
        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

        return (short) (EarthRadiusInMeters * c);
    }


    public double DegreeToRadian(double degree)
    {
        return (Math.PI * degree) / 180.0;
    }
}