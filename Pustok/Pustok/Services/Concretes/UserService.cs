using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Migrations;
using Pustok.Services.Abstract;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pustok.Services.Concretes;

public class UserService : IUserService
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static List<UserConnectionViewModel> _userConnection;
    private User _currentUser = null;


    static UserService()
    {
        _userConnection = new List<UserConnectionViewModel>();
    }

    public UserService(
    PustokDbContext pustokDbContext,
    IHttpContextAccessor httpContextAccessor)
    {
        _pustokDbContext = pustokDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public List<string> GetUserConnections(int userId)
    {
        return _userConnection.SingleOrDefault(uc => uc.UserId == userId)?.ConnectionIds ?? new List<string>();
    }

    public void AddCurrentUserConnection(string userConnection)
    {
        var connectionIds = _userConnection
            .SingleOrDefault(uc => uc.UserId == CurrentUser.Id)?
            .ConnectionIds;

        if (connectionIds == null)
        {
            _userConnection.Add(new UserConnectionViewModel
            {
                UserId = CurrentUser.Id,
                ConnectionIds = new List<string> { userConnection }
            });
        }
        else
        {
            connectionIds.Add(userConnection);
        }
    }

    public void RemoveCurrentUserConnection(string connectionId)
    {
        var connectionIds = _userConnection
            .SingleOrDefault(uc => uc.UserId == CurrentUser.Id)?
            .ConnectionIds;

        if (connectionIds != null)
        {
            connectionIds.Remove(connectionId);
        }
    }


    public bool IsOnline(int userId)
    {
        return _userConnection.Any(uc => uc.UserId == userId && uc.ConnectionIds.Any());
    }

    public User CurrentUser
    {
        get
        {

            return _currentUser ??= GetCurrentLoggedUser();
        }
    }

    public bool IsAuthenticateed
    {
        get
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }


    public bool IsCurrentUserInRole(params string[] roles)
    {
        return roles.Any(r => _httpContextAccessor.HttpContext.User.IsInRole(r));
    }



    private User GetCurrentLoggedUser()
    {
        var currentUserId = _httpContextAccessor.HttpContext.User
            .FindFirst(c => c.Type == "Id").Value;

        return _pustokDbContext.Users
            .Include(u => u.UserRoles)
            .Single(u => u.Id == Convert.ToInt32(currentUserId));
    }

    public string GetFullName(User user)
    {
        return user.Name + " " + user.LastName;
    }

    public string GetCurrentUserFullName()
    {
        return GetFullName(CurrentUser);
    }

    public bool IsUserSeeded(User user)
    {
        return user.Id < 0;
    }

    public List<User> GetWholeStaff()
    {
        var staff = _pustokDbContext.Users
            .Where(u => u.UserRoles
                .Any(ur =>
                    ur.Role == Role.Moderator ||
                    ur.Role == Role.Admin ||
                    ur.Role == Role.SuperAdmin ||
                    ur.Role == Role.SMM))
            .ToList();


        return staff;
    }
}
