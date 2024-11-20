public interface ICsvParserService
{
    public CsvResult<T> ParseFile<T>(string filepath) where T : class;
}