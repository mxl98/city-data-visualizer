namespace WebApi.Services.ExternalApiService
{
    /// <summary>
    /// Represents a service for making external API calls.
    /// </summary>
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalApiService"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"=> class.</param>
        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Sends a GET request to the speficied API url asynchronously.
        /// </summary>
        /// <param name="apiUrl">The URL to make the GET request to.</param>
        /// <returns>The data as a string Task object.</returns>
        public async Task<string> GetDataFromApiAsync(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            return data;
        }
    }
}