namespace CandyBackend.Api.Dto;

public class OrderOut
{
    public long Id { get; init; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public long OrderTotal { get; set; }

    public required List<OrderItemDto> Items { get; set; }
}