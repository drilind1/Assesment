using System.Text.Json.Serialization;

public record CalculationResponse
{
    [JsonPropertyName("result")]
    public decimal Result { get; init; }
}