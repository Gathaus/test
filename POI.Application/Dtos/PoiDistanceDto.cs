using POI.Domain.Entities;

namespace POI.Application.Dto;

public class PoiDistanceDto
{
    public int FirstPoiCatalogId { get;  set; }
    public virtual PoiCatalogDto FirstPoiCatalog { get;  set; }

    public int SecondPoiCatalogId { get;  set; }
    public virtual PoiCatalogDto SecondPoiCatalog { get;  set; }

    public short Distance { get;  set; }
}