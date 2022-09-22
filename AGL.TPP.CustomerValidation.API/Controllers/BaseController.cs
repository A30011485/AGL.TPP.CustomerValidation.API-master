using System.Collections.Generic;
using System.Net;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        public BaseController() { }

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult OkOrFailure(CustomerValidationResponse response, HttpStatusCode successCode = HttpStatusCode.OK)
        {
            switch (response.Code)
            {
                case "200":
                    return Ok(response);
                    break;
                case "400":
                    return BadRequest(response);
                    break;
                default:
                    return BadRequest(response);
            }
        }

        protected IActionResult Failure(ValidationResult result, HttpStatusCode successCode = HttpStatusCode.OK)
        {
            _logger.Error($"Bad request: {string.Join(";", result.GetErrorMessages()) }");

            return BadRequest(new CustomerValidationResponse
            {
                Code = "400",
                Message = "BadRequest",
                Errors = result.GetFieldLevelErrors()
            });
        }

        protected IActionResult Failure()
        {
            _logger.Error("Bad request. Empty payload.");

            return BadRequest(new CustomerValidationResponse
            {
                Code = "400",
                Message = "BadRequest",
                Errors = new List<FieldLevelError>
                {
                    new FieldLevelError
                    {
                        Code = null,
                        Field = null,
                        Message = "The payload is empty."
                    }
                }
            });
        }
    }
}
