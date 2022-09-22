namespace AGL.TPP.CustomerValidation.API.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;

    public interface ICustomerValidationDataProvider
    {
        /// <summary>
        /// Monitors the health of the application
        /// </summary>
        /// <returns></returns>
        Task<bool> Ping();

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialSalesCustomer(ResidentialCustomerSalesValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessSalesCustomer(BusinessCustomerSalesValidationModel businessCustomerValidationModel);

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialMoveOutCustomer(ResidentialCustomerMoveOutValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessMoveOutCustomer(BusinessCustomerMoveOutValidationModel businessCustomerValidationModel);

        /// <summary>
        /// Validates the residential customer change
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialChangeCustomer(ResidentialCustomerChangeValidationModel residentialCustomerValidationModel);

        /// <summary>
        /// Validates the business customer change
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessChangeCustomer(BusinessCustomerChangeValidationModel businessCustomerValidationModel);


        /// <summary>
        /// Residential customer cancellation
        /// </summary>
        /// <param name="residentialCustomerCancellationValidationModel">Residential customer cancellation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ResidentialCustomerCancellation(CancellationResidentialCustomerValidationModel residentialCustomerCancellationValidationModel);

        /// <summary>
        /// Business customer cancellation
        /// </summary>
        /// <param name="businessCustomerCancellationValidationModel">Residential customer cancellation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> BusinessCustomerCancellation(CancellationBusinessCustomerValidationModel businessCustomerCancellationValidationModel);
    }
}
