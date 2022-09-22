using System.Net;
using AGL.TPP.CustomerValidation.API.Controllers;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers;
using AGL.TPP.CustomerValidation.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Serilog;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Controllers
{
    public class StatusControllerTests
    {
        private readonly ILogger _mockLogger;
        private readonly ICustomerValidationDataProvider _salesOrderProvider;
        private readonly IConfiguration _configuration;

        public StatusControllerTests()
        {
            _salesOrderProvider = Substitute.For<ICustomerValidationDataProvider>();
            _mockLogger = Substitute.For<ILogger>();
            _configuration = Substitute.For<IConfiguration>();
        }

        [Theory]
        [InlineData(true)]
        public async void HealthCheckSucceedsReturnsHealthyResponse(bool isHealthy)
        {
            _salesOrderProvider.Ping().Returns(isHealthy);
            var healthCheckService = new HealthCheckService(_mockLogger, _salesOrderProvider);

            var controller = new StatusController(healthCheckService);

            var result = await controller.GetStatus() as OkObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var resultObject = result?.Value as HealthCheckResponse;
            resultObject.Should().NotBe(null);
            resultObject?.IsHealthy.Should().Be(isHealthy);
        }
    }
}
