using Pustok.Contracts;

namespace Pustok.Database.DomainModels
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
