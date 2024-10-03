public interface IJsonParserService
{
    public T ParseJsonFile<T>(string filepath) where T : class;
}