using CandyBackend.Core;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using FluentAssertions;
using Moq;
using Xunit;
using static CandyBackend.Test.Core.ModelHelper;

namespace CandyBackend.Test.Core;

public class OrderServiceTest
{
    private readonly OrderService _orderService;
    private readonly Mock<IOrderDao> _orderDaoMock;
    private readonly Mock<ICandyDao> _candyDaoMock;

    public OrderServiceTest()
    {
        _orderDaoMock = new Mock<IOrderDao>();
        _candyDaoMock = new Mock<ICandyDao>();
        _orderService = new OrderService(_orderDaoMock.Object, _candyDaoMock.Object);
    }

    [Fact]
    public void CalculateOrderTotal()
    {
        var mars = BuildCandy(0, "Mars", 90);
        var snickers = BuildCandy(1, "Snickers", 120);
        var candyIds = new List<long> { mars.Id, snickers.Id };
        _candyDaoMock.Setup(dao => dao.FindCandiesById(candyIds)).Returns([mars, snickers]);

        Order? savedOrder = null;
        _orderDaoMock.Setup(dao => dao.Save(It.IsAny<Order>())).Callback<Order>(order => savedOrder = order);

        var orderIn = BuildOrderIn("Maxi Musterperson", "test@cosee.biz", candyIds);

        _orderService.Save(orderIn);

        _orderDaoMock.Verify(dao => dao.Save(It.IsAny<Order>()), Times.Once);
        savedOrder.Should().NotBeNull()
            .And.Match<Order>(order => order.Name == "Maxi Musterperson")
            .And.Match<Order>(order => order.Mail == "test@cosee.biz")
            .And.Match<Order>(order => order.OrderTotal == 210);
    }
}