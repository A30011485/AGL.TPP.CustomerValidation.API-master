using AGL.TPP.CustomerValidation.API.Models;
using System.Threading.Tasks;

namespace AGL.TPP.CustomerValidation.API.Services
{

    public interface IEmailDomainValidationService
    {
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            ResidentialCustomerSalesValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            ResidentialCustomerMoveOutValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            ResidentialCustomerChangeValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            CancellationResidentialCustomerValidationModel validationModel);

        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            BusinessCustomerChangeValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            BusinessCustomerSalesValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            BusinessCustomerMoveOutValidationModel validationModel);
        Task<CustomerValidationResponse> ValidateEmailDomainAsync(
            CancellationBusinessCustomerValidationModel validationModel);



    }
}
