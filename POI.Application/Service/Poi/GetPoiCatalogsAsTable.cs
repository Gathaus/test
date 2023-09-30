using Microsoft.EntityFrameworkCore;
using POI.Application.Base.Filters;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Application.Dto;
using POI.Application.Extensions;
using POI.Application.Responses;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.Poi;

public class GetPoiCatalogsAsTable : IDynamicResultService<GetPoiCatalogsAsTable.Request, PagedTableResponse<PoiCatalogDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    #region constructor

    public GetPoiCatalogsAsTable(IUnitOfWork unitOfWork)
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
    #endregion

    public async Task<PagedTableResponse<PoiCatalogDto>> ExecuteAsync(Request request)
        {
            var country = await GetEntityIfIdExistsAsync<Country>(request.CountryId);
            var city = await GetEntityIfIdExistsAsync<City>(request.CityId);
            var county = await GetEntityIfIdExistsAsync<County>(request.CountyId);

            var poiCatalogQuery = _unitOfWork.Repository<PoiCatalog>().FindBy();

            // Filtering logic
            if (!string.IsNullOrWhiteSpace(request.Name))
                poiCatalogQuery = poiCatalogQuery.Where(x => x.Name.Contains(request.Name) || x.NameOnBoard.Contains(request.Name));

            if (country != null)
                poiCatalogQuery = poiCatalogQuery.Where(x => x.CountryId == country.Id);

            if (city != null)
                poiCatalogQuery = poiCatalogQuery.Where(x => x.CityId == city.Id);

            if (county != null)
                poiCatalogQuery = poiCatalogQuery.Where(x => x.CountyId == county.Id);

            if (request.PoiType.HasValue)
                poiCatalogQuery = poiCatalogQuery.Where(x => x.PoiType == request.PoiType.Value);

            var totalItemCount = await poiCatalogQuery.CountAsync();

            var poiCatalogs = await poiCatalogQuery
                .Skip((request.PaginationFilter.PageNumber - 1) * request.PaginationFilter.PageSize)
                .Take(request.PaginationFilter.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            if (!poiCatalogs.Any())
            {
                return new PagedTableResponse<PoiCatalogDto>
                {
                    IsSuccess = false,
                    ErrorMessage = "PoiCatalogs not found"
                };
            }

            return new PagedTableResponse<PoiCatalogDto>
            {
                IsSuccess = true,
                Rows = poiCatalogs,
                PageNumber = request.PaginationFilter.PageNumber,
                PageSize = request.PaginationFilter.PageSize,
                TotalItemCount = totalItemCount,
                Headers = new List<string> { "Name", "CountryName", "CityName", "CountyName", "PoiType" },
                PropertiesWithHeaders = new List<Header>
                {
                    new Header { name = "Name", param = "Name", dataType = "string" },
                    new Header { name = "Country Name", param = "CountryName", dataType = "string" },
                    new Header { name = "City Name", param = "CityName", dataType = "string" },
                    new Header { name = "County Name", param = "CountyName", dataType = "string" },
                    new Header { name = "Poi Type", param = "PoiType", dataType = "int" }
                }
            };
        }

        private async Task<TEntity> GetEntityIfIdExistsAsync<TEntity>(int? Id) where TEntity : class
        {
            if (!Id.HasValue || Id <= 0) return null;

            var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(Id.Value);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(TEntity).Name} not found");
            }

            return entity;
        }
}