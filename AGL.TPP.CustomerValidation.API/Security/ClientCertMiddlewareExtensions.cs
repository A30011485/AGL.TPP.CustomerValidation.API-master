using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace AGL.TPP.CustomerValidation.API.Security
{
    public static class ClientCertMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientCertMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientCertificateMiddleware>();
        }

        public static IApplicationBuilder UseClientCertMiddleware(this IApplicationBuilder builder, IOptions<CertificateValidationOptions> options)
        {
            return builder.UseMiddleware<ClientCertificateMiddleware>(options);
        }
    }
}