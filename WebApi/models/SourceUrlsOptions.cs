namespace WebApi.Static.SourceUrlsOptions
{
    /// <summary>
    /// Represents the model for a dictionary of URLs attached to a name for easy identification.
    /// </summary>
    public class SourceUrlsOptions 
    {
        public const string SectionName = "SourceUrls";
        public Dictionary<string, string>? SourceUrls { get; set; }
    }
}