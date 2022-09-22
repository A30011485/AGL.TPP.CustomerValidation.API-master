using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Validators;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Controllers
{
  public class CustomerHeaderValidationTests
  {
    private readonly CustomerValidationHeaderValidator _headerValidator;
    private readonly CustomerValidationHeaderModel _model;

    public CustomerHeaderValidationTests()
    {
      _headerValidator = new CustomerValidationHeaderValidator();
      var fixture = new Fixture();
      _model = fixture.Build<CustomerValidationHeaderModel>().Create();
      _model.DateOfSale = DateTime.Now.ToString("yyyy-MM-dd");
      _model.VendorBusinessPartnerNumber = "0124477888";
      _model.VendorName = "ISE";
      _model.TransactionType = "Sale";
      _model.ResubmissionCount = null;
      _model.OfferType = "CrossSellElectricity";
      _model.Channel = "TeleMarketer";
      _model.Retailer = "AGL";
    }

    [Fact]
    public void ValidHeaderInformation_Valid_Response()
    {
      var validationResult = _headerValidator.Validate(_model);

      validationResult.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("20-01-2000")]
    [InlineData("aa-01-2000")]
    [InlineData("2000-02-30")]
    [InlineData("2000-01-40")]
    public void SaleDateIsNotInValidFormat(string dateOfSale)
    {
      _model.DateOfSale = dateOfSale;

      var validationResult = _headerValidator.Validate(_model);

      validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void SaleDateIsInThePast_Should_Pass()
    {
      _model.DateOfSale = DateTime.UtcNow.CurrentAestDateTime().AddDays(-15).ToString("yyyy-MM-dd");

      var validationResult = _headerValidator.Validate(_model);

      validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void SaleDateIsInThePast30DaysOver_Should_Fail()
    {
      _model.DateOfSale = DateTime.UtcNow.CurrentAestDateTime().AddDays(-31).ToString("yyyy-MM-dd");

      var validationResult = _headerValidator.Validate(_model);

      validationResult.IsValid.Should().BeFalse();
      validationResult.Errors.Any(_ => _.ErrorMessage.Contains("Please provide valid date of sale - cannot be more than 30 days in the past.")).Should().BeTrue();
    }

    [Fact]
    public void SaleDateIsInTheFuture_Should_Fail()
    {
      _model.DateOfSale = DateTime.UtcNow.AddDays(1).CurrentAestDateTime().ToString("yyyy-MM-dd");

      var validationResult = _headerValidator.Validate(_model);

      validationResult.IsValid.Should().BeFalse();
    }
  }
}
