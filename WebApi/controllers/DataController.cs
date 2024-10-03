using Microsoft.AspNetCore.Mvc;
using WebApi.Services.JsonParserService;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    public class DataController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IJsonParserService _jsonParserService;

        public DataController(IExternalApiService externalApiService, IJsonParserService jsonParserService) {
            _externalApiService = externalApiService;
            _jsonParserService = jsonParserService;
        }

        public async Task<string> FetchFromExternalApi(string url)
        {
            var data = await _externalApiService.GetDataFromApiAsync(url);
            return data;
        }

        public Dictionary<string, string> GetSourceUrls()
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "static\\sourceurls.json");
            var SourceUrlsOptions = _jsonParserService.ParseJsonFile<SourceUrlsOptions>(filepath);

            if (SourceUrlsOptions == null || SourceUrlsOptions.SourceUrls == null)
            {
                return new Dictionary<string, string>();
            }

            return SourceUrlsOptions.SourceUrls;
        }
    }
}