using Microsoft.AspNetCore.Mvc;
using Pustok.Database.DomainModels;
using Pustok.Database.Repositories;
using Pustok.ViewModels;

namespace Pustok.Admin;

public class AdminController : Controller
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryRepository _categoryRepository;

    public AdminController()
    {
        _productRepository = new ProductRepository();
        _categoryRepository = new CategoryRepository();
    }


    [HttpGet]
    public IActionResult Products()
    {
        return View(_productRepository.GetAllWithCategories());
    }

    [HttpGet]
    public IActionResult ProductAdd()
    {
        var categories = _categoryRepository.GetAll();
        var model = new ProductAddResponseViewModel
        {
            Categories = categories
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult SubmitProduct(Product product)
    {
        _productRepository.Insert(product);

        return RedirectToAction("Products");
    }

    [HttpGet]
    public IActionResult ProductEdit(int id)
    {
        Product product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductUpdateResponseViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Rating = product.Rating,
            Categories = _categoryRepository.GetAll(),
            CategoryId = product.CategoryId
        };

        return View(model);
    }


    [HttpPost]
    public IActionResult EditSubmitProduct(ProductUpdateRequestViewModel model)
    {
        Product product = _productRepository.GetById(model.Id);
        if (product == null)
        {
            return NotFound();
        }

        product.Price = model.Price;
        product.Rating = model.Rating;
        product.CategoryId = model.CategoryId;

        _productRepository.Update(product);

        return RedirectToAction("Products");
    }

    [HttpGet]
    public IActionResult DeleteProduct(int id)
    {
        Product product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productRepository.RemoveById(id);

        return RedirectToAction("Products");
    }

    protected override void Dispose(bool disposing)
    {
        _productRepository.Dispose();
        _categoryRepository.Dispose();

        base.Dispose(disposing);
    }
}
