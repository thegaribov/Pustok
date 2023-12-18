using Pustok.Areas.Client.ViewModels.Product;
using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.Areas.Admin.ViewModels.Product;

public class ProductAddResponseViewModel : BaseProductViewModel
{
    public List<Category> Categories { get; set; }
}
