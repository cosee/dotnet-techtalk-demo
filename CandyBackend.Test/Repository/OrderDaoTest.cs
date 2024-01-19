using CandyBackend.Core;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using CandyBackend.Test.Core;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CandyBackend.Test.Repository;

public class OrderDaoTest : IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture;

    public OrderDaoTest(ApplicationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SaveAndRetrieveOrder()
    {
        using var scope = _fixture.factory.Services.CreateScope();

        var candyDao = scope.ServiceProvider.GetRequiredService<ICandyDao>();
        var orderDao = scope.ServiceProvider.GetRequiredService<IOrderDao>();

        var candy = candyDao.Save(ModelHelper.BuildCandy(0, "c1", 100));

        List<OrderItem> orderItems = [new OrderItem { Position = 0, Price = candy.Price, Description = candy.Name }];
        var order = ModelHelper.BuildOrder("Test", "test@cosee.biz", orderItems);
        var persistedOrder = orderDao.Save(order);
        persistedOrder.Should().NotBeNull()
            .And.Match<Order>(o => o.Name == order.Name)
            .And.Match<Order>(o => o.Mail == order.Mail)
            .And.Match<Order>(o => o.CreatedAt == order.CreatedAt)
            .And.Match<Order>(o => o.OrderTotal == order.OrderTotal)
            .And.Match<Order>(o => o.Items.Count == order.Items.Count);
    }
}