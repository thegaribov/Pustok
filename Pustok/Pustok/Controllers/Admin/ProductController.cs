using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pustok.Database.DomainModels;
using Pustok.Database.Repositories;
using Pustok.ViewModels;

namespace Pustok.Controllers.Admin;

[Route("admin/products")]
public class ProductController : Controller
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController()
    {
        _productRepository = new ProductRepository();
        _categoryRepository = new CategoryRepository();

        var factory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        _logger = factory.CreateLogger<ProductController>();
    }

    #region Products

    [HttpGet] //admin/products
    public IActionResult Products()
    {
        return View("Views/Admin/Product/Products.cshtml", _productRepository.GetAllWithCategories());
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        var categories = _categoryRepository.GetAll();
        var model = new ProductAddResponseViewModel
        {
            Categories = categories
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
            var category = _categoryRepository.GetById(model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Category doesn't exist");

                return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");
            }
        }

        var product = new Product
        {
            Name = model.Name,
            Price = model.Price,
            Rating = model.Rating,
            CategoryId = model.CategoryId,
        };

        try
        {
            _productRepository.Insert(product);
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
        Product product = _productRepository.GetById(id);
        if (product == null)
            return NotFound();


        var model = new ProductUpdateResponseViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Rating = product.Rating,
            Categories = _categoryRepository.GetAll(),
            CategoryId = product.CategoryId
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
            var category = _categoryRepository.GetById(model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Category doesn't exist");

                return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");
            }
        }

        Product product = _productRepository.GetById(model.Id);
        if (product == null)
            return NotFound();


        product.Name = model.Name;
        product.Price = model.Price;
        product.Rating = model.Rating;
        product.CategoryId = model.CategoryId;


        try
        {
            _productRepository.Update(product);
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
        Product product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productRepository.RemoveById(id);

        return RedirectToAction("Products");
    }

    #endregion

    private IActionResult PrepareValidationView(string viewName)
    {
        var categories = _categoryRepository.GetAll();

        var responseViewModel = new ProductAddResponseViewModel
        {
            Categories = categories
        };

        return View(viewName, responseViewModel);
    }

    protected override void Dispose(bool disposing)
    {
        _productRepository.Dispose();
        _categoryRepository.Dispose();

        base.Dispose(disposing);
    }
}
