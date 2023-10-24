using System.ComponentModel.DataAnnotations.Schema;
using CandyBackend.Repository.Candies;

namespace CandyBackend.Repository.Orders;

[Table("order")]
public class OrderEntity
{
    public long Id { get; init; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    public long OrderTotal { get; set; }

    public ICollection<OrderItemEntity> OrderItems { get; set; } = null!;
}