namespace AGL.TPP.CustomerValidation.API.Models
{
    public class BusinessCustomerChangeValidationBodyModel
    {
        /// <summary>
        /// Customer contact details
        /// </summary>
        public Contact ContactDetail { get; set; }

        public BusinessDetail BusinessDetail { get; set; }

        /// <summary>
        /// Gets or Sets BusinessIdentification
        /// </summary>
        public BusinessIdentification BusinessIdentification { get; set; }

        /// <summary>
        /// Gets or Sets AuthorisedContactPersonDetail
        /// </summary>
        public Customer AuthorisedContactPersonDetail { get; set; }

        /// <summary>
        /// Gets or Sets AuthorisedPersonContact
        /// </summary>
        public Contact AuthorisedPersonContact { get; set; }

        /// <summary>
        /// Gets or Sets AuthorisedPersonIdentification
        /// </summary>
        public ResidentialIdentification AuthorisedPersonIdentification { get; set; }

        /// <summary>
        /// Gets or Sets SiteAddress
        /// </summary>
        public StreetAddress SiteAddress { get; set; }

        /// <summary>
        /// Gets or Sets MailingAddress
        /// </summary>
        public MailingAddress MailingAddress { get; set; }

        /// <summary>
        /// Gets or Sets SiteAdditionalDetail
        /// </summary>
        public SiteAdditionalDetailChange SiteAdditionalDetail { get; set; }

        /// <summary>
        /// Gets or Sets SiteMeterDetail
        /// </summary>
        public SiteMeterDetail SiteMeterDetail { get; set; }

        /// <summary>
        /// Gets or Sets MoveDetail
        /// </summary>
        public MoveDetail MoveDetail { get; set; }

    }
}
