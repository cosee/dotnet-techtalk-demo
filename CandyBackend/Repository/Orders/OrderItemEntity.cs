using System.ComponentModel.DataAnnotations.Schema;

namespace CandyBackend.Repository.Orders;

[Table("order_item")]
public class OrderItemEntity
{
    public long Id { get; init; }

    public long OrderId { get; init; }

    public long Position { get; init; }

    public string Description { get; set; } = null!;

    public long Price { get; set; }
}