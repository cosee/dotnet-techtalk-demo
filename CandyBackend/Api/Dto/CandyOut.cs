namespace CandyBackend.Api.Dto;

public class CandyOut
{
    public long Id { get; init; }

    public required string Name { get; init; }
    
    public required long Price { get; init; }
}