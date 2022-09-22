namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Event Hub Logging service interface
    /// </summary>
    public interface IEventHubLoggingService
    {
        /// <summary>
        /// Sends the data to event hub
        /// </summary>
        void Send(string message, dynamic rawModel);
    }
}