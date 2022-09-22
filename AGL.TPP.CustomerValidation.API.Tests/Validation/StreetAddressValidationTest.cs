using System;
using System.Collections.Generic;
using System.Text;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Validators;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Validation
{
    public class StreetAddressValidationTest
    {
        private Fixture _fixture;

        public StreetAddressValidationTest()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("3000", "VIC", "green ct", "12", "Bentleigh-east", true)]
        [InlineData("3000", "VIC", "green-ct", "12", "Bentleigh-east", true)]
        [InlineData("3000", "VIC", "green'ct", "12", "Bentleigh-east", true)]
        public void Suburb_street_With_Hyphen_Is_Valid(string postCode, string state, string streetName, string streetNumber, string suburb, bool expected)
        {
            IValidator<StreetAddress> v = new StreetAddressValidator();

            var address = new StreetAddress
            {
                Postcode = postCode,
                State = state,
                StreetName = streetName,
                StreetNumber = streetNumber,
                Suburb = suburb
            };

            v.Validate(address).IsValid.Should().Be(expected);
        }
    }
}
