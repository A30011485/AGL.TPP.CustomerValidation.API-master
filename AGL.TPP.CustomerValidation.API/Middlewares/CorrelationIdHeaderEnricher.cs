using System;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Config;
using AGL.TPP.CustomerValidation.API.Extensions.Web;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace AGL.TPP.CustomerValidation.API.Middlewares
{
    public class CorrelationIdHeaderEnricher
    {
        private const string CorrelationLogPropertyName = "CorrelationId";

        private readonly RequestDelegate _next;

        public CorrelationIdHeaderEnricher(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = GetOrGenerateCorrelationId(context);
            using (LogContext.PushProperty(CorrelationLogPropertyName, correlationId))
            using (LogContext.PushProperty("ThreadId", Environment.CurrentManagedThreadId))
            {
                context.Response.Headers.Add("Correlation-Id", correlationId);
                await _next.Invoke(context);
            }
        }

        private string GetOrGenerateCorrelationId(HttpContext context) => context?.Request?.GetRequestHeaderOrDefault(AppConfiguration.CorrelationKey, $"GEN-{Guid.NewGuid().ToString()}");
    }
}
