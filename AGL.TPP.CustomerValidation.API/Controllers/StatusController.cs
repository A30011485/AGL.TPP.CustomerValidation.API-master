using System.Net;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Services;
using AGL.TPP.CustomerValidation.API.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Controllers
{
    /// <summary>
    /// Status controller class
    /// </summary>
    [Route("status")]
    public class StatusController : Controller
    {
        /// <summary>
        /// Health check service
        /// </summary>
        private readonly IHealthCheckService _healthCheckService;

        public StatusController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        ///     Health check endpoint.
        /// </summary>
        /// <response code="200">
        ///     The request was processed successfully. The response contains the health status.
        /// </response>
        /// <response code="500">
        ///     Internal server error. The health check failed.
        /// </response>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [SwaggerHeader("Status-Client-Key", "string", Description = "Api Key to be sent along with this diagnostic endpoint.", Required = true)]
        // 200 Response, Healthy
        [ProducesResponseType(typeof(HealthCheckResponse), (int)HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(HealthCheckResponseHealthyExample))]
        // 500 Response
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _healthCheckService.PerformBasicCheck();
            return new OkObjectResult(result);
        }
    }
}
