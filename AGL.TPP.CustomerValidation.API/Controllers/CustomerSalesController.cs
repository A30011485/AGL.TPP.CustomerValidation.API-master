using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Services;
using AGL.TPP.CustomerValidation.API.Swagger;
using AGL.TPP.CustomerValidation.API.Swagger.Examples;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Controllers
{
    /// <summary>
    /// Customer sales controller class
    /// </summary>
    [Authorize]
    public class CustomerSalesController : BaseController
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Customer validation service
        /// </summary>
        private readonly ICustomerValidationService _customerValidationService;
        
        /// <summary>
        /// Residential customer validator
        /// </summary>
        private readonly IValidator<ResidentialCustomerSalesValidationModel> _residentialCustomerSalesValidator;

        /// <summary>
        /// Business customer validator
        /// </summary>
        private readonly IValidator<BusinessCustomerSalesValidationModel> _businessCustomerSalesValidator;

        /// <summary>
        /// Initializes an instance of customer sales controller
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="customerValidationService">Customer validation service</param>
        /// <param name="cancelResidentialCustomerValidator">Residential customer validator</param>
        /// <param name="cancelBusinessCustomerValidator">Business customer validator</param>
        public CustomerSalesController(
            ILogger logger,
            ICustomerValidationService customerValidationService,
            IValidator<ResidentialCustomerSalesValidationModel> residentialCustomerValidator,
            IValidator<BusinessCustomerSalesValidationModel> businessCustomerValidator) : base(logger)
        {
            _logger = logger;
            _customerValidationService = customerValidationService;
            _residentialCustomerSalesValidator = residentialCustomerValidator;
            _businessCustomerSalesValidator = businessCustomerValidator;
        }

        /// <summary>
        /// Validates the residential customer prior to submission
        /// </summary>
        /// <param name="residentialCustomerValidationModel">Residential customer payload</param>
        /// <returns>Returns Residential Customer Validation response</returns>
        [Route("residential/sales")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(ResidentialCustomerSalesValidationModel), typeof(ResidentialCustomerValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateResidentialCustomer([FromBody]ResidentialCustomerSalesValidationModel residentialCustomerValidationModel)
        {
            _logger.Information($"Received request for Residential Customer Validation {DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)}");
            var timer = new Stopwatch();
            timer.Start();
            if (residentialCustomerValidationModel == null)
            {
                return Failure();
            }

            var validationResult = _residentialCustomerSalesValidator.Validate(residentialCustomerValidationModel);
            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            residentialCustomerValidationModel.Payload = FieldMapper.GetMappedPayload(residentialCustomerValidationModel.Payload);

            var response = await _customerValidationService.ValidateResidentialSalesCustomer(residentialCustomerValidationModel);
            timer.Stop();
            _logger.Debug($"Elapsed time: Residential Sale: {residentialCustomerValidationModel.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }

        /// <summary>
        /// Validates the Business customer prior to submission
        /// </summary>
        /// <param name="residentialCustomerModel">Business customer payload</param>
        /// <returns>Returns business customer validation response</returns>
        [Route("business/sales")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(BusinessCustomerSalesValidationModel), typeof(BusinessCustomerValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateBusinessCustomer([FromBody]BusinessCustomerSalesValidationModel businessCustomerModel)
        {
            _logger.Information($"Received request for Business Customer Validation {DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)}");
            var timer = new Stopwatch();
            timer.Start();

            if (businessCustomerModel == null)
            {
                return Failure();
            }

            var validationResult = _businessCustomerSalesValidator.Validate(businessCustomerModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            var response = await _customerValidationService.ValidateBusinessSalesCustomer(businessCustomerModel);
            timer.Stop();
            _logger.Debug($"Elapsed time: Business Sale: {businessCustomerModel.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }
    }
}
