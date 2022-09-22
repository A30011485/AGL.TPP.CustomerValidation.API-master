using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient
{
    public interface IEmailDomainValidationClient
    {
        Task<EmailDomainValidationClientResponse> ValidateDomainAsync(string domainInput, string correlationId);
    }
}
