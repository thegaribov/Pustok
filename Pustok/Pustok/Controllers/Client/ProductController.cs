using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Extensions;
using Pustok.Services.Abstract;
using Pustok.ViewModels.Product;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers.Client;

public class ProductController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IFileService _fileService;

    public ProductController(
        PustokDbContext pustokDbContext,
        IFileService fileService)
    {
        _pustokDbContext = pustokDbContext;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(
        [FromQuery] string searchName, 
        [FromQuery] int? categoryId,
        [FromQuery] int? colorId,
        [FromQuery] decimal? priceMinRangeFilter,
        [FromQuery] decimal? priceMaxRangeFilter)
    {
        var productPageViewModel = new ProductsPageViewModel();
        productPageViewModel.SearchName = searchName;
        productPageViewModel.CategoryId = categoryId;
        productPageViewModel.ColorId = colorId;

        productPageViewModel.Products = await _pustokDbContext.Products
            .WhereNotNull(searchName, p => EF.Functions.ILike(p.Name, $"%{searchName}%"))
            .WhereNotNull(categoryId, p => p.CategoryId == categoryId)
            .WhereNotNull(colorId, p => p.ProductColors.Any(pc => pc.ColorId == colorId))
            .WhereNotNull(priceMinRangeFilter, p => p.Price > priceMinRangeFilter.Value)
            .WhereNotNull(priceMaxRangeFilter, p => p.Price > priceMinRangeFilter.Value)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Rating = p.Rating,
                ImageUrl = UploadDirectory.Products.GetUrl(p.ImageNameInFileSystem),
            })
            .ToListAsync();
        productPageViewModel.Categories = await _pustokDbContext.Categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.Products.Count,
            })
            .ToListAsync();
        productPageViewModel.Colors = await _pustokDbContext.Colors
            .Select(c => new ColorViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.ProductColors.Count
            })
            .ToListAsync();
        productPageViewModel.PriceMinRange = _pustokDbContext.Products
            .OrderBy(p => p.Price)
            .FirstOrDefault()?
            .Price;
        productPageViewModel.PriceMaxRange = _pustokDbContext.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefault()?
            .Price;

        return View(productPageViewModel);
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
