using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;
using FluentAssertions;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Helper
{
    public class FieldMapperTests
    {
        [Theory]
        [InlineData("Unknown", "")]
        [InlineData("Prof", "")]
        [InlineData("Yes", "")]
        [InlineData("Mr", "Mr")]
        [InlineData("Mrs", "Mrs")]
        [InlineData("Miss", "Miss")]
        [InlineData("Dr", "Dr")]
        public void TitleShouldMappedToExpected(string actualTitle, string expectedTitle)
        {
            FieldMapper.MapPersonDetailTitle(actualTitle).Should().BeEquivalentTo(expectedTitle);
        }

        [Theory]
        [InlineData("Unknown", "")]
        [InlineData("Prof", "")]
        [InlineData("Yes", "")]
        [InlineData("Mr", "Mr")]
        [InlineData("Mrs", "Mrs")]
        [InlineData("Miss", "Miss")]
        [InlineData("Dr", "Dr")]
        public void ResidentialSaleCustomerTitleShouldMappedToExpected(string actualTitle, string expectedTitle)
        {
            ResidentialCustomerSalesValidationBodyModel model = new ResidentialCustomerSalesValidationBodyModel
            {
                PersonDetail = new Customer { Title = actualTitle }
            };

            FieldMapper.GetMappedPayload(model).PersonDetail.Title.Should().BeEquivalentTo(expectedTitle);
        }

        //CancellationResidentialCustomerValidationBodyModel
        [Theory]
        [InlineData("Unknown", "")]
        [InlineData("Prof", "")]
        [InlineData("Yes", "")]
        [InlineData("Mr", "Mr")]
        [InlineData("Mrs", "Mrs")]
        [InlineData("Miss", "Miss")]
        [InlineData("Dr", "Dr")]
        public void ResidentialCancelCustomerTitleShouldMappedToExpected(string actualTitle, string expectedTitle)
        {
            CancellationResidentialCustomerValidationBodyModel model = new CancellationResidentialCustomerValidationBodyModel
            {
                PersonDetail = new Customer { Title = actualTitle }
            };

            FieldMapper.GetMappedPayload(model).PersonDetail.Title.Should().BeEquivalentTo(expectedTitle);
        }

        //ResidentialCustomerMoveOutValidationBodyModel
        [Theory]
        [InlineData("Unknown", "")]
        [InlineData("Prof", "")]
        [InlineData("Yes", "")]
        [InlineData("Mr", "Mr")]
        [InlineData("Mrs", "Mrs")]
        [InlineData("Miss", "Miss")]
        [InlineData("Dr", "Dr")]
        public void ResidentialMoveoutCustomerTitleShouldMappedToExpected(string actualTitle, string expectedTitle)
        {
            ResidentialCustomerMoveOutValidationBodyModel model = new ResidentialCustomerMoveOutValidationBodyModel
            {
                PersonDetail = new Customer { Title = actualTitle }
            };

            FieldMapper.GetMappedPayload(model).PersonDetail.Title.Should().BeEquivalentTo(expectedTitle);
        }

        //ResidentialCustomerChangeValidationBodyModel

        [Theory]
        [InlineData("Unknown", "")]
        [InlineData("Prof", "")]
        [InlineData("Yes", "")]
        [InlineData("Mr", "Mr")]
        [InlineData("Mrs", "Mrs")]
        [InlineData("Miss", "Miss")]
        [InlineData("Dr", "Dr")]
        public void ResidentialChangeCustomerTitleShouldMappedToExpected(string actualTitle, string expectedTitle)
        {
            ResidentialCustomerChangeValidationBodyModel model = new ResidentialCustomerChangeValidationBodyModel
            {
                PersonDetail = new Customer { Title = actualTitle }
            };

            FieldMapper.GetMappedPayload(model).PersonDetail.Title.Should().BeEquivalentTo(expectedTitle);
        }
    }
}
