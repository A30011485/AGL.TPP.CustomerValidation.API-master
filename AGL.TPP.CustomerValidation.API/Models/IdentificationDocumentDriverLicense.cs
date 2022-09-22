namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Drivers license details
    /// </summary>
    public class IdentificationDocumentDriverLicense
    {
        /// <summary>
        /// License number
        /// </summary>
        [LogMasked]
        public string LicenseNumber { get; set; }
    }
}