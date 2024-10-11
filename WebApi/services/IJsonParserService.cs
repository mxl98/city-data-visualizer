public interface IJsonParserService
{
    public T ParseFile<T>(string filepath) where T : class;
}