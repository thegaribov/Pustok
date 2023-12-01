using Pustok.Database.DomainModels;

namespace Pustok.Services.Abstract;

public interface IUserService
{
    User CurrentUser { get; }
    bool IsAuthenticateed { get; }
    bool IsCurrentUserInRole(params string[] roles);
    string GetFullName(User user);
    string GetCurrentUserFullName();
    bool IsUserSeeded(User user);
}
