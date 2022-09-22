using Destructurama.Attributed;

namespace AGL.TPP.CustomerValidation.API.Models
{ 
    public class SiteAdditionalDetailChange
    { 
        /// <summary>
        /// Gets or Sets ChangeRequestDate
        /// </summary>
        public string ChangeRequestDate { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        [LogMasked]
        public string Comments { get; set; }
    }
}
