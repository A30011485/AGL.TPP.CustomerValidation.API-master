namespace AGL.TPP.CustomerValidation.API.Providers.EventHub.Models
{
    /// <summary>
    /// Event Hub settings
    /// </summary>
    public class EventHubSettings
    {
        /// <summary>
        /// Event Hub name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Is Event Hub Enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
