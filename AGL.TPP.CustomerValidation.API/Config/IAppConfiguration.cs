namespace AGL.TPP.CustomerValidation.API.Config
{
    /// <summary>
    /// Interface for application configuration
    /// </summary>
    public interface IAppConfiguration
    {
        string ApplicationIdentifier { get; set; }

        string Environment { get; set; }

        string NameIdentifierNamespace { get; set; }
    }
}