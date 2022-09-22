using Newtonsoft.Json;

namespace AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models
{
    public class EmailDomainValidationClientResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CorrelationId { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
