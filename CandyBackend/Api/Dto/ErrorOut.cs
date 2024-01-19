namespace CandyBackend.Api.Dto;

public record ErrorOut
{
    public required string Message { get; init; }
}