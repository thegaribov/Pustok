using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Contracts;
using Pustok.Database;
using System.Linq;

namespace Pustok.Areas.Admin.Controllers;


[Route("admin/colors")]
[Authorize(Roles = RoleNames.SuperAdmin)]
[Area("Admin")]
public class ColorController : Controller
{
    private readonly PustokDbContext _pustokDbContext;

    public ColorController(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    [HttpGet]
    public IActionResult Colors()
    {
        var colors = _pustokDbContext.Colors.ToList();

        return View(colors);
    }
}
