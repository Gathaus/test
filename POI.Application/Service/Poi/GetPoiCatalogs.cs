using Microsoft.IdentityModel.Tokens;
using POI.Application.Base.Filters;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;
using POI.Domain.Extensions;
using POI.Domain.UnitOfWorks;
using X.PagedList;

namespace POI.Application.Service.Poi;

public class GetPoiCatalogs : IBusinessService<GetPoiCatalogs.Request, GetPoiCatalogs.Response>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetPoiCatalogs(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Request & Response

    public class Request
    {
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? CountyId { get; set; }
        public PoiTypeEnum? PoiType { get; set; } = PoiTypeEnum.Apartment;
        public PaginationFilter? PaginationFilter { get; set; }


    }

    public class Response
    {
        public IPagedList<PoiCatalogDto> PoiCatalogs { get; set; }
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var country = await GetEntityIfIdExistsAsync<Country>(request.CountryId);
        var city = await GetEntityIfIdExistsAsync<City>(request.CityId);
        var county = await GetEntityIfIdExistsAsync<County>(request.CountyId);

        var poiCatalogQuery = _unitOfWork.Repository<PoiCatalog>()
            .FindBy();
        
        if (!request.Name.IsNullOrEmpty())
            poiCatalogQuery = poiCatalogQuery.Where(x => x.Name.Contains(request.Name) || x.NameOnBoard.Contains(request.Name));
        
        if (country != null)
            poiCatalogQuery = poiCatalogQuery.Where(x => x.CountryId == country.Id);
        
        if (city != null)
            poiCatalogQuery = poiCatalogQuery.Where(x => x.CityId == city.Id);
        
        if (county != null)
            poiCatalogQuery = poiCatalogQuery.Where(x => x.CountyId == county.Id);
        
        if (request.PoiType.HasValue)
            poiCatalogQuery = poiCatalogQuery.Where(x => x.PoiType == request.PoiType.Value);

        var poiCatalogs =
            await poiCatalogQuery.Select(x=>x.ToDto()).ToPagedListAsync(request.PaginationFilter.PageNumber,
                request.PaginationFilter.PageSize);

        if (poiCatalogs.IsNullOrEmpty())
            return Result<Response>.Fail("PoiCatalogs not found");

        var response = ToResponse(poiCatalogs);
        
        return Result<Response>.Success(response);
    }

    private Response ToResponse(IPagedList<PoiCatalogDto> poiCatalogs)
    {
        var response = new Response
        {
            PoiCatalogs = poiCatalogs
        };
        return response;
    }

    public async Task<TEntity> GetEntityIfIdExistsAsync<TEntity>(int? Id) where TEntity : class
    {
        if (Id.IsNullOrBelowZero()) 
            return null;
        
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(Id.Value);
        Check.EntityExists(entity,$"{typeof(TEntity).Name} not found");
        
        return entity;
    }
}