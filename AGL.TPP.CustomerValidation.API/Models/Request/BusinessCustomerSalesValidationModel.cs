namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Business customer sales validation model
    /// </summary>
    [DataContract]
    public class BusinessCustomerSalesValidationModel
    {
        /// <summary>
        /// Business customer header
        /// </summary>
        [DataMember(Name = "header")]
        public CustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Business customer sales payload
        /// </summary>
        [DataMember(Name = "payload")]
        public BusinessCustomerSalesValidationBodyModel Payload { get; set; }
    }
}