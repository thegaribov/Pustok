using Pustok.Database.DomainModels;

namespace Pustok.Services.Abstract;

public interface IUserService
{
    User GetCurrentLoggedUser();
}
