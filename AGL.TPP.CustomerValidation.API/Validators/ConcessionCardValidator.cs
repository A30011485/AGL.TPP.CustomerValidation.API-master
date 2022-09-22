using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Concession card validator
    /// </summary>
    public class ConcessionCardValidator : AbstractValidator<ConcessionCard>
    {
        /// <summary>
        /// Builds validation rules for concession card validator
        /// </summary>
        public ConcessionCardValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.ConcessionCardNumberBlank.Code)
                .WithMessage(ApiErrorMessages.ConcessionCardNumberBlank.Message)
                .When(x => x.CardType?.Length > 0);

            RuleFor(x => x.CardType)
                .NotEmpty()
                .When(x => x.CardNumber?.Length > 0)
                .WithErrorCode(ApiErrorMessages.ConcessionCardTypeInvalid.Code)
                .WithMessage(ApiErrorMessages.ConcessionCardTypeInvalid.Message);

            RuleFor(x => x.DateOfExpiry)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.DateOfExpiry?.Length > 0)
                .WithErrorCode(ApiErrorMessages.ConcessionCardExpiryInvalid.Code)
                .WithMessage(ApiErrorMessages.ConcessionCardExpiryInvalid.Message);
        }
    }
}
