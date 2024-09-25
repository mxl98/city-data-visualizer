using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Services.ExternalApiService;
using WebApi.Static.UrlsConfig;

namespace WebApi.Controllers.DataController {
    public class DataController : Controller
{
    private readonly IExternalApiService _externalApiService;
    public readonly UrlsConfig _urlsConfig;

    public DataController(IExternalApiService externalApiService, IOptions<UrlsConfig> urlsConfig) {
        _externalApiService = externalApiService;
        _urlsConfig = urlsConfig.Value;
    }

    public async Task<string> FetchFromExternalApi(string apiUrl)
    {
        var data = await _externalApiService.GetDataFromApiAsync(apiUrl);
        return data;
    }
}
}