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
    /// Customer move out controller class
    /// </summary>
    [Authorize]
    public class CustomerMoveOutController : BaseController
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
        /// Residential customer move out validator
        /// </summary>
        private readonly IValidator<ResidentialCustomerMoveOutValidationModel> _residentialCustomerMoveOutValidator;

        /// <summary>
        /// Business customer move out validator
        /// </summary>
        private readonly IValidator<BusinessCustomerMoveOutValidationModel> _businessCustomerMoveOutValidator;

        /// <summary>
        /// Initializes an instance of customer sales controller
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="customerValidationService">Customer validation service</param>
        /// <param name="cancelResidentialCustomerValidator">Residential customer validator</param>
        /// <param name="cancelBusinessCustomerValidator">Business customer validator</param>
        public CustomerMoveOutController(
            ILogger logger,
            ICustomerValidationService customerValidationService,
            IValidator<ResidentialCustomerMoveOutValidationModel> residentialCustomerMoveOutValidator,
            IValidator<BusinessCustomerMoveOutValidationModel> businessCustomerMoveOutValidator) : base(logger)
        {
            _logger = logger;
            _customerValidationService = customerValidationService;
            _residentialCustomerMoveOutValidator = residentialCustomerMoveOutValidator;
            _businessCustomerMoveOutValidator = businessCustomerMoveOutValidator;
        }

        /// <summary>
        /// Validates the residential customer move out prior to submission
        /// </summary>
        /// <param name="residentialCustomerMoveOutValidationModel">Residential customer move out payload</param>
        /// <returns>Returns Residential Customer Validation response</returns>
        [Route("residential/moveOut")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(ResidentialCustomerMoveOutValidationModel), typeof(ResidentialCustomerMoveOutValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateResidentialCustomer([FromBody]ResidentialCustomerMoveOutValidationModel residentialCustomerMoveOutValidationModel)
        {
            _logger.Information($"Received request for Residential Customer Move Out Validation {DateTime.Now.ToLocalTime().ToString()}");

            var timer = new Stopwatch();
            timer.Start();
            if (residentialCustomerMoveOutValidationModel == null)
            {
                return Failure();
            }

            var validationResult = _residentialCustomerMoveOutValidator.Validate(residentialCustomerMoveOutValidationModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            residentialCustomerMoveOutValidationModel.Payload = FieldMapper.GetMappedPayload(residentialCustomerMoveOutValidationModel.Payload);

            var response = await _customerValidationService.ValidateResidentialMoveOutCustomer(residentialCustomerMoveOutValidationModel);

            timer.Stop();
            _logger.Debug($"Elapsed time: Residential Move Out: {residentialCustomerMoveOutValidationModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }

        /// <summary>
        /// Validates the Business customer move out prior to submission
        /// </summary>
        /// <param name="residentialCustomerModel">Business customer move out payload</param>
        /// <returns>Returns business customer validation response</returns>
        [Route("business/moveOut")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(BusinessCustomerMoveOutValidationModel), typeof(BusinessCustomerMoveOutValidationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ValidateBusinessCustomer([FromBody]BusinessCustomerMoveOutValidationModel businessCustomerMoveOutModel)
        {
            _logger.Information($"Received request for Business Customer Move Out Validation {DateTime.Now.ToLocalTime().ToString()}");

            var timer = new Stopwatch();
            timer.Start();
            if (businessCustomerMoveOutModel == null)
            {
                return Failure();
            }

            var validationResult = _businessCustomerMoveOutValidator.Validate(businessCustomerMoveOutModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            var response = await _customerValidationService.ValidateBusinessMoveOutCustomer(businessCustomerMoveOutModel);

            timer.Stop();
            _logger.Debug($"Elapsed time: Business Move Out: {businessCustomerMoveOutModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }
    }
}
