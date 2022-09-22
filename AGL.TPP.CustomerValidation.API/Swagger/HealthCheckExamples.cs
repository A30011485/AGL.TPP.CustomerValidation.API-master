using AGL.TPP.CustomerValidation.API.Models;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Swagger
{
    public class HealthCheckResponseHealthyExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new HealthCheckResponse
            {
                IsHealthy = true
            };
        }
    }

    public class InternalServerErrorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new InternalServerError
            {
                Message = "Internal Server Error",
                CorrelationId = "b5564401-b5d3-4f2c-8b27-bf5ca78c609f"
            };
        }
    }
}