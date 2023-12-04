using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System;
using System.Linq;

namespace Pustok.Controllers.Client;

[Route("order")]
[Authorize]
public class OrderController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;

    public OrderController(
        PustokDbContext pustokDbContext,
        IUserService userService,
        IOrderService orderService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
        _orderService = orderService;
    }

    [HttpGet("execute")]
    public IActionResult Execute()
    {
        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.New,
            User = _userService.CurrentUser,
            TrackingCode = _orderService.GenerateAndGetTrackingCode(),
        };

        var basketProducts = _pustokDbContext.BasketProducts
            .Where(bp => bp.User == _userService.CurrentUser)
            .ToList();

        order.OrderProducts = basketProducts
            .Select(bp => new OrderProduct
                {
                    ColorId = bp.ColorId,
                    SizeId = bp.SizeId,
                    ProductId = bp.ProductId,
                    Quantity = bp.Quantity,
                    Order = order
                })
            .ToList();

        _pustokDbContext.Orders.Add(order);
        _pustokDbContext.BasketProducts.RemoveRange(basketProducts);

        var notifications = _orderService.CreateOrderNotifications(order);

        _pustokDbContext.SaveChanges();

        return RedirectToAction("Orders", "Dashboard");
    }
}
