namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Details of the business
    /// </summary>
    public class BusinessDetail
    {
        /// <summary>
        /// Business name
        /// </summary>
        [LogMasked]
        public string Name { get; set; }
    }
}