using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.ViewModels;
using System.Linq;

namespace Pustok.Admin
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Products()
        {
            return View(DbContext._products);
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitProduct(Product product)
        {
            //object intializer
            DbContext._products.Add(product);

            return RedirectToAction("Products");
        }

        [HttpGet]
        public IActionResult ProductEdit(int id)
        {
            var product = DbContext._products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        public IActionResult EditSubmitProduct(ProductUpdateViewModel model)
        {
            var product = DbContext._products.FirstOrDefault(p => p.Id == model.Id);
            if (product == null)
            {
                return NotFound();
            }

            product.Price = model.Price;
            product.Rating = model.Rating;

            return RedirectToAction("Products");
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = DbContext._products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            DbContext._products.Remove(product);

            return RedirectToAction("Products");
        }
    }
}
