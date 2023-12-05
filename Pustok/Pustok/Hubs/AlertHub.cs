using Microsoft.AspNetCore.SignalR;
using Pustok.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Pustok.Hubs;

public class AlertHub : Hub
{
    private readonly IUserService _userService;

    public AlertHub(IUserService userService)
    {
        _userService = userService;
    }

    public override Task OnConnectedAsync()
    {
        _userService.AddCurrentUserConnection(Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _userService.RemoveCurrentUserConnection();

        return base.OnDisconnectedAsync(exception);
    }
}
