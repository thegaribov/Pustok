using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Hubs;
using Pustok.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Client.Controllers;

[Authorize]
[Route("order")]
[Area("Client")]
public class OrderController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly IHubContext<AlertHub> _alertHubContext;
    private readonly INotificationService _notificationService;

    public OrderController(
        PustokDbContext pustokDbContext,
        IUserService userService,
        IOrderService orderService,
        IHubContext<AlertHub> alertHubContext,
        INotificationService notificationService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
        _orderService = orderService;
        _alertHubContext = alertHubContext;
        _notificationService = notificationService;
    }

    [HttpGet("execute")]
    public async Task<IActionResult> ExecuteAsync()
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

        var notifications = _notificationService.CreateNotificationsForOrderCreation(order);
        await _notificationService.SendNotificationsAsync(notifications);

        _pustokDbContext.SaveChanges();

        return RedirectToAction("Orders", "Dashboard");
    }
}
