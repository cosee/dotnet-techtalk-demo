using System.ComponentModel.DataAnnotations.Schema;
using CandyBackend.Repository.Candies;

namespace CandyBackend.Repository.Orders;

[Table("order")]
public class OrderEntity
{
    public long Id { get; init; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public long OrderTotal { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<OrderItemEntity> OrderItems { get; set; } = null!;
}