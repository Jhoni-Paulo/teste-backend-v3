using FluentValidation;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Validations
{
    public class PlayValidator : AbstractValidator<PlayEntity>
    {
        public PlayValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Lines)
                .GreaterThan(0).WithMessage("Lines must be at least 1.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid play type.");
        }
    }
}
