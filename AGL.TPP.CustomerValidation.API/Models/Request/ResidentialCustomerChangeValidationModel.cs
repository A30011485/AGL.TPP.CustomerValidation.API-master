namespace AGL.TPP.CustomerValidation.API.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    public class ResidentialCustomerChangeValidationModel
    { 
        /// <summary>
        /// Gets or Sets Header
        /// </summary>
        public ChangeCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Gets or Sets Payload
        /// </summary>
        public ResidentialCustomerChangeValidationBodyModel Payload { get; set; }

    }
}
