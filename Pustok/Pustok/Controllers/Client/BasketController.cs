using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using Pustok.Services.Concretes;
using Pustok.ViewModels.Product;
using System.Linq;

namespace Pustok.Controllers.Client;

[Route("basket")]
[Authorize]
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
                _userService.CurrentUser
            );

        _pustokDbContext.SaveChanges();

        return RedirectToAction("index", "home");
    }

    [HttpGet("remove-basket-product/{basketProductId}")]
    public IActionResult RemoveBasketProduct(int basketProductId)
    {
        var basketProduct = _pustokDbContext
            .BasketProducts
            .FirstOrDefault(bp =>
                bp.UserId == _userService.CurrentUser.Id &&
                bp.Id == basketProductId);

        if (basketProduct == null)
        {
            return NotFound();
        }

        _pustokDbContext.BasketProducts.Remove(basketProduct);
        _pustokDbContext.SaveChanges();

        return RedirectToAction("cart", "basket");
    }

    [HttpGet("increase-basket-product/{basketProductId}")]
    public IActionResult IncreaseBasketProductQuantity(int basketProductId)
    {
        var basketProduct = _pustokDbContext
            .BasketProducts
            .Include(bp => bp.Product)
            .FirstOrDefault(bp =>
                bp.UserId == _userService.CurrentUser.Id &&
                bp.Id == basketProductId);

        if (basketProduct == null)
        {
            return NotFound();
        }

        basketProduct.Quantity++;

        _pustokDbContext.SaveChanges();

        return Json(new BasketQuantityUpdateResponseViewModel
        {
            Total = basketProduct.Quantity * basketProduct.Product.Price,
            Quantity = basketProduct.Quantity
        });
    }

    [HttpGet("decrease-basket-product/{basketProductId}")]
    public IActionResult DecreaseBasketProductQuantity(int basketProductId)
    {
        var basketProduct = _pustokDbContext
            .BasketProducts
            .Include(bp => bp.Product)
            .FirstOrDefault(bp =>
                bp.UserId == _userService.CurrentUser.Id &&
                bp.Id == basketProductId);

        if (basketProduct == null)
        {
            return NotFound();
        }

        basketProduct.Quantity--;


        if (basketProduct.Quantity <= 0)
        {
            _pustokDbContext.BasketProducts.Remove(basketProduct);
            _pustokDbContext.SaveChanges();

            return NoContent();
        }
        else
        {
            _pustokDbContext.SaveChanges();

            var updateResponseViewModel = new BasketQuantityUpdateResponseViewModel
            {
                Total = basketProduct.Quantity * basketProduct.Product.Price,
                Quantity = basketProduct.Quantity
            };

            return Json(updateResponseViewModel);
        }
    }



    [HttpGet("cart")]
    public IActionResult GetBasketProducts()
    {
        var basketProducts = _pustokDbContext.BasketProducts
            .Where(bp => bp.UserId == _userService.CurrentUser.Id)
            .Include(bp => bp.Product)
            .Include(bp => bp.Color)
            .Include(bp => bp.Size)
            .ToList();

        return View(basketProducts);
    }
}
