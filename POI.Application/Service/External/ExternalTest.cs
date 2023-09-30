using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Domain.UnitOfWorks;

namespace POI.Application.Service.External;

public class ExternalTest : IBusinessService<ExternalTest.Request, ExternalTest.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _httpClient;


    #region constructor

    public ExternalTest(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
    {
        _unitOfWork = unitOfWork;
        _httpClient = httpClientFactory.CreateClient("externalApi");

    }

    #endregion

    #region Request & Response

    public class Request
    {
        public string endpoint { get; set; }
    }

    public class Response
    {
    }

    #endregion

    public async Task<Result<Response>> ExecuteAsync(Request request)
    {
        var response = await _httpClient.GetAsync(request.endpoint);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API request failed with status {response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        // return JsonSerializer.Deserialize<TResponseType>(responseContent);
        
        return null;
    }
}