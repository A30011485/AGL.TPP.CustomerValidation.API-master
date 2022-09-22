using System.Net;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Extensions.Web;
using AGL.TPP.CustomerValidation.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AGL.TPP.CustomerValidation.API.Middlewares
{
    public class StatusClientKeyChecker
    {
        private readonly RequestDelegate _next;

        private static readonly CustomerValidationResponse ResponseError = new CustomerValidationResponse(Constants.ErrorCode.InvalidStatusCheckKey.ToString(),
            Constants.ErrorMessages.StatusClientKeyHeaderInvalid);

        private static string _expectedStatusKey;
        private static string _headerName;

        public StatusClientKeyChecker(RequestDelegate next, IConfiguration configuration)
        {
            _headerName = configuration["StatusKeyHeaderName"];
            _expectedStatusKey = configuration["StatusClientKey"];
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var statusClientKey = context.Request?.GetRequestHeaderOrDefault(_headerName);

            if (statusClientKey == null || statusClientKey != _expectedStatusKey)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseError));
                return;
            }

            await _next.Invoke(context);
        }
    }
}
