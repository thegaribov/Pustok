using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Services.Abstract;
using System.Linq;

namespace Pustok.Controllers.Client;

[Route("dashboard")]
public class DashboardController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IUserService _userService;

    public DashboardController(PustokDbContext pustokDbContext, IUserService userService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
    }

    [HttpGet("orders")]
    public IActionResult Orders()
    {
        var orders = _pustokDbContext.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.User == _userService.GetCurrentLoggedUser())
            .ToList();

        return View(orders);
    }

    [HttpGet("orders/{id}")]
    public IActionResult OrderDetails(int id)
    {
        var orderProducts = _pustokDbContext.OrderProducts
            .Where(op => op.OrderId == id && op.Order.User == _userService.GetCurrentLoggedUser())
            .Include(op => op.Product)
            .Include(op => op.Product.Category)
            .Include(op => op.Size)
            .Include(op => op.Color)
            .ToList();

        return View(orderProducts);
    }

    public IActionResult Index()
    {
        return View();
    }
}
