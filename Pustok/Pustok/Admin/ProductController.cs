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
        var model = new ProductAddRequestViewModel
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

        return View(product);
    }


    [HttpPost]
    public IActionResult EditSubmitProduct(ProductUpdateViewModel model)
    {
        Product product = _productRepository.GetById(model.Id);
        if (product == null)
        {
            return NotFound();
        }

        _productRepository.Update(model);

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
}
