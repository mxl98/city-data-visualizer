public interface ICsvParserService
{
    public T ParseFile<T>(string filepath) where T : class;
}