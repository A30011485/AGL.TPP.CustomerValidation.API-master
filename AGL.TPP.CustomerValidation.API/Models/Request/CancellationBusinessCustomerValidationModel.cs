namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Residential customer cancellation validation model
    /// </summary>
    public class CancellationBusinessCustomerValidationModel
    {
        /// <summary>
        /// Business customer cancellation header
        /// </summary>
        [DataMember(Name = "header")]
        public CancellationCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Business customer cancellation payload
        /// </summary>
        [DataMember(Name = "payload")]
        public CancellationBusinessCustomerValidationBodyModel Payload { get; set; }
    }
}