using EnhancedProject.Models;
using EnhancedProject.Services;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace EnhancedProject.UnitTests;

public class CalculatorServiceTests
{
    private readonly CalculatorService _calculatorService;
    private readonly Mock<IValidator<CalculationRequest>> _validatorMock;

    public CalculatorServiceTests()
    {
        _validatorMock = new Mock<IValidator<CalculationRequest>>();
        Mock<ILogger<CalculatorService>> loggerMock = new();
        _calculatorService = new CalculatorService(loggerMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Addition_AddingTwoNumbers_ReturnsSum()
    {
        // Arrange
        const double number1 = 10;
        const double number2 = 20;
        const double expectedResult = 30;
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2,
        };
        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act
        var calculationResponse = await _calculatorService.Addition(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeTrue();
        calculationResponse.IsFailure.Should().BeFalse();
        calculationResponse.Value?.Should().NotBeNull();
        calculationResponse.Value!.Result.Should().Be(expectedResult);
        calculationResponse.Value!.Operation.Should().Be(Operations.Addition.ToString());
    }

    [Fact]
    public async Task Addition_AddingWhenNumberIsInvalid_ReturnsFailure()
    {
        // Arrange
        const double number1 = 10;
        double? number2 = null;
        const string errorMessage = "Number2 is invalid.";
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2
        };

        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(number2), errorMessage)]));

        // Act
        var calculationResponse = await _calculatorService.Addition(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeFalse();
        calculationResponse.IsFailure.Should().BeTrue();
        calculationResponse.Errors.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Addition_AddingWhenValidatingThrowException_ReturnsFailure()
    {
        // Arrange
        const double number1 = 10;
        double? number2 = null;
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2
        };

        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentOutOfRangeException());

        // Act
        var calculationResponse = await _calculatorService.Addition(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeFalse();
        calculationResponse.IsFailure.Should().BeTrue();
        calculationResponse.Errors.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Subtraction_SubtractingTwoNumbers_ReturnsSum()
    {
        // Arrange
        const double number1 = 10;
        const double number2 = 20;
        const double expectedResult = -10;
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2,
        };
        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act
        var calculationResponse = await _calculatorService.Subtraction(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeTrue();
        calculationResponse.IsFailure.Should().BeFalse();
        calculationResponse.Value?.Should().NotBeNull();
        calculationResponse.Value!.Result.Should().Be(expectedResult);
        calculationResponse.Value!.Operation.Should().Be(Operations.Subtraction.ToString());
    }

    [Fact]
    public async Task Subtraction_SubtractingWhenNumberIsInvalid_ReturnsFailure()
    {
        // Arrange
        const double number1 = 10;
        double? number2 = null;
        const string errorMessage = "Number2 is invalid.";
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2
        };

        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(number2), errorMessage)]));

        // Act
        var calculationResponse = await _calculatorService.Subtraction(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeFalse();
        calculationResponse.IsFailure.Should().BeTrue();
        calculationResponse.Errors.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Subtraction_SubtractingWhenValidatingThrowException_ReturnsFailure()
    {
        // Arrange
        const double number1 = 10;
        double? number2 = null;
        var calculationRequest = new CalculationRequest
        {
            Number1 = number1, Number2 = number2
        };

        _validatorMock.Setup(x => x.ValidateAsync(calculationRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentOutOfRangeException());

        // Act
        var calculationResponse = await _calculatorService.Subtraction(calculationRequest);

        // Assert
        calculationResponse.IsSuccess.Should().BeFalse();
        calculationResponse.IsFailure.Should().BeTrue();
        calculationResponse.Errors.Should().NotBeNullOrEmpty();
    }
}