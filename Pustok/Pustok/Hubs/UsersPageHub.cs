using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pustok.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Pustok.Hubs;

[Authorize]
public class UsersPageHub : Hub
{
    private readonly ILogger<UsersPageHub> _logger;
    private readonly IUserService _userService;

    public UsersPageHub(ILogger<UsersPageHub> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation($"New user etered to users page, Email : {_userService.CurrentUser.Email}, Connection id : {Context.ConnectionId}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _logger.LogInformation($"New user left users page, Email : {_userService.CurrentUser.Email}, Connection id : {Context.ConnectionId}");

        return base.OnDisconnectedAsync(exception);
    }
}
