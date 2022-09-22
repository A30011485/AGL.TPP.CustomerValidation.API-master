namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Residential customer validation body
    /// </summary>
    public class CancellationBusinessCustomerValidationBodyModel
    {
        /// <summary>
        /// Customer contact details
        /// </summary>
        public Contact ContactDetail { get; set; }

        /// <summary>
        /// Business details
        /// </summary>
        public BusinessDetail BusinessDetail { get; set; }

        /// <summary>
        /// Authorised contact person details
        /// </summary>
        public Customer AuthorisedContactPersonDetail { get; set; }

        /// <summary>
        /// Contact details of the business
        /// </summary>
        public Contact AuthorisedPersonContact { get; set; }

        /// <summary>
        /// Site address to provision a connection
        /// </summary>
        public StreetAddress SiteAddress { get; set; }

        /// <summary>
        /// Mailing address for the connection
        /// </summary>
        public MailingAddress MailingAddress { get; set; }

        /// <summary>
        /// Identification document
        /// </summary>
        public BusinessIdentification BusinessIdentification { get; set; }

        /// <summary>
        /// Site Meter details
        /// </summary>
        public SiteMeterDetail SiteMeterDetail { get; set; }

        /// <summary>
        /// Authorised person identification document
        /// </summary>
        public ResidentialIdentification AuthorisedPersonIdentification { get; set; }

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