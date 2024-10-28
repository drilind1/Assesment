using FirstSonnet3._5Response;
using FluentValidation;

public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
{
    public CalculationRequestValidator()
    {
        RuleFor(x => x.Number1)
            .NotNull()
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Number1 must be a valid decimal number");

        RuleFor(x => x.Number2)
            .NotNull()
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Number2 must be a valid decimal number");
    }
}