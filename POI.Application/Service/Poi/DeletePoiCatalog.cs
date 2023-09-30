using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Poi;

public class DeletePoiCatalog : IBusinessService<DeletePoiCatalog.Request, DeletePoiCatalog.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public DeletePoiCatalog(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        //TODO write business code here
        return null;
    }
}