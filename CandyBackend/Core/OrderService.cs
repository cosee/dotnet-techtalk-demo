using CandyBackend.Api.Dto;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;

namespace CandyBackend.Core;

public class OrderService : IOrderService
{
    private readonly IOrderDao _orderDao;
    private readonly ICandyDao _candyDao;

    public OrderService(IOrderDao orderDao, ICandyDao candyDao)
    {
        _orderDao = orderDao;
        _candyDao = candyDao;
    }

    public Order Save(OrderIn orderIn)
    {
        var orderItems = _mapToOrderItems(orderIn.CandyIds);
        var order = new Order
        {
            Name = orderIn.Name,
            Mail = orderIn.Mail,
            Items = orderItems,
            OrderTotal = orderItems.Sum(item => item.Price),
            CreatedAt = DateTime.Now,
        };
        return _orderDao.Save(order);
    }

    private List<OrderItem> _mapToOrderItems(IReadOnlyList<long> candyIds)
    {
        var candies = _candyDao.FindCandiesById(candyIds.ToHashSet())
            .ToDictionary(c => c.Id);

        return candyIds.Select((candyId, index) =>
        {
            var candy = candies[candyId];
            return new OrderItem
            {
                Description = candy.Name,
                Position = index,
                Price = candy.Price
            };
        }).ToList();
    }

    public List<Order> GetOrders()
    {
        return _orderDao.GetOrders();
    }
}