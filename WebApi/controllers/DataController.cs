using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    public class DataController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IParserService _jsonParserService;

        public DataController(IExternalApiService externalApiService, IParserService jsonParserService) {
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
            var SourceUrlsOptions = _jsonParserService.ParseFile<SourceUrlsOptions>(filepath);

            if (SourceUrlsOptions == null || SourceUrlsOptions.SourceUrls == null)
            {
                return new Dictionary<string, string>();
            }

            return SourceUrlsOptions.SourceUrls;
        }

        public void WriteFile(string data, KeyValuePair<string, string> pair)
        {
            try
            {
                string extension = Path.GetExtension(pair.Value);
                string filepath = Path.Combine([Directory.GetCurrentDirectory(), "db\\", $"{ pair.Key }{ extension }"]);
                System.IO.File.WriteAllText(filepath, data);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DataController.WriteFile - Error occured: { e.Message }");
            }
        }
    }
}