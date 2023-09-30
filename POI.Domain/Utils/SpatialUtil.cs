using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace POI.Domain.Utils;

    public static class SpatialUtil
    {
        public static Point? CreatePoint(decimal? latitude, decimal? longitude)
        {
            if (!latitude.HasValue || !longitude.HasValue)
                return null;

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            return geometryFactory.CreatePoint(new Coordinate((double)longitude.Value, (double)latitude.Value));
        }
}