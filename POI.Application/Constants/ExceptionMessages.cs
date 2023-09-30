namespace POI.Application.Constants;

public class ExceptionMessages
{
    public const string GenericTitle = "Application Error";
    public const string GenericDetail = "An unexpected error occurred.";

    public const string CityNotFoundTitle = "City Not Found";
    public const string CityNotFoundDetail = "The requested city could not be located.";

    public const string CountyNotFoundTitle = "County Not Found";
    public const string CountyNotFoundDetail = "The requested county could not be found.";

    public const string PoiCatalogNotFoundTitle = "POI Catalog Not Found";
    public const string PoiCatalogNotFoundDetail = "The requested POI catalog could not be identified.";

    public const string ValidationTitle = "Validation Error";
    public const string ValidationDetail = "The provided data is invalid.";

    public const string UnauthorizedAccessTitle = "Unauthorized Access";
    public const string UnauthorizedAccessDetail = "You do not have the necessary permissions to perform this operation.";
}