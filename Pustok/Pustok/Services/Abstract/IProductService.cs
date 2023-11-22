using Pustok.Database.DomainModels;

namespace Pustok.Services.Abstract;

public interface IProductService
{
    Color GetDefaultColor(int productId);
    Size GetDefaultSize(int productId);

    int GetDefaultColorId(int productId);
    int GetDefaultSizeId(int productId);
}
