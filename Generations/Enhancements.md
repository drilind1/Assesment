# Enhancements

## Removing Dependencies
The project included a dependency to the `Carter` project for add the endpoints, to remove this dependency I changed to an simple extension method.
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

