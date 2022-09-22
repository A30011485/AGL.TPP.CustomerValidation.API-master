using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Street address validator
    /// </summary>
    public class StreetAddressValidator : AbstractValidator<StreetAddress>
    {
        /// <summary>
        /// Builds validation rules for street address
        /// </summary>
        public StreetAddressValidator()
        {
            RuleFor(x => x.StreetName)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.StreetNameBlank.Code)
                .WithMessage(ApiErrorMessages.StreetNameBlank.Message)
                .MaximumLength(60)
                .WithErrorCode(ApiErrorMessages.StreetNameBlank.Code)
                .WithMessage(ApiErrorMessages.StreetNameBlank.Message)
                .Matches(@"^[a-zA-Z \-&'./]*$")
                .WithErrorCode(ApiErrorMessages.StreetNameBlank.Code)
                .WithMessage(ApiErrorMessages.StreetNameBlank.Message);

            RuleFor(x => x.Suburb)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message)
                .MaximumLength(60)
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message)
                .Matches(@"^[a-zA-Z \-&'./]*$")
                .WithErrorCode(ApiErrorMessages.SuburbInvalid.Code)
                .WithMessage(ApiErrorMessages.SuburbInvalid.Message);

            RuleFor(x => x.State)
                .Must(x =>
                    x != null &&
                    Enum.GetNames(typeof(AustralianStates))
                    .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant())))
                .WithErrorCode(ApiErrorMessages.StateInvalid.Code)
                .WithMessage(ApiErrorMessages.StateInvalid.Message);

            RuleFor(x => x.Postcode)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PostcodeBlank.Code)
                .WithMessage(ApiErrorMessages.PostcodeBlank.Message)
                .MaximumLength(4)
                .WithErrorCode(ApiErrorMessages.PostcodeBlank.Code)
                .WithMessage(ApiErrorMessages.PostcodeBlank.Message);
        }
    }
}
