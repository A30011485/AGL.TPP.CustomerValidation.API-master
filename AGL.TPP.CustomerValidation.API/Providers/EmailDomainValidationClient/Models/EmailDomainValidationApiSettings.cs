using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models
{
    public class EmailDomainValidationApiSettings
    {
        public string Endpoint { get; set; }
        public string SubscriptionKey { get; set; }
        public string KasadaBypassSecretKey { get; set; }
    }
}
