namespace Pustok.Database.DomainModels
{
    public class Product
    {
        public Product()
        {

        }

        public Product(int ıd, string name, decimal price, int rating)
        {
            Id = ıd;
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
