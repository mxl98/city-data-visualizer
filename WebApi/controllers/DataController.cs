using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    public class DataController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IJsonParserService _parserService;

        public DataController(IExternalApiService externalApiService, IJsonParserService parserService) {
            _externalApiService = externalApiService;
            _parserService = parserService;
        }

        public async Task<string> FetchFromExternalApi(string url)
        {
            var data = await _externalApiService.GetDataFromApiAsync(url);
            return data;
        }

        public void ReadFile(string filepath)
        {
            return;
        }

        public Dictionary<string, string> GetSourceUrls()
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "static\\sourceurls.json");
            var SourceUrlsOptions = _parserService.ParseFile<SourceUrlsOptions>(filepath);

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