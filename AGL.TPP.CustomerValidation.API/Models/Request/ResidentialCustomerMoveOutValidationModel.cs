namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Residential customer move out validation model
    /// </summary>
    [DataContract]
    public class ResidentialCustomerMoveOutValidationModel
    {
        /// <summary>
        /// Business customer header
        /// </summary>
        [DataMember(Name = "header")]
        public MoveOutCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Business customer move out body
        /// </summary>
        [DataMember(Name = "payload")]
        public ResidentialCustomerMoveOutValidationBodyModel Payload { get; set; }
    }
}