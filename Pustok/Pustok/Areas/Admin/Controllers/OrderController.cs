using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.ViewModels.Notification;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Hubs;
using Pustok.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Admin.Controllers;

[Route("admin/orders")]
[Authorize(Roles = RoleNames.SuperAdmin)]
[Area("Admin")]
public class OrderController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;
    private readonly IHubContext<AlertHub> _alertHubContext;

    public OrderController(
        PustokDbContext pustokDbContext,
        INotificationService notificationService,
        IUserService userService,
        IHubContext<AlertHub> alertHubContext)
    {
        _pustokDbContext = pustokDbContext;
        _notificationService = notificationService;
        _userService = userService;
        _alertHubContext = alertHubContext;
    }

    #region Orders

    [HttpGet]
    public IActionResult Index()
    {
        var orders = _pustokDbContext.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();

        return View(orders);
    }

    #endregion

    #region Edit status

    [HttpGet("{id}/edit")]
    public IActionResult Edit(int id)
    {
        var order = _pustokDbContext.Orders
            .Include(o => o.User)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();


        return View(order);
    }

    [HttpPost("{id}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] OrderStatus status)
    {
        var order = _pustokDbContext.Orders
           .Include(o => o.User)
           .FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        order.Status = status;

        var notification = _notificationService.CreateNotificationForOrderUpdate(order);
        await _notificationService.SendNotificationAsync(notification);
        

        _pustokDbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    #endregion
}
