namespace Pustok.ViewModels;

public class ProductUpdateRequestViewModel
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }

    public int? CategoryId { get; set; }
}
