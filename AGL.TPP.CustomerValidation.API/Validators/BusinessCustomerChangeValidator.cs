using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Business customer validator
    /// </summary>
    public class BusinessCustomerChangeValidator : AbstractValidator<BusinessCustomerChangeValidationModel>
    {
        /// <summary>
        /// Builds the business customer validator rules
        /// </summary>
        public BusinessCustomerChangeValidator()
        {
            RuleFor(x => x.Header)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.HeaderBlank.Code)
                .WithMessage(ApiErrorMessages.HeaderBlank.Message)
                .SetValidator(new ChangeCustomerValidationHeaderValidator());

            RuleFor(x => x.Payload)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.PayloadBlank.Code)
                .WithMessage(ApiErrorMessages.PayloadBlank.Message)
                .SetValidator(new BusinessCustomerChangeBodyValidator());
        }
    }
}
