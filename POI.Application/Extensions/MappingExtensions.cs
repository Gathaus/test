using POI.Application.Dto;
using POI.Domain.Entities;
using POI.Domain.Entities.ExaEntities.Models;

namespace POI.Application.Extensions;

public static class MappingExtensions
{
    public static PoiCatalogDto ToDto(this PoiCatalog entity)
    {
        return new PoiCatalogDto
        {
            Id = entity.Id,
            Name = entity.Name,
            NameOnBoard = entity.NameOnBoard,
            CountryId = entity.CountryId,
            CountryName = entity.CountryName,
            CityId = entity.CityId,
            CityName = entity.CityName,
            CountyId = entity.CountyId,
            CountyName = entity.CountyName,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            PoiType = entity.PoiType
        };
    }
    
    public static PoiCatalogDto ToDtoWithDistances(this PoiCatalog entity)
    {
        return new PoiCatalogDto
        {
            Id = entity.Id,
            Name = entity.Name,
            NameOnBoard = entity.NameOnBoard,
            CountryId = entity.CountryId,
            CountryName = entity.CountryName,
            CityId = entity.CityId,
            CityName = entity.CityName,
            CountyId = entity.CountyId,
            CountyName = entity.CountyName,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            PoiType = entity.PoiType,
            PoiDistances = entity.FirstPoiDistances.Select(poiDistance => new PoiDistanceDto()
            {
                SecondPoiCatalogId = poiDistance.SecondPoiCatalogId,
                SecondPoiCatalog = new PoiCatalogDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    NameOnBoard = entity.NameOnBoard,
                    CountryId = entity.CountryId,
                    CountryName = entity.CountryName,
                    CityId = entity.CityId,
                    CityName = entity.CityName,
                    CountyId = entity.CountyId,
                    CountyName = entity.CountyName,
                    Address = entity.Address,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    PoiType = entity.PoiType,
                },
                Distance = poiDistance.Distance
                
            }).ToList()
        };
    }
    
    public static List<PoiCatalogDto> ToDtoWithDistances(this List<PoiCatalog> entities)
    {
        return entities.Select(entity => new PoiCatalogDto
        {
            Id = entity.Id,
            Name = entity.Name,
            NameOnBoard = entity.NameOnBoard,
            CountryId = entity.CountryId,
            CountryName = entity.CountryName,
            CityId = entity.CityId,
            CityName = entity.CityName,
            CountyId = entity.CountyId,
            CountyName = entity.CountyName,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            PoiType = entity.PoiType,
            PoiDistances = entity.FirstPoiDistances.Select(poiDistance => new PoiDistanceDto()
            {
                SecondPoiCatalogId = poiDistance.SecondPoiCatalogId,
                SecondPoiCatalog = new PoiCatalogDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    NameOnBoard = entity.NameOnBoard,
                    CountryId = entity.CountryId,
                    CountryName = entity.CountryName,
                    CityId = entity.CityId,
                    CityName = entity.CityName,
                    CountyId = entity.CountyId,
                    CountyName = entity.CountyName,
                    Address = entity.Address,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    PoiType = entity.PoiType,
                },
                Distance = poiDistance.Distance

            }).ToList()
        }).ToList();
    }

    public static PoiCatalog ToEntity(this PoiCatalogDto dto)
    {
        return new PoiCatalog
        {
            Name = dto.Name,
            NameOnBoard = dto.NameOnBoard,
            CountryId = dto.CountryId,
            CountryName = dto.CountryName,
            CityId = dto.CityId,
            CityName = dto.CityName,
            CountyId = dto.CountyId,
            CountyName = dto.CountyName,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Geography = dto.Geography,
            PoiType = dto.PoiType
        };
    }

    public static GetCountryDto ToDto(this Country entity)
    {
        return new GetCountryDto
        {
            Id = entity.Id,
            Name = entity?.Name ?? "-"
        };
    }

    public static GetPoiCityDto ToCityDto(this PoiCatalog entity)
    {
        return new GetPoiCityDto
        {
            Id = entity.City?.Id ?? 0,
            Name = entity.City?.Name ?? "-"
        };
    }


    public static GetPoiCountyDto ToDto(this County entity)
    {
        return new GetPoiCountyDto
        {
            Id = entity.Id,
            Name = entity?.Name ?? "-"
        };
    }

    public static PoiDistanceDto ToDistancesDto(this PoiDistance entity)
    {
        return new PoiDistanceDto()
        {
            FirstPoiCatalog = new PoiCatalogDto()
            {
                Id = entity.FirstPoiCatalogId,
                Name = entity.FirstPoiCatalog.Name,
                NameOnBoard = entity.FirstPoiCatalog.NameOnBoard,
                CountryId = entity.FirstPoiCatalog.CountryId,
                CountryName = entity.FirstPoiCatalog.CountryName,
                CityId = entity.FirstPoiCatalog.CityId,
                CityName = entity.FirstPoiCatalog.CityName,
                CountyId = entity.FirstPoiCatalog.CountyId,
                CountyName = entity.FirstPoiCatalog.CountyName,
                Address = entity.FirstPoiCatalog.Address,
                Latitude = entity.FirstPoiCatalog.Latitude,
                Longitude = entity.FirstPoiCatalog.Longitude,
                PoiType = entity.FirstPoiCatalog.PoiType
            },
            SecondPoiCatalog = new PoiCatalogDto()
            {
                Id = entity.SecondPoiCatalogId,
                Name = entity.SecondPoiCatalog.Name,
                NameOnBoard = entity.SecondPoiCatalog.NameOnBoard,
                CountryId = entity.SecondPoiCatalog.CountryId,
                CountryName = entity.SecondPoiCatalog.CountryName,
                CityId = entity.SecondPoiCatalog.CityId,
                CityName = entity.SecondPoiCatalog.CityName,
                CountyId = entity.SecondPoiCatalog.CountyId,
                CountyName = entity.SecondPoiCatalog.CountyName,
                Address = entity.SecondPoiCatalog.Address,
                Latitude = entity.SecondPoiCatalog.Latitude,
                Longitude = entity.SecondPoiCatalog.Longitude,
                PoiType = entity.SecondPoiCatalog.PoiType
            },
            Distance = entity.Distance
        };
    }
    public static SalesPointDto ToDto(this SalesPoint entity)
    {
        return new SalesPointDto
        {
            Id = entity.Id,
            Name = entity.Name,
            NameOnBoard = entity.NameOnBoard,
            //CountryId = entity.CountryId,
            CountryName = entity.City.Country.Name,
            //CityId = entity.CityId,
            CityName = entity.City.Name,
            //CountyId = entity.CountyId,
            CountyName = entity.County.Name,
            Address = entity.Address,
            Latitude = (decimal?)entity.Geography?.Coordinate.Y,
            Longitude = (decimal?)entity.Geography?.Coordinate.X
        };
    }

}