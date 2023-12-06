using Microsoft.AspNetCore.SignalR;
using Pustok.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Pustok.Hubs;

public class AlertHub : Hub
{
    private readonly IUserService _userService;
    private readonly IHubContext<UsersPageHub> _usersPageHubContext;

    public AlertHub(IUserService userService, IHubContext<UsersPageHub> usersPageHubContext)
    {
        _userService = userService;
        _usersPageHubContext = usersPageHubContext;
    }

    public override Task OnConnectedAsync()
    {
        _userService
            .AddCurrentUserConnection(Context.ConnectionId);

        //_usersPageHubContext.Clients.Clients()

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _userService
            .RemoveCurrentUserConnection(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
