using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/add", (ArithmeticRequest request) =>
{
    if (!request.IsValid())
    {
        return Results.BadRequest("Invalid input. Both numbers are required and should be numeric.");
    }

    var result = request.Number1 + request.Number2;
    return Results.Ok(new { result });
});

app.MapPost("/subtract", (ArithmeticRequest request) =>
{
    if (!request.IsValid())
    {
        return Results.BadRequest("Invalid input. Both numbers are required and should be numeric.");
    }

    var result = request.Number1 - request.Number2;
    return Results.Ok(new { result });
});

app.Run();

public record ArithmeticRequest
{
    public double? Number1 { get; init; }
    public double? Number2 { get; init; }

    public bool IsValid() => Number1.HasValue && Number2.HasValue;
}