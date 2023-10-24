using CandyBackend.Core;

namespace CandyBackend.Repository.Orders;

public interface IOrderDao
{
    Order Save(Order order);

    Order GetOrder(long candyId);

    List<Order> GetOrders();
}