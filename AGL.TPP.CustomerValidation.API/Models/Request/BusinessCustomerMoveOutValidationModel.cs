namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Business customer move out validation model
    /// </summary>
    [DataContract]
    public class BusinessCustomerMoveOutValidationModel
    {
        /// <summary>
        /// Business customer header
        /// </summary>
        [DataMember(Name = "header")]
        public MoveOutCustomerValidationHeaderModel Header { get; set; }

        /// <summary>
        /// Business customer move out payload
        /// </summary>
        [DataMember(Name = "payload")]
        public BusinessCustomerMoveOutValidationBodyModel Payload { get; set; }
    }
}