using Pustok.Database.DomainModels;

namespace Pustok.Services.Abstract;

public interface IUserService
{
    User CurrentUser { get; }
    bool IsAuthenticateed { get; }
    string GetFullName(User user);
    string GetCurrentUserFullName();
}
