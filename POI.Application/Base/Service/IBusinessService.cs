namespace POI.Application.Base.Service;
using POI.Application.Base.Result;

public interface IBusinessService<TRequest, TResponse>
{
    Task<Result<TResponse>> ExecuteAsync(TRequest request);
    
}

public interface IBusinessService<TRequest>
{
    Task<Result> ExecuteAsync(TRequest request);
    
}
