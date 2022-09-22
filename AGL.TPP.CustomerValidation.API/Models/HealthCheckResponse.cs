namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Response class for Health check endpoint
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        ///  Is healthy
        /// </summary>
        public bool IsHealthy { get; set; }
    }
}