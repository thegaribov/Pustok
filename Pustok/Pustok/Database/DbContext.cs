using System.Collections.Generic;
using Pustok.Database.DomainModels;

namespace Pustok.Database
{
    public class DbContext
    {
        //mock data
        public static List<Product> _products = new List<Product>
        {
            new Product(1, "American", 200, 4),
            new Product(2, "It burnu", 400, 2),
            new Product(3, "Blossom", 300, 5),
        };
    }
}
