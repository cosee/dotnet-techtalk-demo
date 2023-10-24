using CandyBackend.Api.Dto;

namespace CandyBackend.Core;

public interface IOrderService
{
    Order Save(OrderIn orderIn);
    
    List<Order> GetOrders();
}