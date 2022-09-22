namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Internal server error
    /// </summary>
    public class InternalServerError
    {
        /// <summary>
        /// Correlation id
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Error Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }
    }
}
