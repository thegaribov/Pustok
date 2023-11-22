using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System.Linq;

namespace Pustok.Services.Concretes;

public class ProductService : IProductService
{
    private readonly PustokDbContext _pustokDbContext;

    public ProductService(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    public Color GetDefaultColor(int productId)
    {
        return _pustokDbContext.ProductColors
            .Where(pc => pc.ProductId == productId)
            .Include(pc => pc.Color)
            .First()
            .Color;
    }
    public int GetDefaultColorId(int productId)
    {
        var color = GetDefaultColor(productId);
        return color.Id;
    }

    public Size GetDefaultSize(int productId)
    {
        return _pustokDbContext.ProductSizes
           .Where(pc => pc.ProductId == productId)
           .Include(pc => pc.Size)
           .First()
           .Size;
    }

    public int GetDefaultSizeId(int productId)
    {
        var size = GetDefaultSize(productId);
        return size.Id;
    }
}
