using System;

namespace Pustok.Database.DomainModels
{
    public class Product
    {
        public Product()
            : this(default, default, default)
        {

        }

        public Product(string name, decimal price, int rating)
        {
            Id = ++DbContext._productId;
            Name = name;
            Price = price;
            Rating = rating;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
