using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Residential customer validator
    /// </summary>
    public class ResidentialCustomerMoveOutValidator : AbstractValidator<ResidentialCustomerMoveOutValidationModel>
    {
        /// <summary>
        /// Builds validation rules for Residential customer
        /// </summary>
        public ResidentialCustomerMoveOutValidator()
        {
            RuleFor(x => x.Header)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.HeaderBlank.Code)
                .WithMessage(ApiErrorMessages.HeaderBlank.Message)
                .SetValidator(new MoveOutCustomerValidationHeaderValidator());

            RuleFor(x => x.Payload)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PayloadBlank.Code)
                .WithMessage(ApiErrorMessages.PayloadBlank.Message)
                .SetValidator(new ResidentialCustomerMoveOutBodyValidator());
        }
    }
}
