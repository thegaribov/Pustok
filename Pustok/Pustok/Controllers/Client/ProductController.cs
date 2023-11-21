using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.ViewModels.Product;
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

    public IActionResult ProductDetails(int id, [FromServices] PustokDbContext pustokDbContext)
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

        var productViewModel = new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Rating = product.Rating,
            CategoryName = product.Category.Name,
            Colors = product.ProductColors
                .Select(pc =>
                    new ProductDetailsViewModel.ColorDetailsViewModel
                    {
                        Id = pc.ColorId,
                        Name = pc.Color.Name
                    })
                .ToList(),
            Sizes = product.ProductSizes
                .Select(pc =>
                    new ProductDetailsViewModel.SizeDetailsViewModel
                    {
                        Id = pc.SizeId,
                        Name = pc.Size.Name
                    })
                .ToList()
        };

        return Json(productViewModel);
    }

    public IActionResult ProductDetailsModalView(int id, [FromServices] PustokDbContext pustokDbContext)
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

        var productViewModel = new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Rating = product.Rating,
            CategoryName = product.Category.Name,
            ImageNameInFileSystem = product.ImageNameInFileSystem,
            Colors = product.ProductColors
                .Select(pc =>
                    new ProductDetailsViewModel.ColorDetailsViewModel
                    {
                        Id = pc.ColorId,
                        Name = pc.Color.Name
                    })
                .ToList(),
            Sizes = product.ProductSizes
                .Select(pc =>
                    new ProductDetailsViewModel.SizeDetailsViewModel
                    {
                        Id = pc.SizeId,
                        Name = pc.Size.Name
                    })
                .ToList()
        };

        return PartialView("Partials/Client/_ProductDetailsModalBodyPartialView", productViewModel);
    }
}
