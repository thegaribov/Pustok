using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using System.Linq;

namespace Pustok.Controllers.Admin;

[Route("admin/sizes")]
public class SizeController : Controller
{
    private readonly PustokDbContext _pustokDbContext;

    public SizeController(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    [HttpGet]
    public IActionResult Sizes()
    {
        var sizes = _pustokDbContext.Sizes.ToList();

        return View("Views/Admin/Size/Sizes.cshtml", sizes);
    }
}
