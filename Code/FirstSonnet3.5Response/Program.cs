using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Text.Json.Serialization;
using FirstSonnet3._5Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure error handling middleware
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/add", async ([FromBody] CalculationRequest request, IValidator<CalculationRequest> validator) =>
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        try
        {
            var result = request.Number1 + request.Number2;
            return Results.Ok(new CalculationResponse { Result = result });
        }
        catch (OverflowException)
        {
            return Results.Problem(
                title: "Calculation Error",
                detail: "The calculation resulted in an overflow",
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    })
    .WithName("Add")
    .WithOpenApi();

app.MapPost("/subtract", async ([FromBody] CalculationRequest request, IValidator<CalculationRequest> validator) =>
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        try
        {
            var result = request.Number1 - request.Number2;
            return Results.Ok(new CalculationResponse { Result = result });
        }
        catch (OverflowException)
        {
            return Results.Problem(
                title: "Calculation Error",
                detail: "The calculation resulted in an overflow",
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    })
    .WithName("Subtract")
    .WithOpenApi();

app.Run();