using Pustok.Database.Abstracts;
using System;

namespace Pustok.Database.DomainModels;

public class Notification : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}
