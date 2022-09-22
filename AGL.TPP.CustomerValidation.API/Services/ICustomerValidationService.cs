using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;

namespace AGL.TPP.CustomerValidation.API.Services
{
    public interface ICustomerValidationService
    {
        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateResidentialSalesCustomer(ResidentialCustomerSalesValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateBusinessSalesCustomer(BusinessCustomerSalesValidationModel businessCustomerValidationModel);

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateResidentialMoveOutCustomer(ResidentialCustomerMoveOutValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateBusinessMoveOutCustomer(BusinessCustomerMoveOutValidationModel businessCustomerValidationModel);

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateResidentialChangeCustomer(ResidentialCustomerChangeValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateBusinessChangeCustomer(BusinessCustomerChangeValidationModel businessCustomerValidationModel);


        /// <summary>
        /// Cancellation of the residential customer
        /// </summary>
        /// <param name="residentialCustomerCancellationModel">Residential Customer cancellation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateResidentialCustomerCancellation(CancellationResidentialCustomerValidationModel residentialCustomerCancellationModel);

        /// <summary>
        /// Cancellation of the business customer
        /// </summary>
        /// <param name="businessCustomerCancellationModel">Business Customer cancellation payload</param>
        /// <returns></returns>
        Task<CustomerValidationResponse> ValidateBusinessCustomerCancellation(CancellationBusinessCustomerValidationModel businessCustomerCancellationModel);
    }
}