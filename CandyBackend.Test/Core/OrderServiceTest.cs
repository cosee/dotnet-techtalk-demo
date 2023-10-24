using CandyBackend.Api.Dto;
using CandyBackend.Core;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using Moq;
using NUnit.Framework;

namespace CandyBackend.Test.Core;

public class OrderServiceTest
{
    private OrderService _orderService = null!;
    private Mock<IOrderDao> _orderDaoMock = null!;
    private Mock<ICandyDao> _candyDaoMock = null!;

    [SetUp]
    public void SetUp()
    {
        _orderDaoMock = new Mock<IOrderDao>();
        _candyDaoMock = new Mock<ICandyDao>();
        _orderService = new OrderService(_orderDaoMock.Object, _candyDaoMock.Object);
    }
    
    [Test]
    public void CalculateOrderTotal()
    {
        var mars = new Candy
        {
            Id = 0,
            Name = "Mars",
            Price = 90
        };
        var snickers = new Candy
        {
            Id = 1,
            Name = "Snickers",
            Price = 120
        };
        _candyDaoMock.Setup(dao => dao.FindCandiesById(new List<long> { 0, 1 }))
            .Returns(new List<Candy> { mars, snickers });

        Order? savedOrder = null;
        _orderDaoMock.Setup(dao => dao.Save(It.IsAny<Order>()))
            .Callback<Order>(order => savedOrder = order);
        
        var orderIn = new OrderIn
        {
            FirstName = "Test",
            LastName = "Test",
            CandyIds = new List<long> { 0, 1 }
        };

        _orderService.Save(orderIn);
        
        _orderDaoMock.Verify(dao => dao.Save(It.IsAny<Order>()), Times.Once);
        Assert.IsNotNull(savedOrder);
        Assert.That(savedOrder.FirstName, Is.EqualTo("Test"));
        Assert.That(savedOrder.LastName, Is.EqualTo("Test"));
        Assert.That(savedOrder.OrderTotal, Is.EqualTo(210));
    }
}