using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels;

public abstract class BaseProductViewModel
{
    [Required(ErrorMessage = "Pls enter name")]
    public string Name { get; set; }

    [Range(1, 1000)]
    public decimal Price { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public int? CategoryId { get; set; }
}
