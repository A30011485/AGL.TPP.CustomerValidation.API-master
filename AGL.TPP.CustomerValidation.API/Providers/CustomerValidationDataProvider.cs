namespace AGL.TPP.CustomerValidation.API.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Models.Repository;
    using SapClient;
    using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
    using Services;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Serilog;

    /// <summary>
    /// Customer validation data provider
    /// </summary>
    public class CustomerValidationDataProvider : ICustomerValidationDataProvider
    {
        /// <summary>
        /// Customer validation endpoint
        /// </summary>
        private readonly string _customerValidationEndpoint;

        /// <summary>
        /// Event Hub Logging Provider
        /// </summary>
        private readonly IEventHubLoggingProvider _eventHubLoggingProvider;

        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Azure storage table repository
        /// </summary>
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        /// <summary>
        /// Sap Pi client to get multiple messages
        /// </summary>
        private readonly ISapPiClientMultipleMessage _sapPiClient;

        /// <summary>
        /// Sap server address
        /// </summary>
        private readonly string _sapServerAddress;

        /// <summary>
        /// User context instance that stores correlation across the request
        /// </summary>
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initialized an instance of customer validation data provider
        /// </summary>
        /// <param name="sapPiClient">Sap Pi client to get multiple messages</param>
        /// <param name="settings">Sap Pi settings</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="salesOrderRepository">Sales order repository</param>
        /// <param name="userContext">User context that stores the correlation id</param>
        public CustomerValidationDataProvider(
            ISapPiClientMultipleMessage sapPiClient,
            IOptions<SapPiSettings> settings,
            ILogger logger,
            ITableRepository<CustomerValidationData> salesOrderRepository,
            IEventHubLoggingProvider eventHubLoggingProvider,
            IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
            _salesOrderRepository = salesOrderRepository;
            _customerValidationEndpoint = settings.Value?.CustomerValidationEndpoint;
            _sapServerAddress = settings.Value?.Host + ":" + settings.Value?.Port + _customerValidationEndpoint;
            _sapPiClient = sapPiClient;
            _eventHubLoggingProvider = eventHubLoggingProvider;
        }

        /// <summary>
        /// Cancellation of the business customer
        /// </summary>
        /// <param name="businessCustomerCancellationModel">Business customer cancellation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> BusinessCustomerCancellation(CancellationBusinessCustomerValidationModel businessCustomerCancellationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@businessCustomerCancellationModel}", _sapServerAddress, businessCustomerCancellationModel);

            var businessCustomerCancellationValidation = $"{ _customerValidationEndpoint}/business/validation/cancellation";

            var response = await _sapPiClient.PostAsync<CancellationBusinessCustomerValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                businessCustomerCancellationValidation,
                businessCustomerCancellationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = businessCustomerCancellationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = businessCustomerCancellationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = businessCustomerCancellationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = businessCustomerCancellationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Monitors the health of the application
        /// </summary>
        /// <returns>Returns the health check status of the application</returns>
        public async Task<bool> Ping()
        {
            try
            {
                var isTableExists = await _salesOrderRepository.IsTableExists();
                return isTableExists;
            }
            catch (Exception e)
            {
                _logger.Error("Table storage for Sales Order API Ping failed {@exception}", e);
                return false;
            }
        }

        /// <summary>
        /// Cancellation of the residential customer
        /// </summary>
        /// <param name="residentialCustomerCancellationModel">Residential customer cancellation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ResidentialCustomerCancellation(CancellationResidentialCustomerValidationModel residentialCustomerCancellationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@residentialCustomerCancellationModel}", _sapServerAddress, residentialCustomerCancellationModel);

            var residentialCustomerCancellationValidation = $"{ _customerValidationEndpoint}/residential/validation/cancellation";

            var response = await _sapPiClient.PostAsync<CancellationResidentialCustomerValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                residentialCustomerCancellationValidation,
                residentialCustomerCancellationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = residentialCustomerCancellationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = residentialCustomerCancellationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = residentialCustomerCancellationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = residentialCustomerCancellationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessChangeCustomer(BusinessCustomerChangeValidationModel businessCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@businessCustomerValidationModel} for business Change type", _sapServerAddress, businessCustomerValidationModel);

            var businessCustomerValidation = $"{ _customerValidationEndpoint}/business/validation/change";

            var response = await _sapPiClient.PostAsync<BusinessCustomerChangeValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                businessCustomerValidation,
                businessCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = businessCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = businessCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = businessCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = businessCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessMoveOutCustomer(BusinessCustomerMoveOutValidationModel businessCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@businessCustomerValidationModel} for business move out type", _sapServerAddress, businessCustomerValidationModel);

            var businessCustomerValidation = $"{ _customerValidationEndpoint}/business/validation/moveOut";

            var response = await _sapPiClient.PostAsync<BusinessCustomerMoveOutValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                businessCustomerValidation,
                businessCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = businessCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = businessCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = businessCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = businessCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the business customer
        /// </summary>
        /// <param name="businessCustomerValidationModel">Business customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateBusinessSalesCustomer(BusinessCustomerSalesValidationModel businessCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@businessCustomerValidationModel} for business sales type", _sapServerAddress, businessCustomerValidationModel);

            var businessCustomerValidation = $"{ _customerValidationEndpoint}/business/validation/salesAndSdfi";

            var response = await _sapPiClient.PostAsync<BusinessCustomerSalesValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                businessCustomerValidation,
                businessCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = businessCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = businessCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = businessCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = businessCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the residential customer for move out
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer change validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialChangeCustomer(ResidentialCustomerChangeValidationModel residentialCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@residentialCustomerValidationModel} for residential Change type", _sapServerAddress, residentialCustomerValidationModel);

            var residentialCustomerValidation = $"{ _customerValidationEndpoint}/residential/validation/change";

            var response = await _sapPiClient.PostAsync<ResidentialCustomerChangeValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                residentialCustomerValidation,
                residentialCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = residentialCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = residentialCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = residentialCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = residentialCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the residential customer for move out
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer move out validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialMoveOutCustomer(ResidentialCustomerMoveOutValidationModel residentialCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {_sapServerAddress} with body {@residentialCustomerValidationModel} for residential move out type", _sapServerAddress, residentialCustomerValidationModel);

            var residentialCustomerValidation = $"{ _customerValidationEndpoint}/residential/validation/moveOut";

            var response = await _sapPiClient.PostAsync<ResidentialCustomerMoveOutValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                residentialCustomerValidation,
                residentialCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = residentialCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = residentialCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = residentialCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = residentialCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        /// <summary>
        /// Validates the residential customer
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer validation payload</param>
        /// <returns>Returns Sap pi multiple message response</returns>
        public async Task<SapPiMultipleMessageResponse<CustomerValidationResponse>> ValidateResidentialSalesCustomer(ResidentialCustomerSalesValidationModel residentialCustomerValidationModel)
        {
            _logger.Debug("Preparing to call Sap PO {sapServerAddress} with body {@residentialCustomerValidationModel} for residential sales type", _sapServerAddress, residentialCustomerValidationModel);

            var residentialCustomerValidation = $"{ _customerValidationEndpoint}/residential/validation/salesAndSdfi";

            var response = await _sapPiClient.PostAsync<ResidentialCustomerSalesValidationModel, CustomerValidationResponse>(
                new SapPiRequestHeadersContainer(_userContext.CorrelationId),
                residentialCustomerValidation,
                residentialCustomerValidationModel
            );

            var eventHubHeader = new EventHubHeader
            {
                VendorName = residentialCustomerValidationModel?.Header?.VendorName,
                VendorBusinessPartnerNumber = residentialCustomerValidationModel?.Header?.VendorBusinessPartnerNumber,
                VendorLeadId = residentialCustomerValidationModel?.Header?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = residentialCustomerValidationModel?.Header?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        private string GetTimestamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Serializes the model to json
        /// </summary>
        /// <param name="model">Input object model</param>
        /// <returns>Returns Json string</returns>
        private string JsonSerialize(object model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }

        /// <summary>
        /// Sends a copy of Sap response to event hub
        /// </summary>
        /// <param name="vendorBusinessPartnerNumber"></param>
        /// <param name="vendorLeadId"></param>
        /// <param name="response"></param>
        private void SendToEventHub(EventHubHeader eventHubHeader, SapPiMultipleMessageResponse<CustomerValidationResponse> response)
        {
            var isSuccess = Convert.ToBoolean(response?.Return.Any(x => x.Type == "S"));
            var updatedResponse = new
            {
                MessageType = isSuccess ? Constants.ThirdPartySapSuccess : Constants.ThirdPartySapError,
                Timestamp = GetTimestamp(),
                MessageBody = new
                {
                    Header = new
                    {
                        VendorName = eventHubHeader?.VendorName,
                        VendorBusinessPartnerNumber = eventHubHeader?.VendorBusinessPartnerNumber,
                        VendorLeadId = eventHubHeader?.VendorLeadId,
                        CorrelationId = _userContext?.CorrelationId,
                        TransactionType = eventHubHeader?.TransactionType,
                        _userContext?.ApiName
                    },
                    Payload = new
                    {
                        Data = response?.Data,
                        Return = response?.Return,
                        Timestamp = response?.TimeStamp
                    }
                }
            };
            _eventHubLoggingProvider.Send(JsonSerialize(updatedResponse), updatedResponse);
        }
    }
}
