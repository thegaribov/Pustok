using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System.Linq;

namespace Pustok.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly PustokDbContext _pustokDbContext;

        public BasketService(PustokDbContext pustokDbContext)
        {
            _pustokDbContext = pustokDbContext;
        }

        public void CreateOrIncrementQuantity(int productId, int sizeId, int colorId, User user)
        {
            var basketProduct = _pustokDbContext.BasketProducts
                .FirstOrDefault(bp =>
                    bp.UserId == user.Id &&
                    bp.ProductId == productId &&
                    bp.SizeId == sizeId &&
                    bp.ColorId == colorId);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                return;
            }

            basketProduct = new BasketProduct
            {
                ProductId = productId,
                SizeId = sizeId,
                ColorId = colorId,
                User = user,
                Quantity = 1
            };

            _pustokDbContext.BasketProducts.Add(basketProduct);
        }
    }
}
