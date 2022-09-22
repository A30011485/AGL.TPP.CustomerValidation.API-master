using System;
using AGL.TPP.CustomerValidation.API.Providers.SapClient;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Microsoft.Extensions.Options;

namespace AGL.TPP.CustomerValidation.API.Config
{
    public class SapPiSecureSettings : ISapPiClientSettings
    {
        public SapPiSecureSettings(IOptions<SapPiSettings> settings)
        {
            var settingsValue = settings.Value;
            BaseAddress = new Uri($"http://{settingsValue.Host}:{settingsValue.Port}");
            UserId = settingsValue.UserId;
            Password = settingsValue.Password;
            Timeout = settingsValue.Timeout;
        }

        public Uri BaseAddress { get; }
            
        public string UserId { get; }

        public string Password { get; }

        public string Timeout { get; }
    }
}
