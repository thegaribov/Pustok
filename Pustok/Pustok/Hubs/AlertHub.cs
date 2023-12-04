using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pustok.Hubs;

public class AlertHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
