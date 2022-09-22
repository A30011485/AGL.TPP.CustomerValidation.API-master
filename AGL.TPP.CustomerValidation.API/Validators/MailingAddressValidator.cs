using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Mailing address type validators
    /// </summary>
    public class MailingAddressValidator : AbstractValidator<MailingAddress>
    {
        /// <summary>
        /// Builds validation rules for mailing address types
        /// </summary>
        public MailingAddressValidator()
        {
            RuleFor(x => x.StreetAddress.StreetName)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.StreetNameBlank.Code)
                .WithMessage(ApiErrorMessages.StreetNameBlank.Message)
                .When(a => a.StreetAddress?.PostOfficeBoxNumber?.Length == 0);

            RuleFor(x => x.StreetAddress.Suburb)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message)
                .MaximumLength(60)
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message)
                .Matches(@"^[a-zA-Z &./]*$")
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message);

            RuleFor(x => x.StreetAddress.State)
                .Must(x =>
                    x != null &&
                    Enum.GetNames(typeof(AustralianStates))
                    .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant())))
                .WithErrorCode(ApiErrorMessages.StateInvalid.Code)
                .WithMessage(ApiErrorMessages.StateInvalid.Message);

            RuleFor(x => x.StreetAddress.Postcode)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PostcodeBlank.Code)
                .WithMessage(ApiErrorMessages.PostcodeBlank.Message)
                .MaximumLength(4)
                .WithErrorCode(ApiErrorMessages.PostcodeBlank.Code)
                .WithMessage(ApiErrorMessages.PostcodeBlank.Message);
        }
    }
}
