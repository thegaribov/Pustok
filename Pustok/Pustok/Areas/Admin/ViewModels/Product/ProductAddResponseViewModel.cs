using Pustok.Database.DomainModels;
using Pustok.ViewModels.Product;
using System.Collections.Generic;

namespace Pustok.Areas.Admin.ViewModels.Product;

public class ProductAddResponseViewModel : BaseProductViewModel
{
    public List<Category> Categories { get; set; }
}
