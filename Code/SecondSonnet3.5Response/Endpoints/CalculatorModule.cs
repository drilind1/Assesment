using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public class CalculatorModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/add", async ([FromBody] CalculationRequest request, IValidator<CalculationRequest> validator) =>
        {
            try
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(new ErrorResponse
                    {
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray()
                    });
                }

                var result = request.Number1 + request.Number2;
                return Results.Ok(new CalculationResponse
                {
                    Result = result,
                    Operation = "Addition"
                });
            }
            catch (Exception ex)
            {
                // Results.StatusCode(500, new ErrorResponse
                // {
                //     Message = "An error occurred while processing your request",
                //     Errors = new[] { ex.Message }
                // });
                return Results.StatusCode(500);
            }
        })
        .WithName("Add")
        .WithOpenApi();

        app.MapPost("/subtract", async ([FromBody] CalculationRequest request, IValidator<CalculationRequest> validator) =>
        {
            try
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(new ErrorResponse
                    {
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray()
                    });
                }

                var result = request.Number1 - request.Number2;
                return Results.Ok(new CalculationResponse
                {
                    Result = result,
                    Operation = "Subtraction"
                });
            }
            catch (Exception ex)
            {
                // Fixed: Incorrect usage/ compile error
                // an exception log would also be good to be here and log the stacktrace
                // Results.StatusCode(500, new ErrorResponse
                // {
                //     Message = "An error occurred while processing your request",
                //     Errors = new[] { ex.Message }
                // });
                return Results.StatusCode(500);
            }
        })
        .WithName("Subtract")
        .WithOpenApi();
    }
}