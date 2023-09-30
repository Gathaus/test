using POI.Application.Base.Result;

namespace POI.Application.Base.Service;


public interface IFilterService<TRequest, TResponse>
{
    Task<FilterResult<TResponse>> ExecuteAsync(TRequest request);
}