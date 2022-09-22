using System;
using AGL.TPP.CustomerValidation.API.Models;
using Microsoft.Extensions.Options;
using Serilog;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient;
using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using AGL.TPP.CustomerValidation.API.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AGL.TPP.CustomerValidation.API.Providers
{
    public class EmailDomainValidationDataProvider: IEmailDomainValidationDataProvider
    {
        private readonly ILogger _logger;
        private readonly IOptions<EmailDomainValidationApiSettings> _domainValidationApiSettings;
        private readonly IEmailDomainValidationClient _emailDomainValidationClient;
        private readonly IUserContext _userContext;
        private readonly IEventHubLoggingProvider _eventHubLoggingProvider;

        public EmailDomainValidationDataProvider(
            IEmailDomainValidationClient emailDomainValidationClient,
            IOptions<EmailDomainValidationApiSettings> domainValidationApiSettings,
            IEventHubLoggingProvider eventHubLoggingProvider,
            IUserContext userContext,
            ILogger logger)
        {
            _emailDomainValidationClient = emailDomainValidationClient;
            _domainValidationApiSettings = domainValidationApiSettings;
            _eventHubLoggingProvider = eventHubLoggingProvider;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, CancellationCustomerValidationHeaderModel customerValidationHeaderModel)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return new EmailDomainValidationClientResponse();

            var endpoint = _domainValidationApiSettings.Value?.Endpoint;
            var domain = GetEmailDomain(emailAddress);

            _logger.Debug("Preparing to call domain validation API {ApiEndPoint} for domain {DomainInput}",
                endpoint, domain);

            var response = await _emailDomainValidationClient.ValidateDomainAsync(domain, _userContext.CorrelationId);

            var eventHubHeader = new EventHubHeader
            {
                VendorName = customerValidationHeaderModel?.VendorName,
                VendorBusinessPartnerNumber = customerValidationHeaderModel?.VendorBusinessPartnerNumber,
                VendorLeadId = customerValidationHeaderModel?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = customerValidationHeaderModel?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        public async Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, ChangeCustomerValidationHeaderModel customerValidationHeaderModel)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return new EmailDomainValidationClientResponse();

            var endpoint = _domainValidationApiSettings.Value?.Endpoint;
            var domain = GetEmailDomain(emailAddress);

            _logger.Debug("Preparing to call domain validation API {ApiEndPoint} for domain {DomainInput}",
                endpoint, domain);

            var response = await _emailDomainValidationClient.ValidateDomainAsync(domain, _userContext.CorrelationId);

            var eventHubHeader = new EventHubHeader
            {
                VendorName = customerValidationHeaderModel?.VendorName,
                VendorBusinessPartnerNumber = customerValidationHeaderModel?.VendorBusinessPartnerNumber,
                VendorLeadId = customerValidationHeaderModel?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = customerValidationHeaderModel?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }
        public async Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, MoveOutCustomerValidationHeaderModel customerValidationHeaderModel)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return new EmailDomainValidationClientResponse();

            var endpoint = _domainValidationApiSettings.Value?.Endpoint;
            var domain = GetEmailDomain(emailAddress);

            _logger.Debug("Preparing to call domain validation API {ApiEndPoint} for domain {DomainInput}",
                endpoint, domain);

            var response = await _emailDomainValidationClient.ValidateDomainAsync(domain, _userContext.CorrelationId);

            var eventHubHeader = new EventHubHeader
            {
                VendorName = customerValidationHeaderModel?.VendorName,
                VendorBusinessPartnerNumber = customerValidationHeaderModel?.VendorBusinessPartnerNumber,
                VendorLeadId = customerValidationHeaderModel?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = customerValidationHeaderModel?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        public async Task<EmailDomainValidationClientResponse> ValidateEmailDomainAsync(string emailAddress, CustomerValidationHeaderModel customerValidationHeaderModel)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return new EmailDomainValidationClientResponse();
            
            var endpoint = _domainValidationApiSettings.Value?.Endpoint;
            var domain = GetEmailDomain(emailAddress); 

            _logger.Debug("Preparing to call domain validation API {ApiEndPoint} for domain {DomainInput}", 
                endpoint, domain);

            var response = await _emailDomainValidationClient.ValidateDomainAsync(domain, _userContext.CorrelationId);

            var eventHubHeader = new EventHubHeader
            {
                VendorName = customerValidationHeaderModel?.VendorName,
                VendorBusinessPartnerNumber = customerValidationHeaderModel?.VendorBusinessPartnerNumber,
                VendorLeadId = customerValidationHeaderModel?.VendorLeadId,
                CorrelationId = _userContext?.CorrelationId,
                TransactionType = customerValidationHeaderModel?.TransactionType
            };

            SendToEventHub(eventHubHeader, response);

            return response;
        }

        private string GetEmailDomain(string emailAddress)
        {
            return emailAddress.Split('@')[1];
        }

        private void SendToEventHub(EventHubHeader eventHubHeader, EmailDomainValidationClientResponse response)
        {
            var updatedResponse = new
            {
                MessageType = response.IsSuccess ? Constants.ThirdPartyApiSuccess : Constants.ThirdPartyApiError,
                Timestamp = GetTimestamp(),
                MessageBody = new
                {
                    Header = new
                    {
                        eventHubHeader?.VendorName,
                        eventHubHeader?.VendorBusinessPartnerNumber,
                        eventHubHeader?.VendorLeadId,
                        _userContext?.CorrelationId,
                        eventHubHeader?.TransactionType,
                        ApiName = $"{_userContext?.ApiName}_DomainValidation"
                    },
                    Payload = new
                    {
                        response.Code,
                        response.Message,
                        Errors = ""
                    }
                }
            };
            _eventHubLoggingProvider.Send(JsonSerialize(updatedResponse), updatedResponse);
        }

        private string JsonSerialize(object model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }
        private string GetTimestamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
