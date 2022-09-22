using System.Collections.Generic;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapPiMultipleMessageResponse<T> : SapPiResponse<T>
    {
        public List<SapPiResponseMessage> Return { get; set; }
        public List<SapPiTimeStamp> TimeStamp { get; set; }
    }
}
