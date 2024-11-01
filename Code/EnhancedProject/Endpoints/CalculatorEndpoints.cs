using Microsoft.AspNetCore.Mvc;
using EnhancedProject.Models;
using EnhancedProject.Services;

namespace EnhancedProject.Endpoints;

public static class CalculatorEndpoints
{
    public static void AddCalculatorEndpoints(this IEndpointRouteBuilder app)
    {
        var calculatorService = app.ServiceProvider.GetRequiredService<ICalculatorService>();
        app.MapPost("/add", async ([FromBody] CalculationRequest request) =>
            {
                var result = await calculatorService.Addition(request);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("Add")
            .WithOpenApi();

        app.MapPost("/subtract", async ([FromBody] CalculationRequest request) =>
            {
                var result = await calculatorService.Subtraction(request);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("Subtract")
            .WithOpenApi();
    }
}