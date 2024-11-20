using System.IO;
using System.Text.Json;

namespace WebApi.Services.JsonParserService
{
    /// <summary>
    /// Represents a parser for JSON files.
    /// </summary>
    public class JsonParserService : IJsonParserService
    {
        /// <summary>
        /// Parses the data of a specified JSON file into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The object type to parse the data into.</typeparam>
        /// <param name="filepath">The path to the file.</param>
        /// <returns>An object of the specified type containing the parsed data.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the specified JSON file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a parsing error occurs.</exception>
        public T ParseFile<T>(string filepath) where T : class
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