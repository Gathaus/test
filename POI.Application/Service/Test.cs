using MassTransit;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Exceptions;
using POI.Domain.UnitOfWorks;
using POI.Infrastructure.Ef.Context;
using POI.Infrastructure.MessageBus.Messages;

namespace POI.Application.Service;

public class Test : IBusinessService<Test.Request, Test.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBusControl _busControl;
    #region constructor

    public Test(IUnitOfWork unitOfWork,IBusControl busControl)
    {
        _unitOfWork = unitOfWork;
        _busControl = busControl;
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
        var message = new SampleMessage
        {
            Content = "Test"
        };

        // await _busControl.Publish(message);
        throw new PoiCatalogNotFoundException();
        
        throw new DirectoryNotFoundException("test");

        using var context = new AgentDbContext();

        var poicatalog = context.SalesPoint.FirstOrDefault(x => x.Id == 1491548);
        poicatalog.POIId = 15;
        poicatalog.LastUpdatedDateTime = DateTime.UtcNow;
        context.SalesPoint.Update(poicatalog);
        await context.SaveChangesAsync(false);
        
        
        
        return Result<Response>.Success(new());
    }
}