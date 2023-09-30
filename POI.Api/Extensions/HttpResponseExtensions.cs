using Microsoft.AspNetCore.Mvc;
using POI.Application.Base.Result;

namespace POI.Api.Extensions;

public static class HttpResponseExtensions
{
    public static IActionResult ToActionResult(this Result response)
    {
        return response.IsSuccess ? new OkObjectResult(response) : new BadRequestObjectResult(response);
    }

}