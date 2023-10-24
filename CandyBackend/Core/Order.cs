namespace CandyBackend.Core;

public class Order
{
    public long Id { get; init; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public long OrderTotal { get; set; }

    public List<OrderItem> Items { get; set; } = null!;
}