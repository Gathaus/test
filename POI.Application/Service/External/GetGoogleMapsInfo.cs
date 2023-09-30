using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using POI.Application.Base.Result;
using POI.Application.Base.Service;
using POI.Domain.UnitOfWorks;
using Serilog;

public class GetGoogleMapsInfo : IDynamicResultService<GetGoogleMapsInfo.Request, GetGoogleMapsInfo.Response>
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _httpClient;

    public GetGoogleMapsInfo(IUnitOfWork unitOfWork, IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
    }
    
    public class Request
    {
        public string Address { get; set; }
    }
    public class Response
    {
        public JObject Data { get; set; }
    }

    public async Task<Response> ExecuteAsync(Request request)
    {
        try
        {
            var apiKey = _configuration["GoogleMaps:ApiKey"];
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(request.Address)}&key={apiKey}";

            var apiResponse = await _httpClient.GetStringAsync(url);
            var jsonResponse = JObject.Parse(apiResponse);

            var response = new Response
            {
                Data = jsonResponse
            };
            
            // if (jsonResponse["status"].ToString() == "REQUEST_DENIED")
            //     throw new Exception(jsonResponse["error_message"].ToString());
            //     

            Log.Warning($"GetGoogleMapsInfo.ExecuteAsync Response with api key: {_configuration["GoogleMaps:ApiKey"]}");

            return response;
        }
        catch(Exception ex)
        {
            Log.Error(ex, $"GetGoogleMapsInfo.ExecuteAsync ERROR with api key: {_configuration["GoogleMaps:ApiKey"]}");
            throw;
        }
    }

}