using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services.Abstract;
using System.Linq;

namespace Pustok.Services.Concretes;

public class UserService : IUserService
{
    private readonly PustokDbContext _pustokDbContext;

    public UserService(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    public User GetCurrentLoggedUser()
    {
        return _pustokDbContext.Users.Single(u => u.Id == -1);
    }
}
