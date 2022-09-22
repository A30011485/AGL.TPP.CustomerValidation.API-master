namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Residential identification documents
    /// </summary>
    public class ResidentialIdentification
    {
        /// <summary>
        /// Drivers license details
        /// </summary>
        public IdentificationDocumentDriverLicense DriversLicense { get; set; }

        /// <summary>
        /// Medicare details
        /// </summary>
        public IdentificationDocumentMedicare Medicare { get; set; }

        /// <summary>
        /// Passport details
        /// </summary>
        public IdentificationDocumentPassport Passport { get; set; }
    }
}