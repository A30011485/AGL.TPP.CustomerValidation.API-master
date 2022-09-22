using AGL.TPP.CustomerValidation.API.Models.Repository;
using AGL.TPP.CustomerValidation.API.Storage.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ClearExtensions;
using System.Net.Http;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures
{
    public class TestServerFixture
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseEnvironment("Development")
                .UseStartup(typeof(TestServerStartup))
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton(Substitute.For<ITableRepository<CustomerValidationData>>());
                });

            Server = new TestServer(builder);

            Client = Server.CreateClient();
        }

        public TService GetService<TService>()
            where TService : class
        {
            return Server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }

        public void Reset()
        {
            Client.DefaultRequestHeaders.Remove("Status-Client-Key");

            var tariffDataTableRepository = GetService<ITableRepository<CustomerValidationData>>();
            tariffDataTableRepository.ClearSubstitute();
        }
    }
}
