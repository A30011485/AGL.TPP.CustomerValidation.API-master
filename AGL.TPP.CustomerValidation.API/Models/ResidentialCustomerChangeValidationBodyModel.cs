namespace AGL.TPP.CustomerValidation.API.Models
{ 
    public class ResidentialCustomerChangeValidationBodyModel
    { 
        /// <summary>
        /// Gets or Sets PersonDetail
        /// </summary>
        public Customer PersonDetail { get; set; }

        /// <summary>
        /// Gets or Sets ContactDetail
        /// </summary>
        public Contact ContactDetail { get; set; }

        /// <summary>
        /// Gets or Sets Identification
        /// </summary>
        public ResidentialIdentification Identification { get; set; }

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
