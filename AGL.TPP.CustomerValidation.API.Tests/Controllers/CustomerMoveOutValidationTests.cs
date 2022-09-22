namespace AGL.TPP.CustomerValidation.API.Tests.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using AGL.TPP.CustomerValidation.API.Controllers;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.API.Providers;
    using AGL.TPP.CustomerValidation.API.Services;
    using AGL.TPP.CustomerValidation.API.Validators;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NSubstitute;
    using Serilog;
    using Xunit;

    /// <summary>
    /// Unit tests for Customer sales controller
    /// </summary>
    public class CustomerMoveOutValidationTests
    {
        private readonly ILogger mockLogger;
        private readonly IConfiguration mockConfiguration;

        private readonly ResidentialCustomerMoveOutValidator residentialMoveOutCustomerValidator;
        private readonly BusinessCustomerMoveOutValidator businessMoveOutCustomerValidator;
        private readonly Mock<ICustomerValidationService> mockCustomerValidationService;
        private readonly ICustomerValidationDataProvider customerValidationProvider;

        public CustomerMoveOutValidationTests()
        {
            mockLogger = Substitute.For<ILogger>();
            mockConfiguration = Substitute.For<IConfiguration>();
            customerValidationProvider = Substitute.For<ICustomerValidationDataProvider>();

            residentialMoveOutCustomerValidator = new ResidentialCustomerMoveOutValidator();
            businessMoveOutCustomerValidator = new BusinessCustomerMoveOutValidator();
            mockCustomerValidationService = new Mock<ICustomerValidationService>();
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldSuccessfullyValidateResidentialMoveOutCustomer(bool isHealthy)
        {
            var residentialMoveOutCustomer = JsonDataReader.GetData<ResidentialCustomerMoveOutValidationModel>("residential-moveout-customer");
            customerValidationProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialMoveOutCustomer(residentialMoveOutCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateResidentialCustomer(residentialMoveOutCustomer) as OkObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("200");
            response.Message.Should().Be("Validation is successful");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldSuccessfullyValidateBusinessCustomer(bool isHealthy)
        {
            var businessCustomer = JsonDataReader.GetData<BusinessCustomerMoveOutValidationModel>("business-moveout-customer");
            customerValidationProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessMoveOutCustomer(businessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateBusinessCustomer(businessCustomer) as OkObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("200");
            response.Message.Should().Be("Validation is successful");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBadRequestForInvalidResidentialCustomerData(bool isHealthy)
        {
            var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerMoveOutValidationModel>("residential-moveout-customer-invalid");
            invalidResidentialCustomer.Header.Channel = "";

            customerValidationProvider.Ping().Returns(isHealthy);

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateResidentialCustomer(invalidResidentialCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Errors.Count.Should().Be(1);
            response.Message.Should().Be("BadRequest");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBackEndErrorForInvalidResidentialCustomerData(bool isHealthy)
        {
            var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerMoveOutValidationModel>("residential-moveout-customer-invalid");
            customerValidationProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialMoveOutCustomer(invalidResidentialCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateResidentialCustomer(invalidResidentialCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBadRequestForInvalidBusinessCustomerData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerMoveOutValidationModel>("business-moveout-customer-invalid");
            invalidBusinessCustomer.Header.Channel = "";

            customerValidationProvider.Ping().Returns(isHealthy);

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateBusinessCustomer(invalidBusinessCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Errors.Count.Should().Be(1);
            response.Message.Should().Be("BadRequest");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBackEndErrorForInvalidBusinessCustomerData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerMoveOutValidationModel>("business-moveout-customer-invalid");
            customerValidationProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessMoveOutCustomer(invalidBusinessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            var controller = new CustomerMoveOutController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialMoveOutCustomerValidator,
                businessMoveOutCustomerValidator);

            var result = await controller.ValidateBusinessCustomer(invalidBusinessCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }
    }
}
