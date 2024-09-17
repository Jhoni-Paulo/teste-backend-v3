using FluentValidation;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Validations
{
    public class InvoiceValidator : AbstractValidator<InvoiceEntity>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(x => x.Performances)
                .NotNull().WithMessage("Performances list cannot be null.")
                .Must(p => p != null && p.Count > 0).WithMessage("Invoice must contain at least one performance.");
        }
    }
}
