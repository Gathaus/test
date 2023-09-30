namespace POI.Domain.Entities
{
    public class PoiSalesPointMatchHistory : BaseEntity<int>
    {
        public int PoiId { get; set; }
        public int SalesPointId { get; set; }
        public DateTime MatchDate { get; set; }
        public string? MatchSource { get; set; }
        public virtual PoiCatalog PoiCatalog { get; set; }

    }
}