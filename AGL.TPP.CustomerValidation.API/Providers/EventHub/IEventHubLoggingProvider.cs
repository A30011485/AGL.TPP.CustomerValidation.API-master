namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Event Hub Logging Provider interface
    /// </summary>
    public interface IEventHubLoggingProvider
    {
        /// <summary>
        /// Sends the data to event hub
        /// </summary>
        /// <param name="data"></param>
        void Send(string data, dynamic rawModel);
    }
}