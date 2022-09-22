using System.ComponentModel.DataAnnotations;
using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;
using FluentAssertions;
using Xunit;

namespace AGL.TPP.CustomerValidation.API.Tests.Helper
{
    public class EnumHelperTests

    {
        [Theory]
        [InlineData(CancellationReasons.OutsideCoolOffPeriod, "CUSTOMER CANCELLATION - OUTSIDE COOL OFF PERIOD")]
        [InlineData(CancellationReasons.WithinCoolOffPeriod, "CUSTOMER CANCELLATION - WITHIN COOL OFF PERIOD")]
        [InlineData(CancellationReasons.IncorrectPremise, "INCORRECT PERMISE")]
        [InlineData(CancellationReasons.SiteWonInError, "SITE WON IN ERROR")]
        [InlineData(CancellationReasons.MoveInCancellation, "MOVE IN CANCELLATION")]
        [InlineData(CancellationReasons.Other, "OTHER")]
        public void Cancellation_Reason_Has_Mapped_Sap_Message(CancellationReasons cancellationReason, string cancellationSapReason)
        {
            var helper = EnumHelper.GetAttribute<DisplayAttribute>(cancellationReason);
            helper.Name.Should().Be(cancellationSapReason);
        }
    }
}
