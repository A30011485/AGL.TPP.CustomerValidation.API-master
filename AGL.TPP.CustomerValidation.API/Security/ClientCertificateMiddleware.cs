using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ILogger = Serilog.ILogger;

namespace AGL.TPP.CustomerValidation.API.Security
{
    public class ClientCertificateMiddleware
    {
        private readonly bool _enabled;

        private readonly ILogger _logger;
        private readonly RequestDelegate _nextMiddleware;
        private readonly CertificateValidator _validator;

        public ClientCertificateMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger logger, IOptions<CertificateValidationOptions> options)
        {
            _nextMiddleware = next;
            _enabled = options.Value.Enabled;
            _logger = logger;
            _validator = new CertificateValidator(_logger, options.Value);

            if (env.IsDevelopment())
            {
                _enabled = false;
                _logger.Debug("Mutual Authentication set to disabled in development environment.");
            }

            if (!_enabled)
            {
                _logger.Debug("Mutual Authentication using client certificate disabled!");
            }
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_enabled)
            {
                await _nextMiddleware.Invoke(context);
                return;
            }

            try
            {
                if (!context.Request.Headers.Keys.Contains("X-ARR-ClientCert"))
                {
                    _logger.Debug("No Certificate provided. Terminating request");
                    context.Response.StatusCode = 403;
                }
                var clientCertBytes = Convert.FromBase64String(context.Request.Headers["X-ARR-ClientCert"]);
                var certificate = new X509Certificate2(clientCertBytes);

                if (_validator.CheckValid(certificate))
                {
                    _logger.Debug("Client certificate authentication successful.");
                    await _nextMiddleware.Invoke(context);
                    return;
                }

                context.Response.StatusCode = 403;
            }
            catch (Exception ex)
            {
                // Assume that an error means unable to parse the certificate or an invalid cert was provided.
                _logger.Warning("Exception authenticating client certificate. " + ex.Message, ex);
                context.Response.StatusCode = 403;
            }
        }
    }
}
