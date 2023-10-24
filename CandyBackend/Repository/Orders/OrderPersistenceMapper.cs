using AutoMapper;
using CandyBackend.Core;

namespace CandyBackend.Repository.Orders;

public class OrderPersistenceMapper : Profile
{
    public OrderPersistenceMapper()
    {
        CreateMap<OrderEntity, Order>()
            .ForMember(entity => entity.Items, opt => opt.MapFrom(order => order.OrderItems));
        CreateMap<Order, OrderEntity>()
            .ForMember(order => order.OrderItems, opt => opt.MapFrom(entity => entity.Items));

        CreateMap<OrderItemEntity, OrderItem>();
        CreateMap<OrderItem, OrderItemEntity>()
            .ForMember(item => item.OrderId, opt => opt.Ignore());
    }
}