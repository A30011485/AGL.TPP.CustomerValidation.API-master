namespace AGL.TPP.CustomerValidation.API.Services
{
    using System;
    using System.Text;
    using AGL.TPP.CustomerValidation.API.Providers.EventHub.Models;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Extensions.Options;
    using Serilog;

    /// <summary>
    /// Customer validation service
    /// </summary>
    public class EventHubLoggingProvider : IEventHubLoggingProvider
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Event Hub Settings
        /// </summary>
        private readonly EventHubSettings _eventHubSettings;

        /// <summary>
        /// Event Hub Client
        /// </summary>
        private EventHubClient _eventHubClient;

        /// <summary>
        /// Initializes a new instance of <cref name="EventHubLoggingService"></cref> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public EventHubLoggingProvider(
            ILogger logger,
            IOptions<EventHubSettings> eventHubSettings)
        {
            _logger = logger;
            _eventHubSettings = eventHubSettings.Value;

            ConfigureEventHub();
        }

        /// <summary>
        /// Configures event hub
        /// </summary>
        private void ConfigureEventHub()
        {
            try
            {
                // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath.
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(_eventHubSettings.ConnectionString)
                {
                    EntityPath = _eventHubSettings.Name
                };

                _eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "An exception has occured while creating connection to event hub");
            }
        }

        /// <summary>
        /// Sends the data to event hub
        /// </summary>
        /// <param name="data">Data to be sent</param>
        public void Send(string data, dynamic rawModel)
        {
            if (_eventHubSettings.Enabled)
            {
                try
                {
                    _logger.Debug("Sending data to event hub: {@rawModel}", rawModel);
                    _eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(data)));
                    _logger.Debug("Data successfully sent to event hub");
                }
                catch (Exception exception)
                {
                    this._logger.Error(exception, "An exception occured while sending data to event hub");
                }
            }
        }
    }
}
