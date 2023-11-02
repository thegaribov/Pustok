using Pustok.Database.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Database.DomainModels;

[Table("categories")]
public class Category : IEntity
{
    public Category()
    {
    }

    public Category(int ıd, string name)
    {
        Id = ıd;
        Name = name;
    }

    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}
