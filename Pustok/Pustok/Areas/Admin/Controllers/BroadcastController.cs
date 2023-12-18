using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.ViewModels.BroadcastMessage;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Admin.Controllers;

[Route("admin/broadcast-messages")]
[Authorize(Roles = RoleNames.SuperAdmin)]
[Area("Admin")]
public class BroadcastController : Controller
{
    private readonly INotificationService _notificationService;
    private readonly PustokDbContext _pustokDbContext;

    public BroadcastController(
        INotificationService notificationService, 
        PustokDbContext pustokDbContext)
    {
        _notificationService = notificationService;
        _pustokDbContext = pustokDbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var broadcastMessages = _pustokDbContext.BroadcastMessages
            .Include(bc => bc.Notifications)
            .OrderByDescending(bcm => bcm.CreatedAt)
            .ToList();

        return View(broadcastMessages);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(BroadcastMessageViewModel model)
    {
        var broadcastMessage = new BroadcastMessage
        {
            Message = model.Message,
            CreatedAt = DateTime.UtcNow
        };

        _notificationService.CreateNotificationsForBroadcasting(broadcastMessage);
        await _notificationService.SendBroadcastNotifications(broadcastMessage);

        _pustokDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
