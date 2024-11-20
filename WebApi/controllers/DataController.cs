using Microsoft.AspNetCore.Mvc;
using Mysqlx.Cursor;
using WebApi.Static.SourceUrlsOptions;

namespace WebApi.Controllers.DataController {
    /// <summary>
    /// Represents the controller handling CRUD data operations
    /// </summary>
    public class DataController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IJsonParserService _jsonParserService;
        private readonly ICsvParserService _csvParserService;
        private readonly IPiscineService _piscineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataController"/> class.
        /// </summary>
        /// <param name="externalApiService">The ExternalApiService instance.</param>
        /// <param name="parserService">The JsonParserService instance.</param>
        public DataController(IExternalApiService externalApiService, 
                              IJsonParserService jsonParserService, 
                              ICsvParserService csvParserService, 
                              IPiscineService piscineService) 
        {
            _externalApiService = externalApiService;
            _jsonParserService = jsonParserService;
            _csvParserService = csvParserService;
            _piscineService = piscineService;
        }

        /// <summary>
        /// Runs all file and database update tasks.
        /// </summary>
        /// <returns>Nothing is returned.</returns>
        public async Task UpdateAllAsync()
        {
            Dictionary<string, string> sourceUrls = GetSourceUrls();
            await UpdateFilesAsync(sourceUrls);

            List<PiscineModel> piscinesData = ReadCsvFile<PiscineModel>("db/piscines.csv").List;
            await UpdatePiscinesAsync(piscinesData);
        }

        
        /// <summary>
        /// Updates the content of files based on the fetched data from the specified source urls.
        /// </summary>
        /// <param name="sourceUrls">The source urls to fetch data from.</param>
        /// <returns>Nothing is returned.</returns>
        public async Task UpdateFilesAsync(Dictionary<string, string> sourceUrls)
        {
            foreach (var pair in sourceUrls)
            {
                var data = await FetchFromExternalApi(pair.Value);
                WriteFile(data, pair);
            }
        }

        /// <summary>
        /// Updates the content of the database based on the provided list of PiscineModel.
        /// </summary>
        /// <param name="piscines">The updated list of PiscineModel.</param>
        /// <returns>Nothing is returned.</returns>
        public async Task UpdatePiscinesAsync(List<PiscineModel> piscines)
        {
            await _piscineService.AddCollectionAsync(piscines);
        }

        /// <summary>
        /// Gets all piscines from the corresponding service method.
        /// </summary>
        /// <returns>A list of Piscine entities.</returns>
        public async Task<List<PiscineModel>> GetAllPiscinesAsync()
        {
            return await _piscineService.GetAllPiscinesAsync();
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
        /// Calls the CSV parser instance to read the data at the specified filepath location.
        /// </summary>
        /// <typeparam name="T">The data type to parse into.</typeparam>
        /// <param name="filepath">The path to the file.</param>
        /// <exception cref="InvalidOperationException">thrown if used on file format other than .csv</exception>
        public CsvResult<T> ReadCsvFile<T>(string filepath) where T : class
        {
            string extension = Path.GetExtension(filepath);
            if(!extension.Equals(".csv"))
            {
                throw new InvalidOperationException("DataController.ReadCsvFile - Error: can only call on .csv files.");
            }
            var data = _csvParserService.ParseFile<T>(filepath);
            return data;
        }

        /// <summary>
        /// Gets the dictionary of URLs that will be used in the <see cref="FetchFromExternalApi"/> method.
        /// </summary>
        /// <returns>
        /// A dictionary, where the key is the name of the endpoint, and the value is the url.
        /// </returns>
        public Dictionary<string, string> GetSourceUrls()
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "static/sourceurls.json");
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
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "db/");

                Console.WriteLine("Checking for directory DB...");
                bool exists = Directory.Exists(directory);

                if (!exists) {
                    Console.WriteLine("Creating DB directory...");
                    Directory.CreateDirectory(directory);
                    Console.WriteLine("Directory DB created");
                }

                string filepath = Path.Combine(directory, $"{ pair.Key }{ extension }");

                System.IO.File.WriteAllText(filepath, data);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DataController.WriteFile - Error occured: { e.Message }");
            }
        }
    }
}