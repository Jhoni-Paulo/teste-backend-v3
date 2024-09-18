using FluentValidation;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Validations
{
    public class PerformanceValidator : AbstractValidator<PerformanceEntity>
    {
        public PerformanceValidator()
        {
            RuleFor(x => x.FkPlaySlug)
                .NotEmpty().WithMessage("PlayId is required.");

            RuleFor(x => x.Audience)
                .GreaterThan(0).WithMessage("Audience must be at least 1.");
        }
    }
}
