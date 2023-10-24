namespace CandyBackend.Api.Dto;

public class OrderItemDto
{
    public long Position { get; set; }

    public required string Description { get; set; }

    public long Price { get; set; }
}