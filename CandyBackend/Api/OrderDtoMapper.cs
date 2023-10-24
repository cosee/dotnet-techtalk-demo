using AutoMapper;
using CandyBackend.Api.Dto;
using CandyBackend.Core;

namespace CandyBackend.Api;

public class OrderDtoMapper : Profile
{
    public OrderDtoMapper()
    {
        CreateMap<Order, OrderOut>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}