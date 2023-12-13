using Pustok.Helpers.Paging;
using System.Collections.Generic;

namespace Pustok.ViewModels.Product;

public class ProductsPageViewModel : QueryParams
{
    public List<CategoryViewModel> Categories { get; set; }
    public List<ColorViewModel> Colors { get; set; }
    public List<ProductViewModel> Products { get; set; }
    public Paginator<Database.DomainModels.Product> Pagination { get; set; }
    public decimal? PriceMinRange { get; set; }
    public decimal? PriceMaxRange { get; set; }
    public int? CategoryId { get; set; }
    public int? ColorId { get; set; }
    public decimal? PriceMinRangeFilter { get; set; }
    public decimal? PriceMaxRangeFilter { get; set; }
}

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public string ImageUrl { get; set; }
}

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductsCount { get; set; }
}

public class ColorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductsCount { get; set; }
}
