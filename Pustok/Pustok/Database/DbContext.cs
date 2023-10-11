using System.Collections.Generic;
using Pustok.Database.DomainModels;

namespace Pustok.Database
{
    public class DbContext
    {
        public static int _productId;

        //mock data
        public static List<Product> _products = new List<Product>
        {
            new Product("American", 200, 4),
            new Product("It burnu", 400, 2),
            new Product("Blossom", 300, 5),
        };
    }
}
