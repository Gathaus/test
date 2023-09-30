using NetTopologySuite.Geometries;

namespace POI.Domain.Extensions;

public static class GeographyExtensions
{
    // There's a 1% deviation
    public static short DistanceInMeters(Point point1, Point point2)
    {
        const double EarthRadiusInMeters = 6371000.0; // Earth's mean radius in meters

        double lat1Radians = DegreeToRadian(point1.Y);
        double lon1Radians = DegreeToRadian(point1.X);
        double lat2Radians = DegreeToRadian(point2.Y);
        double lon2Radians = DegreeToRadian(point2.X);

        double deltaLat = lat2Radians - lat1Radians;
        double deltaLon = lon2Radians - lon1Radians;

        double a = Math.Sin(deltaLat / 2.0) * Math.Sin(deltaLat / 2.0) +
                   Math.Cos(lat1Radians) * Math.Cos(lat2Radians) *
                   Math.Sin(deltaLon / 2.0) * Math.Sin(deltaLon / 2.0);
        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

        return (short) (EarthRadiusInMeters * c);
    }


    public static double DegreeToRadian(double degree)
    {
        return (Math.PI * degree) / 180.0;
    }
}