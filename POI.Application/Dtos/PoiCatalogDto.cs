using NetTopologySuite.Geometries;
using POI.Domain.Entities;
using POI.Domain.Entities.Enums;

namespace POI.Application.Dto;

public class PoiCatalogDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? NameOnBoard { get; set; }

    //Address
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int CityId { get; set; }
    public string? CityName { get; set; }
    public int CountyId { get; set; }
    public string? CountyName { get; set; }
    public string? Address { get; set; }

    //Geolocation
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public Point? Geography { get; set; }

    public PoiTypeEnum? PoiType { get; set; }
    public IEnumerable<PoiDistanceDto> PoiDistances { get; set; }
}