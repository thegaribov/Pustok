using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.ViewModels
{
    public class ProductUpdateResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public int? CategoryId { get; set; }

        public List<Category> Categories { get; set; }
    }
}
