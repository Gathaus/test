using NetTopologySuite.Geometries;
using POI.Domain.Entities.Enums;
using POI.Domain.Utils;

namespace POI.Domain.Entities
{
    public class PoiCatalog : BaseEntity<int>
    {   
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
        
        public County County { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public ICollection<PoiDistance> FirstPoiDistances { get; set; }


        public bool IsPassive { get; set; }
        public Point? Geography { get; set; }

        public PoiTypeEnum? PoiType { get; set; }
        
        private decimal? _longitude;
        private decimal? _latitude;


        public decimal? Longitude
        {
            get
            {
                _longitude = (Geography != null ? (decimal)Geography.X : null);
                return _longitude;
            }
            set
            {
                _longitude = value;
                SetGeography();
            }
        }


        public decimal? Latitude
        {
            get
            {
                _latitude = (Geography != null ? (decimal)Geography.Y : null);
                return _latitude;
            }
            set
            {
                _latitude = value;
                SetGeography();
            }
        }
        
        
        private void SetGeography()
        {
            Geography = SpatialUtil.CreatePoint(_latitude, _longitude);
        }
      
    }
}