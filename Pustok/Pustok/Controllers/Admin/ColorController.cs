using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using System.Linq;

namespace Pustok.Controllers.Admin;


[Route("admin/colors")]
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

        return View("Views/Admin/Color/Colors.cshtml", colors);
    }
}
