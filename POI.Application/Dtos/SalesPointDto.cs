using NetTopologySuite.Geometries;
using POI.Domain.Entities.Enums;

namespace POI.Application.Dto;

public class SalesPointDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? NameOnBoard { get; set; }

    //Address
    public string? CountryName { get; set; }
    public string? CityName { get; set; }
    public string? CountyName { get; set; }
    public string? Address { get; set; }

    //Geolocation
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

}