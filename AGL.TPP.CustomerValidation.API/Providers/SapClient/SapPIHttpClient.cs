using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public class SapPiHttpClient : HttpClient, IHttpClient
    {
        public SapPiHttpClient(ISapPiClientSettings settings)
        {
            Timeout = TimeSpan.FromSeconds(int.TryParse(settings.Timeout, out int timeout) ? timeout : 300);

            BaseAddress = settings.BaseAddress;

            var basicHeaderValue = GetBasicAuthenticationHeaderValue();

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicHeaderValue);

            string GetBasicAuthenticationHeaderValue()
            {
                return Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                    .GetBytes($"{settings.UserId}:{settings.Password}"));
            }
        }
    }
}
