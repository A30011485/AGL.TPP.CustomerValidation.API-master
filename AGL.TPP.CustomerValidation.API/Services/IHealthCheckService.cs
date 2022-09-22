using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;

namespace AGL.TPP.CustomerValidation.API.Services
{
    public interface IHealthCheckService
    {
        /// <summary>
        /// Performs basic health check
        /// </summary>
        /// <returns>Returns a boolean indicating true or false</returns>
        Task<HealthCheckResponse> PerformBasicCheck();
    }
}