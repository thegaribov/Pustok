using Pustok.Database.DomainModels;

namespace Pustok.Services.Abstract;

public interface IBasketService
{
    void CreateOrIncrementQuantity(int productId, int sizeId, int colorId, User user);
}
