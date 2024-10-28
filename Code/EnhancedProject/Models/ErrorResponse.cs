public record ErrorResponse
{
    public string Message { get; init; }
    public string[] Errors { get; init; }
}