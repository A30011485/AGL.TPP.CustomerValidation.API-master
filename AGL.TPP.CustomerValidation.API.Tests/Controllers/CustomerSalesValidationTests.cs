using System;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers;
using AGL.TPP.CustomerValidation.API.Validators;

namespace AGL.TPP.CustomerValidation.API.Tests.Controllers
{
    using AGL.TPP.CustomerValidation.API.Controllers;
    using AGL.TPP.CustomerValidation.API.Services;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NSubstitute;
    using Serilog;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Unit tests for Customer sales controller
    /// </summary>
    public class CustomerSalesValidationTests
  {
    private readonly ILogger mockLogger;
    private readonly IConfiguration mockConfiguration;

    private readonly ResidentialCustomerSalesValidator residentialCustomerValidator;
    private readonly BusinessCustomerSalesValidator businessCustomerValidator;
    private readonly Mock<ICustomerValidationService> mockCustomerValidationService;
    private readonly ICustomerValidationDataProvider salesOrderProvider;

    public CustomerSalesValidationTests()
    {
      mockLogger = Substitute.For<ILogger>();
      mockConfiguration = Substitute.For<IConfiguration>();
      salesOrderProvider = Substitute.For<ICustomerValidationDataProvider>();

      residentialCustomerValidator = new ResidentialCustomerSalesValidator();
      businessCustomerValidator = new BusinessCustomerSalesValidator();
      mockCustomerValidationService = new Mock<ICustomerValidationService>();
    }

    [Theory]
    [InlineData(true)]
    public async void ShouldSuccessfullyValidateResidentialCustomer(bool isHealthy)
    {
      var residentialCustomer = JsonDataReader.GetData<ResidentialCustomerSalesValidationModel>("residential-customer");
      residentialCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      salesOrderProvider.Ping().Returns(isHealthy);

      mockCustomerValidationService
          .Setup(x => x.ValidateResidentialSalesCustomer(residentialCustomer))
          .Returns(Task.FromResult(new CustomerValidationResponse
          {
            Code = "200",
            Message = "Validation is successful",
            Errors = null
          }));

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

      OkObjectResult result = await controller.ValidateResidentialCustomer(residentialCustomer) as OkObjectResult;

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
      var businessCustomer = JsonDataReader.GetData<BusinessCustomerSalesValidationModel>("business-customer");
      businessCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      salesOrderProvider.Ping().Returns(isHealthy);

      mockCustomerValidationService
          .Setup(x => x.ValidateBusinessSalesCustomer(businessCustomer))
          .Returns(Task.FromResult(new CustomerValidationResponse
          {
            Code = "200",
            Message = "Validation is successful",
            Errors = null
          }));

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

      OkObjectResult result = await controller.ValidateBusinessCustomer(businessCustomer) as OkObjectResult;

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
      var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerSalesValidationModel>("residential-customer-invalid");
      invalidResidentialCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      invalidResidentialCustomer.Header.Channel = "";

      salesOrderProvider.Ping().Returns(isHealthy);

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

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
      var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerSalesValidationModel>("business-customer-invalid");
      invalidBusinessCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      invalidBusinessCustomer.Header.Channel = "";

      salesOrderProvider.Ping().Returns(isHealthy);

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

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
      var invalidResidentialCustomer = JsonDataReader.GetData<ResidentialCustomerSalesValidationModel>("residential-customer-invalid");

      invalidResidentialCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      salesOrderProvider.Ping().Returns(isHealthy);

      mockCustomerValidationService
          .Setup(x => x.ValidateResidentialSalesCustomer(invalidResidentialCustomer))
          .Returns(Task.FromResult(new CustomerValidationResponse
          {
            Code = "400",
            Message = "BackEndValidationFailure",
            Errors = null
          }));

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

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
      var invalidBusinessCustomer = JsonDataReader.GetData<BusinessCustomerSalesValidationModel>("business-customer-invalid");
      invalidBusinessCustomer.Header.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      salesOrderProvider.Ping().Returns(isHealthy);

      mockCustomerValidationService
          .Setup(x => x.ValidateBusinessSalesCustomer(invalidBusinessCustomer))
          .Returns(Task.FromResult(new CustomerValidationResponse
          {
            Code = "400",
            Message = "BackEndValidationFailure",
            Errors = null
          }));

      CustomerSalesController controller = new CustomerSalesController(
          mockLogger,
          mockCustomerValidationService.Object,
          residentialCustomerValidator,
          businessCustomerValidator);

      var result = await controller.ValidateBusinessCustomer(invalidBusinessCustomer) as BadRequestObjectResult;

      result.Should().NotBe(null);
      result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

      var response = result?.Value as CustomerValidationResponse;
      response.Code.Should().Be("400");
      response.Message.Should().Be("BackEndValidationFailure");
    }
  }
}
