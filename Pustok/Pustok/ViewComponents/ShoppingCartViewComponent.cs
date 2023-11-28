using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly PustokDbContext _pustokDbContext;
        private readonly IUserService _userService;

        public ShoppingCartViewComponent(
            PustokDbContext pustokDbContext, 
            IUserService userService)
        {
            _pustokDbContext = pustokDbContext;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketProducts = _pustokDbContext.BasketProducts
                .Include(bp => bp.Product)
                .Where(bp => bp.User == _userService.CurrentUser)
                .ToList();

            return View(basketProducts);
        }
    }
}
