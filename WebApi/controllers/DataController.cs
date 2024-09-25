using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Services.ExternalApiService;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    public class DataController : Controller
{
    private readonly IExternalApiService _externalApiService;
    public readonly SourceUrlsOptions _urlsOptions;

    public DataController(IExternalApiService externalApiService, IOptions<SourceUrlsOptions> urlsConfig) {
        _externalApiService = externalApiService;
        _urlsOptions = urlsConfig.Value;
    }

    public async Task<string> FetchFromExternalApi(string apiUrl)
    {
        var data = await _externalApiService.GetDataFromApiAsync(apiUrl);
        return data;
    }
}
}