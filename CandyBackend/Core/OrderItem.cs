namespace CandyBackend.Core;

public class OrderItem
{
    public long Id { get; init; }

    public long Position { get; set; }

    public string Description { get; set; } = null!;

    public long Price { get; set; }
}