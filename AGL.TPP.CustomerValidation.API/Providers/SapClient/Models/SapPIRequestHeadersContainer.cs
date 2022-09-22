using System.Collections.Generic;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapPiRequestHeadersContainer
    {
        /// <remarks>
        /// This must always be provided
        /// </remarks>
        public string CorrelationId { get; }

        public Dictionary<string, string> OptionalHeaders { get; set; } = new Dictionary<string, string>();

        public SapPiRequestHeadersContainer(string correlationId, string nameId = null)
        {
            CorrelationId = correlationId;
            if (!string.IsNullOrEmpty(nameId))
            {
                OptionalHeaders.Add("X-NameId", nameId);
            }
        }
    }
}
