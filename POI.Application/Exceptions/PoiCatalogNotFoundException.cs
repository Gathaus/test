using POI.Application.Base.Exceptions;
using POI.Application.Constants;

namespace POI.Application.Exceptions;

public class PoiCatalogNotFoundException : BaseException
{
    public PoiCatalogNotFoundException() : base(ExceptionMessages.PoiCatalogNotFoundTitle
        ,ExceptionMessages.PoiCatalogNotFoundDetail)
    {
    }
}