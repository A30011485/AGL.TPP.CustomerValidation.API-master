using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Residential customer cancellation validator
    /// </summary>
    public class CancellationResidentialCustomerValidator : AbstractValidator<CancellationResidentialCustomerValidationModel>
    {
        /// <summary>
        /// Builds validation rules for Residential customer cancellation
        /// </summary>
        public CancellationResidentialCustomerValidator()
        {
            RuleFor(x => x.Header)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.HeaderBlank.Code)
                .WithMessage(ApiErrorMessages.HeaderBlank.Message)
                .SetValidator(new CancellationCustomerValidationHeaderValidator());

            RuleFor(x => x.Payload)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PayloadBlank.Code)
                .WithMessage(ApiErrorMessages.PayloadBlank.Message)
                .SetValidator(new CancellationResidentialCustomerBodyValidator());
        }
    }
}