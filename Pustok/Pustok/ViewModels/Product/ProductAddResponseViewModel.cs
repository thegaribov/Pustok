using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.ViewModels.Product;

public class ProductAddResponseViewModel : BaseProductViewModel
{
    public List<Category> Categories { get; set; }
}
