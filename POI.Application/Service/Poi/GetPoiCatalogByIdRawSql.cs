using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Domain.Entities;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Poi;

public class
    GetPoiCatalogByIdRawSql : IBusinessService<GetPoiCatalogByIdRawSql.Request, GetPoiCatalogByIdRawSql.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetPoiCatalogByIdRawSql(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public int Id { get; set; }
    }

    public class Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    #endregion



    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var sql = "SELECT Id, Name FROM PoiCatalogs WHERE Id = {0}";
        var result = await _unitOfWork.Repository<PoiCatalog>()
            .QueryFromSql(sql,request.Id)
            .Select(x => new Response
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();
        
        
        if (result == null)
            throw new Exception("Poi not found");

        
        return Result<Response>.Success(result);
    }

    private Response ToResponse(PoiCatalogDto result)
    {
        // var response = new Response
        // {
        //     PoiCatalog = result
        // };
        // return response;
        return null;
    }
}