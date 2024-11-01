using EnhancedProject.Models;

namespace EnhancedProject.Services;

public interface ICalculatorService
{
    public Task<Result<CalculationResponse>> Addition(CalculationRequest calculationRequest);
    public Task<Result<CalculationResponse>> Subtraction(CalculationRequest calculationRequest);
}