using Pustok.Contracts;
using System;

namespace Pustok.Extensions;

public static class OrderStatusExtensions
{
    public static string GetPublicName(this OrderStatus orderStatus)
    {
        switch (orderStatus)
        {
            case OrderStatus.None:
                return "None";
            case OrderStatus.New:
                return "New";
            case OrderStatus.InProgress:
                return "In progress";
            case OrderStatus.InCourier:
                return "In courier";
            case OrderStatus.Delivered:
                return "Delivered";
            case OrderStatus.Rejected:
                return "In Rejected";
            default:
                throw new Exception("Order status not found");
        }
    }
}
