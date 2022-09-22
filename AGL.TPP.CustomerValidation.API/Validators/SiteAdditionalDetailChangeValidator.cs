using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Site Meter Detail validator
    /// </summary>
    public class SiteAdditionalDetailChangeValidator : AbstractValidator<SiteAdditionalDetailChange>
    {
        /// <summary>
        /// Builds validation rules for Site Meter Details
        /// </summary>
        public SiteAdditionalDetailChangeValidator()
        {
            RuleFor(x => x.ChangeRequestDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.ChangeRequestDateBlank.Code)
                .WithMessage(ApiErrorMessages.ChangeRequestDateBlank.Message)
                .Must(CustomValidator.CheckDateFormat)
                .WithErrorCode(ApiErrorMessages.ChangeRequestDateInvalid.Code)
                .WithMessage(ApiErrorMessages.ChangeRequestDateInvalid.Message);

            RuleFor(x => x.Comments)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CommentsBlank.Code)
                .WithMessage(ApiErrorMessages.CommentsBlank.Message);
        }
    }
}
