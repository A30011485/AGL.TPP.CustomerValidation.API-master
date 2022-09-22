using AGL.TPP.CustomerValidation.API.Models;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Swagger
{
    public class CustomerValidationSuccessResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CustomerValidationResponse
            {
                Code = "200",
                Message = "Success",
                Errors = null
            };
        }
    }
}