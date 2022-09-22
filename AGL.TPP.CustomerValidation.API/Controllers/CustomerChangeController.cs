using System;
using System.Diagnostics;
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
    /// Customer change controller class
    /// </summary>
    [Authorize]
    public class CustomerChangeController : BaseController
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
        /// Residential customer change validator
        /// </summary>
        private readonly IValidator<ResidentialCustomerChangeValidationModel> _residentialCustomerChangeValidator;

        /// <summary>
        /// Business customer change validator
        /// </summary>
        private readonly IValidator<BusinessCustomerChangeValidationModel> _businessCustomerChangeValidator;

        /// <summary>
        /// Initializes an instance of customer sales controller
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="customerValidationService">Customer validation service</param>
        /// <param name="cancelResidentialCustomerValidator">Residential customer validator</param>
        /// <param name="cancelBusinessCustomerValidator">Business customer validator</param>
        public CustomerChangeController(
            ILogger logger,
            ICustomerValidationService customerValidationService,
            IValidator<ResidentialCustomerChangeValidationModel> residentialCustomerChangeValidator,
            IValidator<BusinessCustomerChangeValidationModel> businessCustomerChangeValidator) : base(logger)
        {
            _logger = logger;
            _customerValidationService = customerValidationService;
            _residentialCustomerChangeValidator = residentialCustomerChangeValidator;
            _businessCustomerChangeValidator = businessCustomerChangeValidator;
        }

        /// <summary>
        /// Validates the residential customer change prior to submission
        /// </summary>
        /// <param name="residentialCustomerChangeValidationModel">Residential customer change payload</param>
        /// <returns>Returns Residential Customer Validation response</returns>
        [Route("residential/change")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(ResidentialCustomerChangeValidationModel), typeof(ResidentialCustomerChangeValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateResidentialCustomer([FromBody]ResidentialCustomerChangeValidationModel residentialCustomerChangeValidationModel)
        {
            _logger.Information($"Received request for Residential Customer Change Validation {DateTime.Now.ToLocalTime().ToString()}");
            var timer = new Stopwatch();
            timer.Start();

            if (residentialCustomerChangeValidationModel == null)
            {
                return Failure();
            }

            var validationResult = _residentialCustomerChangeValidator.Validate(residentialCustomerChangeValidationModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            residentialCustomerChangeValidationModel.Payload = FieldMapper.GetMappedPayload(residentialCustomerChangeValidationModel.Payload);

            var response = await _customerValidationService.ValidateResidentialChangeCustomer(residentialCustomerChangeValidationModel);

            timer.Stop();
            _logger.Debug($"Elapsed time: Residential Change: {residentialCustomerChangeValidationModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }

        /// <summary>
        /// Validates the Business customer change prior to submission
        /// </summary>
        /// <param name="businessCustomerChangeValidationModel">Business customer change payload</param>
        /// <returns>Returns business customer validation response</returns>
        [Route("business/change")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(BusinessCustomerChangeValidationModel), typeof(BusinessCustomerChangeValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateBusinessCustomer([FromBody]BusinessCustomerChangeValidationModel businessCustomerChangeValidationModel)
        {
            _logger.Information($"Received request for Business Customer Change Validation {DateTime.Now.ToLocalTime().ToString()}");

            var timer = new Stopwatch();
            timer.Start();
            if (businessCustomerChangeValidationModel == null)
            {
                return Failure();
            }

            var validationResult = _businessCustomerChangeValidator.Validate(businessCustomerChangeValidationModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            var response = await _customerValidationService.ValidateBusinessChangeCustomer(businessCustomerChangeValidationModel);

            timer.Stop();
            _logger.Debug($"Elapsed time: Business Change: {businessCustomerChangeValidationModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }
    }
}
