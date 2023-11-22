using Pustok.Database.Abstracts;

namespace Pustok.Database.DomainModels;

public class BasketProduct : IEntity
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ColorId { get; set; }
    public Color Color { get; set; }

    public int SizeId { get; set; }
    public Size Size { get; set; }

}
