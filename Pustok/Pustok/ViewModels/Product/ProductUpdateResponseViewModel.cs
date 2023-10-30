using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.ViewModels.Product
{
    public class ProductUpdateResponseViewModel : BaseProductViewModel
    {
        public int Id { get; set; }

        public List<Category> Categories { get; set; }
    }
}
