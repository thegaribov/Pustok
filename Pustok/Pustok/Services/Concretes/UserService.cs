using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pustok.Services.Concretes;

public class UserService : IUserService
{
    private readonly PustokDbContext _pustokDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private User _currentUser = null;

    public User CurrentUser
    {
        get { 

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

    public UserService(
        PustokDbContext pustokDbContext, 
        IHttpContextAccessor httpContextAccessor)
    {
        _pustokDbContext = pustokDbContext;
        _httpContextAccessor = httpContextAccessor;
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
