using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Business customer cancellation validator
    /// </summary>
    public class CancellationBusinessCustomerValidator : AbstractValidator<CancellationBusinessCustomerValidationModel>
    {
        /// <summary>
        /// Builds validation rules for Business customer cancellation 
        /// </summary>
        public CancellationBusinessCustomerValidator()
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
                .SetValidator(new CancellationBusinessCustomerBodyValidator());
        }
    }
}
