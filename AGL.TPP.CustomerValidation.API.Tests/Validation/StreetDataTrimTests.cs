using AGL.TPP.CustomerValidation.API.Models;
using FluentAssertions;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Validation
{
  public class StreetDataTrimTests
  {
    [Fact]
    public void given_space_in_street_name_valid_return()
    {
      var a = new StreetAddress
      {
        StreetName = "    Test  test  test ",
        State = "  VIC ",
        StreetNumber = " 34   ",
        Suburb = " Test suburb   test "
      };
      a.StreetName.Should().Be("Test test test");
      a.State.Should().Be("VIC");
      a.StreetNumber.Should().Be("34");
      a.Suburb.Should().Be("Test suburb test");
    }

    [Fact]
    public void site_address_ignore_validation_to_be_false_by_default()
    {
      var siteAddress = new StreetAddress();
      siteAddress.IgnoreAddressValidation.Should().BeFalse();
    }
  }
}
