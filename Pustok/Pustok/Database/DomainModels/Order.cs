using Pustok.Contracts;
using Pustok.Database.Abstracts;
using System;
using System.Collections.Generic;

namespace Pustok.Database.DomainModels;

public class Order : IEntity
{
    public int Id { get; set; }
    public string TrackingCode { get; set; }
    public OrderStatus Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
}
