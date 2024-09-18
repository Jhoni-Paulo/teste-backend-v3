using FluentValidation;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Validations
{
    public class PlayValidator : AbstractValidator<PlayEntity>
    {
        public PlayValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.");

            RuleFor(x => x.Lines)
                .GreaterThan(0).WithMessage("Lines must be at least 1.");

        }
    }
}
