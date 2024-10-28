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