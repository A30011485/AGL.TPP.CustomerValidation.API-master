using System;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Monitors the health of the application
    /// </summary>
    public class HealthCheckService : IHealthCheckService
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
        /// Initializes a new instance of <cref name="HealthCheckService"></cref> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="salesOrderDataProvider">Sales order data provider</param>
        public HealthCheckService(
            ILogger logger,
            ICustomerValidationDataProvider salesOrderDataProvider)
        {
            _logger = logger;
            _salesOrderDataProvider = salesOrderDataProvider;
        }

        /// <summary>
        /// Performs basic health check
        /// </summary>
        /// <returns>Returns a boolean indicating true or false</returns>
        public async Task<HealthCheckResponse> PerformBasicCheck()
        {
            try
            {
                return new HealthCheckResponse { IsHealthy = await _salesOrderDataProvider.Ping() };
            }
            catch (Exception ex)
            {
                _logger.Error("HealthCheck error {@exception}", ex);
                return new HealthCheckResponse { IsHealthy = false };
            }
        }
    }
}
