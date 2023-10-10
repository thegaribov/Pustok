using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Database.DomainModels;

namespace Pustok.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Products()
        {
            return View(DbContext._products);
        }

        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitProduct(string name, decimal price, int rating)
        {
            DbContext._products.Add(new Product
            {
                Id = 0,
                Name = name,
                Price = price,
                Rating = rating
            });

            return RedirectToAction("Products");
        }
    }
}
