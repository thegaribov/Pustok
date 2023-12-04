using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pustok.Services.Concretes;

public class OrderService : IOrderService
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IUserService _userService;

    public OrderService(PustokDbContext pustokDbContext, IUserService userService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
    }

    public string GenerateAndGetTrackingCode()
    {
        var random = new Random();
        string numberPart;
        string trackingCode;

        do
        {
            numberPart = random.Next(1, 100000).ToString();
            trackingCode = $"OR{numberPart.PadLeft(5, '0')}";

        } while (DoesCodeExist(trackingCode));

        return trackingCode;
    }

    private bool DoesCodeExist(string trackingCode)
    {
        return _pustokDbContext.Orders.Any(o => o.TrackingCode == trackingCode);
    }

    public List<Notification> CreateOrderNotifications(Order order)
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
}
