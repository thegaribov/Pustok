using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using System.Linq;

namespace Pustok.Areas.Admin.Controllers;

[Route("admin/orders")]
[Authorize(Roles = RoleNames.SuperAdmin)]
[Area("Admin")]
public class OrderController : Controller
{
    private readonly PustokDbContext _pustokDbContext;

    public OrderController(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    #region Orders

    [HttpGet]
    public IActionResult Index()
    {
        var orders = _pustokDbContext.Orders
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
    public IActionResult Edit([FromRoute] int id, [FromForm] OrderStatus status)
    {
        var order = _pustokDbContext.Orders
           .Include(o => o.User)
           .FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        order.Status = status;

        _pustokDbContext.SaveChanges();


        return RedirectToAction("Index");
    }

    #endregion
}
