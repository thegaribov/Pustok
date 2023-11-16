using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using System.Linq;

namespace Pustok.Controllers.Client;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SingleProduct(int id, [FromServices] PustokDbContext pustokDbContext)
    {
        var product = pustokDbContext.Products
            .Include(p => p.Category)
            .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
            .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}
