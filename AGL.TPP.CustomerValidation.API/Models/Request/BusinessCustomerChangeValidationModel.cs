namespace AGL.TPP.CustomerValidation.API.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    public partial class BusinessCustomerChangeValidationModel
    { 
        /// <summary>
        /// Gets or Sets Header
        /// </summary>
        public ChangeCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Gets or Sets Payload
        /// </summary>
        public BusinessCustomerChangeValidationBodyModel Payload { get; set; }

    }
}
