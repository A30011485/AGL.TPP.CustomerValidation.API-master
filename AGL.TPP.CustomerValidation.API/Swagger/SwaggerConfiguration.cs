using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AGL.TPP.CustomerValidation.API.Swagger
{
    internal class SwaggerConfiguration
    {
        private const string DefaultScheme = "https";
        private const string DefaultBasePath = "/";
        private const string DefaultEnvironmentName = "";
        private readonly string _apiDescription;
        private static string _apiHost;

        private readonly string _apiName;
        private readonly string _apiVersion;

        public SwaggerConfiguration(string apiName, string apiVersion, string apiDescription, string apiHost)
        {
            _apiName = apiName;
            _apiVersion = apiVersion;
            _apiDescription = apiDescription;
            _apiHost = apiHost;
        }

        public static string ExtractApiNameFromEnvironmentVariable()
        {
            var environmentName = Environment.GetEnvironmentVariable(EnvironmentConstants.SwaggerEnvironmentName) ??
                                  DefaultEnvironmentName;
            var apiName = $"Third Party Customer Validation API {environmentName}".Trim();
            return apiName;
        }

        public static void SetupSwaggerOptions(SwaggerOptions options)
        {
            var scheme = Environment.GetEnvironmentVariable(EnvironmentConstants.SwaggerScheme)?.ToLowerInvariant() ??
                         DefaultScheme;
            options.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Schemes = new[] { scheme };

                swagger.Host = _apiHost;

                // Set base path if found in environment variables, or default to "/"
                swagger.BasePath = Environment.GetEnvironmentVariable(EnvironmentConstants.SwaggerApiBasePath) ??
                                   DefaultBasePath;
            });
        }

        public void SetupSwaggerGenService(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();

            options.SwaggerDoc(
                _apiVersion,
                new Info
                {
                    Title = _apiName,
                    Version = _apiVersion,
                    Description = _apiDescription
                });

            // add Swashbuckle.Examples handling
            options.OperationFilter<ExamplesOperationFilter>();

            options.OperationFilter<SwaggerHeaderFilter>();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "AGL.TPP.CustomerValidation.API.xml");
            options.IncludeXmlComments(filePath);
        }

        public void SetupSwaggerUiOptions(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", _apiName);
        }
    }
}
