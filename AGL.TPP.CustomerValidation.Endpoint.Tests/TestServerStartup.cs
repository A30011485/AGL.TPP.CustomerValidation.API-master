using AGL.TPP.CustomerValidation.API;
using Microsoft.Extensions.Configuration;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests
{
    public class TestServerStartup : Startup
    {
        public TestServerStartup(IConfiguration conf) : base(conf)
        {
        }
    }
}
