using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.ViewModels;

public class ProductAddRequestViewModel
{
    public List<Category> Categories { get; set; }
}
