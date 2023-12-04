using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.Services.Abstract;

public interface IOrderService
{
    string GenerateAndGetTrackingCode();
    List<Notification> CreateOrderNotifications(Order order);
}
