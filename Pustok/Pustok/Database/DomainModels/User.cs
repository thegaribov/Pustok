using Pustok.Contracts;
using Pustok.Database.Abstracts;
using System.Collections.Generic;

namespace Pustok.Database.DomainModels
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
