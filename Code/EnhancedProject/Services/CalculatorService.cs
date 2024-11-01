using EnhancedProject.Models;
using FluentValidation;

namespace EnhancedProject.Services;

public class CalculatorService(ILogger<CalculatorService> logger, IValidator<CalculationRequest> validator) : ICalculatorService
{
    public async Task<Result<CalculationResponse>> Addition(CalculationRequest request)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result<CalculationResponse>
                    .Failure(validationResult.Errors.Select(x => x.ErrorMessage)
                        .ToArray());
            }

            var result = request.Number1 + request.Number2;

            return Result<CalculationResponse>.Success(
                new CalculationResponse
                {
                    Result = result!.Value,
                    Operation = "Addition"
                });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while calculating operation");

            return Result<CalculationResponse>.Failure("An error occurred while processing your request");
        }
    }

    public async Task<Result<CalculationResponse>> Subtraction(CalculationRequest request)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result<CalculationResponse>
                    .Failure(validationResult.Errors.Select(x => x.ErrorMessage)
                        .ToArray());
            }

            var result = request.Number1 - request.Number2;

            return Result<CalculationResponse>.Success(
                new CalculationResponse
                {
                    Result = result!.Value,
                    Operation = "Addition"
                });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while calculating operation");

            return Result<CalculationResponse>.Failure("An error occurred while processing your request");
        }
    }
}