using Pustok.Database.Abstracts;
using System;
using System.Collections.Generic;

namespace Pustok.Database.DomainModels;

public class BroadcastMessage : IEntity
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<Notification> Notifications { get; set; }
}
