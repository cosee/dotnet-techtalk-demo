using CandyBackend.Api.Dto;
using CandyBackend.Core;

namespace CandyBackend.Test.Core;

public static class ModelHelper
{
    public static Candy BuildCandy(long id, string name, long price)
    {
        return new Candy
        {
            Id = id,
            Name = name,
            Price = price,
        };
    }

    public static OrderIn BuildOrderIn(string name, string mail, List<long> candyIds)
    {
        return new OrderIn
        {
            Name = name,
            Mail = mail,
            CandyIds = candyIds,
        };
    }

    public static Order BuildOrder(string name, string mail, List<OrderItem> orderItems)
    {
        return new Order
        {
            Name = name,
            Mail = mail,
            Items = orderItems,
            CreatedAt = DateTime.Now,
            OrderTotal = orderItems.Sum(i => i.Price),
        };
    }
}