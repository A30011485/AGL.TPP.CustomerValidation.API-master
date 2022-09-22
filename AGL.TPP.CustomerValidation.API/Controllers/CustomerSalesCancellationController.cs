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
    /// Customer sales cancellation controller
    /// </summary>
    [Authorize]
    public class CustomerSalesCancellationController : BaseController
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
        /// Residential customer cancellation validator
        /// </summary>
        private readonly IValidator<CancellationResidentialCustomerValidationModel> _cancelResidentialCustomerValidator;

        /// <summary>
        /// Business customer cancellation validator
        /// </summary>
        private readonly IValidator<CancellationBusinessCustomerValidationModel> _cancelBusinessCustomerValidator;

        /// <summary>
        /// Initializes an instance of customer sales cancellation controller
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="customerValidationService">Customer validation service</param>
        /// <param name="cancelResidentialCustomerValidator">Residential customer cancellation validator</param>
        /// <param name="cancelBusinessCustomerValidator">Business customer cancellation validator</param>
        public CustomerSalesCancellationController(
            ILogger logger,
            ICustomerValidationService customerValidationService,
            IValidator<CancellationResidentialCustomerValidationModel> cancelResidentialCustomerValidator,
            IValidator<CancellationBusinessCustomerValidationModel> cancelBusinessCustomerValidator) : base(logger)
        {
            _logger = logger;
            _customerValidationService = customerValidationService;
            _cancelResidentialCustomerValidator = cancelResidentialCustomerValidator;
            _cancelBusinessCustomerValidator = cancelBusinessCustomerValidator;
        }

        /// <summary>
        /// Submits the cancellation request for Residential customer
        /// </summary>
        /// <param name="residentialCustomerCancellationModel">Residential customer cancellation payload</param>
        /// <returns>Returns residential customer cancellation response</returns>
        [Route("residential/cancellation")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(CancellationResidentialCustomerValidationModel), typeof(ResidentialCustomerCancellationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> ResidentialCustomerCancellation([FromBody] CancellationResidentialCustomerValidationModel residentialCustomerCancellationModel)
        {
            _logger.Debug($"Received Cancellation request for Residential Customer {DateTime.Now.ToLocalTime().ToString()}");
            var timer = new Stopwatch();
            timer.Start();
            if (residentialCustomerCancellationModel == null)
            {
                return Failure();
            }

            var validationResult = _cancelResidentialCustomerValidator.Validate(residentialCustomerCancellationModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            residentialCustomerCancellationModel.Payload = FieldMapper.GetMappedPayload(residentialCustomerCancellationModel.Payload);

            var response = await _customerValidationService.ValidateResidentialCustomerCancellation(residentialCustomerCancellationModel);
            timer.Stop();
            _logger.Debug($"Elapsed time: Residential Cancel: {residentialCustomerCancellationModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }

        /// <summary>
        /// Submits the cancellation request for Business customer
        /// </summary>
        /// <param name="businessCustomerCancellationModel">Business customer cancellation payload</param>
        /// <returns>Returns business customer cancellation response</returns>
        [Route("business/cancellation")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerHeader("Authorization", "string", Description = "Authorization Bearer token for secure endpoints. Value shall be in the format 'Bearer X' where X is the OAuth2 token issued by Auth0 for Builders or AD for AGL staff.", Required = true)]
        [SwaggerHeader("Correlation-Id", "string", Description = "GUID or equivalent that can be used to uniquely identify any request.", Required = false)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(CancellationBusinessCustomerValidationModel), typeof(BusinessCustomerCancellationExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CustomerValidationSuccessResponseExample))]
        // 400 Response, BadRequest
        [ProducesResponseType(typeof(CustomerValidationResponse), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CustomerValidationBadRequestResponseExample))]
        // 500 Response
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> BusinessCustomerCancellation([FromBody] CancellationBusinessCustomerValidationModel businessCustomerCancellationModel)
        {
            _logger.Debug($"Received Cancellation request for Business Customer {DateTime.Now.ToLocalTime().ToString()}");

            var timer = new Stopwatch();
            timer.Start();
            if (businessCustomerCancellationModel == null)
            {
                return Failure();
            }

            var validationResult = _cancelBusinessCustomerValidator.Validate(businessCustomerCancellationModel);

            if (!validationResult.IsValid)
            {
                return Failure(validationResult);
            }

            var response = await _customerValidationService.ValidateBusinessCustomerCancellation(businessCustomerCancellationModel);

            timer.Stop();
            _logger.Debug($"Elapsed time: Business Cancel: {businessCustomerCancellationModel?.Header?.VendorLeadId}: {timer.ElapsedMilliseconds}");
            return OkOrFailure(response);
        }
    }
}
