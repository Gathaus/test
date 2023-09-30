namespace POI.Domain.Entities
{
    public class PoiDistance : BaseEntity<int>
    {
        public int FirstPoiCatalogId { get;  set; }
        public virtual PoiCatalog FirstPoiCatalog { get;  set; }

        public int SecondPoiCatalogId { get;  set; }
        public virtual PoiCatalog SecondPoiCatalog { get;  set; }

        public short Distance { get;  set; }
        
    }
}