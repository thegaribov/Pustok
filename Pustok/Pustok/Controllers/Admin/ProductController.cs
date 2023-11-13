using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.ViewModels.Product;
using System.Linq;

namespace Pustok.Controllers.Admin;

[Route("admin/products")]
public class ProductController : Controller
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly ILogger<ProductController> _logger;

    public ProductController(
        PustokDbContext pustokDbContext,
        ILogger<ProductController> logger)
    {
        _pustokDbContext = pustokDbContext;
        _logger = logger;
    }

    #region Products

    [HttpGet] //admin/products
    public IActionResult Products()
    {
        var products = _pustokDbContext.Products
            .Include(p => p.Category)
            .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
            .ToList();

        return View("Views/Admin/Product/Products.cshtml", products);
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        var model = new ProductAddResponseViewModel
        {
            Categories = _pustokDbContext.Categories.ToList(),
            Colors = _pustokDbContext.Colors.ToList()
        };

        return View("Views/Admin/Product/ProductAdd.cshtml", model);
    }

    [HttpPost("add")]
    public IActionResult Add(ProductAddRequestViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");

        if (model.CategoryId != null)
        {
            var category = _pustokDbContext.Categories.FirstOrDefault(c => c.Id == model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Category doesn't exist");

                return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");
            }
        }

        try
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            _pustokDbContext.Products.Add(product);
            _pustokDbContext.SaveChanges();


            foreach (var colorId in model.SelectedColorIds)
            {
                var productColor = new ProductColor
                {
                    ColorId = colorId,
                    ProductId = product.Id
                };

                _pustokDbContext.ProductColors.Add(productColor);
                _pustokDbContext.SaveChanges();
            }

        }
        catch (PostgresException e)
        {
            _logger.LogError(e, "Postgresql Exception");

            throw e;
        }

        return RedirectToAction("Products");
    }

    #endregion

    #region Edit

    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        Product product = _pustokDbContext.Products
            .Include(p => p.ProductColors)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        var model = new ProductUpdateResponseViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Rating = product.Rating,
            Categories = _pustokDbContext.Categories.ToList(),
            CategoryId = product.CategoryId,
            SelectedColorIds = product.ProductColors.Select(pc => pc.ColorId).ToArray(),
            Colors = _pustokDbContext.Colors.ToList()
        };

        return View("Views/Admin/Product/ProductEdit.cshtml", model);
    }

    [HttpPost("edit")]
    public IActionResult Edit(ProductUpdateRequestViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/Product/ProductEdit.cshtml");

        if (model.CategoryId != null)
        {
            var category = _pustokDbContext.Categories.FirstOrDefault(c => c.Id == model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Category doesn't exist");

                return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");
            }
        }

        Product product = _pustokDbContext.Products
            .Include(p => p.ProductColors)
            .FirstOrDefault(p => p.Id == model.Id);

        if (product == null)
            return NotFound();

        try
        {
            product.ProductColors.Clear();
            _pustokDbContext.SaveChanges();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Rating = model.Rating;
            product.CategoryId = model.CategoryId;

            _pustokDbContext.Products.Update(product);
            _pustokDbContext.SaveChanges();

            foreach (var colorId in model.SelectedColorIds)
            {
                var productColor = new ProductColor
                {
                    ColorId = colorId,
                    ProductId = product.Id
                };

                _pustokDbContext.ProductColors.Add(productColor);
                _pustokDbContext.SaveChanges();
            }           
        }
        catch (PostgresException e)
        {
            _logger.LogError(e, "Postgresql Exception");

            throw e;
        }


        return RedirectToAction("Products");
    }

    #endregion

    #region Delete

    [HttpGet("delete")]
    public IActionResult Delete(int id)
    {
        Product product = _pustokDbContext.Products
            .FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        _pustokDbContext.Remove(product);
        _pustokDbContext.SaveChanges();


        return RedirectToAction("Products");
    }

    #endregion

    private IActionResult PrepareValidationView(string viewName)
    {
        var responseViewModel = new ProductAddResponseViewModel
        {
            Categories = _pustokDbContext.Categories.ToList()
        };
        return View(viewName, responseViewModel);
    }
}
