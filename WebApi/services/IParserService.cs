public interface IParserService
{
    public T ParseFile<T>(string filepath) where T : class;
}