namespace CandyBackend.Api.Dto;

public record OrderItemDto
{
    public required long Position { get; init; }

    public required string Description { get; init; }

    public required long Price { get; init; }
}