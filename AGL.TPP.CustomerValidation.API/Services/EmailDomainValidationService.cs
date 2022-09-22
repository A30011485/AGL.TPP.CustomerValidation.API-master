
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers;
using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using static AGL.TPP.CustomerValidation.API.Models.Constants;

namespace AGL.TPP.CustomerValidation.API.Services
{
    public class EmailDomainValidationService : IEmailDomainValidationService
    {
        private readonly ILogger _logger;
        private readonly IEmailDomainValidationDataProvider _emailDomainValidationDataProvider;
        private readonly string _authorisedPersonContactEmail = "AuthorisedPersonContact.EmailAddress";
        private readonly string _contactDetailsEmail = "ContactDetail.EmailAddress";
        private readonly string _exceptionErrorMessage = "ValidateEmailDomainAsync error {0}. Proceed with submission if validation is successful otherwise.";

        public IHeaderDictionary Headers { get; set; }

        public EmailDomainValidationService(IEmailDomainValidationDataProvider emailDomainValidationDataProvider, ILogger logger)
        {
            _emailDomainValidationDataProvider = emailDomainValidationDataProvider;
            _logger = logger;
        }

        
        
        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(BusinessCustomerSalesValidationModel validationModel)
        {
            var validationResponse = new CustomerValidationResponse();
            try
            {
                var authorisedPersonEmail = validationModel?.Payload?.AuthorisedPersonContact.EmailAddress;
                if (!string.IsNullOrWhiteSpace(authorisedPersonEmail))
                {
                    var authorisedPersonEmailResponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                        authorisedPersonEmail,
                        validationModel?.Header);

                    validationResponse = MapResponse(
                        authorisedPersonEmailResponse,
                        authorisedPersonEmail, _authorisedPersonContactEmail, validationResponse);
                }

                var contactDetailEmail = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(contactDetailEmail))
                    return validationResponse;

                var contactDetailresponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    contactDetailEmail,
                    validationModel?.Header);

                return MapResponse(
                    contactDetailresponse, contactDetailEmail, _contactDetailsEmail, validationResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
            }

            return validationResponse;
        }


        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(BusinessCustomerMoveOutValidationModel validationModel)
        {
            var validationResponse = new CustomerValidationResponse();
            try
            {
                var authorisedPersonEmail = validationModel?.Payload?.AuthorisedPersonContact.EmailAddress;
                if (!string.IsNullOrWhiteSpace(authorisedPersonEmail))
                {
                    var authorisedPersonEmailResponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                        authorisedPersonEmail,
                        validationModel?.Header);

                    validationResponse = MapResponse(
                        authorisedPersonEmailResponse,
                        authorisedPersonEmail, _authorisedPersonContactEmail, validationResponse);
                }

                var contactDetailEmail = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(contactDetailEmail))
                    return validationResponse;

                var contactDetailresponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    contactDetailEmail,
                    validationModel?.Header);

                return MapResponse(
                    contactDetailresponse, contactDetailEmail, _contactDetailsEmail, validationResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
            }

            return validationResponse;
        }
        
        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(CancellationBusinessCustomerValidationModel validationModel)
        {
            var validationResponse = new CustomerValidationResponse();
            try
            {
                var authorisedPersonEmail = validationModel?.Payload?.AuthorisedPersonContact.EmailAddress;
                if (!string.IsNullOrWhiteSpace(authorisedPersonEmail))
                {
                    var authorisedPersonEmailResponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                        authorisedPersonEmail,
                        validationModel?.Header);

                    validationResponse = MapResponse(
                        authorisedPersonEmailResponse,
                        authorisedPersonEmail, _authorisedPersonContactEmail, validationResponse);
                }

                var contactDetailEmail = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(contactDetailEmail))
                    return validationResponse;

                var contactDetailresponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    contactDetailEmail,
                    validationModel?.Header);

                return MapResponse(
                    contactDetailresponse, contactDetailEmail, _contactDetailsEmail, validationResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
            }

            return validationResponse;
        }
        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(BusinessCustomerChangeValidationModel validationModel)
        {
            var validationResponse = new CustomerValidationResponse();

            try
            {
                var authorisedPersonEmail = validationModel?.Payload?.AuthorisedPersonContact.EmailAddress;
                if (!string.IsNullOrWhiteSpace(authorisedPersonEmail))
                {
                    var authorisedPersonEmailResponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                        authorisedPersonEmail,
                        validationModel?.Header);

                    validationResponse = MapResponse(
                        authorisedPersonEmailResponse, 
                        authorisedPersonEmail, _authorisedPersonContactEmail, validationResponse);
                }

                var contactDetailEmail = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(contactDetailEmail))
                    return validationResponse;

                var contactDetailresponse = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    contactDetailEmail,
                    validationModel?.Header);

                return MapResponse(
                    contactDetailresponse, contactDetailEmail, _contactDetailsEmail, validationResponse);

            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
            }

            return validationResponse;
        }


        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(CancellationResidentialCustomerValidationModel validationModel)
        {
            try
            {
                var email = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(email))
                    return new CustomerValidationResponse();

                var response = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    email,
                    validationModel?.Header);

                return MapResponse(response, email, _contactDetailsEmail, null);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
                return new CustomerValidationResponse();
            }
        }

        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(ResidentialCustomerChangeValidationModel validationModel)
        {
            try
            {
                var email = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(email))
                    return new CustomerValidationResponse();

                var response = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    email,
                    validationModel?.Header);

                return MapResponse(response, email, _contactDetailsEmail, null);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
                return new CustomerValidationResponse();
            }
        }

        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(ResidentialCustomerMoveOutValidationModel validationModel)
        {
            try
            {
                var email = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(email))
                    return new CustomerValidationResponse();

                var response = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    email,
                    validationModel?.Header);

                return MapResponse(response, email, _contactDetailsEmail, null);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
                return new CustomerValidationResponse();
            }
        }

        public async Task<CustomerValidationResponse> ValidateEmailDomainAsync(ResidentialCustomerSalesValidationModel validationModel)
        {
            try
            {
                var email = validationModel?.Payload?.ContactDetail.EmailAddress;
                if (string.IsNullOrWhiteSpace(email))
                    return new CustomerValidationResponse();

                var response = await _emailDomainValidationDataProvider.ValidateEmailDomainAsync(
                    email, 
                    validationModel?.Header);

                return MapResponse(response, email, _contactDetailsEmail, null);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format(_exceptionErrorMessage, ex));
                return new CustomerValidationResponse();
            }
        }

        private CustomerValidationResponse MapResponse(
            EmailDomainValidationClientResponse domainValidationClientResponse, 
            string emailAddress, 
            string field,
            CustomerValidationResponse existingResponse)
        {
            var fieldLevelErrors = new List<FieldLevelError>();

            if (!domainValidationClientResponse.IsSuccess)
            {
                var error = new FieldLevelError();

                switch (domainValidationClientResponse.Code)
                {
                    case "DomainNameValidationError":
                        error.Code = ApiErrorMessages.DomainValidationInvalidDomain.Code;
                        error.Field = field;
                        error.Message = string.Format( ApiErrorMessages.DomainValidationInvalidDomain.Message, emailAddress);
                        fieldLevelErrors.Add(error);
                        break;
                    case "BadRequest":
                        error.Code = ApiErrorMessages.DomainValidationEmptyDomain.Code;
                        error.Field = field;
                        error.Message = ApiErrorMessages.DomainValidationEmptyDomain.Message;
                        fieldLevelErrors.Add(error);
                        break;
                    default:
                        // we don't want to add any other errors to be returned to the gateway
                        break;
                }
            }

            if (existingResponse == null)
            {
                var response = new CustomerValidationResponse
                {
                    //Code = ""; // we ignore code, message
                    Errors = domainValidationClientResponse.IsSuccess ? new List<FieldLevelError>() : fieldLevelErrors
                };

                return response;
            }
            else
            {
                existingResponse.Errors.AddRange(fieldLevelErrors);
                return existingResponse;
            }
           
        }

    }

}
