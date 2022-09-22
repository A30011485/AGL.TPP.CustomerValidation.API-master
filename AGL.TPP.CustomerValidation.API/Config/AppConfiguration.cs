namespace AGL.TPP.CustomerValidation.API.Config
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public class AppConfiguration : IAppConfiguration
    {
        public static string CorrelationKey = "Correlation-Id";

        public string ApplicationIdentifier { get; set; }

        public string Environment { get; set; }

        public string NameIdentifierNamespace { get; set; }
    }
}
