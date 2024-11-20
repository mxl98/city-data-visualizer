using System.Globalization;
using CsvHelper;

namespace WebApi.Services.CsvParserService
{
    /// <summary>
    /// Represents a parser for CSV files
    /// </summary>
    public class CsvParserService : ICsvParserService
    {

        /// <summary>
        /// Parses the data from the specified CSV file into a list of the specified object type.
        /// </summary>
        /// <typeparam name="T">The specified type to parse the data into.</typeparam>
        /// <param name="filepath">The path to the CSV file</param>
        /// <returns>An object of the specified type containing the parsed data.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public CsvResult<T> ParseFile<T>(string filepath) where T : class
        {
            if (!File.Exists(filepath)) 
            {
                throw new FileNotFoundException("CsvParserService - CSV file not found.", filepath);
            }
            try
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<T>();
                    return new CsvResult<T>(new List<T>(records));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("CsvParserService - Error parsing CSV file.", e);
            }
        }
    }
}