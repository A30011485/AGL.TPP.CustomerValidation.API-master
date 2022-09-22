using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Passport validator
    /// </summary>
    public class PassportValidator : AbstractValidator<IdentificationDocumentPassport>
    {
        /// <summary>
        /// Builds validation rules for passport
        /// </summary>
        public PassportValidator()
        {
            RuleFor(x => x.PassportNumber)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PassportNumberBlank.Code)
                .WithMessage(ApiErrorMessages.PassportNumberBlank.Message)
                .MaximumLength(20)
                .WithErrorCode(ApiErrorMessages.PassportNumberBlank.Code)
                .WithMessage(ApiErrorMessages.PassportNumberBlank.Message);
        }
    }
}
