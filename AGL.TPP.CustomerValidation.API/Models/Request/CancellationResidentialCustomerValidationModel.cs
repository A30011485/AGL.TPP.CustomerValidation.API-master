namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Residential customer cancellation validation model
    /// </summary>
    public class CancellationResidentialCustomerValidationModel
    {
        /// <summary>
        /// Residential customer cancellation header
        /// </summary>
        [DataMember(Name = "header")]
        public CancellationCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Residential customer cancellation body
        /// </summary>
        [DataMember(Name = "payload")]
        public CancellationResidentialCustomerValidationBodyModel Payload { get; set; }
    }
}