namespace WebApi.Services.CsvParserService
{
    /// <summary>
    /// Represents a parser for CSV files
    /// </summary>
    public class CsvParserService : ICsvParserService
    {

        /// <summary>
        /// Parses the data from the specified file into a list of the specified object type.
        /// </summary>
        /// <typeparam name="T">The specified type to parse the data into.</typeparam>
        /// <param name="filepath">The path to the file</param>
        /// <returns>An object of the specified type containing the parsed data.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public T ParseFile<T>(string filepath) where T : class
        {
            throw new NotImplementedException();
        }
    }
}