using Microsoft.AspNetCore.Http;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System;
using System.Linq;

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

    public UserService(
        PustokDbContext pustokDbContext, 
        IHttpContextAccessor httpContextAccessor)
    {
        _pustokDbContext = pustokDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public void IsAuthenticated()
    {

    }

    private User GetCurrentLoggedUser()
    {
        var currentUserId = _httpContextAccessor.HttpContext.User
            .FindFirst(c => c.Type == "Id").Value;

        return _pustokDbContext.Users.Single(u => u.Id == Convert.ToInt32(currentUserId));
    }

    public string GetFullName(User user)
    {
        return user.Name + " " + user.LastName;
    }
}
