namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Customer validation service
    /// </summary>
    public class EventHubLoggingService : IEventHubLoggingService
    {
        /// <summary>
        /// Event hub logging provider
        /// </summary>
        private readonly IEventHubLoggingProvider _eventHubLoggingProvider;

        /// <summary>
        /// Initialized an instance of <cref name="EventHubLoggingService">EventHubLoggingService</cref>
        /// </summary>
        /// <param name="eventHubLoggingProvider"></param>
        public EventHubLoggingService(IEventHubLoggingProvider eventHubLoggingProvider)
        {
            _eventHubLoggingProvider = eventHubLoggingProvider;
        }

        /// <summary>
        /// Sends the data to event hub
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data, dynamic rawModel)
        {
            _eventHubLoggingProvider.Send(data, rawModel);
        }
    }
}
