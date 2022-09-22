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
    public class CustomerChangeValidationTests
    {
        private readonly ILogger mockLogger;
        private readonly IConfiguration mockConfiguration;

        private readonly ResidentialCustomerChangeValidator residentialChangeCustomerValidator;
        private readonly BusinessCustomerChangeValidator businessChangeCustomerValidator;
        private readonly Mock<ICustomerValidationService> mockCustomerValidationService;
        private readonly ICustomerValidationDataProvider salesOrderProvider;

        public CustomerChangeValidationTests()
        {
            mockLogger = Substitute.For<ILogger>();
            mockConfiguration = Substitute.For<IConfiguration>();
            salesOrderProvider = Substitute.For<ICustomerValidationDataProvider>();

            residentialChangeCustomerValidator = new ResidentialCustomerChangeValidator();
            businessChangeCustomerValidator = new BusinessCustomerChangeValidator();
            mockCustomerValidationService = new Mock<ICustomerValidationService>();
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldSuccessfullyValidateResidentialChangeCustomer(bool isHealthy)
        {
            var residentialChangeCustomer = JsonDataReader.GetData<ResidentialCustomerChangeValidationModel>("residential-change-customer");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialChangeCustomer(residentialChangeCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

            var result = await controller.ValidateResidentialCustomer(residentialChangeCustomer) as OkObjectResult;

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
            var businessCustomer = JsonDataReader.GetData<BusinessCustomerChangeValidationModel>("business-change-customer");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessChangeCustomer(businessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

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
            var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerChangeValidationModel>("residential-change-customer-invalid");
            invalidResidentialCustomer.Header.Channel = "";

            salesOrderProvider.Ping().Returns(isHealthy);

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

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
        public async void ShouldThrowBadRequestForInvalidBusinessCustomerData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerChangeValidationModel>("business-change-customer-invalid");
            invalidBusinessCustomer.Header.Channel = "";

            salesOrderProvider.Ping().Returns(isHealthy);

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

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
        public async void ShouldThrowBackEndErrorForInvalidResidentialCustomerData(bool isHealthy)
        {
            var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerChangeValidationModel>("residential-change-customer-invalid");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialChangeCustomer(invalidResidentialCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

            var result = await controller.ValidateResidentialCustomer(invalidResidentialCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBackEndErrorForInvalidBusinessCustomerData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerChangeValidationModel>("business-change-customer-invalid");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessChangeCustomer(invalidBusinessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            var controller = new CustomerChangeController(
                mockLogger,
                mockCustomerValidationService.Object,
                residentialChangeCustomerValidator,
                businessChangeCustomerValidator);

            var result = await controller.ValidateBusinessCustomer(invalidBusinessCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }
    }
}
