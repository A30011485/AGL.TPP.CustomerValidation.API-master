namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Residential customer validation body
    /// </summary>
    public class CancellationResidentialCustomerValidationBodyModel
    {
        /// <summary>
        /// Customer details
        /// </summary>
        public Customer PersonDetail { get; set; }

        /// <summary>
        /// Customer contact details
        /// </summary>
        public Contact ContactDetail { get; set; }

        /// <summary>
        /// Site address
        /// </summary>
        public StreetAddress SiteAddress { get; set; }

        /// <summary>
        /// Mailing address
        /// </summary>
        public MailingAddress MailingAddress { get; set; }

        /// <summary>
        /// Residential customer identification document
        /// </summary>
        public ResidentialIdentification Identification { get; set; }

        /// <summary>
        /// Site Meter details
        /// </summary>
        public SiteMeterDetail SiteMeterDetail { get; set; }

        /// <summary>
        /// Concession details
        /// </summary>
        public ConcessionCard ConcessionCardDetail { get; set; }

        /// <summary>
        /// Customer consent
        /// </summary>
        public CustomerConsent CustomerConsent { get; set; }

        /// <summary>
        /// Cancellation details
        /// </summary>
        public CancellationDetail CancellationDetail { get; set; }
    }
}