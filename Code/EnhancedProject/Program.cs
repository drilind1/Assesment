using EnhancedProject.Endpoints;
using EnhancedProject.Services;
using FluentValidation;
using EnhancedProject.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.AddCalculatorEndpoints();
app.Run();