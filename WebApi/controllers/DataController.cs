using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    /// <summary>
    /// Represents the controller handling CRUD data operations
    /// </summary>
    public class DataController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IJsonParserService _jsonParserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataController"/> class.
        /// </summary>
        /// <param name="externalApiService">The ExternalApiService instance.</param>
        /// <param name="parserService">The JsonParserService instance.</param>
        public DataController(IExternalApiService externalApiService, IJsonParserService jsonParserService) {
            _externalApiService = externalApiService;
            _jsonParserService = jsonParserService;
        }

        /// <summary>
        /// Calls the <see cref="ExternalParserService"/> instance to fetch the data from the specified URL.
        /// </summary>
        /// <param name="url">The url to fetch the data from.</param>
        /// <returns>The data in a string format</returns>
        public async Task<string> FetchFromExternalApi(string url)
        {
            var data = await _externalApiService.GetDataFromApiAsync(url);
            return data;
        }

        /// <summary>
        /// Calls the appropriate parser instance to read the data at the specified filepath location.
        /// </summary>
        /// <param name="filepath">The path to the file.</param>
        public void ReadFile(string filepath)
        {
            return;
        }

        /// <summary>
        /// Gets the dictionary of URLs that will be used in the <see cref="FetchFromExternalApi"/> method.
        /// </summary>
        /// <returns>
        /// A dictionary, where the key is the name of the endpoint, and the value is the url.
        /// </returns>
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

        /// <summary>
        /// Creates or updates a file corresponding to the specified pair containing the specified data.
        /// </summary>
        /// <param name="data">The data to write to the file.</param>
        /// <param name="pair">The key-value pair containing the endpoint name and the url.</param>
        /// <remarks>
        /// The key-value pair is required to get both the endpoint name that will serve as the file name and
        /// the url containing the file extension format.
        /// </remarks>
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