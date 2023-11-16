using Pustok.Database.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Database.DomainModels;

[Table("products")]
public class Product : IEntity
{
    public Product()
        : this(default, default, default)
    {

    }

    public Product(string name, decimal price, int rating)
    {
        Name = name;
        Price = price;
        Rating = rating;
    }

    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [Column("categoryid")]
    public int? CategoryId { get; set; }
    public Category Category { get; set; }

    public List<ProductColor> ProductColors { get; set; }
    public List<ProductSize> ProductSizes { get; set; }
}
