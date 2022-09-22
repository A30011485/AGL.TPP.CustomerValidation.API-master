namespace AGL.TPP.CustomerValidation.API.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Response type for customer validation
    /// </summary>
    public class CustomerValidationResponse
    {
        /// <summary>
        /// Initializes a new instance of <cref name="CustomerValidationResponse"></cref> class
        /// </summary>
        public CustomerValidationResponse()
        {
            Errors = new List<FieldLevelError>();
        }

        /// <summary>
        /// Initializes a new instance of <cref name="CustomerValidationResponse"></cref> class with status code and message
        /// </summary>
        public CustomerValidationResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Status code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Correlation Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CorrelationId { get; set; }

        /// <summary>
        /// Field level validation errors
        /// </summary>
        public List<FieldLevelError> Errors { get; set; }
    }
}