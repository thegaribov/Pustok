using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.ViewModels;

public class ProductAddResponseViewModel : BaseProductViewModel
{

    public List<Category> Categories { get; set; }
}
