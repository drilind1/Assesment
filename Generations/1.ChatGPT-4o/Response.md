Here's a basic .NET 6 Web API project that implements your requirements. We'll use a `POST` method with a JSON body for both endpoints, as this approach is generally preferable for structured input, easy validation, and future extensibility. Error handling, input validation, and performance optimization are implemented, followed by a `Dockerfile` for containerization.

---

### Step 1: Setting up the Controller

```csharp
// MathController.cs
using Microsoft.AspNetCore.Mvc;

namespace MathApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] MathRequest request)
        {
            if (request == null || !request.IsValid())
                return BadRequest("Invalid input. Please ensure two numbers are provided.");

            try
            {
                // Performing the addition
                var result = request.Number1 + request.Number2;
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                // Log error in real-world applications
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] MathRequest request)
        {
            if (request == null || !request.IsValid())
                return BadRequest("Invalid input. Please ensure two numbers are provided.");

            try
            {
                // Performing the subtraction
                var result = request.Number1 - request.Number2;
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                // Log error in real-world applications
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class MathRequest
    {
        public double? Number1 { get; set; }
        public double? Number2 { get; set; }

        // Input validation
        public bool IsValid()
        {
            return Number1.HasValue && Number2.HasValue;
        }
    }
}
```

### Step 2: `Program.cs` - Setting Up the Web API Project

In a new `.NET 6` project, `Program.cs` will look like this:

```csharp
// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adding necessary services for controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware for routing
app.UseRouting();
app.MapControllers();

app.Run();
```

### Step 3: Dockerfile

The following `Dockerfile` builds a clean image for easy deployment:

```dockerfile
# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code and build the app
COPY . .
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the API port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "MathApi.dll"]
```

### Step 4: Running the Docker Image

After creating the Dockerfile, use these commands to build and run the container:

```bash
# Build the Docker image
docker build -t mathapi .

# Run the Docker container
docker run -d -p 8080:80 mathapi
```

This will expose the API on port `8080` on your machine.

### Testing the Endpoints

- **Add**: Send a `POST` request to `http://localhost:8080/math/add` with body:
    ```json
    {
        "number1": 10,
        "number2": 5
    }
    ```
- **Subtract**: Send a `POST` request to `http://localhost:8080/math/subtract` with body:
    ```json
    {
        "number1": 10,
        "number2": 5
    }
    ```

Both endpoints will return a JSON result, and validation will respond with `400 Bad Request` if either input is missing or invalid. This approach includes error handling and is optimized for performance, ensuring lightweight operations on each request.