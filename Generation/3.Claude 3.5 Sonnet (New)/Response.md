I'll help you create a RESTful API with the specified endpoints using .NET 8, including proper validation, error handling, and a Dockerfile for containerization.


```csharp
// Program.cs
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Text.Json.Serialization;

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

// Models
public record CalculationRequest
{
    [JsonPropertyName("number1")]
    public decimal Number1 { get; init; }

    [JsonPropertyName("number2")]
    public decimal Number2 { get; init; }
}

public record CalculationResponse
{
    [JsonPropertyName("result")]
    public decimal Result { get; init; }
}

// Validator
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

// Endpoints
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

// Calculator.csproj
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="FluentValidation" Version="11.9.0" />
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>
</Project>

// Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["Calculator.csproj", "./"]
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Calculator.dll"]

```

I've created a complete solution for your calculator API. Here are the key features and design decisions:

1. API Design:
   - Used POST endpoints for both operations as they modify state (perform calculations)
   - Body parameters instead of URI parameters because:
     - Better type safety
     - Easier to extend in the future
     - Better handling of decimal numbers
     - More secure (parameters not visible in logs/browser history)

2. Input Validation:
   - Used FluentValidation for robust input validation
   - Validates that both numbers are valid decimals
   - Returns proper validation error responses

3. Error Handling:
   - Global exception handling middleware
   - Specific handling for overflow exceptions
   - Returns problem details format for errors
   - Status code pages middleware for unhandled cases

4. Performance Considerations:
   - Minimal API approach for reduced overhead
   - Uses record types for improved memory usage
   - Async validation for better scalability
   - Invariant globalization enabled for better number handling

5. Documentation:
   - OpenAPI/Swagger integration
   - API endpoint documentation
   - Clear naming conventions

6. Docker Support:
   - Multi-stage build for smaller final image
   - Uses official .NET 8 base images
   - Proper layer caching for faster builds
   - Exposes both HTTP and HTTPS ports

To use the API:

1. Build the Docker image:
```bash
docker build -t calculator-api .
```

2. Run the container:
```bash
docker run -p 8080:80 calculator-api
```

3. Example API calls:
```bash
# Addition
curl -X POST http://localhost:8080/add \
  -H "Content-Type: application/json" \
  -d '{"number1": 10.5, "number2": 5.5}'

# Subtraction
curl -X POST http://localhost:8080/subtract \
  -H "Content-Type: application/json" \
  -d '{"number1": 10.5, "number2": 5.5}'
```

The API will also be available with Swagger UI at `http://localhost:8080/swagger` when running in development mode.

Would you like me to add any additional features or make any modifications to the implementation?