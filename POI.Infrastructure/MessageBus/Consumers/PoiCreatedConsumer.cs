using System.Diagnostics;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.Extensions;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.MessageBus.Messages;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace POI.Infrastructure.MessageBus.Consumers;


public class PoiCreatedConsumer : IConsumer<PoiCreatedMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PoiCreatedConsumer> _logger;
    
    public PoiCreatedConsumer(IUnitOfWork unitOfWork, ILogger<PoiCreatedConsumer> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task Consume(ConsumeContext<PoiCreatedMessage> context)
    {
        try
        {
            _logger.LogInformation("PoiCreatedConsumer Started With PoiId: {PoiId}", context.Message.PoiId);
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var poiCatalog = await _unitOfWork.Repository<PoiCatalog>()
                .FindBy(x => x.Id == context.Message.PoiId)
                .FirstOrDefaultAsync();
            Check.EntityExists(poiCatalog);

            var nearbyPoiCatalogs = await _unitOfWork.Repository<PoiCatalog>()
                .FindBy(p => p.Geography.Distance(poiCatalog.Geography) <= 1000 && p.Id != poiCatalog.Id)
                .ToListAsync();

            foreach (var nearbyPoi in nearbyPoiCatalogs)
            {
                var distanceInMeters = GeographyExtensions.DistanceInMeters(poiCatalog.Geography, nearbyPoi.Geography);
                var poiDistance = new PoiDistance
                {
                    FirstPoiCatalogId = poiCatalog.Id,
                    SecondPoiCatalogId = nearbyPoi.Id,
                    Distance = distanceInMeters
                };
                await _unitOfWork.Repository<PoiDistance>().InsertAsync(poiDistance);
            }


            await _unitOfWork.SaveChangesAsync();
            stopWatch.Stop();
            _logger.LogInformation("PoiCreatedConsumer End With PoiId: {PoiId} in {ElapsedMilliseconds} ms", context.Message.PoiId, stopWatch.ElapsedMilliseconds);

        }
        catch (Exception e)
        {
            
            _logger.LogError("PoiCreatedConsumer Error With PoiId: {PoiId}", context.Message.PoiId);
            throw;
            //message bus exception will be created
        }
    }
}