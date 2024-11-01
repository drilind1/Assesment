using FluentValidation;
using EnhancedProject.Models;

namespace EnhancedProject.Validators;

public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
{
    public CalculationRequestValidator()
    {
        RuleFor(x => x.Number1)
            .NotNull()
            .WithMessage("Number1 is required");

        RuleFor(x => x.Number2)
            .NotNull()
            .WithMessage("Number2 is required");
    }
}