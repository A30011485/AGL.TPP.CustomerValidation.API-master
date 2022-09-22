namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Residential customer validation model
    /// </summary>
    [DataContract]
    public class ResidentialCustomerSalesValidationModel
    {
        /// <summary>
        /// Business customer sales header
        /// </summary>
        [DataMember(Name = "header")]
        public CustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Business customer sales body
        /// </summary>
        [DataMember(Name = "payload")]
        public ResidentialCustomerSalesValidationBodyModel Payload { get; set; }
    }
}