namespace Pustok.Database.DomainModels
{
    public class Product
    {
        public Product()
        {
            Id = ++DbContext._productId;
        }

        public Product(string name, decimal price, int rating)
        {
            Id = ++DbContext._productId;
            Name = name;
            Price = price;
            Rating = rating;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}
