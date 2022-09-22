namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Passport details
    /// </summary>
    public class IdentificationDocumentPassport
    {
        /// <summary>
        /// Passport number
        /// </summary>
        [LogMasked]
        public string PassportNumber { get; set; }
    }
}