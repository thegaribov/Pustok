using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Extensions;
using Pustok.Helpers.Paging;
using Pustok.Services.Abstract;
using Pustok.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Client.Controllers;

[Area("Client")]
[Route("product")]
public class ProductController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IFileService _fileService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductController(
        PustokDbContext pustokDbContext,
        IFileService fileService,
        IHttpContextAccessor httpContextAccessor)
    {
        _pustokDbContext = pustokDbContext;
        _fileService = fileService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index(
        [FromQuery] string search,
        [FromQuery] int? categoryId,
        [FromQuery] int? colorId,
        [FromQuery(Name = "price-range-filter")] string priceRangeFilter,
        [FromQuery] string sort,
        [FromQuery] int? page,
        [FromQuery] int? pageSize)

    {
        (decimal? priceMinRangeFilter, decimal? priceMaxRangeFilter) = GetRanges(priceRangeFilter);

        var productPageViewModel = new ProductsPageViewModel();

        (var products, var paginator) = await GetProductsAsync();

        productPageViewModel.Products = products;
        productPageViewModel.Pagination = paginator;
        productPageViewModel.Categories = await GetCategoriesAsync();
        productPageViewModel.Colors = await GetColorsAsync();
        productPageViewModel.PriceMinRange = GetPriceMinRange();
        productPageViewModel.PriceMaxRange = GetPriceMaxRange();

        productPageViewModel.CategoryId = categoryId;
        productPageViewModel.ColorId = colorId;
        productPageViewModel.PriceMinRangeFilter = priceMinRangeFilter;
        productPageViewModel.PriceMaxRangeFilter = priceMaxRangeFilter;

        productPageViewModel.Page = page ?? 1;
        productPageViewModel.Search = search;
        productPageViewModel.PageSize = pageSize;
        productPageViewModel.Sort = sort;

        return View(productPageViewModel);

        // 1. with out keyword (Try pattern)
        // 2. Custom class, with two props
        // 3. Tuple 
        // 4. dynamic

        async Task<(List<ProductViewModel> products, Paginator<Product> paginator)> GetProductsAsync()
        {
            var productsQuery = _pustokDbContext.Products
                .WhereNotNull(search, p => EF.Functions.ILike(p.Name, $"%{search}%"))
                .WhereNotNull(categoryId, p => p.CategoryId == categoryId)
                .WhereNotNull(colorId, p => p.ProductColors.Any(pc => pc.ColorId == colorId))
                .WhereNotNull(priceMinRangeFilter, p => p.Price >= priceMinRangeFilter.Value)
                .WhereNotNull(priceMaxRangeFilter, p => p.Price <= priceMaxRangeFilter.Value);

            productsQuery = ImplementProductSorting(productsQuery, sort);
            //productsQuery = ImplementPaging(productsQuery, page);
            var paginator = new Paginator<Product>(productsQuery, page, pageSize ?? 9);
            var products = await paginator.QuerySet
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Rating = p.Rating,
                ImageUrl = UploadDirectory.Products.GetUrl(p.ImageNameInFileSystem),
            })
            .ToListAsync();

            return (products, paginator);
        }

        (decimal? priceMinRangeFilter, decimal? priceMaxRangeFilter) GetRanges(string priceRangeFilter)
        {
            if (priceRangeFilter == null)
            {
                return (null, null);
            }

            var ranges = priceRangeFilter.Split(";");

            decimal? priceMinRangeFilter = priceRangeFilter != null ? decimal.Parse(ranges[0]) : null;
            decimal? priceMaxRangeFilter = priceRangeFilter != null ? decimal.Parse(ranges[1]) : null;

            return (priceMinRangeFilter, priceMaxRangeFilter);
        }

        IOrderedQueryable<Product> ImplementProductSorting(IQueryable<Product> productQuery, string sortQuery)
        {
            if (sortQuery == null)
            {
                return productQuery.OrderByDescending(p => p.Id);
            }

            switch (sortQuery)
            {
                case "price_desc":
                    return productQuery.OrderByDescending(p => p.Price);
                case "price_asc":
                    return productQuery.OrderBy(p => p.Price);
                case "rate_desc":
                    return productQuery.OrderByDescending(p => p.Rating);
                case "rate_asc":
                    return productQuery.OrderBy(p => p.Rating);
                default:
                    throw new Exception("Sort query doesn't found");
            }

        }

        IQueryable<Product> ImplementPaging(IQueryable<Product> productQuery, int? page)
        {
            const int pageSize = 6;
            int skipCount = ((page ?? 1) - 1) * pageSize;

            return productQuery
                .Skip(skipCount)
                .Take(pageSize);
        }

        async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            return await _pustokDbContext.Categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.Products.Count,
            })
            .ToListAsync();
        }

        async Task<List<ColorViewModel>> GetColorsAsync()
        {
            return await _pustokDbContext.Colors
            .Select(c => new ColorViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.ProductColors.Count
            })
            .ToListAsync();
        }

        decimal? GetPriceMinRange()
        {
            return _pustokDbContext.Products
            .OrderBy(p => p.Price)
            .FirstOrDefault()?
            .Price;
        }

        decimal? GetPriceMaxRange()
        {
            return _pustokDbContext.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefault()?
            .Price;
        }
    }

    [HttpGet("single-product/{id}")]
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

    [HttpGet("product-details/{id}")]
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

    [HttpGet("product-details-modal/{id}")]
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
