using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Customer validator
    /// </summary>
    public class CustomerConsentValidator : AbstractValidator<CustomerConsent>
    {
        /// <summary>
        /// Builds validation rules for customer
        /// </summary>
        public CustomerConsentValidator()
        {
            RuleFor(x => x.HasGivenCreditCheckConsent)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CreditCheckBlank.Code)
                .WithMessage(ApiErrorMessages.CreditCheckBlank.Message)
                .Must(CustomValidator.IsBoolean)
                .WithErrorCode(ApiErrorMessages.CreditCheckInvalid.Code)
                .WithMessage(ApiErrorMessages.CreditCheckInvalid.Message);

            RuleFor(x => x.HasGivenEBillingConsent)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(CustomValidator.IsBoolean)
                .WithErrorCode(ApiErrorMessages.EbillingInvalid.Code)
                .WithMessage(ApiErrorMessages.EbillingInvalid.Message)
                .When(x => x.HasGivenEBillingConsent?.Length > 0);
        }
    }
}
