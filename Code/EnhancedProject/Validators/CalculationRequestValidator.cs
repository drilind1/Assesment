using FluentValidation;
using EnhancedProject.Models;

namespace EnhancedProject.Validators;

public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
{
    public CalculationRequestValidator()
    {
        RuleFor(x => x.Number1)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Number1 is required")
            .Must(x => !double.IsInfinity(x!.Value))
            .WithMessage("Number1 cannot be infinity")
            .Must(x => !double.IsNaN(x!.Value))
            .WithMessage("Number1 must be a valid number");

        RuleFor(x => x.Number2)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Number2 is required")
            .Must(x => !double.IsInfinity(x!.Value))
            .WithMessage("Number2 cannot be infinity")
            .Must(x => !double.IsNaN(x!.Value))
            .WithMessage("Number2 must be a valid number");
    }
}