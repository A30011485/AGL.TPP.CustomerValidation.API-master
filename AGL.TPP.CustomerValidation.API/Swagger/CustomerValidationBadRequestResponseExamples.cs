using AGL.TPP.CustomerValidation.API.Models;
using Swashbuckle.AspNetCore.Examples;
using System.Collections.Generic;

namespace AGL.TPP.CustomerValidation.API.Swagger
{
    public class CustomerValidationBadRequestResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CustomerValidationResponse
            {
                Code = "400",
                Message = "BadRequest",
                Errors = new List<FieldLevelError>
                {
                    new FieldLevelError
                    {
                        Code = null,
                        Field = "Header.VendorLeadId",
                        Message = "Please provide a value for Vendor Lead ID."
                    }
                }
            };
        }
    }
}