using Microsoft.AspNetCore.Mvc;
using WebApi.Services.ExternalApiService;

namespace WebApi.Controllers.DataController {
    public class DataController : Controller
{
    private readonly IExternalApiService _externalApiService;

    public DataController(IExternalApiService externalApiService) {
        _externalApiService = externalApiService;
    }

    public async Task<string> FetchFromExternalApi(string apiUrl)
    {
        var data = await _externalApiService.GetDataFromApiAsync(apiUrl);
        return data;
    }
}
}