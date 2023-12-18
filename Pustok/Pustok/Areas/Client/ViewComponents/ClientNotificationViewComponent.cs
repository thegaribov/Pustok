using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Client.ViewComponents;

public class ClientNotificationViewComponent : ViewComponent
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IUserService _userService;

    public ClientNotificationViewComponent(PustokDbContext pustokDbContext, IUserService userService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var notifications = _pustokDbContext.Notifications
            .Where(n => n.User == _userService.CurrentUser)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();

        return View(notifications);
    }
}
