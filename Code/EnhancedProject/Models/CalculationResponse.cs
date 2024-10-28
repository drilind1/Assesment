namespace EnhancedProject.Models;

public record CalculationResponse
{
    public double Result { get; init; }
    public string Operation { get; init; }
}