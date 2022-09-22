namespace AGL.TPP.CustomerValidation.Endpoint.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using FluentAssertions;
    using Newtonsoft.Json;
    using NSubstitute;
    using Xunit;
    using AppConfiguration = AGL.TPP.CustomerValidation.API.Config.AppConfiguration;

    public class TestStepsBase
    {
        protected readonly HttpClient HttpClient;

        protected HttpResponseMessage ResponseMessage;
        protected string CorrId = Guid.NewGuid().ToString();

        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public TestStepsBase(TestServerFixture testServerFixture)
        {
            HttpClient = testServerFixture.Client;
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }

        public void GivenTheStatusKeyHeaderExists()
        {
            HttpClient.DefaultRequestHeaders.Add("Status-Client-Key", "123");
        }

        public void GivenPingReturns(bool pingStatus)
        {
            _salesOrderRepository.IsTableExists().Returns(pingStatus);
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

        public async void WhenThePostRequestExecutesWithContent(string resource, HttpContent content)
        {
            ResponseMessage = await HttpClient.PostAsync(resource, content);
        }

        public void TheResponseCodeIs(HttpStatusCode expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, ResponseMessage.StatusCode);
        }

        public async void TheReturnedContentContains(string expectedResponse)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();

            Assert.Contains(expectedResponse, responseString);
        }

        public void AnAuthenticatedUser()
        {
            var jwtTokenGenerator = new TokenGenerator();
            var accessToken = jwtTokenGenerator.GetJwtToken();
            if (!HttpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            }
        }

        public async Task TheErrorResponseContent(CustomerValidationResponse expectedObj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<CustomerValidationResponse>(responseString);


            expectedObj.Should().BeEquivalentTo(actualResponse);
        }

        public async Task TheErrorResponseContent(FieldLevelError expectedObj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<FieldLevelError>(responseString);

            expectedObj.Should().BeEquivalentTo(actualResponse);
        }

        public async Task TheReturnedContentModelIs<T>(T expected)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<T>(responseString);

            actual.Should().BeEquivalentTo(expected, opt => opt
                .Using<object>(_ => { })
                .When(e => e.RuntimeType.IsValueType)
                .Using<string>(_ => { })
                .WhenTypeIs<string>());

        }

        public async Task TheReturnedContentIs<T>(T obj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<T>(responseString);
            obj.Should().BeEquivalentTo(deserializeObject);
        }

        public async Task TheReturnedContentIsInternalServerError(string correlationId)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<InternalServerError>(responseString);
            obj.CorrelationId.Should().NotBeEmpty();
            obj.Message.Should().NotBeEmpty();
        }

        public async Task TheReturnedContentIsAString(string expectedObj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();

            expectedObj.Should().BeEquivalentTo(responseString);
        }

        public void GivenACorrelationIdHeaderIsProvided(bool isAttached, string correlationId)
        {
            if (isAttached)
            {
                HttpClient.DefaultRequestHeaders.Add(AppConfiguration.CorrelationKey, correlationId ?? CorrId);
            }
            else
            {
                HttpClient.DefaultRequestHeaders.Remove(AppConfiguration.CorrelationKey);
            }
        }

        public void ThenReturnValidResponseHeaders(string correlationId)
        {
            ResponseMessage.Headers.GetValues("correlation-id").First().Should().NotBeNullOrEmpty();
        }

        public void GivenACorrelationIdHeaderIsProvided(bool isAttached)
        {
            if (isAttached)
            {
                HttpClient.DefaultRequestHeaders.Add(AppConfiguration.CorrelationKey, Guid.NewGuid().ToString());
            }
            else
            {
                HttpClient.DefaultRequestHeaders.Remove(AppConfiguration.CorrelationKey);
            }
        }

        public void GivenAKnownCorrelationIdHeaderIsProvided(bool isAttached, string correlationId)
        {
            if (isAttached)
            {
                HttpClient.DefaultRequestHeaders.Add(AppConfiguration.CorrelationKey, correlationId);
            }
            else
            {
                HttpClient.DefaultRequestHeaders.Remove(AppConfiguration.CorrelationKey);
            }
        }
    }
}
