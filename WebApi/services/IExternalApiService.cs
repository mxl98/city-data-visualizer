public interface IExternalApiService
{
    Task<string> GetDataFromApiAsync(string apiUrl);
}