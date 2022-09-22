namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Interface for User context class
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Correlation id
        /// </summary>
        string CorrelationId { get; }

        string ApiName { get; }
    }
}
