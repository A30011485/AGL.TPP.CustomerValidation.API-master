using System;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public interface ISapPiClientSettings
    {
        Uri BaseAddress { get; }
        string UserId { get; }
        string Password { get; }

        string Timeout { get; }
    }
}
