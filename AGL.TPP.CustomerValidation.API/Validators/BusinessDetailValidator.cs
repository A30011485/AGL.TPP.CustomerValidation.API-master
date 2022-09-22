using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Business detail validator
    /// </summary>
    public class BusinessDetailValidator : AbstractValidator<BusinessDetail>
    {
        /// <summary>
        /// Builds the business detail validator rules
        /// </summary>
        public BusinessDetailValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.BusinessNameBlank.Code)
                .WithMessage(ApiErrorMessages.BusinessNameBlank.Message)
                .Length(1, 60)
                .WithErrorCode(ApiErrorMessages.BusinessNameInvalid.Code)
                .WithMessage(ApiErrorMessages.BusinessNameInvalid.Message);
        }
    }
}
