using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pustok.Services.Abstract;

public interface INotificationService
{
    List<Notification> CreateNotificationsForOrderCreation(Order order);
    Notification CreateNotificationForOrderUpdate(Order order);

    Task SendNotificationsAsync(List<Notification> notifications);
    Task SendNotificationAsync(Notification notification);
}
