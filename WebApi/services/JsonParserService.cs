using System.IO;
using System.Text.Json;

namespace WebApi.Services.JsonParserService
{
    public class JsonParserService : IJsonParserService
    {
        public T ParseJsonFile<T>(string filepath) where T : class
        {
            if (!File.Exists(filepath)) 
            {
                throw new FileNotFoundException("JSON file not found.", filepath);
            }

            try
            {
                var jsonContent = File.ReadAllText(filepath);
                var result = JsonSerializer.Deserialize<T>(jsonContent);
                return result;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error parsing JSON file.", e);
            }
        }
    }
}