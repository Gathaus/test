using POI.Application.Base.Exceptions;
using POI.Application.Constants;

namespace POI.Application.Exceptions;

public class CountyNotFoundException:BaseException
{
    public CountyNotFoundException(): base(ExceptionMessages.CountyNotFoundTitle,
        ExceptionMessages.CountyNotFoundDetail)
    {
    }
}