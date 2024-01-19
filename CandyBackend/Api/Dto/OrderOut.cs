namespace CandyBackend.Api.Dto;

public record OrderOut
{
    public required long Id { get; init; }

    public required string Name { get; init; }
    public required string Mail { get; init; }

    public required DateTime OrderDate { get; init; }

    public required long OrderTotal { get; init; }

    public required List<OrderItemDto> Items { get; init; }
}