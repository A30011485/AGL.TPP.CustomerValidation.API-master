using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AGL.TPP.CustomerValidation.API.Swagger
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var headers = context.ControllerActionDescriptor.GetControllerAndActionAttributes(true)
                .OfType<SwaggerHeaderAttribute>().ToList();

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            if (context.ApiDescription.ParameterDescriptions.Any(x => x.ModelMetadata.ContainerType == typeof(IFormFile)))
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "FilePayload", // must match parameter name from controller method
                    In = "formData",
                    Description = "Upload file.",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("application/form-data");
            }

            foreach (var header in headers)
            {
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = header.Name,
                    In = "header",
                    Type = header.Type,
                    Description = header.Description,
                    Required = header.Required
                });
            }
        }
    }
}