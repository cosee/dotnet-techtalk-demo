namespace CandyBackend.Core;

public class Order
{
    public long Id { get; init; }

    public string Name { get; set; } = null!;
    public string Mail { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public long OrderTotal { get; set; }

    public List<OrderItem> Items { get; set; } = null!;
}