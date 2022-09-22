namespace AGL.TPP.CustomerValidation.EndToEnd.Integration.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using AGL.TPP.CustomerValidation.API;
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Providers.EventHub.Models;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class TestStepBase
    {
        protected readonly HttpClient HttpClient;
        protected HttpResponseMessage ResponseMessage;

        public string CustomerValidationBaseUrl;
        public string ApiVersion;
        public string OcpApimSubscriptionKey;
        public string StatusClientKey;

        public ITableRepository<CustomerValidationData> SalesOrderDataRepository;
        private readonly IConfiguration _config;

        public TestStepBase()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("e2e-appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            CustomerValidationBaseUrl = _config.GetSection("CustomerValidationBaseUrl").Value;
            ApiVersion = _config.GetSection("ApiVersion").Value;
            OcpApimSubscriptionKey = _config.GetSection("OcpApimSubscriptionKey").Value;
            StatusClientKey = _config.GetSection("StatusClientKey").Value;

            HttpClient = new HttpClient();
        }

        public void SeedData()
        {
            var logger = Logging.GetLogger(_config);
            var cloudSettings = _config.GetSection(nameof(AzureCloudSettings));
            var eventHubSettings = _config.GetSection(nameof(EventHubSettings));

            var services = new ServiceCollection();

            services.AddSingleton(_config);
            services.Configure<AzureCloudSettings>(cloudSettings);
            services.Configure<AzureCloudSettings>(eventHubSettings);
            services
                .AddSingleton<ITableRepository<CustomerValidationData>, TableRepository<CustomerValidationData>>()
                .AddSingleton(logger);

            var serviceProvider = services.BuildServiceProvider();
        }

        public void GivenCorrectHeadersExists()
        {
            HttpClient.DefaultRequestHeaders.Add("Api-Version", ApiVersion);
            HttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
        }

        public async Task WhenTheRequestExecutes(string resource)
        {
            ResponseMessage = await HttpClient.GetAsync(resource);
        }

        public async Task WhenTheRequestExecutes(string resource, object request)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            ResponseMessage = await HttpClient.PostAsync(resource, content);
        }
        public void TheResponseCodeIs(HttpStatusCode expectedStatusCode)
        {
            ResponseMessage.StatusCode.Should().BeEquivalentTo(expectedStatusCode);
        }

        public async Task ResponseModelIsValid<T>(T expected)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<T>(responseString);

            actualResponse.Should().BeEquivalentTo(expected, opt => opt
                .Using<object>(_ => { })
                .When(e => e.RuntimeType.IsValueType)
                .Using<string>(_ => { })
                .WhenTypeIs<string>());

        }

        public async void WhenThePostRequestExecutesWithContent(string resource, HttpContent content)
        {
            ResponseMessage = await HttpClient.PostAsync(resource, content);
        }

        public async Task TheReturnedContentIs<T>(T obj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<T>(responseString);
            obj.Should().BeEquivalentTo(deserializeObject);
        }
    }
}
