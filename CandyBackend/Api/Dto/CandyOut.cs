namespace CandyBackend.Api.Dto;

public record CandyOut
{
    public required long Id { get; init; }

    public required string Name { get; init; }
    
    public required long Price { get; init; }
}