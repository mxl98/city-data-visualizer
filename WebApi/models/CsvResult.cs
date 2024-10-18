/// <summary>
/// Represents the model for a list of typed objects.
/// </summary>
public class CsvResult<T>
{
    public List<T> List { set; get; } = new List<T>();

    public CsvResult(List<T> list)
    {
        List = list;
    }
}