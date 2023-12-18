using Pustok.Contracts;
using Pustok.Database;
using Pustok.Services.Abstract;
using System.Collections.Generic;
using System;
using Pustok.Database.DomainModels;
using Pustok.Extensions;
using Pustok.Areas.Admin.ViewModels.Notification;
using Microsoft.AspNetCore.SignalR;
using Pustok.Hubs;
using System.Threading.Tasks;

namespace Pustok.Services.Concretes;

public class NotificationService : INotificationService
{
    private readonly IUserService _userService;
    private readonly PustokDbContext _pustokDbContext;
    private readonly IHubContext<AlertHub> _alertHubContext;

    public NotificationService(
        IUserService userService,
        PustokDbContext pustokDbContext,
        IHubContext<AlertHub> hubContext)
    {
        _userService = userService;
        _pustokDbContext = pustokDbContext;
        _alertHubContext = hubContext;
    }

    public List<Notification> CreateNotificationsForOrderCreation(Order order)
    {
        var staff = _userService.GetWholeStaff();
        List<Notification> notifications = new List<Notification>();

        foreach (var user in staff)
        {
            var notification = new Notification
            {
                Title = NotificationTemplate.Order.Created.TITLE,
                Content = NotificationTemplate.Order.Created.CONTENT
                    .Replace(NotificationTemplateKeyword.TRACKING_CODE, order.TrackingCode)
                    .Replace(NotificationTemplateKeyword.USER_FULL_NAME, _userService.GetFullName(order.User)),

                User = user,
                CreatedAt = DateTime.UtcNow
            };

            notifications.Add(notification);

            _pustokDbContext.Notifications.Add(notification);
        }


        return notifications;
    }

    public Notification CreateNotificationForOrderUpdate(Order order)
    {
        var notification = new Notification
        {
            Title = NotificationTemplate.Order.Updated.TITLE,
            Content = NotificationTemplate.Order.Updated.CONTENT
                .Replace(NotificationTemplateKeyword.TRACKING_CODE, order.TrackingCode)
                .Replace(NotificationTemplateKeyword.ORDER_STATUS_NAME, order.Status.GetPublicName()),

            User = order.User,
            CreatedAt = DateTime.UtcNow
        };

        _pustokDbContext.Notifications.Add(notification);

        return notification;
    }

    public async Task SendNotificationsAsync(List<Notification> notifications)
    {
        foreach (var notification in notifications)
        {
            var connectionIds = _userService
                .GetUserConnections(notification.UserId);

            foreach (var connectionId in connectionIds)
            {
                var model = new NotificationViewModel
                {
                    Title = notification.Title,
                    Content = notification.Content,
                    CreatedAt = notification.CreatedAt.ToString("dd/MM/yyyy HH:mm")
                };

                await _alertHubContext.Clients
                    .Client(connectionId)
                    .SendAsync("ReceiveAlertMessage", model);
            }
        }
    }

    public async Task SendNotificationAsync(Notification notification)
    {
        await SendNotificationsAsync(new List<Notification> { notification });
    }
}

