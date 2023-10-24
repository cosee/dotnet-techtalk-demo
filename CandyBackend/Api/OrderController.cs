using AutoMapper;
using CandyBackend.Api.Dto;
using CandyBackend.Core;
using Microsoft.AspNetCore.Mvc;

namespace CandyBackend.Api;

[ApiController]
[Route("/orders")]
public class OrderController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IMapper mapper, IOrderService orderService)
    {
        _mapper = mapper;
        _orderService = orderService;
    }
    
    [HttpGet]
    public List<OrderOut> Get()
    {
        return _mapper.Map<List<OrderOut>>(_orderService.GetOrders());
    }
    
    [HttpPost]
    public OrderOut Save(OrderIn orderIn)
    {
        return _mapper.Map<OrderOut>(_orderService.Save(orderIn));
    }
}