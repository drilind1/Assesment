To create a RESTful API in .NET with two endpoints (`/add` and `/subtract`) that performs basic arithmetic operations, here’s an approach with best practices in input validation, error handling, and performance considerations. 

### Project Structure

We'll set up a minimal API project in `.NET 6` or later, given its simplicity and efficiency for small RESTful services.

#### 1. **Create a new .NET project**:
```bash
dotnet new webapi -n ArithmeticApi
cd ArithmeticApi
```

### API Endpoints Implementation

Here’s the main code in `Program.cs` to define the `Add` and `Subtract` endpoints with proper validation and error handling.

```csharp
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
```

#### Explanation
- **Endpoints**: Defined as POST for both `/add` and `/subtract`, which helps in ensuring idempotency and security.
- **Validation**: The `ArithmeticRequest` record validates that both `Number1` and `Number2` are provided.
- **Error Handling**: Returns a `400 Bad Request` with an error message if validation fails.
- **Response**: Returns a JSON object with the result of the operation.

### Dockerfile

To create a Docker image of this API, use the following `Dockerfile`:

```Dockerfile
# Use official .NET runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

# Restore dependencies
RUN dotnet restore "ArithmeticApi.csproj"

# Build the application
RUN dotnet publish "ArithmeticApi.csproj" -c Release -o /app/publish

# Final stage / runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ArithmeticApi.dll"]
```

#### Explanation of Dockerfile
- **Base Image**: Uses a `.NET runtime` image to keep the final image lean.
- **Build Image**: Uses a separate `.NET SDK` image for building, ensuring dependencies are only restored and built once.
- **Expose Port**: Opens port `80` to allow the container to accept requests.
- **Multi-stage Build**: Ensures a smaller final image with only the runtime and built artifacts.

### Build and Run the Docker Image

To build and run the image locally, follow these steps:

1. **Build the Docker Image**:
   ```bash
   docker build -t arithmetic-api .
   ```

2. **Run the Docker Container**:
   ```bash
   docker run -d -p 8080:80 --name arithmetic-api arithmetic-api
   ```

### Testing the Endpoints

#### Example Request to `/add`
```bash
curl -X POST http://localhost:8080/add -H "Content-Type: application/json" -d '{ "Number1": 5, "Number2": 3 }'
```

#### Example Request to `/subtract`
```bash
curl -X POST http://localhost:8080/subtract -H "Content-Type: application/json" -d '{ "Number1": 10, "Number2": 4 }'
```

Both requests should return JSON responses with the computed `result`.