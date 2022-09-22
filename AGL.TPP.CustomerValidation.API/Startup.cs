using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Config;
using AGL.TPP.CustomerValidation.API.Filters;
using AGL.TPP.CustomerValidation.API.Middlewares;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Models.Repository;
using AGL.TPP.CustomerValidation.API.Providers;
using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient;
using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using AGL.TPP.CustomerValidation.API.Providers.EventHub.Models;
using AGL.TPP.CustomerValidation.API.Providers.SapClient;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using AGL.TPP.CustomerValidation.API.Security;
using AGL.TPP.CustomerValidation.API.Services;
using AGL.TPP.CustomerValidation.API.Storage.Services;
using AGL.TPP.CustomerValidation.API.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AGL.TPP.CustomerValidation.API
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Version of Api
        /// </summary>
        private const string ApiVersion = "v1";

        /// <summary>
        /// Swagger configuration
        /// </summary>
        private readonly SwaggerConfiguration _swaggerConfiguration;

        /// <summary>
        /// Application configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Custom Application configuration
        /// </summary>
        private readonly IAppConfiguration _appConfiguration;

        /// <summary>
        /// Initializes an instance of <cref name="Startup"></cref> class
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            // Initialize swagger setup values.
            var apiName = SwaggerConfiguration.ExtractApiNameFromEnvironmentVariable();
            var apiDescription = configuration["ApiDescription"];
            var apiHost = configuration["ApiOriginHost"];
            _swaggerConfiguration = new SwaggerConfiguration(apiName, ApiVersion, apiDescription, apiHost);
            _appConfiguration = configuration.GetSection("AppConfiguration").Get<AppConfiguration>();
        }

        /// <summary>
        /// Middleware configuration
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var protectedEndpoints = new[] { "status" };

            app.UseClientCertMiddleware()
               .UseAuthentication()
               .UseMiddleware<CorrelationIdHeaderEnricher>()
               .UseMiddleware<InternalServerHandler>()
               .UseWhen(context => protectedEndpoints.Any(endPoint => context.Request.Path.Value.Contains(endPoint, StringComparison.OrdinalIgnoreCase)), appBuilder =>
               {
                   appBuilder.UseMiddleware<StatusClientKeyChecker>();
               })
               .UseSwagger(SwaggerConfiguration.SetupSwaggerOptions)
               .UseSwaggerUI(_swaggerConfiguration.SetupSwaggerUiOptions)
               .UseMvc();
        }

        /// <summary>
        /// Configuration of Dependency Injection services
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var issuer = _configuration["Auth:Issuer"];
            var audiences = _configuration["Auth:Audiences"];
            var logger = Logging.GetLogger(_configuration);

            services.Configure<SapPiSettings>(options =>
            {
                var settings = GetSapSettings().Get<SapPiSettings>();
                options.CustomerValidationEndpoint = settings.CustomerValidationEndpoint;
                options.Host = settings.Host;
                options.Port = settings.Port;
                options.UserId = settings.UserId;
                options.Password = settings.Password;
                options.Timeout = settings.Timeout;
            });

            services.Configure<AzureCloudSettings>(GetAzureConnectionSettings());
            services.Configure<EmailDomainValidationApiSettings>(GetEmailDomainValidationApiKeys());

            services.Configure<EventHubSettings>(options =>
            {
                var settings = GetEventHubSettings().Get<EventHubSettings>();
                options.ConnectionString = settings.ConnectionString;
                options.Name = settings.Name;
                options.Enabled = settings.Enabled;
            });

            services
                .AddHttpClient<IEmailDomainValidationClient, EmailDomainValidationClient>();

            services
                .AddScoped<IHealthCheckService, HealthCheckService>()
                .AddScoped<IEmailDomainValidationService, EmailDomainValidationService>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<ISapPiClientSettings, SapPiSecureSettings>()
                .AddSingleton<ISapPiClientMultipleMessage, SapPiClientMultipleMessage>()
                .AddSingleton<IHttpClient, SapPiHttpClient>()
                .AddScoped<ICustomerValidationService, CustomerValidationService>()
                .AddSingleton<IEventHubLoggingService, EventHubLoggingService>()
                .AddScoped<ICustomerValidationDataProvider, CustomerValidationDataProvider>()
                .AddScoped<IEmailDomainValidationDataProvider, EmailDomainValidationDataProvider>()
                .AddSingleton<IEventHubLoggingProvider, EventHubLoggingProvider>()
                .AddSingleton<ITableRepository<CustomerValidationData>, TableRepository<CustomerValidationData>>()
                .AddSingleton(_configuration)
                .AddSingleton(_appConfiguration)
                .Configure<CertificateValidationOptions>(_configuration.GetSection("CertificateValidation"))
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = issuer;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudiences = audiences.Split(';')
                    };
                });

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>()
                    .AddSingleton<ISapErrorMessageService, SapErrorMessageService>();

            services
            .AddSwaggerGen(_swaggerConfiguration.SetupSwaggerGenService)
            .AddSingleton(logger)
            .AddMvc()
            .AddMvcOptions(options => options.Filters.Add(new EventHubLoggingFilter()))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };

                    opts.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
        }

        /// <summary>
        /// Gets Sap pi settings
        /// </summary>
        /// <returns>Returns an instance of IConfiguration</returns>
        private IConfiguration GetSapSettings()
        {
            return _configuration.GetSection("SapPiSettings");
        }

        /// <summary>
        /// Gets Azure connection settings
        /// </summary>
        /// <returns>Returns an instance of IConfiguration</returns>
        private IConfiguration GetAzureConnectionSettings()
        {
            return _configuration.GetSection(nameof(AzureCloudSettings));
        }

        /// <summary>
        /// Gets Event Hub connection settings
        /// </summary>
        /// <returns>Returns an instance of IConfiguration</returns>
        private IConfiguration GetEventHubSettings()
        {
            return _configuration.GetSection(nameof(EventHubSettings));

        }

        /// <summary>
        /// Gets subscriptions keys to be used for email validation api
        /// </summary>
        /// <returns>Returns an instance of IConfiguration</returns>
        private IConfiguration GetEmailDomainValidationApiKeys()
        {
            return _configuration.GetSection(nameof(EmailDomainValidationApiSettings));
        }
    }
}
