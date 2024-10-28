using System.Text.Json.Serialization;

namespace FirstSonnet3._5Response;

public record CalculationRequest
{
    [JsonPropertyName("number1")]
    public decimal Number1 { get; init; }

    [JsonPropertyName("number2")]
    public decimal Number2 { get; init; }
}