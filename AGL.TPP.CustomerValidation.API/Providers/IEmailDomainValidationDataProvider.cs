using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;

namespace AGL.TPP.CustomerValidation.API.Providers
{
    public interface IEmailDomainValidationDataProvider
    {
        
        Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, MoveOutCustomerValidationHeaderModel customerValidationHeaderModel);
        Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, CustomerValidationHeaderModel customerValidationHeaderModel);

        Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress,
            ChangeCustomerValidationHeaderModel customerValidationHeaderModel);

        Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress,
            CancellationCustomerValidationHeaderModel customerValidationHeaderModel);
    }
}
