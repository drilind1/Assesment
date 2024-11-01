# Enhancements

## Removing Dependencies
The project included a dependency to the `Carter` package for adding the endpoints, to remove this dependency I changed to an simple extension method to add the endpoints.

```csharp
//Program.cs

using FluentValidation;
//using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.MapCarter();
app.AddCalculatorEndpoints();
app.Run();
```

Also the `CalculatorModule.cs` was renamed to [CalculatorEndpoints.cs](/Code/EnhancedProject/Endpoints/CalculatorEndpoints.cs) for better clarity and changed to be a static class and an extension method of `IEndpointRouteBuilder`

## Error handling and logging the details.
Introduced logging in case of exceptions being thrown while processing business logic,
and return a more appropriate response, the same logic was applied for both endpoints.

```csharp
catch (Exception ex)
{
    // Results.StatusCode(500, new ErrorResponse
    // {
    //     Message = "An error occurred while processing your request",
    //     Errors = new[] { ex.Message }
    // });
    return Results.StatusCode(500);
}
```

```csharp
var loggerFactory = app.ServiceProvider.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger(nameof(CalculatorEndpoints));

catch (Exception ex)
{
    logger.LogError(ex, "Error occurred while calculating operation");

    return Results.Problem(
        detail: "An error occurred while processing your request",
        title: "Internal Server Error",
        statusCode: 500);
}
```

## Unit testing
To make the code more maintainable and to reduce the amount of bugs that can be introduced in the future
I introduced unit testing for the business logic of the calculations.

First I needed to separate the logic to a separate Service for separating concerns and making it easier to test. 

So all the Service is covered with unit testing this will make sure that any new change or feature which will be added won't break the functionality of the existing code.
