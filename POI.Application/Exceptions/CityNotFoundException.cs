using POI.Application.Base.Exceptions;
using POI.Application.Constants;

namespace POI.Application.Exceptions;

public class CityNotFoundException : BaseException
{
    public CityNotFoundException() : base(ExceptionMessages.CityNotFoundTitle, ExceptionMessages.CityNotFoundDetail)
    {
    }
}