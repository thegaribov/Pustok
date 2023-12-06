using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pustok.Services.Abstract;

public interface IUserService
{
    User CurrentUser { get; }
    bool IsAuthenticateed { get; }
    bool IsCurrentUserInRole(params string[] roles);
    string GetFullName(User user);
    string GetCurrentUserFullName();
    bool IsUserSeeded(User user);
    List<User> GetWholeStaff();

    List<string> GetUserConnections(int userId);
    void AddCurrentUserConnection(string userConnection);
    void RemoveCurrentUserConnection(string connectionId);

    bool IsOnline(int userId);
}
