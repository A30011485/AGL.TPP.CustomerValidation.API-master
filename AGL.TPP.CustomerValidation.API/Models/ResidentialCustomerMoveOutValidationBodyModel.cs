namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Residential customer validation body
    /// </summary>
    public class ResidentialCustomerMoveOutValidationBodyModel
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
        /// Residential Identification document
        /// </summary>
        public ResidentialIdentification Identification { get; set; }

        /// <summary>
        /// Site address
        /// </summary>
        public StreetAddress SiteAddress { get; set; }

        /// <summary>
        /// Mailing address
        /// </summary>
        public MailingAddress MailingAddress { get; set; }

        /// <summary>
        /// Site Meter details
        /// </summary>
        public SiteMeterDetail SiteMeterDetail { get; set; }

        /// <summary>
        /// Move details
        /// </summary>
        public MoveDetail MoveDetail { get; set; }

        /// <summary>
        /// Concession Card Details
        /// </summary>
        public ConcessionCard ConcessionCardDetail { get; set; }
    }
}