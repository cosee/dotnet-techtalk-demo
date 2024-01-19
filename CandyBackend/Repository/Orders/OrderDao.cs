using AutoMapper;
using CandyBackend.Core;
using Microsoft.EntityFrameworkCore;

namespace CandyBackend.Repository.Orders;

public class OrderDao : IOrderDao
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public OrderDao(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Order Save(Order order)
    {
        using var transaction = _context.Database.BeginTransaction();

        var orderEntity = _mapper.Map<OrderEntity>(order);
        _context.Order.Add(orderEntity);
        _context.SaveChanges();

        transaction.Commit();

        return _mapper.Map<Order>(orderEntity);
    }

    public Order GetOrder(long candyId)
    {
        return _mapper.Map<Order>(_context.Order.Include(o => o.OrderItems).First(c => c.Id == candyId));
    }

    public List<Order> GetOrders()
    {
        var orders = _context.Order.Include(o => o.OrderItems);
        return _mapper.Map<List<Order>>(orders);
    }
}