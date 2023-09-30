using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using POI.Domain.Utils;

namespace POI.Domain.Entities;

public class Country : BaseEntity<int>
{
    public string Name { get; set; }
    public bool IsPassive { get; set; }



    public virtual IList<City> Cities { get; set; }

    public Point? Geography { get; set; }

    private decimal? _longitude;
    private decimal? _latitude;


    [NotMapped]
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


    [NotMapped]
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