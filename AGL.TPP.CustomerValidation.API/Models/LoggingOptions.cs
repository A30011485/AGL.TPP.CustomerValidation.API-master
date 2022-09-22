namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Logging options
    /// </summary>
    public class LoggingOptions
    {
        public string LogOutputTemplate { get; set; }
        public Trace Trace { get; set; }
        public Splunk Splunk { get; set; }
        public string LogLevel { get; set; }
    }

    /// <summary>
    /// Trace
    /// </summary>
    public class Trace
    {
        public bool Enabled { get; set; }
        public string LogLevel { get; set; }
    }

    /// <summary>
    /// Splunk
    /// </summary>
    public class Splunk
    {
        public bool Enabled { get; set; }
        public string LogLevel { get; set; }
        public string Token { get; set; }
        public string Host { get; set; }
    }
}