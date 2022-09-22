using System;
using System.Collections.Generic;
using System.Text;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using AGL.TPP.CustomerValidation.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using NSubstitute;
using Serilog;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Services
{
  public class SapErrorMessageServiceTests
  {
    private SapErrorMessageService _sapErrorMessageService;

    public SapErrorMessageServiceTests()
    {
      var logger = Substitute.For<ILogger>();
      var hostEnv = Substitute.For<IHostingEnvironment>();
      _sapErrorMessageService = new SapErrorMessageService(hostEnv, logger);
    }

    [Theory]
    [InlineData(128, "ZCSS_WEBREQ_VALIDATE", 1112, "SDFI cannot be raised for weekend or public holiday connection")]
    [InlineData(122, "ZCSS_WEBREQ_VALIDATE", 1111, "Move out date cannot be in the past")]
    [InlineData(97, "ZCSS_WEBREQ_VALIDATE", 1110, "AGL cannot accept Concession Cards for SA. Please remove details and provide Concession directly to DHS")]
    public void Test_Sap_Error_Are_Loading(int sapCode, string sapClassId, int expectedApiCode, string expectedErrorMessage)
    {
      var sapPiResponseMessage = new SapPiResponseMessage { Id = sapClassId };
      var apiErrorMessage = _sapErrorMessageService.GetApiErrorMessage(sapCode, sapPiResponseMessage);
      apiErrorMessage.Should().NotBeNull();
      apiErrorMessage.Message.Should().BeEquivalentTo(expectedErrorMessage);
      apiErrorMessage.ApiCode.Should().Be(expectedApiCode);
    }
  }
}
