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
    public class CustomerCancellationTests
    {
        private readonly ILogger mockLogger;
        private readonly IConfiguration mockConfiguration;

        private readonly CancellationResidentialCustomerValidator cancellationResidentialCustomerValidator;
        private readonly CancellationBusinessCustomerValidator cancellationBusinessCustomerValidator;
        private readonly Mock<ICustomerValidationService> mockCustomerValidationService;
        private readonly ICustomerValidationDataProvider salesOrderProvider;

        public CustomerCancellationTests()
        {
            mockLogger = Substitute.For<ILogger>();
            mockConfiguration = Substitute.For<IConfiguration>();
            salesOrderProvider = Substitute.For<ICustomerValidationDataProvider>();

            cancellationResidentialCustomerValidator = new CancellationResidentialCustomerValidator();
            cancellationBusinessCustomerValidator = new CancellationBusinessCustomerValidator();
            mockCustomerValidationService = new Mock<ICustomerValidationService>();
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldSuccessfullyValidateResidentialCustomerCancellation(bool isHealthy)
        {
            var residentialCustomer = JsonDataReader.GetData<CancellationResidentialCustomerValidationModel>("residential-cancel");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialCustomerCancellation(residentialCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            OkObjectResult result = await controller.ResidentialCustomerCancellation(residentialCustomer) as OkObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("200");
            response.Message.Should().Be("Validation is successful");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldSuccessfullyValidateBusinessCustomerCancellation(bool isHealthy)
        {
            var businessCustomer = JsonDataReader.GetData<CancellationBusinessCustomerValidationModel>("business-cancel");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessCustomerCancellation(businessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "200",
                    Message = "Validation is successful",
                    Errors = null
                }));

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            OkObjectResult result = await controller.BusinessCustomerCancellation(businessCustomer) as OkObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("200");
            response.Message.Should().Be("Validation is successful");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBadRequestForInvalidResidentialCustomerCancellationData(bool isHealthy)
        {
            var invalidResidentialCustomer = JsonDataReader.GetData<CancellationResidentialCustomerValidationModel>("residential-cancel-invalid");
            invalidResidentialCustomer.Header.Channel = "";

            salesOrderProvider.Ping().Returns(isHealthy);

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            var result = await controller.ResidentialCustomerCancellation(invalidResidentialCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Errors.Count.Should().Be(1);
            response.Message.Should().Be("BadRequest");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBadRequestForInvalidBusinessCustomerCancellationData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<CancellationBusinessCustomerValidationModel>("business-cancel-invalid");
            invalidBusinessCustomer.Header.Channel = "";

            salesOrderProvider.Ping().Returns(isHealthy);

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            var result = await controller.BusinessCustomerCancellation(invalidBusinessCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Errors.Count.Should().Be(1);
            response.Message.Should().Be("BadRequest");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBackEndErrorForInvalidResidentialCustomerCancellationData(bool isHealthy)
        {
            var invalidResidentialCustomer = JsonDataReader.GetData<CancellationResidentialCustomerValidationModel>("residential-cancel-invalid");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateResidentialCustomerCancellation(invalidResidentialCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            var result = await controller.ResidentialCustomerCancellation(invalidResidentialCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }

        [Theory]
        [InlineData(true)]
        public async void ShouldThrowBackEndErrorForInvalidBusinessCustomerCancellationData(bool isHealthy)
        {
            var invalidBusinessCustomer = JsonDataReader.GetData<CancellationBusinessCustomerValidationModel>("business-cancel-invalid");
            salesOrderProvider.Ping().Returns(isHealthy);

            mockCustomerValidationService
                .Setup(x => x.ValidateBusinessCustomerCancellation(invalidBusinessCustomer))
                .Returns(Task.FromResult(new CustomerValidationResponse
                {
                    Code = "400",
                    Message = "BackEndValidationFailure",
                    Errors = null
                }));

            CustomerSalesCancellationController controller = new CustomerSalesCancellationController(
                mockLogger,
                mockCustomerValidationService.Object,
                cancellationResidentialCustomerValidator,
                cancellationBusinessCustomerValidator);

            var result = await controller.BusinessCustomerCancellation(invalidBusinessCustomer) as BadRequestObjectResult;

            result.Should().NotBe(null);
            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            var response = result?.Value as CustomerValidationResponse;
            response.Code.Should().Be("400");
            response.Message.Should().Be("BackEndValidationFailure");
        }
    }
}
