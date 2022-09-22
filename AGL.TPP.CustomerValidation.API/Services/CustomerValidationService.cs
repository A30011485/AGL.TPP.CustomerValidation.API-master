using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Newtonsoft.Json;
using Serilog;
using static System.Enum;

namespace AGL.TPP.CustomerValidation.API.Services
{
    using static Constants;

    /// <summary>
    /// Customer validation service
    /// </summary>
    public class CustomerValidationService : ICustomerValidationService
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Customer validation data provider instance
        /// </summary>
        private readonly ICustomerValidationDataProvider _salesOrderDataProvider;
        
        /// <summary>
        /// Sap Error Message service
        /// </summary>
        private readonly ISapErrorMessageService _sapErrorMessageService;

        private readonly IEmailDomainValidationService _emailDomainValidationService;

        /// <summary>
        /// User Context
        /// </summary>
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of <cref name="CustomerValidationService"></cref> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="salesOrderDataProvider">Sales order data provider</param>
        public CustomerValidationService(
            ILogger logger,
            ICustomerValidationDataProvider salesOrderDataProvider,
            ISapErrorMessageService sapErrorMessageService,
            IEmailDomainValidationService emailDomainValidationService,
            IUserContext userContext)
        {
            _logger = logger;
            _salesOrderDataProvider = salesOrderDataProvider;
            _sapErrorMessageService = sapErrorMessageService;
            _emailDomainValidationService = emailDomainValidationService;
            _userContext = userContext;
        }

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns customer validation response</returns>
        public async Task<CustomerValidationResponse> ValidateResidentialSalesCustomer(ResidentialCustomerSalesValidationModel residentialCustomerValidationModel)
        {

            try
            {
                if (residentialCustomerValidationModel.Payload.Identification?.Medicare != null)
                {
                    residentialCustomerValidationModel.Payload.Identification.Medicare.MedicareNumber =
                        residentialCustomerValidationModel.Payload.Identification.Medicare.ToMedicareNumber();
                }

                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(residentialCustomerValidationModel);

                var sapResponse = await _salesOrderDataProvider.ValidateResidentialSalesCustomer(residentialCustomerValidationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);

                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateResidentialCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns business validation response</returns>
        public async Task<CustomerValidationResponse> ValidateBusinessSalesCustomer(BusinessCustomerSalesValidationModel businessCustomerValidationModel)
        {
            try
            {
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(businessCustomerValidationModel);
                var sapResponse = await _salesOrderDataProvider.ValidateBusinessSalesCustomer(businessCustomerValidationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateBusinessCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns customer validation response</returns>
        public async Task<CustomerValidationResponse> ValidateResidentialMoveOutCustomer(ResidentialCustomerMoveOutValidationModel residentialCustomerValidationModel)
        {
            try
            {
                residentialCustomerValidationModel.Payload.Identification.Medicare.MedicareNumber = residentialCustomerValidationModel.Payload.Identification.Medicare.ToMedicareNumber();
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(residentialCustomerValidationModel);
                var sapResponse = await _salesOrderDataProvider.ValidateResidentialMoveOutCustomer(residentialCustomerValidationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;

            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateResidentialCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns business validation response</returns>
        public async Task<CustomerValidationResponse> ValidateBusinessMoveOutCustomer(BusinessCustomerMoveOutValidationModel businessCustomerValidationModel)
        {
            try
            {
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(businessCustomerValidationModel);
                var sapResponse = await _salesOrderDataProvider.ValidateBusinessMoveOutCustomer(businessCustomerValidationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;

            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateBusinessCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns customer validation response</returns>
        public async Task<CustomerValidationResponse> ValidateResidentialChangeCustomer(ResidentialCustomerChangeValidationModel residentialCustomerValidationModel)
        {
            try
            {
                residentialCustomerValidationModel.Payload.Identification.Medicare.MedicareNumber = residentialCustomerValidationModel.Payload.Identification.Medicare.ToMedicareNumber();
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(residentialCustomerValidationModel);
                var sapResponse = await _salesOrderDataProvider.ValidateResidentialChangeCustomer(residentialCustomerValidationModel);

                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateResidentialCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns business validation response</returns>
        public async Task<CustomerValidationResponse> ValidateBusinessChangeCustomer(BusinessCustomerChangeValidationModel businessCustomerValidationModel)
        {
            try
            {
                var sapResponse = await _salesOrderDataProvider.ValidateBusinessChangeCustomer(businessCustomerValidationModel);
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(businessCustomerValidationModel);
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;

            }
            catch (Exception ex)
            {
                _logger.Error($"ValidateBusinessCustomer error {ex}");
                throw;
            }
        }

        /// <summary>
        /// Cancellation of residential customer
        /// </summary>
        /// <param name="residentialCustomerCancellationModel">Residential customer validation model</param>
        /// <returns></returns>
        public async Task<CustomerValidationResponse> ValidateResidentialCustomerCancellation(CancellationResidentialCustomerValidationModel residentialCustomerCancellationModel)
        {
            try
            {
                residentialCustomerCancellationModel.Payload.Identification.Medicare.MedicareNumber = residentialCustomerCancellationModel.Payload.Identification.Medicare.ToMedicareNumber();

                var displayAttribute = GetCancellationReason(residentialCustomerCancellationModel.Payload.CancellationDetail);
                residentialCustomerCancellationModel.Payload.CancellationDetail.Reason = displayAttribute.Name;
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(residentialCustomerCancellationModel);
                var sapResponse = await _salesOrderDataProvider.ResidentialCustomerCancellation(residentialCustomerCancellationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;

            }
            catch (Exception ex)
            {
                _logger.Error($"ResidentialCustomerCancellation error {ex}");
                throw;
            }
        }
        
        /// <summary>
        /// Cancellation of business customer
        /// </summary>
        /// <param name="businessCustomerCancellationModel">Business customer validation model</param>
        /// <returns></returns>
        public async Task<CustomerValidationResponse> ValidateBusinessCustomerCancellation(CancellationBusinessCustomerValidationModel businessCustomerCancellationModel)
        {
            try
            {
                var displayAttribute = GetCancellationReason(businessCustomerCancellationModel.Payload.CancellationDetail);
                businessCustomerCancellationModel.Payload.CancellationDetail.Reason = displayAttribute.Name;
                var domainValidationResponse = await _emailDomainValidationService.ValidateEmailDomainAsync(businessCustomerCancellationModel);
                var sapResponse = await _salesOrderDataProvider.BusinessCustomerCancellation(businessCustomerCancellationModel);

                // TODO : Update the code after error codes are finalized
                var validationResponse = MapSapResponse(sapResponse);
                if (domainValidationResponse.Errors.Any())
                {
                    validationResponse.Code = "400";
                    validationResponse.Message = BackEndValidationError;
                    validationResponse.Errors.AddRange(domainValidationResponse.Errors);
                }

                return validationResponse;

            }
            catch (Exception ex)
            {
                _logger.Error($"BusinessCustomerCancellation error {ex}");
                throw;
            }
        }
        
        private DisplayAttribute GetCancellationReason(CancellationDetail cancellationDetailReason)
        {
            TryParse(cancellationDetailReason.Reason, true, out CancellationReasons p);

            return EnumHelper.GetAttribute<DisplayAttribute>(p);
        }
        /// <summary>
        /// Maps Sap response
        /// </summary>
        /// <param name="sapResult">Sap result object</param>
        /// <returns>Returns an instance of Customer validation response</returns>
        private CustomerValidationResponse MapSapResponse(SapPiMultipleMessageResponse<CustomerValidationResponse> sapResult)
        {
            var fieldLevelErrors = new List<FieldLevelError>();

            var isSuccess = sapResult.Return.Any(r => r.Type == "S");
            if (isSuccess)
            {
                fieldLevelErrors.Add(GetFieldLevelError(sapResult.Return.FirstOrDefault()));
            }
            else
            {
                sapResult.Return.ForEach(result =>
                {
                    fieldLevelErrors.Add(GetFieldLevelError(result));
                });
            }

            var response = new CustomerValidationResponse
            {
                Code = isSuccess ? "200" : "400",
                Message = isSuccess ? fieldLevelErrors.FirstOrDefault()?.Message : BackEndValidationError,
                Errors = isSuccess ? new List<FieldLevelError>() : fieldLevelErrors
            };

            // If SAP returns an error that is not in the errors list
            // return unknown error with correlation Id
            if (fieldLevelErrors.Any(e => e.Code == ApiErrorMessages.UnknownError.Code))
            {
                response.Code = "5000";
                response.CorrelationId = _userContext.CorrelationId;
            }

            return response;
        }

        private FieldLevelError GetFieldLevelError(SapPiResponseMessage result)
        {
            int.TryParse(result.Number, out int errorNumber);

            var apiError = _sapErrorMessageService.GetApiErrorMessage(errorNumber, result);
            if (apiError == null)
            {
                _logger.Warning($"Failed to map Sap error codes to API - {JsonConvert.SerializeObject(result)} - CorrelationId: {_userContext.CorrelationId}");

                return new FieldLevelError
                {
                    Code = ApiErrorMessages.UnknownError.Code,
                    Field = null,
                    Message = ApiErrorMessages.UnknownError.Message
                };
            }

            return new FieldLevelError
            {
                Code = apiError.ApiCode.ToString(),
                Field = null,
                Message = apiError.Message
            };
        }
    }
}
