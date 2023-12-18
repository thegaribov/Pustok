﻿using Microsoft.EntityFrameworkCore;
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
}
