using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Site Meter Detail validator
    /// </summary>
    public class SiteMeterDetailValidator : AbstractValidator<SiteMeterDetail>
    {
        /// <summary>
        /// Builds validation rules for Site Meter Details
        /// </summary>
        public SiteMeterDetailValidator()
        {
            RuleFor(x => x.Electricity.Nmi)
                .Length(11)
                .WithErrorCode(ApiErrorMessages.NmiInvalid.Code)
                .WithMessage(ApiErrorMessages.NmiInvalid.Message)
                .When(x => x?.Electricity?.Nmi?.Length > 0);

            RuleFor(x => x.Gas.Mirn)
                .Length(11)
                .WithErrorCode(ApiErrorMessages.MirnInvalid.Code)
                .WithMessage(ApiErrorMessages.MirnInvalid.Message)
                .When(x => x?.Gas?.Mirn?.Length > 0);
        }
    }
}
