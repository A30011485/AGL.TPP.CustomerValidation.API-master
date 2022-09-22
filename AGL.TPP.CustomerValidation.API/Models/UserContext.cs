using System;
using AGL.TPP.CustomerValidation.API.Extensions.Web;
using Microsoft.AspNetCore.Http;

namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// User context class
    /// </summary>
    public class UserContext : IUserContext
    {
        /// <summary>
        /// Http context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// Initialized a new instance of <cref name="UserContext"></cref> class
        /// </summary>
        /// <param name="httpContext"></param>
        public UserContext(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }

        /// <summary>
        /// Correlation id
        /// </summary>
        public string CorrelationId => _httpContext.HttpContext.Request?.GetRequestHeaderOrDefault(Constants.CorrelationKey, $"GEN-{Guid.NewGuid().ToString()}");

        public string ApiName => _httpContext.HttpContext.Request?.GetRequestHeaderOrDefault(Constants.ApiName, EnvironmentConstants.EndpointSourceCustomerValidation);
    }
}
