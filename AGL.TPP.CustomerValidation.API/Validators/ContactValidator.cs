using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Contact validator
    /// </summary>
    public class ContactValidator : AbstractValidator<Contact>
    {
        /// <summary>
        /// Builds validation rules for contact
        /// </summary>
        public ContactValidator()
        {
            RuleFor(x => x.MobilePhone)
                .Matches(CustomValidator.NumbersWithoutSpaces)
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message);

            RuleFor(x => x.MobilePhone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message)
                .Must(x => x.StartsWith("04") || x.StartsWith("614"))
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message)
                .When(x => x.MobilePhone?.Length > 0);

            RuleFor(x => x.MobilePhone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message)
                .Length(10)
                .WithErrorCode(ApiErrorMessages.MobilePhoneLength.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneLength.Message)
                .Must(x => x.StartsWith("04"))
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message)
                .When(x => x.MobilePhone != null && x.MobilePhone.StartsWith("04"));

            RuleFor(x => x.MobilePhone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(11)
                .WithErrorCode(ApiErrorMessages.MobilePhoneLength.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneLength.Message)
                .Must(x => x.StartsWith("614"))
                .WithErrorCode(ApiErrorMessages.MobilePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.MobilePhoneInvalid.Message)
                .When(x => x.MobilePhone != null && x.MobilePhone.StartsWith("614"));

            RuleFor(x => x.HomePhone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(10)
                .WithErrorCode(ApiErrorMessages.HomePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.HomePhoneInvalid.Message)
                .Matches(CustomValidator.NumbersWithoutSpaces)
                .Must(x =>
                        HomeAndWorkPhonePrefixes
                        .Count(y => y.StartsWith(x.Substring(0, 2)) || x.StartsWith("1")) > 0)
                .When(x => x?.HomePhone?.Length > 0)
                .WithErrorCode(ApiErrorMessages.HomePhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.HomePhoneInvalid.Message);

            RuleFor(x => x.WorkPhone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(10)
                .WithErrorCode(ApiErrorMessages.WorkPhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.WorkPhoneInvalid.Message)
                .Matches(CustomValidator.NumbersWithoutSpaces)
                .Must(x =>
                        HomeAndWorkPhonePrefixes
                        .Count(y => y.StartsWith(x.Substring(0, 2)) || x.StartsWith("1")) > 0)
                .When(x => x?.WorkPhone?.Length > 0)
                .WithErrorCode(ApiErrorMessages.WorkPhoneInvalid.Code)
                .WithMessage(ApiErrorMessages.WorkPhoneInvalid.Message);
        }
    }
}
