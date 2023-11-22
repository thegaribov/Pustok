using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using Pustok.Services.Concretes;
using System.Linq;

namespace Pustok.Controllers.Client
{
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly IUserService _userService;
        private readonly PustokDbContext _pustokDbContext;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public BasketController(
            IUserService userService,
            PustokDbContext pustokDbContext,
            IProductService productService,
            IBasketService basketService)
        {
            _userService = userService;
            _pustokDbContext = pustokDbContext;
            _productService = productService;
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("add-product")]
        public IActionResult AddProduct(int productId, int? sizeId, int? colorId)
        {
            _basketService.CreateOrIncrementQuantity
                (
                    productId,
                    sizeId ?? _productService.GetDefaultSizeId(productId),
                    colorId ?? _productService.GetDefaultColorId(productId),
                    _userService.GetCurrentLoggedUser()
                );

            _pustokDbContext.SaveChanges();

            return RedirectToAction("index", "home");
        }


        [HttpGet("cart")]
        public IActionResult GetBasketProducts()
        {
            var basketProducts = _pustokDbContext.BasketProducts
                .Where(bp => bp.UserId == _userService.GetCurrentLoggedUser().Id)
                .Include(bp => bp.Product)
                .Include(bp => bp.Color)
                .Include(bp => bp.Size)
                .ToList();

            return View(basketProducts);
        }
    }
}
