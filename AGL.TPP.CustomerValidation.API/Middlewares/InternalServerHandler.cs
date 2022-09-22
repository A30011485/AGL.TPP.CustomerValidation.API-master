using System;
using System.IO;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Config;
using AGL.TPP.CustomerValidation.API.Extensions.Web;
using AGL.TPP.CustomerValidation.API.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Middlewares
{
    /// <summary>
    /// Unhandled exception handler
    /// </summary>
    public class InternalServerHandler
    {
        /// <summary>
        /// Request delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes an instance of Internal Server Handler class
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public InternalServerHandler(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the next request in the pipeline
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error occurred on customer validation API");
                using (var writer = new StreamWriter(context.Response.Body))
                {
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await writer.WriteAsync(
                        JsonConvert.SerializeObject(new InternalServerError
                        {
                            CorrelationId = GetOrGenerateCorrelationId(context),
                            Code = "500",
                            Message = "An exception has occured."
                        }, Formatting.None, settings)
                    );
                }
            }
        }

        /// <summary>
        /// Gets or generates correlation id
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>Returns correlation id</returns>
        private string GetOrGenerateCorrelationId(HttpContext context) => context?.Request?.GetRequestHeaderOrDefault(AppConfiguration.CorrelationKey, $"GEN-{Guid.NewGuid().ToString()}");
    }
}
