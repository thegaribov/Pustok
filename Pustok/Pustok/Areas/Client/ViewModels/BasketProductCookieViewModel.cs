using Pustok.Database.DomainModels;

namespace Pustok.ViewModels;

public class BasketProductCookieViewModel
{
    public BasketProductCookieViewModel(int productId, int colorId, int sizeId, int quantity = 1)
    {
        ProductId = productId;
        ColorId = colorId;
        SizeId = sizeId;
        Quantity = quantity;
    }

    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int ColorId { get; set; }
    public int SizeId { get; set; }
}
